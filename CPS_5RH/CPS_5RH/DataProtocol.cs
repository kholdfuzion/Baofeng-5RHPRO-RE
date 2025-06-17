using System;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using CPS_5RH.Data;
using CPS_5RH.Properties;

namespace CPS_5RH;

internal class DataProtocol
{
    private byte[] Header_SYNC = Encoding.ASCII.GetBytes("PROGRAM\0");
    
    private byte[] Header_SYNC1 = Encoding.ASCII.GetBytes("Picture\u00FF");

    private byte[] ReqPassword = Encoding.ASCII.GetBytes("ENCRYPTT"); // ENCRYPTT

    private byte[] Header_INFO = Encoding.ASCII.GetBytes("INFORMATION"); // INFORMATION

    private byte[] End_Info = Encoding.ASCII.GetBytes("END\0"); // END\0

    private byte[] T_Info = new byte[16]
    {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 255, 255, 255, 255
    };

    private static byte[] FullData = new byte[0x20000];

    public DataApp bufForData = null;

    private DataApp appData = null;

    private SerialPort sp = null;

    private OPT_DIR opDir = OPT_DIR.DIR_READ;

    public STATE state = STATE.STATE_HANDSHAKE1;

    private System.Timers.Timer timer;

    public bool flagTransmitting = false;

    public static bool FileOpenFlg = false;

	public static bool DevReadFlg = false;

    public bool flagRetry = false;

    public static bool flagK6 = false;

    private int timesForRetry = 5;

    private static byte seed = 0;

    public int rxCnt = 0;

	public static int DpDataLen = 49152;

    private void TimerInit()
    {
        timer = new System.Timers.Timer();
        timer.Interval = 200.0;
        timer.Elapsed += Timer_Elapsed;
        timer.AutoReset = false;
        timer.Enabled = true;
    }

    public DataProtocol(SerialPort sP, DataApp data, OPT_DIR dir)
    {
        sp = sP;
        switch (dir)
        {
            case OPT_DIR.DIR_READ:
                appData = data;
                bufForData = new DataApp();
                opDir = OPT_DIR.DIR_READ;
                break;
            case OPT_DIR.DIR_WRITE:
                bufForData = data;
                appData = data;
                opDir = OPT_DIR.DIR_WRITE;
                DataToWrite();
                break;
            case OPT_DIR.DIR_BOOT_PIC:
                opDir = OPT_DIR.DIR_BOOT_PIC;
                break;
        }
        TimerInit();
    }

    public PROMPT DoIt()
    {
		PROMPT result = PROMPT.SUCCESS;
        flagTransmitting = true;
        try
        {
			if ((result = handShake()) == PROMPT.SUCCESS)
            {
                if (opDir == OPT_DIR.DIR_READ)
                {
                    state = STATE.STATE_READ1;
                    if (readInfo())
                    {
                        DataToRead();
						DevReadFlg = true;
                        return PROMPT.SUCCESS;
                    }
                    return PROMPT.FAIL;
                }
                if (opDir == OPT_DIR.DIR_WRITE)
                {
					if (!DevReadFlg)
					{
						if (!readDevInfo())
						{
							return PROMPT.FAIL;
						}
						if ((result = handShake()) != PROMPT.SUCCESS)
						{
							return PROMPT.FAIL;
						}
					}
                    state = STATE.STATE_WRITE1;
                    if (writeInfo())
                    {
                        return PROMPT.SUCCESS;
                    }
                    return PROMPT.FAIL;
                }
                if (opDir == OPT_DIR.DIR_BOOT_PIC)
                {
                    state = STATE.STATE_WRITE1;
                    if (writePicInfo())
                    {
                        return PROMPT.SUCCESS;
                    }
                    return PROMPT.FAIL;
                }
                return PROMPT.SUCCESS;
            }
			return result;
        }
        catch (Exception)
        {
            flagTransmitting = false;
            return PROMPT.ILLEGAL_DATA;
        }
    }

    private PROMPT handShake()
    {
		byte[] buf = new byte[32];
		byte[] tmpbuf = new byte[11];
		byte[] tmpReq = new byte[8];
		byte[] PwdReq = new byte[8] { 255, 255, 255, 255, 255, 255, 255, 255 };
		byte[] tmpInfo = new byte[16];
		string Pwd = CPS_5RH.Properties.Settings.Default.ProgramPwd;
		ASCIIEncoding code = new ASCIIEncoding();
        timesForRetry = 25;
		seed = (byte)new Random().Next(1, 255);
        state = STATE.STATE_HANDSHAKE1;
        while (flagTransmitting)
        {
            switch (state)
            {
                case STATE.STATE_HANDSHAKE1:
                    sp.Write(T_Info, 0, 16);
                    state = STATE.STATE_HANDSHAKE2;
				timer.Start();
                    break;
                case STATE.STATE_HANDSHAKE2:
                    if (sp.BytesToRead <= 0)
                    {
                        break;
                    }
				if (sp.ReadByte() == 65)
                    {
                        timer.Stop();
                        if (opDir == OPT_DIR.DIR_BOOT_PIC)
                        {
                            sp.Write(Header_SYNC1, 0, 8);
                        }
                        else
                        {
                            Header_SYNC[7] = seed;
						for (int i = 0; i < 7; i++)
						{
							tmpReq[i] = (byte)(seed ^ Header_SYNC[i]);
						}
						tmpReq[7] = seed;
						sp.Write(tmpReq, 0, 8);
                        }
                        state = STATE.STATE_HANDSHAKE3;
					timesForRetry = 3;
                        timer.Start();
                        break;
                    }
                    sp.Close();
                    sp.BaudRate = 115200;
                    sp.Open();
                    if (opDir == OPT_DIR.DIR_BOOT_PIC)
                    {
                        sp.Write(Header_SYNC1, 0, 8);
                    }
                    else
                    {
                        Header_SYNC[7] = seed;
					for (int i = 0; i < 7; i++)
					{
						tmpReq[i] = (byte)(seed ^ Header_SYNC[i]);
					}
					tmpReq[7] = seed;
					sp.Write(tmpReq, 0, 8);
                    }
                    state = STATE.STATE_HANDSHAKE3;
                    timesForRetry = 3;
                    timer.Start();
                    break;
                case STATE.STATE_HANDSHAKE3:
                    if (sp.BytesToRead <= 0)
                    {
                        break;
                    }
                    if (opDir == OPT_DIR.DIR_BOOT_PIC)
                    {
					buf[0] = (byte)sp.ReadByte();
					if (buf[0] == 65)
                        {
                            timer.Stop();
						return PROMPT.SUCCESS;
                        }
                        break;
                    }
				buf[0] = (byte)sp.ReadByte();
				buf[1] = (byte)(seed ^ buf[0]);
				if (buf[1] == 65)
                    {
                        timer.Stop();
					byte[] bbt = code.GetBytes(Pwd);
					if (bbt.Length > 0)
                        {
						Array.Copy(bbt, 0, PwdReq, 0, bbt.Length);
                        }
                        for (int i = 0; i < 8; i++)
                        {
						tmpReq[i] = (byte)(seed ^ PwdReq[i]);
                        }
					sp.Write(tmpReq, 0, 8);
                        state = STATE.STATE_HANDSHAKE4;
                        timesForRetry = 3;
                        timer.Start();
                    }
                    break;
                case STATE.STATE_HANDSHAKE4:
                    if (sp.BytesToRead > 0)
                    {
					buf[0] = (byte)sp.ReadByte();
					buf[1] = (byte)(seed ^ buf[0]);
					if (buf[1] != 65)
                        {
						return PROMPT.PWD_ERR;
                        }
                        timer.Stop();
                        for (int i = 0; i < 11; i++)
                        {
						tmpbuf[i] = (byte)(seed ^ Header_INFO[i]);
                        }
					sp.Write(tmpbuf, 0, 11);
                        state = STATE.STATE_HANDSHAKE5;
                        timesForRetry = 3;
                        timer.Start();
                    }
                    break;
                case STATE.STATE_HANDSHAKE5:
                    {
                        if (sp.BytesToRead < 16)
                        {
                            break;
                        }
				sp.Read(buf, 0, 16);
                        timer.Stop();
                        int i;
                        for (i = 0; i < 16; i++)
                        {
					tmpInfo[i] = (byte)(seed ^ buf[i]);
					if (tmpInfo[i] == byte.MaxValue)
                            {
                                break;
                            }
                        }
				int cmp = string.Compare(code.GetString(tmpInfo, 0, i), "BF-K6");
                        if (opDir == OPT_DIR.DIR_WRITE)
                        {
					if (cmp == 0 && !flagK6)
                            {
                                return PROMPT.FAIL;
                            }
					if (cmp != 0 && flagK6)
                            {
                                return PROMPT.FAIL;
                            }
                        }
				else if (0 == cmp)
                        {
                            flagK6 = true;
                        }
                        else
                        {
                            flagK6 = false;
                        }
				tmpbuf[0] = (byte)(seed ^ buf[8]);
                        if (opDir == OPT_DIR.DIR_READ)
                        {
					Settings.FreqBand = tmpbuf[0];
					tmpbuf[0] = (byte)(seed ^ 0x52);
                        }
                        else
                        {
					if (FormMain.VER_TYPE == 1 && tmpbuf[0] != Settings.FreqBand)
                            {
						return PROMPT.FREQBAND_ERR;
                            }
					tmpbuf[0] = (byte)(seed ^ 0x57);
                        }
				sp.Write(tmpbuf, 0, 1);
                        state = STATE.STATE_HANDSHAKE6;
                        timesForRetry = 3;
                        timer.Start();
                        break;
                    }
                case STATE.STATE_HANDSHAKE6:
                    if (sp.BytesToRead > 0)
                    {
					buf[0] = (byte)sp.ReadByte();
					buf[1] = (byte)(seed ^ buf[0]);
					if (buf[1] == 65)
                        {
						return PROMPT.SUCCESS;
                        }
                    }
                    break;
            }
			Thread.Sleep(50);
        }
		return PROMPT.FAIL;
    }

	private bool readDevInfo()
    {
		byte[] dat2 = new byte[4] { 82, 0, 0, 0 };
		byte[] dat3 = new byte[4];
		byte[] buf = new byte[4100];
		int addr = 0;
		int len = 0;
        rxCnt = 0;
		state = STATE.STATE_READ1;
        while (flagTransmitting)
        {
            switch (state)
            {
                case STATE.STATE_READ1:
                    {
				dat2[0] = 82;
				dat2[1] = (byte)(addr >> 8);
				dat2[2] = (byte)(addr & 0xFF);
				dat2[3] = 0;
                        for (int i = 0; i < 4; i++)
                        {
					dat3[i] = (byte)(seed ^ dat2[i]);
                        }
				sp.Write(dat3, 0, 4);
                        state = STATE.STATE_READ2;
                        timer.Start();
				timer.Interval = 4000.0;
				timesForRetry = 1;
				len = 4100;
                        break;
                    }
                case STATE.STATE_READ2:
				if (sp.BytesToRead >= len)
				{
					sp.Read(buf, 0, len);
					timer.Stop();
					for (int i = 0; i < 128; i++)
					{
						FullData[i] = (byte)(seed ^ buf[i + 4]);
					}
					state = STATE.STATE_READ3;
					for (int i = 0; i < 4; i++)
                    {
						buf[i] = (byte)(seed ^ End_Info[i]);
					}
					sp.Write(buf, 0, 4);
					timer.Start();
					timer.Interval = 1000.0;
				}
                        break;
			case STATE.STATE_READ3:
				if (sp.BytesToRead > 0)
				{
					buf[0] = (byte)sp.ReadByte();
					buf[1] = (byte)(seed ^ buf[0]);
					if (buf[1] == 65)
					{
						timer.Stop();
						return true;
					}
				}
				break;
			}
			Thread.Sleep(10);
		}
		return false;
	}

	private bool readInfo()
	{
		byte[] dat2 = new byte[4] { 82, 0, 0, 0 };
		byte[] dat3 = new byte[4];
		byte[] buf = new byte[4100];
		int addr = 0;
		int len = 0;
		bool frameHeader = false;
		rxCnt = 0;
		while (flagTransmitting)
		{
			switch (state)
			{
			case STATE.STATE_READ1:
			{
				len = 16;
				dat2[0] = 82;
				dat2[1] = (byte)(addr >> 8);
				dat2[2] = (byte)(addr & 0xFF);
				dat2[3] = 0;
				for (int i = 0; i < 4; i++)
				{
					dat3[i] = (byte)(seed ^ dat2[i]);
				}
				sp.Write(dat3, 0, 4);
				state = STATE.STATE_READ2;
				timer.Start();
				timer.Interval = 4000.0;
				timesForRetry = 1;
				len = 4100;
				break;
			}
			case STATE.STATE_READ2:
				if (sp.BytesToRead < len)
				{
					break;
				}
				sp.Read(buf, 0, len);
                    timer.Stop();
				Array.Copy(buf, 4, FullData, rxCnt, len - 4);
				rxCnt += len - 4;
				addr += len - 4;
                    if (DpDataLen - rxCnt <= 0)
                    {
                        state = STATE.STATE_READ3;
                        for (int i = 0; i < 4; i++)
                        {
						buf[i] = (byte)(seed ^ End_Info[i]);
                        }
					sp.Write(buf, 0, 4);
                        timer.Start();
					timer.Interval = 1000.0;
                    }
                    else
                    {
                        state = STATE.STATE_READ1;
                    }
                    break;
                case STATE.STATE_READ3:
                    if (sp.BytesToRead > 0)
                    {
					buf[0] = (byte)sp.ReadByte();
					buf[1] = (byte)(seed ^ buf[0]);
					if (buf[1] == 65)
                        {
                            timer.Stop();
                            return true;
                        }
                    }
                    break;
            }
            Thread.Sleep(10);
        }
        return false;
    }

    private bool writeInfo()
    {
		int len = 0;
		byte[] buf = new byte[4100];
		byte[] Dat = new byte[4100];
        rxCnt = 0;
        while (flagTransmitting)
        {
            switch (state)
            {
                case STATE.STATE_WRITE1:
                    {
				len = 4096;
				buf[0] = 87;
				buf[1] = (byte)(rxCnt >> 8);
				buf[2] = (byte)(rxCnt & 0xFF);
				buf[3] = 0;
				Array.Copy(FullData, rxCnt, buf, 4, len);
				for (int i = 0; i < 4 + len; i++)
                        {
					Dat[i] = (byte)(seed ^ buf[i]);
                        }
				sp.Write(Dat, 0, len + 4);
                        timesForRetry = 1;
				timer.Interval = 4000.0;
                        timer.Start();
                        state = STATE.STATE_WRITE2;
                        break;
                    }
                case STATE.STATE_WRITE2:
                    if (sp.BytesToRead < 1)
                    {
                        break;
                    }
				buf[0] = (byte)sp.ReadByte();
				buf[1] = (byte)(seed ^ buf[0]);
                    timer.Stop();
				if (buf[1] == 65)
                    {
					rxCnt += len;
                        if (rxCnt >= DpDataLen)
                        {
                            for (int i = 0; i < 4; i++)
                            {
							buf[i] = (byte)(seed ^ End_Info[i]);
                            }
						sp.Write(buf, 0, 4);
                            state = STATE.STATE_WRITE3;
                        }
                        else
                        {
                            state = STATE.STATE_WRITE1;
                        }
                    }
                    else
                    {
                        flagTransmitting = false;
                    }
                    break;
                case STATE.STATE_WRITE3:
                    if (sp.BytesToRead > 0)
                    {
					buf[0] = (byte)sp.ReadByte();
					buf[1] = (byte)(seed ^ buf[0]);
					if (buf[1] == 65)
                        {
                            return true;
                        }
                    }
                    break;
            }
            Thread.Sleep(10);
        }
        return false;
    }

    private bool writePicInfo()
    {
		int len = 0;
		byte[] buf = new byte[4100];
		byte[] Dat = new byte[4100];
        rxCnt = 0;
        while (flagTransmitting)
        {
            switch (state)
            {
                case STATE.STATE_WRITE1:
				len = 4096;
				Array.Copy(FormProgressBar.BmpPicData, rxCnt, buf, 0, len);
				sp.Write(buf, 0, len);
                    timesForRetry = 1;
				timer.Interval = 4000.0;
                    timer.Start();
                    state = STATE.STATE_WRITE2;
                    break;
                case STATE.STATE_WRITE2:
                    if (sp.BytesToRead < 1)
                    {
                        break;
                    }
				buf[0] = (byte)sp.ReadByte();
                    timer.Stop();
				if (buf[0] == 65)
                    {
					rxCnt += len;
                        if (rxCnt >= FormProgressBar.BmpPicLen)
                        {
                            for (int i = 0; i < 4; i++)
                            {
							buf[i] = End_Info[i];
                            }
						sp.Write(buf, 0, 4);
                            state = STATE.STATE_WRITE3;
                        }
                        else
                        {
                            state = STATE.STATE_WRITE1;
                        }
                    }
                    else
                    {
                        flagTransmitting = false;
                    }
                    break;
                case STATE.STATE_WRITE3:
                    if (sp.BytesToRead > 0)
                    {
					buf[0] = (byte)sp.ReadByte();
					if (buf[0] == 65)
                        {
                            return true;
                        }
                    }
                    break;
            }
            Thread.Sleep(10);
        }
        return false;
    }

    private string AscChar2String(byte[] bt, int st, int len)
    {
		byte[] dtcode = new byte[8];
		string byStr = "";
		ASCIIEncoding asc = new ASCIIEncoding();
		int j = 0;
        for (int i = st; i < st + len && bt[i] != byte.MaxValue && bt[i] != 0; i++)
        {
			dtcode[j] = bt[i];
			j++;
        }
		return asc.GetString(dtcode, 0, j);
    }

    private void ConvertDevice()
    {
		int cnt = 0;
		byte[] devbuf = new byte[128];
		byte[] rxbuf = new byte[32];
		byte[] txbuf = new byte[32];
		string freqstr = "";
		ASCIIEncoding asc = new ASCIIEncoding();
		int len = 0;
		Array.Copy(FullData, len, devbuf, 0, 128);
		bufForData.dataDevice.DevName = CharSwap2String(devbuf, 0, 16);
		len += 16;
        try
        {
			Array.Copy(devbuf, len, rxbuf, 0, 32);
			Array.Copy(devbuf, len + 32, txbuf, 0, 32);
            bufForData.dataDevice.FreqRxPt = 0;
            bufForData.dataDevice.FreqTxPt = 0;
            for (int i = 0; i < 4; i++)
            {
				if (rxbuf[cnt + 3] != byte.MaxValue && rxbuf[cnt + 7] != byte.MaxValue)
                {
					int val = Convert.ToInt32((rxbuf[cnt] + (rxbuf[cnt + 1] << 8) + (rxbuf[cnt + 2] << 16) + (rxbuf[cnt + 3] << 24)).ToString("X"));
                    bufForData.dataDevice.SetRxStartFreq(i, val);
					val = Convert.ToInt32((rxbuf[cnt + 4] + (rxbuf[cnt + 5] << 8) + (rxbuf[cnt + 6] << 16) + (rxbuf[cnt + 7] << 24)).ToString("X"));
                    bufForData.dataDevice.SetRxEndFreq(i, val);
                    if (val > 100000 && bufForData.dataDevice.GetRxStartFreq(i) < bufForData.dataDevice.GetRxEndFreq(i))
                    {
                        bufForData.dataDevice.FreqRxPt++;
                    }
                }
				if (txbuf[cnt + 3] != byte.MaxValue && txbuf[cnt + 7] != byte.MaxValue)
                {
					int val = Convert.ToInt32((txbuf[cnt] + (txbuf[cnt + 1] << 8) + (txbuf[cnt + 2] << 16) + (txbuf[cnt + 3] << 24)).ToString("X"));
                    bufForData.dataDevice.SetTxStartFreq(i, val);
					val = Convert.ToInt32((txbuf[cnt + 4] + (txbuf[cnt + 5] << 8) + (txbuf[cnt + 6] << 16) + (txbuf[cnt + 7] << 24)).ToString("X"));
                    bufForData.dataDevice.SetTxEndFreq(i, val);
                    if (val > 100000 && bufForData.dataDevice.GetTxStartFreq(i) < bufForData.dataDevice.GetTxEndFreq(i))
                    {
                        bufForData.dataDevice.FreqTxPt++;
                    }
                }
				cnt += 8;
            }
        }
        catch
        {
		}
		len = 80;
		bufForData.dataDevice.SoftVer = AscChar2String(devbuf, len, 8);
		bufForData.dataDevice.HardVer = AscChar2String(devbuf, len + 8, 8);
		bufForData.dataDevice.Pdate = asc.GetString(devbuf, 96, 16);
		len = 112;
		bufForData.dataDevice.Stand = devbuf[len];
		bufForData.dataDevice.Dis200m = (byte)((devbuf[len + 1] >> 7) & 1);
		bufForData.dataDevice.Dis350m = (byte)((devbuf[len + 1] >> 6) & 1);
		bufForData.dataDevice.Dis500m = (byte)((devbuf[len + 1] >> 5) & 1);
		bufForData.dataDevice.Scramble = devbuf[len + 2];
		bufForData.dataDevice.EnGps = (byte)((devbuf[len + 3] >> 7) & 1);
		bufForData.dataDevice.EnBT = (byte)((devbuf[len + 3] >> 6) & 1);
		bufForData.dataDevice.EnFalldn = (byte)((devbuf[len + 3] >> 5) & 1);
		bufForData.dataDevice.EnNoise = (byte)((devbuf[len + 3] >> 4) & 1);
		bufForData.dataDevice.EnFlight = (byte)((devbuf[len + 3] >> 3) & 1);
		if (flagK6)
		{
			bufForData.dataDevice.EnZone = 0;
		}
		else
		{
			bufForData.dataDevice.EnZone = (byte)((devbuf[len + 3] >> 2) & 1);
		}
		bufForData.dataDevice.EnDTMF = (byte)((devbuf[len + 4] >> 7) & 1);
		bufForData.dataDevice.En2Tone = (byte)((devbuf[len + 4] >> 6) & 1);
		bufForData.dataDevice.En5Tone = (byte)((devbuf[len + 4] >> 5) & 1);
		bufForData.dataDevice.EnMdc1200 = (byte)((devbuf[len + 4] >> 4) & 1);
		bufForData.dataDevice.EnBdc1200 = (byte)((devbuf[len + 4] >> 3) & 1);
		if (devbuf[len + 7] < 1 || devbuf[len + 7] > 4)
		{
			devbuf[len + 7] = 2;
		}
		bufForData.dataDevice.PkeyCnt = (byte)(devbuf[len + 7] - 1);
		len = 120;
        for (int i = 0; i < 8; i++)
        {
			bufForData.dataDevice.SetProKey(i, devbuf[len + i]);
        }
    }

    private string SubVoiceConvert(byte datH, byte datL)
    {
		bool flagNormalOfInvert = false;
		string str = "OFF";
		if ((datH & 0x80) != 128)
        {
			if (datH != 0)
			{
				try
				{
					ushort buf = Convert.ToUInt16(((ushort)((datH << 8) + datL)).ToString("x"));
					str = ((buf < 1000) ? buf.ToString().Insert(2, ".") : buf.ToString().Insert(3, "."));
				}
				catch
				{
					str = "";
				}
				return str;
			}
			return "OFF";
            }
		if (datH != byte.MaxValue)
		{
			if ((datH & 0xC0) == 192)
			{
				flagNormalOfInvert = true;
			}
			ushort buf = 0;
			str = Convert.ToString((ushort)(((datH & 7) << 8) + datL), 16);
			str = ((str.Length == 2) ? ("D0" + str) : ((str.Length != 1) ? ("D" + str) : ("D00" + str)));
			return (!flagNormalOfInvert) ? (str + "N") : (str + "I");
		}
		return "OFF";
    }

    private void ConvertChn()
    {
		byte[] tmpInfo = new byte[DatChannelInfo.Chn_Info_Size * DatChannelInfo.Max_Chn_Num];
		byte[] buf = new byte[DatChannelInfo.Chn_Info_Size];
		Array.Copy(FullData, 128, tmpInfo, 0, DatChannelInfo.Chn_Info_Size * DatChannelInfo.Max_Chn_Num);
        DatChannelInfo.ChnTotal = 0;
        for (int i = 0; i < DatChannelInfo.Max_Chn_Num; i++)
        {
			Array.Copy(tmpInfo, DatChannelInfo.Chn_Info_Size * i, buf, 0, DatChannelInfo.Chn_Info_Size);
            if (bufForData.dataChnInfor[i].UseFlg == 1)
            {
				int val;
                try
                {
					val = Convert.ToInt32((buf[0] + (buf[1] << 8) + (buf[2] << 16) + (buf[3] << 24)).ToString("X"));
					bufForData.dataChnInfor[i].RxFreq = val.ToString().Insert(3, ".");
					val = Convert.ToInt32((buf[4] + (buf[5] << 8) + (buf[6] << 16) + (buf[7] << 24)).ToString("X"));
					bufForData.dataChnInfor[i].TxFreq = val.ToString().Insert(3, ".");
                }
                catch
                {
				}
				bufForData.dataChnInfor[i].RxCtsDcs = SubVoiceConvert(buf[8], buf[9]);
				bufForData.dataChnInfor[i].TxCtsDcs = SubVoiceConvert(buf[10], buf[11]);
				val = (buf[16] & 0xC0) >> 6;
                if (FormMain.HIGH_VALID != 2)
                {
					val = ((val == 2) ? 1 : 0);
                }
				bufForData.dataChnInfor[i].Power = (byte)val;
				val = (buf[16] & 0x30) >> 4;
				bufForData.dataChnInfor[i].Wideth = ((val > 0) ? ((byte)1) : ((byte)0));
				bufForData.dataChnInfor[i].Offsetdir = (byte)((buf[16] & 0xC) >> 2);
				bufForData.dataChnInfor[i].FreqInvert = (byte)((buf[16] & 2) >> 1);
				bufForData.dataChnInfor[i].TalkAround = (byte)(buf[16] & 1);
				bufForData.dataChnInfor[i].FivetonePtt = (byte)((buf[17] & 0xC0) >> 6);
				bufForData.dataChnInfor[i].DtmfPtt = (byte)((buf[17] & 0x30) >> 4);
				bufForData.dataChnInfor[i].SqType = (byte)(buf[17] & 0xF);
				bufForData.dataChnInfor[i].SignalType = (byte)((buf[18] & 0xE0) >> 5);
				bufForData.dataChnInfor[i].JumpFreq = (byte)(buf[18] & 3);
				bufForData.dataChnInfor[i].BusyLock = (byte)((buf[19] & 0xC0) >> 6);
				bufForData.dataChnInfor[i].TxDis = (byte)((buf[19] & 0x20) >> 5);
				bufForData.dataChnInfor[i].Scram = (byte)((buf[20] >> 4) & 1);
				bufForData.dataChnInfor[i].Compand = (byte)((buf[20] >> 5) & 1);
				bufForData.dataChnInfor[i].CepinDcs = (byte)((buf[20] >> 6) & 1);
				bufForData.dataChnInfor[i].Cepin24bit = (byte)((buf[20] >> 7) & 1);
				bufForData.dataChnInfor[i].FreqStep = buf[24];
				bufForData.dataChnInfor[i].DtmfIdx = buf[25];
				bufForData.dataChnInfor[i].TwotoneIdx = buf[26];
				bufForData.dataChnInfor[i].FivetoneIdx = buf[27];
				bufForData.dataChnInfor[i].MdcIdx = buf[28];
				bufForData.dataChnInfor[i].Scanlist = buf[29];
				bufForData.dataChnInfor[i].Emerglist = buf[30];
				bufForData.dataChnInfor[i].ChnName = CharSwap2String(buf, 32, 16);
                DatChannelInfo.ChnTotal++;
            }
        }
    }

    private void ConvertVFO()
    {
		byte[] tmpInfo = new byte[DatChannelInfo.Chn_Info_Size * 2];
		byte[] buf = new byte[DatChannelInfo.Chn_Info_Size];
		Array.Copy(FullData, 30976, tmpInfo, 0, DatChannelInfo.Chn_Info_Size * 2);
        for (int i = 0; i < 2; i++)
        {
			Array.Copy(tmpInfo, DatChannelInfo.Chn_Info_Size * i, buf, 0, DatChannelInfo.Chn_Info_Size);
			int val;
            try
            {
				if (buf[3] != byte.MaxValue && buf[7] != byte.MaxValue)
                {
					val = Convert.ToInt32((buf[0] + (buf[1] << 8) + (buf[2] << 16) + (buf[3] << 24)).ToString("X"));
					bufForData.dataVFOInfor[i].RxFreq = val.ToString().Insert(3, ".");
					val = Convert.ToInt32((buf[4] + (buf[5] << 8) + (buf[6] << 16) + (buf[7] << 24)).ToString("X"));
					bufForData.dataVFOInfor[i].TxFreq = val.ToString().Insert(3, ".");
                }
            }
            catch
            {
			}
			bufForData.dataVFOInfor[i].RxCtsDcs = SubVoiceConvert(buf[8], buf[9]);
			bufForData.dataVFOInfor[i].TxCtsDcs = SubVoiceConvert(buf[10], buf[11]);
			double doufreq;
            try
            {
				doufreq = Convert.ToDouble((buf[12] + (buf[13] << 8) + (buf[14] << 16) + (buf[15] << 24)).ToString("X")) / 100000.0;
            }
            catch
            {
				doufreq = 0.0;
            }
			bufForData.dataVFOInfor[i].DivFreq = doufreq.ToString("0.00000");
			val = (buf[16] & 0xC0) >> 6;
			if (2 != FormMain.HIGH_VALID)
            {
				val = ((val == 2) ? 1 : 0);
            }
			bufForData.dataVFOInfor[i].Power = (byte)val;
			val = (buf[16] & 0x30) >> 4;
			bufForData.dataVFOInfor[i].Wideth = ((val > 0) ? ((byte)1) : ((byte)0));
			bufForData.dataVFOInfor[i].Offsetdir = (byte)((buf[16] & 0xC) >> 2);
			bufForData.dataVFOInfor[i].FreqInvert = (byte)((buf[16] & 2) >> 1);
			bufForData.dataVFOInfor[i].TalkAround = (byte)(buf[16] & 1);
			bufForData.dataVFOInfor[i].FivetonePtt = (byte)((buf[17] & 0xC0) >> 6);
			bufForData.dataVFOInfor[i].DtmfPtt = (byte)((buf[17] & 0x30) >> 4);
			bufForData.dataVFOInfor[i].SqType = (byte)(buf[17] & 0xF);
			bufForData.dataVFOInfor[i].SignalType = (byte)((buf[18] & 0xE0) >> 5);
			bufForData.dataVFOInfor[i].JumpFreq = (byte)(buf[18] & 3);
			bufForData.dataVFOInfor[i].BusyLock = (byte)((buf[19] & 0xC0) >> 6);
			bufForData.dataVFOInfor[i].TxDis = (byte)((buf[19] & 0x20) >> 5);
			bufForData.dataVFOInfor[i].Scram = (byte)((buf[20] >> 4) & 1);
			bufForData.dataVFOInfor[i].Compand = (byte)((buf[20] >> 5) & 1);
			bufForData.dataVFOInfor[i].CepinDcs = (byte)((buf[20] >> 6) & 1);
			bufForData.dataVFOInfor[i].Cepin24bit = (byte)((buf[20] >> 7) & 1);
			bufForData.dataVFOInfor[i].FreqStep = buf[24];
			bufForData.dataVFOInfor[i].DtmfIdx = buf[25];
			bufForData.dataVFOInfor[i].TwotoneIdx = buf[26];
			bufForData.dataVFOInfor[i].FivetoneIdx = buf[27];
			bufForData.dataVFOInfor[i].MdcIdx = buf[28];
			bufForData.dataVFOInfor[i].Scanlist = buf[29];
			bufForData.dataVFOInfor[i].Emerglist = buf[30];
			bufForData.dataVFOInfor[i].ChnName = CharSwap2String(buf, 32, 16);
        }
    }

    private string CharSwap2String(byte[] bt, int st, int len)
    {
		byte[] dtcode = new byte[16];
		string byStr = "";
		int j = 0;
		int i = st;
		while (i < st + len && bt[i] != byte.MaxValue && bt[i] != 0)
        {
			dtcode[j] = bt[i];
			dtcode[j + 1] = bt[i + 1];
			i += 2;
			j += 2;
        }
		return Encoding.GetEncoding("GB2312").GetString(dtcode, 0, j);
    }

    private void ConvertSetting()
    {
		byte[] tmp = new byte[128];
		byte[] buf1 = new byte[16];
		ASCIIEncoding code = new ASCIIEncoding();
		Array.Copy(FullData, 31104, tmp, 0, 128);
		bufForData.dataGenSetInfor.ChAMode = tmp[0];
		bufForData.dataGenSetInfor.ChBMode = tmp[1];
		bufForData.dataGenSetInfor.ChANum = (ushort)((tmp[2] << 8) + tmp[3]);
		bufForData.dataGenSetInfor.ChBNum = (ushort)((tmp[4] << 8) + tmp[5]);
		bufForData.dataGenSetInfor.ChAZone = tmp[6];
		bufForData.dataGenSetInfor.ChBZone = tmp[7];
		if (tmp[8] >= 5)
        {
			bufForData.dataGenSetInfor.BlightTime = (byte)(tmp[8] - 4);
        }
        else
        {
            bufForData.dataGenSetInfor.BlightTime = 0;
        }
		bufForData.dataGenSetInfor.BlightLv = tmp[9];
		bufForData.dataGenSetInfor.ChADispMode = (byte)(tmp[10] >> 4);
		bufForData.dataGenSetInfor.ChBDispMode = (byte)(tmp[10] & 0xF);
		bufForData.dataGenSetInfor.DualMode = tmp[11];
		bufForData.dataGenSetInfor.MainBand = tmp[12];
		bufForData.dataGenSetInfor.Sqlv = tmp[13];
		bufForData.dataGenSetInfor.VoxLv = (byte)(tmp[14] - 1);
		bufForData.dataGenSetInfor.Voxdettime = (byte)((tmp[15] - 10) / 5);
		bufForData.dataGenSetInfor.PoSave = tmp[16];
		bufForData.dataGenSetInfor.PoSaveDly = tmp[17];
		bufForData.dataGenSetInfor.LoneWorkTim = tmp[18];
		bufForData.dataGenSetInfor.LoneWorkRsp = tmp[19];
		bufForData.dataGenSetInfor.APO = tmp[20];
		bufForData.dataGenSetInfor.TOT = tmp[21];
		bufForData.dataGenSetInfor.PreTot = tmp[22];
		bufForData.dataGenSetInfor.GpsZone = tmp[24];
		bufForData.dataGenSetInfor.HzTo1750 = tmp[26];
		bufForData.dataGenSetInfor.NoaaCh = tmp[30];
		bufForData.dataGenSetInfor.GpsID = tmp[31];
		bufForData.dataGenSetInfor.VoxSW = (byte)((tmp[32] & 0x80) >> 7);
		bufForData.dataGenSetInfor.AprsSw = (byte)((tmp[32] & 0x40) >> 6);
		bufForData.dataGenSetInfor.LoneWork = (byte)((tmp[32] & 0x20) >> 5);
		bufForData.dataGenSetInfor.DaoDi = (byte)((tmp[32] & 0x10) >> 4);
		bufForData.dataGenSetInfor.Voice = (byte)((tmp[32] & 0xC) >> 2);
		bufForData.dataGenSetInfor.BusyLock = (byte)(tmp[32] & 3);
		bufForData.dataGenSetInfor.KeyLock = (byte)((tmp[33] & 0x80) >> 7);
		bufForData.dataGenSetInfor.AutoKey = (byte)((tmp[33] & 0x40) >> 6);
		bufForData.dataGenSetInfor.Tone = (byte)((tmp[34] & 0x80) >> 7);
		bufForData.dataGenSetInfor.EndTone = (byte)((tmp[34] & 0x60) >> 5);
		bufForData.dataGenSetInfor.GpsSW = (byte)((tmp[35] & 0x80) >> 7);
		bufForData.dataGenSetInfor.GpsMode = (byte)((tmp[35] & 0x60) >> 5);
		bufForData.dataGenSetInfor.GpsShare = (byte)((tmp[35] & 0x10) >> 4);
		bufForData.dataGenSetInfor.GpsReq = (byte)((tmp[35] & 8) >> 3);
		bufForData.dataGenSetInfor.BlueT = (byte)((tmp[36] & 0x80) >> 7);
		bufForData.dataGenSetInfor.BTpair = (byte)((tmp[36] & 0x60) >> 5);
		bufForData.dataGenSetInfor.BluetAPP = (byte)((tmp[36] & 0x10) >> 4);
		bufForData.dataGenSetInfor.Reord = (byte)((tmp[37] & 0x80) >> 7);
		bufForData.dataGenSetInfor.RecordMode = (byte)((tmp[37] & 0x60) >> 5);
		bufForData.dataGenSetInfor.Engineering = (byte)((tmp[37] & 0x10) >> 4);
		bufForData.dataGenSetInfor.TianQi = (byte)((tmp[37] & 8) >> 3);
		bufForData.dataGenSetInfor.LangSel = (byte)((tmp[37] & 4) >> 2);
		bufForData.dataGenSetInfor.PownFace = (byte)(tmp[37] & 3);
		bufForData.dataGenSetInfor.TailFreq = (byte)((tmp[38] & 0xE0) >> 5);
		bufForData.dataGenSetInfor.NOAA = (byte)((tmp[38] & 0x10) >> 4);
		bufForData.dataGenSetInfor.DispDir = (byte)((tmp[38] & 8) >> 3);
		bufForData.dataGenSetInfor.FmInter = (byte)((tmp[38] & 4) >> 2);
		bufForData.dataGenSetInfor.NoiseCancel = (byte)((tmp[38] & 2) >> 1);
		bufForData.dataGenSetInfor.EnhanceFunc = (byte)(tmp[38] & 1);
		bufForData.dataGenSetInfor.BThold = tmp[40];
		bufForData.dataGenSetInfor.BTrxdly = (byte)(tmp[41] / 500);
		bufForData.dataGenSetInfor.BTmic = tmp[42];
		bufForData.dataGenSetInfor.BTspk = tmp[43];
		bufForData.dataGenSetInfor.BTpassword = code.GetString(tmp, 44, 4);
		bufForData.dataGenSetInfor.Skey1 = tmp[48];
		bufForData.dataGenSetInfor.Skey2 = tmp[49];
		bufForData.dataGenSetInfor.Lkey1 = tmp[50];
		bufForData.dataGenSetInfor.Lkey2 = tmp[51];
		bufForData.dataGenSetInfor.Skey3 = tmp[52];
		bufForData.dataGenSetInfor.Skey4 = tmp[53];
		bufForData.dataGenSetInfor.Lkey3 = tmp[54];
		bufForData.dataGenSetInfor.Lkey4 = tmp[55];
        int i;
		for (i = 0; i < 8 && tmp[64 + i] != byte.MaxValue; i++)
		{
		}
		bufForData.dataGenSetInfor.PowPassword = code.GetString(tmp, 64, i);
		for (i = 0; i < 8 && tmp[72 + i] != byte.MaxValue; i++)
		{
		}
		bufForData.dataGenSetInfor.WrPassword = code.GetString(tmp, 72, i);
		bufForData.dataGenSetInfor.Radioname = CharSwap2String(tmp, 80, 16);
		bufForData.dataGenSetInfor.BlueTName = CharSwap2String(tmp, 96, 16);
		bufForData.dataGenSetInfor.PairName = CharSwap2String(tmp, 112, 16);
    }

    private void ConvertChnValidFlg()
    {
		int cnt = DatChannelInfo.Max_Chn_Num / 8;
		byte[] tmpInfo = new byte[cnt];
		Array.Copy(FullData, 31264, tmpInfo, 0, cnt);
		for (int i = 0; i < cnt; i++)
        {
            for (int j = 0; j < 8; j++)
            {
				bool num = ((tmpInfo[i] >> j) & 1) == 0;
				bufForData.dataChnInfor[i * 8 + j].UseFlg = (num ? ((byte)1) : ((byte)0));
            }
        }
    }

    private void ConvertZone()
    {
		byte[] tmp = new byte[DataApp.Zone_Size];
		byte[] buf1 = new byte[32];
		DatZoneInfo.ZoneTotal = FullData[31360];
		int len = 31376;
        for (int i = 0; i < DataApp.Zone_Max_Num; i++)
        {
			Array.Copy(FullData, len + DataApp.Zone_Size * i, tmp, 0, DataApp.Zone_Size);
			bufForData.dataZoneInfor[i].ChnNum = tmp[0];
            if (bufForData.dataZoneInfor[i].ChnNum > DataApp.Zone_Max_Chn_Num)
            {
                bufForData.dataZoneInfor[i].ChnNum = 0;
            }
            for (int j = 0; j < DataApp.Zone_Max_Chn_Num; j++)
            {
				int val = tmp[3 + j * 2] + (tmp[2 + j * 2] << 8);
                bufForData.dataZoneInfor[i].SetChnID(j, val);
            }
			bufForData.dataZoneInfor[i].ZoneName = CharSwap2String(tmp, 136, 16);
            if (bufForData.dataZoneInfor[i].ChnNum > 0 && bufForData.dataZoneInfor[i].ZoneName == "")
            {
                bufForData.dataZoneInfor[i].ZoneName = "Zone " + (i + 1);
            }
        }
    }

    private void ConvertScan()
    {
		byte[] tmp = new byte[80];
		byte[] scanBuf = new byte[DatScanInfo.Scan_Para_Size];
		byte[] buf = new byte[8];
		int len = 33024;
        int val;
        for (int i = 0; i < DatScanInfo.Max_Scan_List_Num; i++)
        {
			Array.Copy(FullData, len + 8 * i, buf, 0, 8);
            try
            {
				val = Convert.ToInt32((buf[0] + (buf[1] << 8) + (buf[2] << 16) + (buf[3] << 24)).ToString("X"));
                bufForData.dataScanInfor.SetUpFreq(i, val);
				val = Convert.ToInt32((buf[4] + (buf[5] << 8) + (buf[6] << 16) + (buf[7] << 24)).ToString("X"));
                bufForData.dataScanInfor.SetDwFreq(i, val);
            }
            catch
            {
            }
        }
		Array.Copy(FullData, 33152, scanBuf, 0, DatScanInfo.Scan_Para_Size);
		bufForData.dataScanInfor.ScanMode = scanBuf[0];
		if (scanBuf[1] > 5)
        {
			bufForData.dataScanInfor.BackScanTim = (ushort)(scanBuf[1] - 5);
        }
        else
        {
            bufForData.dataScanInfor.BackScanTim = 0;
        }
		if (scanBuf[2] > 1)
        {
			bufForData.dataScanInfor.RxResume = (ushort)(scanBuf[2] - 1);
        }
        else
        {
            bufForData.dataScanInfor.RxResume = 0;
        }
		if (scanBuf[3] > 1)
        {
			bufForData.dataScanInfor.TxResume = (ushort)(scanBuf[3] - 1);
        }
        else
        {
            bufForData.dataScanInfor.TxResume = 0;
        }
		bufForData.dataScanInfor.RtnChType = scanBuf[4];
		bufForData.dataScanInfor.PrioScan = scanBuf[5];
		val = (scanBuf[6] << 8) + scanBuf[7];
        bufForData.dataScanInfor.PrioChannel = (ushort)val;
		bufForData.dataScanInfor.ScanRange = scanBuf[8];
        ConvertScanAddFlg();
    }

    private void ConvertScanAddFlg()
    {
		int cnt = DatChannelInfo.Max_Chn_Num / 8;
		byte[] tmpInfo = new byte[cnt];
		Array.Copy(FullData, 33184, tmpInfo, 0, cnt);
		for (int i = 0; i < cnt; i++)
        {
            for (int j = 0; j < 8; j++)
            {
				bool num = ((tmpInfo[i] >> j) & 1) == 0;
				bufForData.dataChnInfor[i * 8 + j].ScanAdd = (num ? ((byte)1) : ((byte)0));
            }
        }
    }

    private string DecChar2String(byte[] bt, int st, int len)
    {
		byte[] dtcode = new byte[20];
		ASCIIEncoding code = new ASCIIEncoding();
		string byStr = "";
		int j = 0;
        for (int i = st; i < st + len; i++)
        {
            if (bt[i] >= 0 && bt[i] <= 9)
            {
				dtcode[j] = (byte)(bt[i] + 48);
            }
            else if (bt[i] >= 10 && bt[i] <= 13)
            {
				dtcode[j] = (byte)(bt[i] + 55);
            }
            else if (bt[i] == 14)
            {
				dtcode[j] = (byte)(bt[i] + 28);
            }
            else
            {
                if (bt[i] != 15)
                {
                    break;
                }
				dtcode[j] = (byte)(bt[i] + 20);
            }
			j++;
        }
		return code.GetString(dtcode, 0, j);
    }

    private string Byte2Str(byte[] bt, int st, int len, int maxl)
    {
		byte[] dtcode = new byte[40];
		ASCIIEncoding code = new ASCIIEncoding();
		string byStr = "";
		int j = 0;
        if (len > 0)
        {
            for (int i = st; i < st + maxl; i++)
            {
				dtcode[j + 1] = (byte)(bt[i] & 0xF);
				dtcode[j] = (byte)(bt[i] >> 4);
				j += 2;
            }
			j = 0;
            for (int i = 0; i < 40; i++)
            {
				if (dtcode[i] >= 0 && dtcode[i] <= 9)
                {
					dtcode[j] = (byte)(dtcode[i] + 48);
                }
				else if (dtcode[i] >= 10 && dtcode[i] <= 13)
                {
					dtcode[j] = (byte)(dtcode[i] + 55);
                }
				else if (dtcode[i] == 14)
                {
					dtcode[j] = (byte)(dtcode[i] + 28);
                }
                else
                {
					if (dtcode[i] != 15)
                    {
                        break;
                    }
					dtcode[j] = (byte)(dtcode[i] + 20);
                }
				j++;
				if (j == len)
                {
                    break;
                }
            }
			byStr = code.GetString(dtcode, 0, j);
        }
		return byStr;
    }

    private byte Code2Char(byte code)
    {
        return (byte)(code - 10);
    }

    private byte Code2Char1(byte code)
    {
        if (code == byte.MaxValue)
        {
            return 0;
        }
        return (byte)(code - 9);
    }

    private void ConvertDTMF()
    {
		byte[] tmp = new byte[DatDtmfSysInfo.Dtmf_Info_Size];
		byte[] buf1 = new byte[16];
		byte[] buf2 = new byte[8];
		string str = "";
		ASCIIEncoding code = new ASCIIEncoding();
		Array.Copy(FullData, 33280, tmp, 0, DatDtmfSysInfo.Dtmf_Info_Size);
		bufForData.dataDtmfSysInfor.DtmfSw = tmp[0];
		bufForData.dataDtmfSysInfor.CodeSpeed = tmp[1];
		bufForData.dataDtmfSysInfor.FirstCodeTim = tmp[2] * 10;
		bufForData.dataDtmfSysInfor.PreTime = tmp[3] * 10;
		bufForData.dataDtmfSysInfor.CodeDly = tmp[4] * 10;
		bufForData.dataDtmfSysInfor.PttIDPause = tmp[5];
		bufForData.dataDtmfSysInfor.DtmfTone = tmp[6];
		if (tmp[7] >= 10)
        {
			bufForData.dataDtmfSysInfor.ResetTime = tmp[7] - 10;
        }
        else
        {
            bufForData.dataDtmfSysInfor.ResetTime = 0;
        }
		bufForData.dataDtmfSysInfor.SepCode = Code2Char(tmp[8]);
		bufForData.dataDtmfSysInfor.GrpCode = Code2Char1(tmp[9]);
		bufForData.dataDtmfSysInfor.DecRsp = tmp[10];
		bufForData.dataDtmfSysInfor.Did = DecChar2String(tmp, 16, 3);
		bufForData.dataDtmfSysInfor.Bot = DecChar2String(tmp, 24, 16);
		bufForData.dataDtmfSysInfor.Eot = DecChar2String(tmp, 40, 16);
		bufForData.dataDtmfSysInfor.Stun = DecChar2String(tmp, 56, 16);
		bufForData.dataDtmfSysInfor.Kill = DecChar2String(tmp, 72, 16);
		Array.Copy(FullData, 33368, buf2, 0, 8);
		ushort val = (ushort)((buf2[1] << 8) + buf2[0]);
		bufForData.dataDtmfEncInfor.UseFlg = (ushort)(~val);
		int len = 33376;
        for (int i = 0; i < 16; i++)
        {
            if (((bufForData.dataDtmfEncInfor.UseFlg >> i) & 1) == 1)
            {
				Array.Copy(FullData, len + i * 16, buf1, 0, 16);
				str = DecChar2String(buf1, 0, 16);
				bufForData.dataDtmfEncInfor.SetEncCode(i, str);
				bufForData.dataDtmfEncInfor.SetEncLen(i, str.Length);
            }
            else
            {
                bufForData.dataDtmfEncInfor.SetEncCode(i, "");
                bufForData.dataDtmfEncInfor.SetEncLen(i, 0);
            }
        }
    }

    private void ConvertTwoTone()
    {
		byte[] buf1 = new byte[8];
		byte[] buf2 = new byte[DatTwoToneInfo.One_Enc_List_Size * DatTwoToneInfo.Enc_List_Num];
		byte[] tmp = new byte[DatTwoToneInfo.One_Enc_List_Size];
		byte[] tmp2 = new byte[DatTwoToneInfo.Dec_Para_Size];
		string str = "";
		ASCIIEncoding code = new ASCIIEncoding();
		Array.Copy(FullData, 33792, buf1, 0, 8);
		bufForData.dataTwoTone.FirstTone = (byte)(buf1[0] / 5 - 1);
		bufForData.dataTwoTone.SecondTone = (byte)(buf1[1] / 5 - 1);
		bufForData.dataTwoTone.ToneDur = (byte)(buf1[2] / 5 - 1);
		bufForData.dataTwoTone.ToneInt = buf1[3];
		bufForData.dataTwoTone.SToneSW = buf1[4];
		int len = 33808;
		int val;
        for (int i = 0; i < DatTwoToneInfo.Enc_List_Num; i++)
        {
			Array.Copy(FullData, len + i * DatTwoToneInfo.One_Enc_List_Size, tmp, 0, DatTwoToneInfo.One_Enc_List_Size);
			val = (tmp[0] << 8) + tmp[1];
			if (val >= 288 && val <= 3116)
            {
				bufForData.dataTwoTone.SetEncFreq1(i, val.ToString());
            }
            else
            {
                bufForData.dataTwoTone.SetEncFreq1(i, "");
            }
			val = (tmp[2] << 8) + tmp[3];
			if (val >= 288 && val <= 3116)
            {
				bufForData.dataTwoTone.SetEncFreq2(i, val.ToString());
            }
            else
            {
                bufForData.dataTwoTone.SetEncFreq2(i, "");
            }
            int j;
			for (j = 0; j < 12 && tmp[4 + j] != 0 && tmp[4 + j] <= 127; j++)
            {
            }
			str = code.GetString(tmp, 4, j);
			bufForData.dataTwoTone.SetEncName(i, str);
        }
		Array.Copy(FullData, 34064, tmp2, 0, DatTwoToneInfo.Dec_Para_Size);
		bufForData.dataTwoTone.DecodeRsp = tmp2[0];
		bufForData.dataTwoTone.ReseTim = tmp2[1] - 10;
		bufForData.dataTwoTone.DecFormat = tmp2[2];
		val = (tmp2[4] << 8) + tmp2[5];
		if (val >= 288 && val <= 3116)
        {
			bufForData.dataTwoTone.Atone = val.ToString();
        }
        else
        {
            bufForData.dataTwoTone.Atone = "";
        }
		val = (tmp2[6] << 8) + tmp2[7];
		if (val >= 288 && val <= 3116)
        {
			bufForData.dataTwoTone.Btone = val.ToString();
        }
        else
        {
            bufForData.dataTwoTone.Btone = "";
        }
		val = (tmp2[8] << 8) + tmp2[9];
		if (val >= 288 && val <= 3116)
        {
			bufForData.dataTwoTone.Ctone = val.ToString();
        }
        else
        {
            bufForData.dataTwoTone.Ctone = "";
        }
		val = (tmp2[10] << 8) + tmp2[11];
		if (val >= 288 && val <= 3116)
        {
			bufForData.dataTwoTone.Dtone = val.ToString();
        }
        else
        {
            bufForData.dataTwoTone.Dtone = "";
        }
    }

    private void ConvertFiveTone()
    {
		byte[] buf1 = new byte[DatFiveToneDecInfo.Dec_Size];
		byte[] tmp = new byte[DatFiveToneEncInfo.One_Enc_List_Size * DatFiveToneEncInfo.Enc_List_Num];
		byte[] tmp2 = new byte[DatFiveToneEncInfo.One_Enc_List_Size];
		byte[] tmp3 = new byte[DatFiveToneEncInfo.Enc_PTTID_Size];
		byte[] tmp4 = new byte[13];
		byte[] infoCD = new byte[16];
		string str = "";
		ASCIIEncoding code = new ASCIIEncoding();
		Array.Copy(FullData, 34432, tmp, 0, DatFiveToneEncInfo.One_Enc_List_Size * DatFiveToneEncInfo.Enc_List_Num);
        for (int i = 0; i < DatFiveToneEncInfo.Enc_List_Num; i++)
        {
			Array.Copy(tmp, i * DatFiveToneEncInfo.One_Enc_List_Size, tmp2, 0, DatFiveToneEncInfo.One_Enc_List_Size);
			if (tmp2[0] > 13)
            {
				tmp2[0] = 0;
            }
			bufForData.dataFiveToneEnc.SetEncStand(i, tmp2[0]);
			bufForData.dataFiveToneEnc.SetEncCodeTim(i, (byte)(tmp2[1] - 7));
			if (tmp2[2] > 24)
            {
				tmp2[2] = 0;
            }
			bufForData.dataFiveToneEnc.SetEncCodeLen(i, tmp2[2]);
			str = Byte2Str(tmp2, 3, tmp2[2], 20);
			bufForData.dataFiveToneEnc.SetEncID(i, str);
			if (tmp2[23] > 3)
            {
				tmp2[23] = 0;
            }
			bufForData.dataFiveToneEnc.SetEncScall(i, tmp2[23]);
            int j;
			for (j = 0; j < 8 && tmp2[24 + j] != 0 && tmp2[24 + j] <= 127; j++)
            {
            }
			str = code.GetString(tmp2, 24, j);
			bufForData.dataFiveToneEnc.SetEncName(i, str);
        }
		Array.Copy(FullData, 37760, tmp4, 0, 13);
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 8; j++)
            {
				bool num = ((tmp4[i] >> j) & 1) == 0;
                bufForData.dataFiveToneEnc.SetTblEn(i * 8 + j, num ? ((byte)1) : ((byte)0));
            }
        }
		Array.Copy(FullData, 37888, tmp3, 0, DatFiveToneEncInfo.Enc_PTTID_Size);
		bufForData.dataFiveToneEnc.PidStandS = tmp3[0];
		bufForData.dataFiveToneEnc.PidCodeTimS = (byte)(tmp3[1] - 7);
		if (tmp3[2] > 20)
        {
			tmp3[2] = 0;
        }
		bufForData.dataFiveToneEnc.PidCodeLenS = tmp3[2];
		str = Byte2Str(tmp3, 3, tmp3[2], 12);
		bufForData.dataFiveToneEnc.PidStart = str;
		bufForData.dataFiveToneEnc.PidStandE = tmp3[16];
		bufForData.dataFiveToneEnc.PidCodeTimE = (byte)(tmp3[17] - 7);
		if (tmp3[18] > 20)
        {
			tmp3[18] = 0;
        }
		bufForData.dataFiveToneEnc.PidCodeLenE = tmp3[18];
		str = Byte2Str(tmp3, 19, tmp3[18], 12);
		bufForData.dataFiveToneEnc.PidEnd = str;
		Array.Copy(FullData, 37952, buf1, 0, DatFiveToneDecInfo.Dec_Size);
		bufForData.dataFiveToneDec.DecRsp = buf1[0];
		bufForData.dataFiveToneDec.DecStand = buf1[1];
		bufForData.dataFiveToneDec.DecToneTim = (byte)(buf1[2] - 7);
		str = DecChar2String(buf1, 3, 5);
		bufForData.dataFiveToneDec.Did = str;
		bufForData.dataFiveToneDec.PreTime = buf1[11] * 10;
		bufForData.dataFiveToneDec.CodeDly = buf1[12] * 10;
		if (buf1[13] >= 5 && buf1[13] <= 75)
        {
			bufForData.dataFiveToneDec.PttIDPause = (byte)(buf1[13] - 4);
        }
        else
        {
            bufForData.dataFiveToneDec.PttIDPause = 0;
        }
		bufForData.dataFiveToneDec.ResetTime = (byte)(buf1[14] - 10);
		bufForData.dataFiveToneDec.FirstCodeTim = buf1[15] * 10;
		bufForData.dataFiveToneDec.FiveAni = buf1[16];
		if (buf1[18] == 11)
        {
            bufForData.dataFiveToneDec.StopCode = 1;
        }
		else if (buf1[18] == 12)
        {
            bufForData.dataFiveToneDec.StopCode = 2;
        }
		else if (buf1[18] == 13)
        {
            bufForData.dataFiveToneDec.StopCode = 3;
        }
		else if (buf1[18] == 15)
        {
            bufForData.dataFiveToneDec.StopCode = 4;
        }
        else
        {
            bufForData.dataFiveToneDec.StopCode = 0;
        }
		bufForData.dataFiveToneDec.StopCodetime = buf1[19] * 10;
		bufForData.dataFiveToneDec.DecCodetime = (ushort)(buf1[20] * 10);
		int len = 38016;
        for (int i = 0; i < 8; i++)
        {
			Array.Copy(FullData, len, infoCD, 0, 16);
			bufForData.dataFiveToneInfoCd.SetFunc(i, infoCD[0]);
			bufForData.dataFiveToneInfoCd.SetRspInfo(i, infoCD[1]);
			if (infoCD[2] > 5)
            {
				infoCD[2] = 0;
            }
			bufForData.dataFiveToneInfoCd.SetCdLen(i, infoCD[2]);
			str = Byte2Str(infoCD, 3, infoCD[2], 12);
			bufForData.dataFiveToneInfoCd.SetDecID(i, str);
			str = code.GetString(infoCD, 10, 6);
			bufForData.dataFiveToneInfoCd.SetDecName(i, str);
			len += 16;
        }
    }

    private void ConvertMdc()
    {
		byte[] buf1 = new byte[DatMdcBiis.BIIS_Size];
		byte[] buf2 = new byte[8];
		byte[] tmp = new byte[DatMdcDecInfo.One_List_Size * DatMdcDecInfo.Max_Num];
		byte[] tmp2 = new byte[DatMdcDecInfo.One_List_Size];
		string str = "";
		ASCIIEncoding code = new ASCIIEncoding();
		Array.Copy(FullData, 38272, buf2, 0, 8);
        for (int i = 0; i < 8; i++)
        {
			bufForData.dataMdcDecInfo.SetSysList(i, buf2[i]);
        }
		int len = 38280;
        for (int i = 0; i < 5; i++)
        {
			Array.Copy(FullData, len + i * 8, buf2, 0, 8);
			bufForData.dataMdcPara[i].CtrlSw = (byte)(buf2[0] >> 7);
			bufForData.dataMdcPara[i].DecTone = (byte)((buf2[0] >> 6) & 1);
			bufForData.dataMdcPara[i].EncID = (ushort)((buf2[1] << 8) + buf2[2]);
			bufForData.dataMdcPara[i].PreTim = (ushort)(buf2[3] * 10);
			bufForData.dataMdcPara[i].SqlDly = (ushort)(buf2[4] * 10);
			bufForData.dataMdcPara[i].DecRst = (ushort)(buf2[5] * 10);
			bufForData.dataMdcPara[i].EncSync = buf2[6];
			bufForData.dataMdcPara[i].DecSync = (byte)(buf2[7] - 29);
        }
		len = 38320;
        for (int i = 0; i < 5; i++)
        {
			Array.Copy(FullData, len + i * 8, buf2, 0, 8);
			int val = (buf2[1] + (buf2[0] << 8)) * 100;
			bufForData.dataMdcPttID[i].BotTime = (ushort)val;
			val = (buf2[3] + (buf2[2] << 8)) * 100;
			bufForData.dataMdcPttID[i].EotTime = (ushort)val;
			bufForData.dataMdcPttID[i].EncEn = (byte)((buf2[4] >> 7) & 1);
			bufForData.dataMdcPttID[i].DecEn = (byte)((buf2[4] >> 6) & 1);
			bufForData.dataMdcPttID[i].BotEn = (byte)((buf2[4] >> 5) & 1);
			bufForData.dataMdcPttID[i].EotEn = (byte)((buf2[4] >> 4) & 1);
			bufForData.dataMdcPttID[i].TxTone = (byte)((buf2[4] >> 3) & 1);
			bufForData.dataMdcPttID[i].RxTone = (byte)((buf2[4] >> 2) & 1);
        }
		Array.Copy(FullData, 38360, tmp, 0, 16);
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 8; j++)
            {
				bool num = ((tmp[i] >> j) & 1) == 0;
				bufForData.dataMdcDecInfo.SetTblEn(i * 8 + j, num ? ((byte)1) : ((byte)0));
            }
        }
		Array.Copy(FullData, 38384, tmp, 0, DatMdcDecInfo.One_List_Size * DatMdcDecInfo.Max_Num);
        for (int i = 0; i < DatMdcDecInfo.Max_Num; i++)
        {
			Array.Copy(tmp, i * DatMdcDecInfo.One_List_Size, tmp2, 0, DatMdcDecInfo.One_List_Size);
			int val = (tmp2[0] << 8) + tmp2[1];
			bufForData.dataMdcDecInfo.SetDecID(i, val.ToString("0000"));
			bufForData.dataMdcDecInfo.SetDecRsp(i, tmp2[2]);
			str = CharSwap2String(tmp2, 4, 12);
			bufForData.dataMdcDecInfo.SetDecName(i, str);
        }
		Array.Copy(FullData, 40064, buf1, 0, DatMdcBiis.BIIS_Size);
		bufForData.dataMdcBiis.SelfID = (ushort)((buf1[0] << 8) + buf1[1]);
		bufForData.dataMdcBiis.GrpID = (ushort)((buf1[2] << 8) + buf1[3]);
		bufForData.dataMdcBiis.Sync = (ushort)((buf1[4] << 8) + buf1[5]);
		bufForData.dataMdcBiis.ZoneCode = buf1[6];
		bufForData.dataMdcBiis.PreTime = (ushort)(buf1[7] * 10);
		bufForData.dataMdcBiis.ToneSw = buf1[8];
    }

    private void ConvertEmerg()
    {
		byte[] buf = new byte[DatEmergInfo.Emerg_Sys_Size];
		byte[] tmp = new byte[DatEmergInfo.Emerg_Sys_Size * DatEmergInfo.Max_Emerg_Sys_Num];
		int len = 40192;
		Array.Copy(FullData, 40200, tmp, 0, DatEmergInfo.Emerg_Sys_Size * DatEmergInfo.Max_Emerg_Sys_Num);
        for (int i = 0; i < DatEmergInfo.Max_Emerg_Sys_Num; i++)
        {
			Array.Copy(tmp, i * DatEmergInfo.Emerg_Sys_Size, buf, 0, DatEmergInfo.Emerg_Sys_Size);
			bufForData.dataEmergInfor[i].Type = buf[7];
			bufForData.dataEmergInfor[i].Mode = buf[6];
			bufForData.dataEmergInfor[i].GrpNo = buf[5];
			bufForData.dataEmergInfor[i].ExgTime = buf[4];
			bufForData.dataEmergInfor[i].TxTime = buf[3];
			bufForData.dataEmergInfor[i].RxTime = buf[2];
			bufForData.dataEmergInfor[i].ChSel = buf[1];
			bufForData.dataEmergInfor[i].Duration = buf[0];
			bufForData.dataEmergInfor[i].Chn = buf[14];
			bufForData.dataEmergInfor[i].Zone = buf[15];
        }
    }

    private string Char2AprsCode(byte[] bt, int st, int len)
    {
		ASCIIEncoding code = new ASCIIEncoding();
		string byStr = "";
		int j = 0;
        for (int i = st; i < st + len && bt[i] != 32 && bt[i] != byte.MaxValue; i++)
        {
			j++;
        }
		return code.GetString(bt, st, j);
	}

	private double LatLongToData(byte data0, byte data1, byte data2, byte data3)
	{
		byte integer_part = 0;
		int decimal_part = 0;
		return (double)(int)(byte)(((data0 & 0xF0) >> 4) * 100 + (data0 & 0xF) * 10 + ((data1 & 0xF0) >> 4)) + (double)((data1 & 0xF) * 10000 + ((data2 & 0xF0) >> 4) * 1000 + (data2 & 0xF) * 100 + ((data3 & 0xF0) >> 4) * 10 + (data3 & 0xF)) / 100000.0;
    }

    private void ConvertAprs()
    {
		byte[] tmp = new byte[456];
		byte[] buf = new byte[6];
		string sst = "";
		string freqstr = "";
		ASCIIEncoding asc = new ASCIIEncoding();
		Array.Copy(FullData, 40448, tmp, 0, 456);
		bufForData.dataAprsSet.DesNo = Char2AprsCode(tmp, 0, 6);
		bufForData.dataAprsSet.DesID = tmp[6];
		bufForData.dataAprsSet.PassAll = (byte)((tmp[7] >> 7) & 1);
		bufForData.dataAprsSet.Position = (byte)((tmp[7] >> 6) & 1);
		bufForData.dataAprsSet.MicE = (byte)((tmp[7] >> 5) & 1);
		bufForData.dataAprsSet.Object = (byte)((tmp[7] >> 4) & 1);
		bufForData.dataAprsSet.Item = (byte)((tmp[7] >> 3) & 1);
		bufForData.dataAprsSet.Message = (byte)((tmp[7] >> 2) & 1);
		bufForData.dataAprsSet.WxReport = (byte)((tmp[7] >> 1) & 1);
		bufForData.dataAprsSet.NMEAReport = (byte)(tmp[7] & 1);
		bufForData.dataAprsSet.SrcNo = Char2AprsCode(tmp, 8, 6);
		bufForData.dataAprsSet.SrcID = tmp[14];
		bufForData.dataAprsSet.StatusReport = (byte)((tmp[15] >> 7) & 1);
		bufForData.dataAprsSet.Other = (byte)((tmp[15] >> 6) & 1);
		byte val = (byte)((tmp[15] & 0x30) >> 4);
		if (FormMain.HIGH_VALID != 2)
		{
			val = ((val == 2) ? ((byte)1) : ((byte)0));
		}
		bufForData.dataAprsSet.Power = val;
		bufForData.dataAprsSet.Band = (byte)((tmp[15] >> 3) & 1);
		bufForData.dataAprsSet.BeepTone = (byte)((tmp[15] >> 2) & 1);
		bufForData.dataAprsSet.LongDir = (byte)((tmp[15] >> 1) & 1);
		bufForData.dataAprsSet.LatDir = (byte)(tmp[15] & 1);
		bufForData.dataAprsSet.PreTime = tmp[16];
		bufForData.dataAprsSet.CodeDly = tmp[17];
		bufForData.dataAprsSet.Ctdcs = SubVoiceConvert(tmp[18], tmp[19]);
		bufForData.dataAprsSet.PostionTable = ((char)tmp[20]).ToString();
		bufForData.dataAprsSet.PostionIcon = ((char)tmp[21]).ToString();
		bufForData.dataAprsSet.RxCallSignNum = 0;
        DatAprsSet.CallSign_Total = 0;
        for (int i = 0; i < 8; i++)
        {
			sst = Char2AprsCode(tmp, i * 8 + 24, 6);
			if (sst != "")
            {
                DatAprsSet.CallSign_Total++;
            }
			bufForData.dataAprsSet.SetCallSignNo(i, sst);
			if (tmp[i * 8 + 24 + 6] > 15)
			{
				tmp[i * 8 + 24 + 6] = 0;
			}
			bufForData.dataAprsSet.SetCallSignID(i, tmp[i * 8 + 24 + 6]);
		}
		bufForData.dataAprsSet.SendInterval = tmp[88];
		bufForData.dataAprsSet.RegularlySend = tmp[89];
		bufForData.dataAprsSet.APRSDisplayTime = tmp[90];
		bufForData.dataAprsSet.Beacon = (byte)((tmp[92] >> 7) & 1);
		bufForData.dataAprsSet.HeightType = (byte)((tmp[92] >> 6) & 1);
		bufForData.dataAprsSet.PttId = (byte)(((byte)((tmp[92] & 0x30) >> 4) <= 3) ? ((byte)((tmp[92] & 0x30) >> 4)) : 0);
		bufForData.dataAprsSet.MicEType = (byte)(tmp[92] & 7);
		bufForData.dataAprsSet.TxtLength = tmp[95];
		bufForData.dataAprsSet.Long = (double)BitConverter.ToInt32(tmp, 96) / 100000.0;
		bufForData.dataAprsSet.Lat = (double)BitConverter.ToInt32(tmp, 100) / 100000.0;
		bufForData.dataAprsSet.Height = BitConverter.ToInt32(tmp, 104);
		bufForData.dataAprsSet.Txt = Encoding.UTF8.GetString(tmp, 108, bufForData.dataAprsSet.TxtLength);
		try
		{
			int freq_tmp = Convert.ToInt32((tmp[168] + (tmp[169] << 8) + (tmp[170] << 16) + (tmp[171] << 24)).ToString("X"));
			bufForData.dataAprsSet.Freq1 = freq_tmp.ToString().Insert(3, ".");
		}
		catch
		{
		}
		try
		{
			int freq_tmp = Convert.ToInt32((tmp[172] + (tmp[173] << 8) + (tmp[174] << 16) + (tmp[175] << 24)).ToString("X"));
			bufForData.dataAprsSet.Freq2 = freq_tmp.ToString().Insert(3, ".");
		}
		catch
		{
		}
		try
		{
			int freq_tmp = Convert.ToInt32((tmp[176] + (tmp[177] << 8) + (tmp[178] << 16) + (tmp[179] << 24)).ToString("X"));
			bufForData.dataAprsSet.Freq3 = freq_tmp.ToString().Insert(3, ".");
		}
		catch
		{
		}
		try
		{
			int freq_tmp = Convert.ToInt32((tmp[180] + (tmp[181] << 8) + (tmp[182] << 16) + (tmp[183] << 24)).ToString("X"));
			bufForData.dataAprsSet.Freq4 = freq_tmp.ToString().Insert(3, ".");
		}
		catch
		{
		}
		try
		{
			int freq_tmp = Convert.ToInt32((tmp[184] + (tmp[185] << 8) + (tmp[186] << 16) + (tmp[187] << 24)).ToString("X"));
			bufForData.dataAprsSet.Freq5 = freq_tmp.ToString().Insert(3, ".");
		}
		catch
		{
		}
		try
		{
			int freq_tmp = Convert.ToInt32((tmp[188] + (tmp[189] << 8) + (tmp[190] << 16) + (tmp[191] << 24)).ToString("X"));
			bufForData.dataAprsSet.Freq6 = freq_tmp.ToString().Insert(3, ".");
		}
		catch
		{
		}
		try
		{
			int freq_tmp = Convert.ToInt32((tmp[192] + (tmp[193] << 8) + (tmp[194] << 16) + (tmp[195] << 24)).ToString("X"));
			bufForData.dataAprsSet.Freq7 = freq_tmp.ToString().Insert(3, ".");
		}
		catch
		{
		}
		try
		{
			int freq_tmp = Convert.ToInt32((tmp[196] + (tmp[197] << 8) + (tmp[198] << 16) + (tmp[199] << 24)).ToString("X"));
			bufForData.dataAprsSet.Freq8 = freq_tmp.ToString().Insert(3, ".");
		}
		catch
		{
		}
		for (int i = 0; i < 32; i++)
		{
			sst = Char2AprsCode(tmp, i * 8 + 200, 6);
			if (sst != "")
			{
				bufForData.dataAprsSet.RxCallSignNum++;
			}
			bufForData.dataAprsSet.SetRxCallSignNo(i, sst);
			if (tmp[i * 8 + 200 + 6] > 15)
			{
				tmp[i * 8 + 200 + 6] = 0;
            }
			bufForData.dataAprsSet.SetRxCallSignID(i, tmp[i * 8 + 200 + 6]);
			bufForData.dataAprsSet.SetRxFilter(i, (tmp[i * 8 + 200 + 7] == 1) ? ((byte)1) : ((byte)0));
        }
    }

    private void ConvertGpsValidFlg()
    {
		int cnt = DatGpsBook.MAX_GpsBook_Num / 8;
		byte[] tmpInfo = new byte[cnt];
		Array.Copy(FullData, 40960, tmpInfo, 0, cnt);
		for (int i = 0; i < cnt; i++)
        {
            for (int j = 0; j < 8; j++)
            {
				bool num = ((tmpInfo[i] >> j) & 1) == 0;
				bufForData.dataGpsBook[i * 8 + j].UseFlg = (num ? ((byte)1) : ((byte)0));
            }
        }
    }

    private void ConvertGpsBook()
    {
		byte[] buf = new byte[16];
        ConvertGpsValidFlg();
        DatGpsBook.GpsBook_Total = 0;
		int len = 40976;
        for (int i = 0; i < DatGpsBook.MAX_GpsBook_Num; i++)
        {
			Array.Copy(FullData, len + i * 16, buf, 0, 16);
            if (bufForData.dataGpsBook[i].UseFlg == 1)
            {
                DatGpsBook.GpsBook_Total++;
				bufForData.dataGpsBook[i].CodeID = buf[0];
				bufForData.dataGpsBook[i].CodeName = CharSwap2String(buf, 2, 14);
            }
        }
    }

    private void DataToRead()
    {
        if (!FileOpenFlg)
        {
            for (int i = 0; i < DpDataLen; i++)
            {
                FullData[i] = (byte)(seed ^ FullData[i]);
            }
        }
        FileOpenFlg = false;
        ConvertDevice();
        ConvertChnValidFlg();
        ConvertChn();
        ConvertVFO();
        ConvertSetting();
        ConvertZone();
        ConvertScan();
        ConvertDTMF();
        ConvertTwoTone();
        ConvertFiveTone();
        ConvertMdc();
        ConvertEmerg();
        ConvertAprs();
        ConvertGpsBook();
    }

    private void DeviceConvert()
    {
		byte[] devbuf = new byte[128];
		string datetime = "";
		ASCIIEncoding asc = new ASCIIEncoding();
		int len = 0;
		byte[] dn = StringSwap2Char(appData.dataDevice.DevName);
		Array.Copy(dn, 0, FullData, len, dn.Length);
		len += 16;
		int val;
        for (int i = 0; i < 4; i++)
        {
			val = Convert.ToInt32(appData.dataDevice.GetRxStartFreq(i).ToString(), 16);
			FullData[len] = (byte)val;
			FullData[len + 1] = (byte)(val >> 8);
			FullData[len + 2] = (byte)(val >> 16);
			FullData[len + 3] = (byte)(val >> 24);
			val = Convert.ToInt32(appData.dataDevice.GetRxEndFreq(i).ToString(), 16);
			FullData[len + 4] = (byte)val;
			FullData[len + 5] = (byte)(val >> 8);
			FullData[len + 6] = (byte)(val >> 16);
			FullData[len + 7] = (byte)(val >> 24);
			len += 8;
        }
		len = 48;
        for (int i = 0; i < 4; i++)
        {
			val = Convert.ToInt32(appData.dataDevice.GetTxStartFreq(i).ToString(), 16);
			FullData[len] = (byte)val;
			FullData[len + 1] = (byte)(val >> 8);
			FullData[len + 2] = (byte)(val >> 16);
			FullData[len + 3] = (byte)(val >> 24);
			val = Convert.ToInt32(appData.dataDevice.GetTxEndFreq(i).ToString(), 16);
			FullData[len + 4] = (byte)val;
			FullData[len + 5] = (byte)(val >> 8);
			FullData[len + 6] = (byte)(val >> 16);
			FullData[len + 7] = (byte)(val >> 24);
			len += 8;
        }
		len = 80;
		byte[] sv = asc.GetBytes(appData.dataDevice.SoftVer);
		Array.Copy(sv, 0, FullData, len, sv.Length);
		len += 8;
		byte[] hv = asc.GetBytes(appData.dataDevice.HardVer);
		Array.Copy(hv, 0, FullData, len, hv.Length);
		len = 96;
		byte[] pd = asc.GetBytes(DateTime.Now.ToString("yyyy.MM.dd HH:mm"));
		Array.Copy(pd, 0, FullData, len, pd.Length);
		len = 112;
        if (FormMain.VER_TYPE == 0)
        {
			FullData[len] = Settings.FreqBand;
        }
        else
        {
			FullData[len] = appData.dataDevice.Stand;
        }
		val = (appData.dataDevice.Dis200m << 7) | (appData.dataDevice.Dis350m << 6) | (appData.dataDevice.Dis500m << 5);
		FullData[len + 1] = (byte)val;
		FullData[len + 2] = appData.dataDevice.Scramble;
		val = (appData.dataDevice.EnZone << 2) | (appData.dataDevice.EnFlight << 3) | (appData.dataDevice.EnNoise << 4) | (appData.dataDevice.EnFalldn << 5) | (appData.dataDevice.EnBT << 6) | (appData.dataDevice.EnGps << 7);
		FullData[len + 3] = (byte)val;
		val = (appData.dataDevice.EnBdc1200 << 3) | (appData.dataDevice.EnMdc1200 << 4) | (appData.dataDevice.En5Tone << 5) | (appData.dataDevice.En2Tone << 6) | (appData.dataDevice.EnDTMF << 7);
		FullData[len + 4] = (byte)val;
		FullData[len + 7] = (byte)(appData.dataDevice.PkeyCnt + 1);
		len = 120;
        for (int i = 0; i < 8; i++)
        {
			FullData[len + i] = (byte)appData.dataDevice.GetProKey(i + 8);
        }
    }

    private byte[] FreqToData(string freq)
    {
		byte[] bufForFreq = new byte[4];
		uint tmp = 0u;
		string result = "";
        try
        {
			result = Regex.Replace(freq, "[^0-9]+", "");
			double buf1 = double.Parse(result);
			tmp = Convert.ToUInt32(result, 16);
        }
        catch
        {
			tmp = (uint)appData.dataDevice.GetRxStartFreq(0);
        }
		bufForFreq[3] = (byte)(tmp >> 24);
		bufForFreq[2] = (byte)(tmp >> 16);
		bufForFreq[1] = (byte)(tmp >> 8);
		bufForFreq[0] = (byte)tmp;
		return bufForFreq;
    }

    private byte[] SubAudioToData(string strDat)
    {
		string strbuf = "";
		byte[] buf = new byte[2];
        try
        {
            if ('D' == strDat[0] && strDat.Length == 5)
            {
				int buffer = Convert.ToUInt16(strDat.Remove(0, 1).Remove(3, 1), 16);
                if ('N' == strDat[4])
                {
					buf[0] = (byte)((byte)(buffer >> 8) | 0x80);
					buf[1] = (byte)buffer;
                }
                else
                {
					buf[0] = (byte)((byte)(buffer >> 8) | 0xC0);
					buf[1] = (byte)buffer;
                }
            }
            else if (strDat != "OFF")
            {
                strDat = strDat.Remove(strDat.Length - 2, 1);
				ushort intDat = Convert.ToUInt16(ushort.Parse(strDat).ToString(), 16);
				buf[0] = (byte)(intDat >> 8);
				buf[1] = (byte)intDat;
            }
            else
            {
				buf[0] = 0;
				buf[1] = 0;
            }
        }
        catch
        {
			buf[0] = 0;
			buf[1] = 0;
        }
		return buf;
    }

    private void ChnConvert()
    {
		byte[] freqbuf = new byte[4];
		byte[] subbuf = new byte[2];
		byte[] nabuf = new byte[16];
		byte[] buf = new byte[DatChannelInfo.Chn_Info_Size];
		byte[] buf2 = new byte[DatChannelInfo.Chn_Info_Size];
		int len = 128;
        for (int i = 0; i < DatChannelInfo.Max_Chn_Num; i++)
        {
			Array.Clear(buf, 0, DatChannelInfo.Chn_Info_Size);
            if (appData.dataChnInfor[i].UseFlg == 1)
            {
				Array.Copy(FreqToData(appData.dataChnInfor[i].RxFreq), 0, buf, 0, 4);
				Array.Copy(FreqToData(appData.dataChnInfor[i].TxFreq), 0, buf, 4, 4);
				Array.Copy(SubAudioToData(appData.dataChnInfor[i].RxCtsDcs), 0, buf, 8, 2);
				Array.Copy(SubAudioToData(appData.dataChnInfor[i].TxCtsDcs), 0, buf, 10, 2);
				buf[16] = 0;
                if (2 == FormMain.HIGH_VALID)
                {
					buf[16] |= (byte)(appData.dataChnInfor[i].Power << 6);
                }
                else if (appData.dataChnInfor[i].Power == 1)
                {
					buf[16] |= 128;
                }
                else
                {
					buf[16] |= (byte)(appData.dataChnInfor[i].Power << 6);
                }
                if (appData.dataChnInfor[i].Wideth == 1)
                {
					buf[16] |= 32;
                }
				buf[16] |= (byte)(appData.dataChnInfor[i].Offsetdir << 2);
				buf[16] |= (byte)(appData.dataChnInfor[i].FreqInvert << 1);
				buf[16] |= appData.dataChnInfor[i].TalkAround;
				buf[17] = 0;
				buf[17] |= (byte)(appData.dataChnInfor[i].FivetonePtt << 6);
				buf[17] |= (byte)(appData.dataChnInfor[i].DtmfPtt << 4);
				buf[17] |= appData.dataChnInfor[i].SqType;
				buf[18] = 0;
				buf[18] |= (byte)(appData.dataChnInfor[i].SignalType << 5);
				buf[18] |= appData.dataChnInfor[i].JumpFreq;
				buf[19] = 0;
				buf[19] |= (byte)(appData.dataChnInfor[i].BusyLock << 6);
				buf[19] |= (byte)(appData.dataChnInfor[i].TxDis << 5);
				buf[20] = (byte)(appData.dataChnInfor[i].Scram << 4);
				buf[20] |= (byte)(appData.dataChnInfor[i].Compand << 5);
				buf[20] |= (byte)(appData.dataChnInfor[i].CepinDcs << 6);
				buf[20] |= (byte)(appData.dataChnInfor[i].Cepin24bit << 7);
				buf[24] = appData.dataChnInfor[i].FreqStep;
				buf[25] = appData.dataChnInfor[i].DtmfIdx;
				buf[26] = appData.dataChnInfor[i].TwotoneIdx;
				buf[27] = appData.dataChnInfor[i].FivetoneIdx;
				buf[28] = appData.dataChnInfor[i].MdcIdx;
				buf[29] = appData.dataChnInfor[i].Scanlist;
				buf[30] = appData.dataChnInfor[i].Emerglist;
				Array.Copy(StringSwap2Char(appData.dataChnInfor[i].ChnName), 0, buf, 32, 16);
            }
			Array.Copy(buf, 0, FullData, len + i * DatChannelInfo.Chn_Info_Size, DatChannelInfo.Chn_Info_Size);
        }
    }

    private void VFOConvert()
    {
		byte[] freqbuf = new byte[4];
		byte[] subbuf = new byte[2];
		byte[] nabuf = new byte[16]
        {
            255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
            255, 255, 255, 255, 255, 255
        };
		byte[] buf = new byte[DatChannelInfo.Chn_Info_Size];
		int len = 30976;
        for (int i = 0; i < 2; i++)
        {
			Array.Clear(buf, 0, DatChannelInfo.Chn_Info_Size);
			Array.Copy(FreqToData(appData.dataVFOInfor[i].RxFreq), 0, buf, 0, 4);
			Array.Copy(FreqToData(appData.dataVFOInfor[i].TxFreq), 0, buf, 4, 4);
			Array.Copy(SubAudioToData(appData.dataVFOInfor[i].RxCtsDcs), 0, buf, 8, 2);
			Array.Copy(SubAudioToData(appData.dataVFOInfor[i].TxCtsDcs), 0, buf, 10, 2);
			Array.Copy(FreqToData(appData.dataVFOInfor[i].DivFreq), 0, buf, 12, 4);
			buf[16] = 0;
            if (2 == FormMain.HIGH_VALID)
            {
				buf[16] |= (byte)(appData.dataVFOInfor[i].Power << 6);
            }
            else if (appData.dataVFOInfor[i].Power == 1)
            {
				buf[16] |= 128;
            }
            else
            {
				buf[16] |= (byte)(appData.dataVFOInfor[i].Power << 6);
            }
            if (appData.dataVFOInfor[i].Wideth == 1)
            {
				buf[16] |= 32;
            }
			buf[16] |= (byte)(appData.dataVFOInfor[i].Offsetdir << 2);
			buf[16] |= (byte)(appData.dataVFOInfor[i].FreqInvert << 1);
			buf[16] |= appData.dataVFOInfor[i].TalkAround;
			buf[17] = 0;
			buf[17] |= (byte)(appData.dataVFOInfor[i].FivetonePtt << 6);
			buf[17] |= (byte)(appData.dataVFOInfor[i].DtmfPtt << 4);
			buf[17] |= appData.dataVFOInfor[i].SqType;
			buf[18] = 0;
			buf[18] |= (byte)(appData.dataVFOInfor[i].SignalType << 5);
			buf[18] |= appData.dataVFOInfor[i].JumpFreq;
			buf[19] = 0;
			buf[19] |= (byte)(appData.dataVFOInfor[i].BusyLock << 6);
			buf[19] |= (byte)(appData.dataVFOInfor[i].TxDis << 5);
			buf[20] = (byte)(appData.dataVFOInfor[i].Scram << 4);
			buf[20] |= (byte)(appData.dataVFOInfor[i].Compand << 5);
			buf[20] |= (byte)(appData.dataVFOInfor[i].CepinDcs << 6);
			buf[20] |= (byte)(appData.dataVFOInfor[i].Cepin24bit << 7);
			buf[24] = appData.dataVFOInfor[i].FreqStep;
			buf[25] = appData.dataVFOInfor[i].DtmfIdx;
			buf[26] = appData.dataVFOInfor[i].TwotoneIdx;
			buf[27] = appData.dataVFOInfor[i].FivetoneIdx;
			buf[28] = appData.dataVFOInfor[i].MdcIdx;
			buf[29] = appData.dataVFOInfor[i].Scanlist;
			buf[30] = appData.dataVFOInfor[i].Emerglist;
			Array.Copy(StringSwap2Char(appData.dataVFOInfor[i].ChnName), 0, buf, 32, 16);
			Array.Copy(buf, 0, FullData, len + i * DatChannelInfo.Chn_Info_Size, DatChannelInfo.Chn_Info_Size);
        }
    }

    public string ToDBC(string input)
    {
		char[] c = input.ToCharArray();
		for (int i = 0; i < c.Length; i++)
        {
			if (c[i] == '\u3000')
            {
				c[i] = ' ';
            }
			else if (c[i] == '、')
            {
				c[i] = '`';
            }
			else if (c[i] == '。' || c[i] == '·')
            {
				c[i] = '.';
            }
			else if (c[i] == '【')
            {
				c[i] = '[';
            }
			else if (c[i] == '】')
            {
				c[i] = ']';
            }
			else if (c[i] == '《')
            {
				c[i] = '>';
            }
			else if (c[i] == '》')
            {
				c[i] = '<';
            }
			else if (c[i] == '…')
            {
				c[i] = '^';
            }
			else if (c[i] == '“' || c[i] == '”')
            {
				c[i] = '"';
            }
			else if (c[i] == '‘' || c[i] == '’')
            {
				c[i] = '\'';
            }
			else if (c[i] > '\uff00' && c[i] < '｟')
            {
				c[i] = (char)(c[i] - 65248);
            }
        }
		return new string(c);
    }

    private byte[] StringSwap2Char(string byStr)
    {
		byte[] dtcode = new byte[16];
		string tmp = ToDBC(byStr);
		byte[] byBr = Encoding.GetEncoding("GB2312").GetBytes(tmp);
		Array.Copy(byBr, 0, dtcode, 0, byBr.Length);
		for (int i = byBr.Length; i < 16; i++)
        {
			dtcode[i] = 0;
        }
		return dtcode;
    }

    private void SettingConvert()
    {
		byte[] buf3 = new byte[4];
		ASCIIEncoding code = new ASCIIEncoding();
		int len = 31104;
		FullData[len] = appData.dataGenSetInfor.ChAMode;
		FullData[len + 1] = appData.dataGenSetInfor.ChBMode;
		FullData[len + 2] = (byte)(appData.dataGenSetInfor.ChANum >> 8);
		FullData[len + 3] = (byte)(appData.dataGenSetInfor.ChANum & 0xFF);
		FullData[len + 4] = (byte)(appData.dataGenSetInfor.ChBNum >> 8);
		FullData[len + 5] = (byte)(appData.dataGenSetInfor.ChBNum & 0xFF);
		FullData[len + 6] = appData.dataGenSetInfor.ChAZone;
		FullData[len + 7] = appData.dataGenSetInfor.ChBZone;
        if (appData.dataGenSetInfor.BlightTime >= 1)
        {
			FullData[len + 8] = (byte)(appData.dataGenSetInfor.BlightTime + 4);
        }
        else
        {
			FullData[len + 8] = 0;
        }
		FullData[len + 9] = appData.dataGenSetInfor.BlightLv;
		FullData[len + 10] = appData.dataGenSetInfor.ChBDispMode;
		FullData[len + 10] |= (byte)(appData.dataGenSetInfor.ChADispMode << 4);
		FullData[len + 11] = appData.dataGenSetInfor.DualMode;
		FullData[len + 12] = appData.dataGenSetInfor.MainBand;
		FullData[len + 13] = appData.dataGenSetInfor.Sqlv;
		FullData[len + 14] = (byte)(appData.dataGenSetInfor.VoxLv + 1);
		FullData[len + 15] = (byte)(appData.dataGenSetInfor.Voxdettime * 5 + 10);
		FullData[len + 16] = appData.dataGenSetInfor.PoSave;
		FullData[len + 17] = appData.dataGenSetInfor.PoSaveDly;
		FullData[len + 18] = appData.dataGenSetInfor.LoneWorkTim;
		FullData[len + 19] = appData.dataGenSetInfor.LoneWorkRsp;
		FullData[len + 20] = appData.dataGenSetInfor.APO;
		FullData[len + 21] = appData.dataGenSetInfor.TOT;
		FullData[len + 22] = appData.dataGenSetInfor.PreTot;
		FullData[len + 24] = appData.dataGenSetInfor.GpsZone;
		FullData[len + 26] = appData.dataGenSetInfor.HzTo1750;
		FullData[len + 30] = appData.dataGenSetInfor.NoaaCh;
		FullData[len + 31] = appData.dataGenSetInfor.GpsID;
		FullData[len + 32] = (byte)(appData.dataGenSetInfor.VoxSW << 7);
		FullData[len + 32] |= (byte)(appData.dataGenSetInfor.AprsSw << 6);
		FullData[len + 32] |= (byte)(appData.dataGenSetInfor.LoneWork << 5);
		FullData[len + 32] |= (byte)(appData.dataGenSetInfor.DaoDi << 4);
		FullData[len + 32] |= (byte)(appData.dataGenSetInfor.Voice << 2);
		FullData[len + 32] |= appData.dataGenSetInfor.BusyLock;
		FullData[len + 33] = (byte)(appData.dataGenSetInfor.KeyLock << 7);
		FullData[len + 33] |= (byte)(appData.dataGenSetInfor.AutoKey << 6);
		FullData[len + 34] = (byte)(appData.dataGenSetInfor.Tone << 7);
		FullData[len + 34] |= (byte)(appData.dataGenSetInfor.EndTone << 5);
		FullData[len + 35] = (byte)(appData.dataGenSetInfor.GpsSW << 7);
		FullData[len + 35] |= (byte)(appData.dataGenSetInfor.GpsMode << 5);
		FullData[len + 35] |= (byte)(appData.dataGenSetInfor.GpsShare << 4);
		FullData[len + 35] |= (byte)(appData.dataGenSetInfor.GpsReq << 3);
		FullData[len + 36] = (byte)(appData.dataGenSetInfor.BlueT << 7);
		FullData[len + 36] |= (byte)(appData.dataGenSetInfor.BTpair << 5);
		FullData[len + 36] |= (byte)(appData.dataGenSetInfor.BluetAPP << 4);
		FullData[len + 37] = (byte)(appData.dataGenSetInfor.Reord << 7);
		FullData[len + 37] |= (byte)(appData.dataGenSetInfor.RecordMode << 5);
		FullData[len + 37] |= (byte)(appData.dataGenSetInfor.Engineering << 4);
		FullData[len + 37] |= (byte)(appData.dataGenSetInfor.TianQi << 3);
		FullData[len + 37] |= (byte)(appData.dataGenSetInfor.LangSel << 2);
		FullData[len + 37] |= appData.dataGenSetInfor.PownFace;
		FullData[len + 38] = (byte)(appData.dataGenSetInfor.TailFreq << 5);
		FullData[len + 38] |= (byte)(appData.dataGenSetInfor.NOAA << 4);
		FullData[len + 38] |= (byte)(appData.dataGenSetInfor.DispDir << 3);
		FullData[len + 38] |= (byte)(appData.dataGenSetInfor.FmInter << 2);
		FullData[len + 38] |= (byte)(appData.dataGenSetInfor.NoiseCancel << 1);
		FullData[len + 38] |= appData.dataGenSetInfor.EnhanceFunc;
		FullData[len + 40] = appData.dataGenSetInfor.BThold;
		FullData[len + 41] = appData.dataGenSetInfor.BTrxdly;
		FullData[len + 42] = appData.dataGenSetInfor.BTmic;
		FullData[len + 43] = appData.dataGenSetInfor.BTspk;
		buf3 = code.GetBytes(appData.dataGenSetInfor.BTpassword);
		Array.Copy(buf3, 0, FullData, len + 44, buf3.Length);
		FullData[len + 48] = appData.dataGenSetInfor.Skey1;
		FullData[len + 49] = appData.dataGenSetInfor.Skey2;
		FullData[len + 50] = appData.dataGenSetInfor.Lkey1;
		FullData[len + 51] = appData.dataGenSetInfor.Lkey2;
		FullData[len + 52] = appData.dataGenSetInfor.Skey3;
		FullData[len + 53] = appData.dataGenSetInfor.Skey4;
		FullData[len + 54] = appData.dataGenSetInfor.Lkey3;
		FullData[len + 55] = appData.dataGenSetInfor.Lkey4;
        for (int i = 0; i < 8; i++)
        {
			FullData[len + 64 + i] = byte.MaxValue;
        }
		byte[] bytePP = code.GetBytes(appData.dataGenSetInfor.PowPassword);
		Array.Copy(bytePP, 0, FullData, len + 64, bytePP.Length);
        for (int i = 0; i < 8; i++)
        {
			FullData[len + 72 + i] = byte.MaxValue;
        }
		byte[] byteWP = code.GetBytes(appData.dataGenSetInfor.WrPassword);
		Array.Copy(byteWP, 0, FullData, len + 72, byteWP.Length);
        for (int i = 0; i < 16; i++)
        {
			FullData[len + 80 + i] = byte.MaxValue;
        }
		byte[] byteRN = StringSwap2Char(appData.dataGenSetInfor.Radioname);
		Array.Copy(byteRN, 0, FullData, len + 80, byteRN.Length);
		byte[] byteBT = StringSwap2Char(appData.dataGenSetInfor.BlueTName);
		Array.Copy(byteBT, 0, FullData, len + 96, byteBT.Length);
		byte[] bytePN = StringSwap2Char(appData.dataGenSetInfor.PairName);
		Array.Copy(bytePN, 0, FullData, len + 112, bytePN.Length);
    }

    private void ChnValidFlgConvert()
    {
		int cnt = DatChannelInfo.Max_Chn_Num / 8;
		int tmpflg = 0;
		byte[] tmpInfo = new byte[cnt];
		int len = 31264;
		for (int i = 0; i < cnt; i++)
        {
			tmpflg = 0;
            for (int j = 0; j < 8; j++)
            {
				tmpflg |= appData.dataChnInfor[i * 8 + j].UseFlg << j;
            }
			tmpInfo[i] = (byte)(~tmpflg);
        }
		Array.Copy(tmpInfo, 0, FullData, len, cnt);
    }

    private void ZoneConvert()
    {
		byte[] tmp = new byte[DataApp.Zone_Size];
		byte[] buf1 = new byte[16];
		FullData[31360] = (byte)DatZoneInfo.ZoneTotal;
		int len = 31376;
        for (int i = 0; i < DataApp.Zone_Max_Num; i++)
        {
			len = 31376 + DataApp.Zone_Size * i;
			FullData[len] = (byte)appData.dataZoneInfor[i].ChnNum;
			int start = i * DataApp.Zone_Max_Chn_Num;
            for (int j = 0; j < DataApp.Zone_Max_Chn_Num; j++)
            {
				if (appData.dataChnInfor[start + j].UseFlg == 1)
                {
					int val = appData.dataZoneInfor[i].GetChnID(j);
					FullData[len + 3 + j * 2] = (byte)val;
					FullData[len + 2 + j * 2] = (byte)(val >> 8);
                }
                else
                {
					FullData[len + 3 + j * 2] = byte.MaxValue;
					FullData[len + 2 + j * 2] = byte.MaxValue;
                }
            }
			byte[] nabuf = StringSwap2Char(appData.dataZoneInfor[i].ZoneName);
			Array.Copy(nabuf, 0, FullData, len + 136, nabuf.Length);
        }
    }

    private void ScanConvert()
    {
		byte[] tmp = new byte[80];
		byte[] scanBuf = new byte[DatScanInfo.Scan_Para_Size];
		byte[] buf = new byte[8];
		int len = 33024;
		int val;
        for (int i = 0; i < DatScanInfo.Max_Scan_List_Num; i++)
        {
			val = Convert.ToInt32(appData.dataScanInfor.GetUpFreq(i).ToString(), 16);
			buf[0] = (byte)val;
			buf[1] = (byte)(val >> 8);
			buf[2] = (byte)(val >> 16);
			buf[3] = (byte)(val >> 24);
			val = Convert.ToInt32(appData.dataScanInfor.GetDwFreq(i).ToString(), 16);
			buf[4] = (byte)val;
			buf[5] = (byte)(val >> 8);
			buf[6] = (byte)(val >> 16);
			buf[7] = (byte)(val >> 24);
			Array.Copy(buf, 0, FullData, len + 8 * i, 8);
        }
		len = 33152;
		scanBuf[0] = appData.dataScanInfor.ScanMode;
		scanBuf[1] = (byte)(appData.dataScanInfor.BackScanTim + 5);
		scanBuf[2] = (byte)(appData.dataScanInfor.RxResume + 1);
		scanBuf[3] = (byte)(appData.dataScanInfor.TxResume + 1);
		scanBuf[4] = appData.dataScanInfor.RtnChType;
		scanBuf[5] = appData.dataScanInfor.PrioScan;
		val = appData.dataScanInfor.PrioChannel;
		scanBuf[6] = (byte)(val >> 8);
		scanBuf[7] = (byte)val;
		scanBuf[8] = appData.dataScanInfor.ScanRange;
		Array.Copy(scanBuf, 0, FullData, len, DatScanInfo.Scan_Para_Size);
        ScanAddFlgConvert();
    }

    private void ScanAddFlgConvert()
    {
		int cnt = DatChannelInfo.Max_Chn_Num / 8;
		int tmpflg = 0;
		byte[] tmpInfo = new byte[cnt];
		int len = 33184;
		for (int i = 0; i < cnt; i++)
        {
			tmpflg = 0;
            for (int j = 0; j < 8; j++)
            {
				tmpflg |= appData.dataChnInfor[i * 8 + j].ScanAdd << j;
            }
			tmpInfo[i] = (byte)(~tmpflg);
        }
		Array.Copy(tmpInfo, 0, FullData, len, cnt);
    }

    private byte[] Str2Byte(string Str)
    {
		int j = 0;
		byte[] tmp = new byte[2];
		byte[] dtcode = new byte[40];
		byte[] dataB = new byte[20];
		byte[] byStr = new ASCIIEncoding().GetBytes(Str);
        int i;
		for (i = 0; i < byStr.Length; i++)
        {
			if (byStr[i] >= 48 && byStr[i] <= 57)
            {
				dtcode[i] = (byte)(byStr[i] - 48);
            }
			else if (byStr[i] >= 65 && byStr[i] <= 68)
            {
				dtcode[i] = (byte)(byStr[i] - 55);
            }
			else if (byStr[i] == 42)
            {
				dtcode[i] = (byte)(byStr[i] - 28);
            }
			else if (byStr[i] == 35)
            {
				dtcode[i] = (byte)(byStr[i] - 20);
            }
            else
            {
				dtcode[i] = 15;
            }
        }
		for (i = 0; i < byStr.Length; i++)
        {
			dataB[j] = (byte)(dtcode[i] << 4);
            i++;
			if (i >= byStr.Length)
            {
				dataB[j] |= 15;
				j++;
                break;
            }
			dataB[j] |= dtcode[i];
			j++;
        }
		for (i = j; i < 20; i++)
        {
			dataB[i] = byte.MaxValue;
        }
		return dataB;
    }

    private byte[] String2DecChar(string Str)
    {
		byte[] dtcode = new byte[20];
		byte[] byStr = new ASCIIEncoding().GetBytes(Str);
		for (int i = 0; i < byStr.Length; i++)
        {
			if (byStr[i] >= 48 && byStr[i] <= 57)
            {
				dtcode[i] = (byte)(byStr[i] - 48);
            }
			else if (byStr[i] >= 65 && byStr[i] <= 68)
            {
				dtcode[i] = (byte)(byStr[i] - 55);
            }
			else if (byStr[i] == 42)
            {
				dtcode[i] = (byte)(byStr[i] - 28);
            }
			else if (byStr[i] == 35)
            {
				dtcode[i] = (byte)(byStr[i] - 20);
            }
            else
            {
				dtcode[i] = byte.MaxValue;
            }
        }
		for (int i = byStr.Length; i < 20; i++)
        {
			dtcode[i] = byte.MaxValue;
        }
		return dtcode;
    }

    private void DtmfConvert()
    {
		byte[] tmp = new byte[DatDtmfSysInfo.Dtmf_Info_Size];
		byte[] buf1 = new byte[20];
		byte[] buf2 = new byte[8];
		string str = "";
		ASCIIEncoding code = new ASCIIEncoding();
		int len = 33280;
		tmp[0] = appData.dataDtmfSysInfor.DtmfSw;
		tmp[1] = appData.dataDtmfSysInfor.CodeSpeed;
		tmp[2] = (byte)(appData.dataDtmfSysInfor.FirstCodeTim / 10);
		tmp[3] = (byte)(appData.dataDtmfSysInfor.PreTime / 10);
		tmp[4] = (byte)(appData.dataDtmfSysInfor.CodeDly / 10);
		tmp[5] = appData.dataDtmfSysInfor.PttIDPause;
		tmp[6] = appData.dataDtmfSysInfor.DtmfTone;
		tmp[7] = (byte)(appData.dataDtmfSysInfor.ResetTime + 10);
		tmp[8] = (byte)(appData.dataDtmfSysInfor.SepCode + 10);
        if (appData.dataDtmfSysInfor.GrpCode == 0)
        {
			tmp[9] = byte.MaxValue;
        }
        else
        {
			tmp[9] = (byte)(appData.dataDtmfSysInfor.GrpCode + 9);
        }
		tmp[10] = appData.dataDtmfSysInfor.DecRsp;
		byte[] byteDid = String2DecChar(appData.dataDtmfSysInfor.Did);
		tmp[16] = byteDid[0];
		tmp[17] = byteDid[1];
		tmp[18] = byteDid[2];
		Array.Copy(String2DecChar(appData.dataDtmfSysInfor.Bot), 0, tmp, 24, 16);
		Array.Copy(String2DecChar(appData.dataDtmfSysInfor.Eot), 0, tmp, 40, 16);
		Array.Copy(String2DecChar(appData.dataDtmfSysInfor.Stun), 0, tmp, 56, 16);
		Array.Copy(String2DecChar(appData.dataDtmfSysInfor.Kill), 0, tmp, 72, 16);
		Array.Copy(tmp, 0, FullData, len, DatDtmfSysInfo.Dtmf_Info_Size);
		len = 33368;
		ushort val = (ushort)(~appData.dataDtmfEncInfor.UseFlg);
		buf2[1] = (byte)(val >> 8);
		buf2[0] = (byte)val;
		Array.Copy(buf2, 0, FullData, len, 8);
		len = 33376;
        for (int i = 0; i < 16; i++)
        {
			Array.Copy(String2DecChar(appData.dataDtmfEncInfor.GetEncCode(i)), 0, FullData, len + i * 16, 16);
        }
    }

    private byte[] AscStr2Byte(string Str)
    {
		byte[] dataB = new byte[20];
		byte[] tmB = new ASCIIEncoding().GetBytes(Str);
		Array.Copy(tmB, 0, dataB, 0, tmB.Length);
		for (int i = tmB.Length; i < 20; i++)
        {
			dataB[i] = 0;
        }
		return dataB;
    }

    private void TwoToneConvert()
    {
		byte[] buf1 = new byte[8];
		byte[] tmp = new byte[DatTwoToneInfo.One_Enc_List_Size];
		byte[] tmp2 = new byte[DatTwoToneInfo.Dec_Para_Size];
		string str = "";
		int len = 33792;
		buf1[0] = (byte)((appData.dataTwoTone.FirstTone + 1) * 5);
		buf1[1] = (byte)((appData.dataTwoTone.SecondTone + 1) * 5);
		buf1[2] = (byte)((appData.dataTwoTone.ToneDur + 1) * 5);
		buf1[3] = (byte)appData.dataTwoTone.ToneInt;
		buf1[4] = (byte)appData.dataTwoTone.SToneSW;
		Array.Copy(buf1, 0, FullData, len, 8);
		len = 33808;
		int val;
        for (int i = 0; i < DatTwoToneInfo.Enc_List_Num; i++)
        {
			str = appData.dataTwoTone.GetFreq1(i);
            try
            {
				val = Convert.ToInt32(str);
            }
            catch
            {
				val = 65535;
            }
			tmp[0] = (byte)(val >> 8);
			tmp[1] = (byte)(val & 0xFF);
			str = appData.dataTwoTone.GetFreq2(i);
            try
            {
				val = Convert.ToInt32(str);
            }
            catch
            {
				val = 65535;
            }
			tmp[2] = (byte)(val >> 8);
			tmp[3] = (byte)(val & 0xFF);
			Array.Copy(AscStr2Byte((tmp[0] != byte.MaxValue || tmp[1] != byte.MaxValue) ? appData.dataTwoTone.GetEncName(i) : ""), 0, tmp, 4, 12);
			Array.Copy(tmp, 0, FullData, i * DatTwoToneInfo.One_Enc_List_Size + len, DatTwoToneInfo.One_Enc_List_Size);
        }
		len = 34064;
		tmp2[0] = (byte)appData.dataTwoTone.DecodeRsp;
		tmp2[1] = (byte)(appData.dataTwoTone.ReseTim + 10);
		tmp2[2] = (byte)appData.dataTwoTone.DecFormat;
        try
        {
			val = Convert.ToInt32(appData.dataTwoTone.Atone);
        }
        catch
        {
			val = 65535;
        }
		tmp2[4] = (byte)(val >> 8);
		tmp2[5] = (byte)(val & 0xFF);
        try
        {
			val = Convert.ToInt32(appData.dataTwoTone.Btone);
        }
        catch
        {
			val = 65535;
        }
		tmp2[6] = (byte)(val >> 8);
		tmp2[7] = (byte)(val & 0xFF);
        try
        {
			val = Convert.ToInt32(appData.dataTwoTone.Ctone);
        }
        catch
        {
			val = 65535;
        }
		tmp2[8] = (byte)(val >> 8);
		tmp2[9] = (byte)(val & 0xFF);
        try
        {
			val = Convert.ToInt32(appData.dataTwoTone.Dtone);
        }
        catch
        {
			val = 65535;
        }
		tmp2[10] = (byte)(val >> 8);
		tmp2[11] = (byte)(val & 0xFF);
		Array.Copy(tmp2, 0, FullData, len, DatTwoToneInfo.Dec_Para_Size);
    }

    private void FiveToneConvert()
    {
		byte[] buf1 = new byte[DatFiveToneDecInfo.Dec_Size];
		byte[] tmp1 = new byte[DatFiveToneEncInfo.One_Enc_List_Size];
		byte[] tmp2 = new byte[13];
		byte[] tmp3 = new byte[DatFiveToneEncInfo.Enc_PTTID_Size];
		byte[] tmp4 = new byte[20];
		byte[] s2c = new byte[20];
		byte[] infoCD = new byte[16];
		string str = "";
		ASCIIEncoding code = new ASCIIEncoding();
		int len = 34432;
        for (int i = 0; i < DatFiveToneEncInfo.Enc_List_Num; i++)
        {
			Array.Clear(tmp1, 0, DatFiveToneEncInfo.One_Enc_List_Size);
			tmp1[0] = appData.dataFiveToneEnc.GetEncStand(i);
			tmp1[1] = (byte)(appData.dataFiveToneEnc.GetEncCodeTim(i) + 7);
			tmp1[2] = appData.dataFiveToneEnc.GetEncCodeLen(i);
			Array.Copy(Str2Byte(appData.dataFiveToneEnc.GetEncID(i)), 0, tmp1, 3, 20);
			tmp1[23] = appData.dataFiveToneEnc.GetEncScall(i);
			Array.Copy(AscStr2Byte(appData.dataFiveToneEnc.GetEncName(i)), 0, tmp1, 24, 8);
			Array.Copy(tmp1, 0, FullData, len + i * DatFiveToneEncInfo.One_Enc_List_Size, DatFiveToneEncInfo.One_Enc_List_Size);
        }
		len = 37760;
        for (int i = 0; i < 13; i++)
        {
			int dat = 0;
            for (int j = 0; j < 8; j++)
            {
                if (appData.dataFiveToneEnc.GetTblEn(i * 8 + j) == 0)
                {
					dat |= 1 << j;
                }
            }
			tmp2[i] = (byte)dat;
        }
		Array.Copy(tmp2, 0, FullData, len, 13);
		len = 37888;
		tmp3[0] = appData.dataFiveToneEnc.PidStandS;
		tmp3[1] = (byte)(appData.dataFiveToneEnc.PidCodeTimS + 7);
		str = appData.dataFiveToneEnc.PidStart;
		Array.Copy(Str2Byte(str), 0, tmp3, 3, 12);
		tmp3[2] = (byte)str.Length;
		tmp3[16] = appData.dataFiveToneEnc.PidStandE;
		tmp3[17] = (byte)(appData.dataFiveToneEnc.PidCodeTimE + 7);
		str = appData.dataFiveToneEnc.PidEnd;
		Array.Copy(Str2Byte(str), 0, tmp3, 19, 12);
		tmp3[18] = (byte)str.Length;
		Array.Copy(tmp3, 0, FullData, len, DatFiveToneEncInfo.Enc_PTTID_Size);
		len = 37952;
		buf1[0] = appData.dataFiveToneDec.DecRsp;
		buf1[1] = appData.dataFiveToneDec.DecStand;
		buf1[2] = (byte)(appData.dataFiveToneDec.DecToneTim + 7);
		Array.Copy(String2DecChar(appData.dataFiveToneDec.Did), 0, buf1, 3, 5);
		buf1[11] = (byte)(appData.dataFiveToneDec.PreTime / 10);
		buf1[12] = (byte)(appData.dataFiveToneDec.CodeDly / 10);
        if (appData.dataFiveToneDec.PttIDPause > 0)
        {
			buf1[13] = (byte)(appData.dataFiveToneDec.PttIDPause + 4);
        }
        else
        {
			buf1[13] = 0;
        }
		buf1[14] = (byte)(appData.dataFiveToneDec.ResetTime + 10);
		buf1[15] = (byte)(appData.dataFiveToneDec.FirstCodeTim / 10);
		buf1[16] = appData.dataFiveToneDec.FiveAni;
        if (appData.dataFiveToneDec.StopCode == 1)
        {
			buf1[18] = 11;
        }
        else if (appData.dataFiveToneDec.StopCode == 2)
        {
			buf1[18] = 12;
        }
        else if (appData.dataFiveToneDec.StopCode == 3)
        {
			buf1[18] = 13;
        }
        else if (appData.dataFiveToneDec.StopCode == 4)
        {
			buf1[18] = 15;
        }
        else
        {
			buf1[18] = 0;
        }
		buf1[19] = (byte)(appData.dataFiveToneDec.StopCodetime / 10);
		buf1[20] = (byte)(appData.dataFiveToneDec.DecCodetime / 10);
		Array.Copy(buf1, 0, FullData, len, DatFiveToneDecInfo.Dec_Size);
		len = 38016;
        for (int i = 0; i < 8; i++)
        {
			infoCD[0] = appData.dataFiveToneInfoCd.GetFunc(i);
			infoCD[1] = appData.dataFiveToneInfoCd.GetRspInfo(i);
			infoCD[2] = appData.dataFiveToneInfoCd.GetCdLen(i);
			Array.Copy(Str2Byte(appData.dataFiveToneInfoCd.GetDecID(i)), 0, infoCD, 3, 6);
			Array.Copy(AscStr2Byte(appData.dataFiveToneInfoCd.GetDecName(i)), 0, infoCD, 10, 6);
			Array.Copy(infoCD, 0, FullData, len, 16);
			len += 16;
        }
    }

    private void MdcConvert()
    {
		byte[] buf1 = new byte[DatMdcBiis.BIIS_Size];
		byte[] buf2 = new byte[8];
		byte[] tmp = new byte[DatMdcDecInfo.One_List_Size * DatMdcDecInfo.Max_Num];
		byte[] tmp2 = new byte[DatMdcDecInfo.One_List_Size];
		string str = "";
		ASCIIEncoding code = new ASCIIEncoding();
		int len = 38272;
        for (int i = 0; i < 8; i++)
        {
			buf2[i] = appData.dataMdcDecInfo.GetSysList(i);
        }
		Array.Copy(buf2, 0, FullData, len, 8);
		len = 38280;
        for (int i = 0; i < 5; i++)
        {
			buf2[0] = (byte)(appData.dataMdcPara[i].CtrlSw << 7);
			buf2[0] |= (byte)(appData.dataMdcPara[i].DecTone << 6);
			buf2[1] = (byte)(appData.dataMdcPara[i].EncID >> 8);
			buf2[2] = (byte)appData.dataMdcPara[i].EncID;
			buf2[3] = (byte)(appData.dataMdcPara[i].PreTim / 10);
			buf2[4] = (byte)(appData.dataMdcPara[i].SqlDly / 10);
			buf2[5] = (byte)(appData.dataMdcPara[i].DecRst / 10);
			buf2[6] = appData.dataMdcPara[i].EncSync;
			buf2[7] = (byte)(appData.dataMdcPara[i].DecSync + 29);
			Array.Copy(buf2, 0, FullData, len + i * 8, 8);
        }
		len = 38320;
        for (int i = 0; i < 5; i++)
        {
			int val = appData.dataMdcPttID[i].BotTime / 100;
			buf2[1] = (byte)val;
			buf2[0] = (byte)(val >> 8);
			val = appData.dataMdcPttID[i].EotTime / 100;
			buf2[3] = (byte)val;
			buf2[2] = (byte)(val >> 8);
			buf2[4] = (byte)(appData.dataMdcPttID[i].EncEn << 7);
			buf2[4] |= (byte)(appData.dataMdcPttID[i].DecEn << 6);
			buf2[4] |= (byte)(appData.dataMdcPttID[i].BotEn << 5);
			buf2[4] |= (byte)(appData.dataMdcPttID[i].EotEn << 4);
			buf2[4] |= (byte)(appData.dataMdcPttID[i].TxTone << 3);
			buf2[4] |= (byte)(appData.dataMdcPttID[i].RxTone << 2);
			Array.Copy(buf2, 0, FullData, len + i * 8, 8);
        }
		len = 38360;
        for (int i = 0; i < 16; i++)
        {
			int dat = 0;
            for (int j = 0; j < 8; j++)
            {
                if (appData.dataMdcDecInfo.GetTblEn(i * 8 + j) == 0)
                {
					dat |= 1 << j;
                }
            }
			tmp[i] = (byte)dat;
        }
		Array.Copy(tmp, 0, FullData, len, 16);
		len = 38384;
        for (int i = 0; i < DatMdcDecInfo.Max_Num; i++)
        {
			Array.Clear(tmp2, 0, DatMdcDecInfo.One_List_Size);
			int val;
            try
            {
				val = Convert.ToInt32(appData.dataMdcDecInfo.GetDecID(i));
            }
            catch
            {
				val = 0;
            }
			tmp2[0] = (byte)(val >> 8);
			tmp2[1] = (byte)val;
			tmp2[2] = appData.dataMdcDecInfo.GetDecRsp(i);
			Array.Copy(StringSwap2Char(appData.dataMdcDecInfo.GetDecName(i)), 0, tmp2, 4, 12);
			Array.Copy(tmp2, 0, tmp, i * DatMdcDecInfo.One_List_Size, DatMdcDecInfo.One_List_Size);
        }
		Array.Copy(tmp, 0, FullData, len, DatMdcDecInfo.One_List_Size * DatMdcDecInfo.Max_Num);
		len = 40064;
		buf1[0] = (byte)(appData.dataMdcBiis.SelfID >> 8);
		buf1[1] = (byte)appData.dataMdcBiis.SelfID;
		buf1[2] = (byte)(appData.dataMdcBiis.GrpID >> 8);
		buf1[3] = (byte)appData.dataMdcBiis.GrpID;
		buf1[4] = (byte)(appData.dataMdcBiis.Sync >> 8);
		buf1[5] = (byte)appData.dataMdcBiis.Sync;
		buf1[6] = appData.dataMdcBiis.ZoneCode;
		buf1[7] = (byte)(appData.dataMdcBiis.PreTime / 10);
		buf1[8] = appData.dataMdcBiis.ToneSw;
		Array.Copy(buf1, 0, FullData, len, DatMdcBiis.BIIS_Size);
    }

    private void EmergConvert()
    {
		byte[] buf = new byte[DatEmergInfo.Emerg_Sys_Size];
		byte[] tmp = new byte[DatEmergInfo.Emerg_Sys_Size * DatEmergInfo.Max_Emerg_Sys_Num];
		int len = 40192;
		FullData[len] = 0;
		FullData[len + 1] = 252;
		len = 40200;
        for (int i = 0; i < DatEmergInfo.Max_Emerg_Sys_Num; i++)
        {
			Array.Clear(buf, 0, DatEmergInfo.Emerg_Sys_Size);
			buf[7] = appData.dataEmergInfor[i].Type;
			buf[6] = appData.dataEmergInfor[i].Mode;
			buf[5] = appData.dataEmergInfor[i].GrpNo;
			buf[4] = appData.dataEmergInfor[i].ExgTime;
			buf[3] = appData.dataEmergInfor[i].TxTime;
			buf[2] = appData.dataEmergInfor[i].RxTime;
			buf[1] = appData.dataEmergInfor[i].ChSel;
			buf[0] = appData.dataEmergInfor[i].Duration;
			buf[14] = appData.dataEmergInfor[i].Chn;
			buf[15] = appData.dataEmergInfor[i].Zone;
			Array.Copy(buf, 0, tmp, i * DatEmergInfo.Emerg_Sys_Size, DatEmergInfo.Emerg_Sys_Size);
        }
		Array.Copy(tmp, 0, FullData, len, DatEmergInfo.Emerg_Sys_Size * DatEmergInfo.Max_Emerg_Sys_Num);
    }

    private byte[] AprsCode2Char(string Str)
    {
		byte[] dtcode = new byte[20];
		byte[] byStr = new ASCIIEncoding().GetBytes(Str);
		Array.Copy(byStr, 0, dtcode, 0, byStr.Length);
		for (int i = byStr.Length; i < 20; i++)
        {
			dtcode[i] = 32;
		}
		return dtcode;
	}

	private byte[] LatLongToData(double data)
	{
		byte[] buf = new byte[4];
		byte integer_part = 0;
		int decimal_part = 0;
		double tmp = 0.0;
		integer_part = (byte)data;
		decimal_part = (int)Math.Round((data - (double)(int)integer_part) * 100000.0);
		if (integer_part > 100)
		{
			buf[0] = (byte)((integer_part / 100 << 4) | (integer_part % 100 / 10));
			buf[1] = (byte)((integer_part % 10 << 4) | (decimal_part / 10000));
			buf[2] = (byte)((decimal_part % 10000 / 1000 << 4) | (decimal_part % 1000 / 100));
			buf[3] = (byte)((decimal_part % 100 / 10 << 4) | (decimal_part % 10));
		}
		else
		{
			buf[0] = (byte)(integer_part / 10);
			buf[1] = (byte)((integer_part % 10 << 4) | (decimal_part / 10000));
			buf[2] = (byte)((decimal_part % 10000 / 1000 << 4) | (decimal_part % 1000 / 100));
			buf[3] = (byte)((decimal_part % 100 / 10 << 4) | (decimal_part % 10));
		}
		return buf;
    }

    private void AprsConvert()
    {
		byte[] latlong = new byte[4];
		byte[] subbuf = new byte[2];
		byte[] tmp = new byte[456];
		byte[] s2c = new byte[20];
		string sst = "";
		UTF8Encoding utf8Encoder = new UTF8Encoding();
		int len = 40448;
		Array.Copy(AprsCode2Char(appData.dataAprsSet.DesNo), 0, tmp, 0, 6);
		tmp[6] = appData.dataAprsSet.DesID;
		tmp[7] |= (byte)(appData.dataAprsSet.PassAll << 7);
		tmp[7] |= (byte)(appData.dataAprsSet.Position << 6);
		tmp[7] |= (byte)(appData.dataAprsSet.MicE << 5);
		tmp[7] |= (byte)(appData.dataAprsSet.Object << 4);
		tmp[7] |= (byte)(appData.dataAprsSet.Item << 3);
		tmp[7] |= (byte)(appData.dataAprsSet.Message << 2);
		tmp[7] |= (byte)(appData.dataAprsSet.WxReport << 1);
		tmp[7] |= appData.dataAprsSet.NMEAReport;
		Array.Copy(AprsCode2Char(appData.dataAprsSet.SrcNo), 0, tmp, 8, 6);
		tmp[14] = appData.dataAprsSet.SrcID;
		tmp[15] |= (byte)(appData.dataAprsSet.StatusReport << 7);
		tmp[15] |= (byte)(appData.dataAprsSet.Other << 6);
		tmp[15] |= (byte)(appData.dataAprsSet.Power << 4);
		tmp[15] |= (byte)(appData.dataAprsSet.Band << 3);
		tmp[15] |= (byte)(appData.dataAprsSet.BeepTone << 2);
		tmp[15] |= (byte)(appData.dataAprsSet.LongDir << 1);
		tmp[15] |= appData.dataAprsSet.LatDir;
		tmp[16] = (byte)appData.dataAprsSet.PreTime;
		tmp[17] = (byte)appData.dataAprsSet.CodeDly;
		Array.Copy(SubAudioToData(appData.dataAprsSet.Ctdcs), 0, tmp, 18, 2);
		tmp[20] = (byte)(string.IsNullOrEmpty(appData.dataAprsSet.PostionTable) ? 32 : utf8Encoder.GetBytes(appData.dataAprsSet.PostionTable)[0]);
		tmp[21] = (byte)(string.IsNullOrEmpty(appData.dataAprsSet.PostionIcon) ? 32 : utf8Encoder.GetBytes(appData.dataAprsSet.PostionIcon)[0]);
		tmp[22] = appData.dataAprsSet.RxCallSignNum;
		tmp[23] = (byte)DatAprsSet.CallSign_Total;
        for (int i = 0; i < 8; i++)
        {
			Array.Copy(AprsCode2Char(appData.dataAprsSet.GetCallSignNo(i)), 0, tmp, i * 8 + 24, 6);
			tmp[i * 8 + 24 + 6] = appData.dataAprsSet.GetCallSignID(i);
        }
		tmp[88] = (byte)appData.dataAprsSet.SendInterval;
		tmp[89] = appData.dataAprsSet.RegularlySend;
		tmp[90] = appData.dataAprsSet.APRSDisplayTime;
		tmp[92] |= (byte)(appData.dataAprsSet.Beacon << 7);
		tmp[92] |= (byte)(appData.dataAprsSet.HeightType << 6);
		tmp[92] |= (byte)(appData.dataAprsSet.PttId << 4);
		tmp[92] |= appData.dataAprsSet.MicEType;
		tmp[95] = appData.dataAprsSet.TxtLength;
		int ll = (int)Math.Round(appData.dataAprsSet.Long * 100000.0);
		latlong[0] = (byte)(ll & 0xFF);
		latlong[1] = (byte)((ll & 0xFF00) >> 8);
		latlong[2] = (byte)((ll & 0xFF0000) >> 16);
		latlong[3] = (byte)((ll & 0xFF000000u) >> 24);
		Array.Copy(latlong, 0, tmp, 96, 4);
		ll = (int)Math.Round(appData.dataAprsSet.Lat * 100000.0);
		latlong[0] = (byte)(ll & 0xFF);
		latlong[1] = (byte)((ll & 0xFF00) >> 8);
		latlong[2] = (byte)((ll & 0xFF0000) >> 16);
		latlong[3] = (byte)((ll & 0xFF000000u) >> 24);
		Array.Copy(latlong, 0, tmp, 100, 4);
		int height = appData.dataAprsSet.Height;
		tmp[104] = (byte)(height & 0xFF);
		tmp[105] = (byte)((height & 0xFF00) >> 8);
		tmp[106] = (byte)((height & 0xFF0000) >> 16);
		tmp[107] = (byte)((height & 0xFF000000u) >> 24);
		Array.Copy(utf8Encoder.GetBytes(appData.dataAprsSet.Txt), 0, tmp, 108, appData.dataAprsSet.TxtLength);
		Array.Copy(FreqToData(appData.dataAprsSet.Freq1), 0, tmp, 168, 4);
		Array.Copy(FreqToData(appData.dataAprsSet.Freq2), 0, tmp, 172, 4);
		Array.Copy(FreqToData(appData.dataAprsSet.Freq3), 0, tmp, 176, 4);
		Array.Copy(FreqToData(appData.dataAprsSet.Freq4), 0, tmp, 180, 4);
		Array.Copy(FreqToData(appData.dataAprsSet.Freq5), 0, tmp, 184, 4);
		Array.Copy(FreqToData(appData.dataAprsSet.Freq6), 0, tmp, 188, 4);
		Array.Copy(FreqToData(appData.dataAprsSet.Freq7), 0, tmp, 192, 4);
		Array.Copy(FreqToData(appData.dataAprsSet.Freq8), 0, tmp, 196, 4);
		for (int i = 0; i < 32; i++)
		{
			Array.Copy(AprsCode2Char(appData.dataAprsSet.GetRxCallSignNo(i)), 0, tmp, i * 8 + 200, 6);
			tmp[i * 8 + 200 + 6] = appData.dataAprsSet.GetRxCallSignID(i);
			tmp[i * 8 + 200 + 7] = appData.dataAprsSet.GetRxFilter(i);
		}
		Array.Copy(tmp, 0, FullData, len, 456);
    }

    private void GpsValidFlgConvert()
    {
		int cnt = DatGpsBook.MAX_GpsBook_Num / 8;
		int tmpflg = 0;
		byte[] tmpInfo = new byte[cnt];
		int len = 40960;
		for (int i = 0; i < cnt; i++)
        {
			tmpflg = 0;
            for (int j = 0; j < 8; j++)
            {
				tmpflg |= appData.dataGpsBook[i * 8 + j].UseFlg << j;
            }
			tmpInfo[i] = (byte)(~tmpflg);
        }
		Array.Copy(tmpInfo, 0, FullData, len, cnt);
    }

    private void GpsBookConvert()
    {
		byte[] buf = new byte[16];
        GpsValidFlgConvert();
		int len = 40976;
        for (int i = 0; i < DatGpsBook.MAX_GpsBook_Num; i++)
        {
			Array.Clear(buf, 0, 16);
            if (appData.dataGpsBook[i].UseFlg == 1)
            {
				buf[0] = appData.dataGpsBook[i].CodeID;
				Array.Copy(StringSwap2Char(appData.dataGpsBook[i].CodeName), 0, buf, 2, 14);
            }
			Array.Copy(buf, 0, FullData, len + i * 16, 16);
        }
    }

    private void DataToWrite()
    {
        DeviceConvert();
        ChnConvert();
        VFOConvert();
        SettingConvert();
        ZoneConvert();
        ChnValidFlgConvert();
        ScanConvert();
        DtmfConvert();
        TwoToneConvert();
        FiveToneConvert();
        MdcConvert();
        EmergConvert();
        AprsConvert();
        GpsBookConvert();
    }

    public static byte[] GetBufFullData()
    {
        return FullData;
    }

    public DataApp SetbufFullData(byte[] bytebuf, int total)
    {
        Array.Copy(bytebuf, 0, FullData, 0, total);
        DataToRead();
        return bufForData;
    }

    public static void InitbufFullData()
    {
        for (int i = 0; i < DpDataLen; i++)
        {
            FullData[i] = 0;
        }
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if (state == STATE.STATE_HANDSHAKE2)
        {
            if (timesForRetry-- > 0)
            {
                state = STATE.STATE_HANDSHAKE1;
            }
            else
            {
                flagTransmitting = false;
            }
        }
        else if (state == STATE.STATE_HANDSHAKE5 || state == STATE.STATE_HANDSHAKE4 || state == STATE.STATE_HANDSHAKE3 || state == STATE.STATE_HANDSHAKE6)
        {
            if (timesForRetry-- == 0)
            {
                flagTransmitting = false;
            }
            else
            {
                timer.Start();
            }
        }
        else if (state == STATE.STATE_READ2 || state == STATE.STATE_WRITE2)
        {
            if (timesForRetry-- == 0)
            {
                flagTransmitting = false;
            }
            else
            {
                timer.Start();
            }
        }
        else if (state == STATE.STATE_WRITE3 || state == STATE.STATE_READ3)
        {
            flagTransmitting = false;
        }
    }
}
