using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace Radio;

internal class PortComm
{
	private const int HEAD_LEN = 4;

	private const int MAX_COMM_LEN = 4096;

	private const int MaxReadTimeout = 5000;

	private const int MaxWriteTimeout = 1000;

	private const int MaxBuf = 160;

	private static readonly byte[] CMD_END = Encoding.ASCII.GetBytes("END\0");

	private static readonly byte[] CMD_AUDIO = Encoding.ASCII.GetBytes("Font");

	private static readonly byte[,] CMD_FLASH = new byte[7, 8]
	{
		{ 70, 45, 80, 82, 79, 71, 255, 255 },
		{ 70, 45, 69, 82, 65, 83, 69, 255 },
		{ 70, 45, 67, 79, 255, 255, 255, 255 },
		{ 70, 45, 77, 79, 68, 255, 255, 255 },
		{ 70, 45, 86, 69, 82, 255, 255, 255 },
		{ 70, 45, 83, 78, 255, 255, 255, 255 },
		{ 70, 45, 84, 73, 77, 69, 255, 255 }
	};

	private static readonly byte[] CMD_FLASH_CO_FIX = new byte[16]
	{
		83, 71, 45, 84, 89, 84, 45, 48, 48, 57,
		255, 255, 255, 255, 255, 255
	};

	private static readonly byte[] CMD_FLASH_MOO_FIX = new byte[8] { 83, 71, 45, 48, 48, 57, 255, 255 };

	private static readonly byte[] CMD_FLASH_CO = new byte[16]
	{
		83, 85, 82, 87, 65, 86, 69, 45, 83, 71,
		45, 48, 48, 57, 255, 255
	};

	private static readonly byte[] CMD_ACK = new byte[3] { 65, 67, 75 };

	private static readonly byte[] CMD_NACK = new byte[3] { 78, 67, 75 };

	public int[] START_ADDR = new int[0];

	public int[] END_ADDR = new int[0];

	private Thread thread;

	public bool CancelComm { get; set; }

	public bool IsRead { get; set; }

	public bool ThreadIsValid
	{
		get
		{
			if (thread != null)
			{
				return thread.IsAlive;
			}
			return false;
		}
	}

	public event FirmwareUpdateProgressEventHandler OnFirmwareUpdateProgress;

	public void Join()
	{
		if (ThreadIsValid)
		{
			thread.Join();
		}
	}

	public void UpdateFirmware()
	{
		if (IsRead)
		{
			thread = new Thread(DoReadData);
		}
		else
		{
			thread = new Thread(DoWriteData);
		}
		thread.Start();
	}

	public void DoReadData()
	{
	}

	public void DoWriteData()
	{
		int i = 0;
		int pos = 0;
		int maxPos = 0;
		byte[] rxBuf = new byte[160];
		int wantLen = 0;
		int realLen = 0;
		int remainder = 0;
		int readedLen = 0;
		int dataAddr = 0;
		int dataStartAddr = 0;
		int dataEndAddr = 0;
		Stopwatch.StartNew();
		SerialPort spt = new SerialPort(MainForm.CurCom, MainForm.CurCbr);
		try
		{
			dataStartAddr = 0;
			dataEndAddr = 458752;
			for (dataAddr = dataStartAddr; dataAddr < dataEndAddr; dataAddr += realLen)
			{
				remainder = dataAddr % 4096;
				realLen = ((dataAddr + 4096 <= dataEndAddr) ? (4096 - remainder) : (dataEndAddr - dataAddr));
				maxPos++;
			}
			spt.ReadTimeout = 5000;
			spt.WriteTimeout = 1000;
			spt.DtrEnable = true;
			spt.RtsEnable = true;
			try
			{
				spt.Open();
				if (!spt.IsOpen)
				{
					return;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				if (this.OnFirmwareUpdateProgress != null)
				{
					this.OnFirmwareUpdateProgress(this, new FirmwareUpdateProgressEventArgs(0f, Lang.SZ_ERR_OPEN_PORT, Failed: false, Closed: false));
				}
				return;
			}
			int curTimes = 0;
			byte[] data = new byte[4096];
			for (i = 0; i < 25; i++)
			{
				for (int j = 0; j < 12; j++)
				{
					rxBuf[j] = 0;
				}
				for (int j = 12; j < 16; j++)
				{
					rxBuf[j] = byte.MaxValue;
				}
				spt.Write(rxBuf, 0, 16);
				Thread.Sleep(200);
				wantLen = 1;
				if (spt.Read(rxBuf, 0, wantLen) >= wantLen)
				{
					break;
				}
			}
			Thread.Sleep(200);
			Array.Clear(rxBuf, 0, rxBuf.Length);
			spt.DiscardInBuffer();
			spt.Write(CMD_AUDIO, 0, CMD_AUDIO.Length);
			data[0] = byte.MaxValue;
			data[1] = byte.MaxValue;
			data[2] = byte.MaxValue;
			data[3] = byte.MaxValue;
			spt.Write(data, 0, 4);
			wantLen = 1;
			for (readedLen = spt.Read(rxBuf, 0, wantLen); readedLen < wantLen; readedLen += spt.Read(rxBuf, readedLen, wantLen - readedLen))
			{
				Thread.Sleep(20);
			}
			Array.Clear(rxBuf, 0, rxBuf.Length);
			spt.DiscardInBuffer();
			curTimes = 0;
			dataAddr = dataStartAddr;
			while (true)
			{
				if (dataAddr < dataEndAddr)
				{
					if (CancelComm)
					{
						Array.Clear(rxBuf, 0, rxBuf.Length);
						spt.DiscardInBuffer();
						spt.Write(CMD_END, 0, CMD_END.Length);
						readedLen = spt.Read(rxBuf, 0, 1);
						spt.Close();
						return;
					}
					remainder = dataAddr % 4096;
					realLen = ((dataAddr + 4096 <= dataEndAddr) ? (4096 - remainder) : (dataEndAddr - dataAddr));
					byte[] realData = new byte[realLen];
					Array.Copy(Global.EEROM, curTimes, realData, 0, realLen);
					curTimes += realLen;
					Array.Clear(rxBuf, 0, rxBuf.Length);
					spt.DiscardInBuffer();
					spt.Write(realData, 0, 1024);
					spt.Write(realData, 1024, 1024);
					spt.Write(realData, 2048, 1024);
					spt.Write(realData, 3072, 1024);
					wantLen = 1;
					for (readedLen = spt.Read(rxBuf, 0, wantLen); readedLen < wantLen; readedLen += spt.Read(rxBuf, readedLen, wantLen - readedLen))
					{
						Thread.Sleep(20);
					}
					if (rxBuf[0] != CMD_ACK[0])
					{
						break;
					}
					if (this.OnFirmwareUpdateProgress != null)
					{
						this.OnFirmwareUpdateProgress(this, new FirmwareUpdateProgressEventArgs((float)(++pos) * 100f / (float)maxPos, dataAddr.ToString(), Failed: false, Closed: false));
					}
					dataAddr += realLen;
					continue;
				}
				data[0] = 69;
				data[1] = 78;
				data[2] = 68;
				for (curTimes = 3; curTimes < 4096; curTimes++)
				{
					data[curTimes] = byte.MaxValue;
				}
				Array.Clear(rxBuf, 0, rxBuf.Length);
				spt.DiscardInBuffer();
				spt.Write(data, 0, 1024);
				spt.Write(data, 1024, 1024);
				spt.Write(data, 2048, 1024);
				spt.Write(data, 3072, 1024);
				wantLen = 1;
				for (readedLen = spt.Read(rxBuf, 0, wantLen); readedLen < wantLen; readedLen += spt.Read(rxBuf, readedLen, wantLen - readedLen))
				{
					Thread.Sleep(20);
				}
				if (rxBuf[0] != CMD_ACK[0])
				{
					break;
				}
				spt.Close();
				if (this.OnFirmwareUpdateProgress != null)
				{
					this.OnFirmwareUpdateProgress(this, new FirmwareUpdateProgressEventArgs(100f, "Communication success", Failed: false, Closed: true));
				}
				return;
			}
			if (this.OnFirmwareUpdateProgress != null)
			{
				this.OnFirmwareUpdateProgress(this, new FirmwareUpdateProgressEventArgs(0f, Lang.SZ_ERR_COMM, Failed: true, Closed: true));
			}
			spt.Close();
		}
		catch (TimeoutException ex2)
		{
			Console.WriteLine(ex2.Message);
			if (this.OnFirmwareUpdateProgress != null)
			{
				this.OnFirmwareUpdateProgress(this, new FirmwareUpdateProgressEventArgs(0f, Lang.SZ_ERR_COMM, Failed: false, Closed: false));
			}
			spt.Close();
		}
	}
}
