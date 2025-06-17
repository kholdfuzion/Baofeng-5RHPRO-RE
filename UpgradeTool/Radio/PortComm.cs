using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace Radio;

internal class PortComm
{
    private const int HEAD_LEN = 4;

    private const int MAX_COMM_LEN = 1024;

    private const int MaxReadTimeout = 5000;

    private const int MaxWriteTimeout = 2000;

    private const int MaxBuf = 1060;

    private static readonly byte[] CMD_INFO = Encoding.ASCII.GetBytes("INFORMATION");

    private static readonly byte[] CMD_END = Encoding.ASCII.GetBytes("END\0");

    private static readonly byte[] CMD_DOWNLOAD = Encoding.ASCII.GetBytes("DOWNLOAD");

    private static readonly byte[] CMD_UPDATE = Encoding.ASCII.GetBytes("#UPDATE?");

    private static readonly byte[] CMD_NEW_HARDWARE = Encoding.ASCII.GetBytes("V2_00_00");

    private static readonly byte[] CMD_PRG = Encoding.ASCII.GetBytes("PROGRAM1");

    private static readonly byte[,] CMD_FLASH = new byte[7, 8]
    {
        { 70, 45, 80, 82, 79, 71, 255, 255 },   //F-PROG
        { 70, 45, 69, 82, 65, 83, 69, 255 },    //F-ERASE
        { 70, 45, 67, 79, 255, 255, 255, 255 }, //F-CO
        { 70, 45, 77, 79, 68, 255, 255, 255 },  //F-MOD
        { 70, 45, 86, 69, 82, 255, 255, 255 },  //F-VER
        { 70, 45, 83, 78, 255, 255, 255, 255 }, //F-SN
        { 70, 45, 84, 73, 77, 69, 255, 255 }    //F-TIME
    };

    private static readonly byte[] CMD_FLASH_CO_FIX = new byte[16]
    {
        83, 71, 45, 84, 89, 84, 45, 48, 48, 57, //SG-TYT-009
        255, 255, 255, 255, 255, 255
    };

    private static readonly byte[] CMD_FLASH_MOO_FIX = new byte[8]
    {
        83, 71, 45, 48, 48, 57,  //SG-009
        255, 255
    };

    private static readonly byte[] CMD_FLASH_CO = new byte[16]
    {
        83, 85, 82, 87, 65, 86, 69, 45, 83, 71, 45, 48, 48, 57, //SURWAVE-SG-009
        255, 255
    };

    private static readonly byte[] CMD_ACK = Encoding.ASCII.GetBytes("ACK");

    private static readonly byte[] CMD_NACK = Encoding.ASCII.GetBytes("NACK");

    public static string GetRadioSpeed { get; set; } = "115200";

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
        byte[] rxBuf = new byte[1060];
        int wantLen = 0;
        int realLen = 0;
        int remainder = 0;
        int readedLen = 0;
        int dataAddr = 0;
        int dataStartAddr = 0;
        int dataEndAddr = 0;
        Stopwatch.StartNew();
        SerialPort spt = new SerialPort(MainForm.CurCom, Int32.Parse(MainForm.GetRadioSpeed));
        try
            {
                Array.Copy(Global.EEROM, 0x30, rxBuf, 0, 32);
                dataEndAddr = ((rxBuf[16] & 0xFF) << 24) | ((rxBuf[17] & 0xFF) << 16) | ((rxBuf[18] & 0xFF) << 8) | (rxBuf[19] & 0xFF);
                dataStartAddr = 0x0;
                for (dataAddr = dataStartAddr; dataAddr < dataEndAddr; dataAddr += realLen)
                {
                    remainder = dataAddr % 1024;
                    realLen = ((dataAddr + 1024 <= dataEndAddr) ? (1024 - remainder) : (dataEndAddr - dataAddr));
                    maxPos++;
                }
                spt.ReadTimeout = 5000;
                spt.WriteTimeout = 2000;
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
                byte[] data = new byte[1060];
                Array.Clear(rxBuf, 0, rxBuf.Length);
                spt.DiscardInBuffer();
                spt.Write(CMD_DOWNLOAD, 0, CMD_DOWNLOAD.Length);
                wantLen = 8;
                for (readedLen = spt.Read(rxBuf, 0, wantLen); readedLen < wantLen; readedLen += spt.Read(rxBuf, readedLen, wantLen - readedLen))
                {
                    Thread.Sleep(20);
                }
                Array.Copy(Global.EEROM, 0x30, data, 0, 8);
                if (data[1] == '2') //2
                {
                    curTimes = 0;
                    while (curTimes < wantLen)
                    {
                        if (rxBuf[curTimes] == CMD_NEW_HARDWARE[curTimes])
                        {
                            curTimes++;
                            continue;
                        }
                        goto IL_061f;
                    }
                }
                else
                {
                    curTimes = 0;
                    while (curTimes < wantLen)
                    {
                        if (rxBuf[curTimes] == CMD_UPDATE[curTimes])
                        {
                            curTimes++;
                            continue;
                        }
                        goto IL_061f;
                    }
                }
                Array.Clear(rxBuf, 0, rxBuf.Length);
                spt.DiscardInBuffer();
                spt.Write(CMD_ACK, 0x0, 1);
                wantLen = 1;
                for (readedLen = spt.Read(rxBuf, 0, wantLen); readedLen < wantLen; readedLen += spt.Read(rxBuf, readedLen, wantLen - readedLen))
                {
                    Thread.Sleep(20);
                }
                if (rxBuf[0] == CMD_ACK[0])
                {
                    Array.Clear(rxBuf, 0, rxBuf.Length);
                    spt.DiscardInBuffer();
                    for (curTimes = 0; curTimes < 8; curTimes++)
                    {
                        data[curTimes] = CMD_FLASH[1, curTimes];
                    }
                    data[8] = 40;
                    data[9] = 6;
                    data[10] = 136;
                    data[11] = 25;
                    data[12] = 19;
                    data[13] = 3;
                    data[14] = 24;
                    data[15] = 32;
                    spt.Write(data, 0x0, 16);
                    wantLen = 1;
                    for (readedLen = spt.Read(rxBuf, 0, wantLen); readedLen < wantLen; readedLen += spt.Read(rxBuf, readedLen, wantLen - readedLen))
                    {
                        Thread.Sleep(20);
                    }
                    if (rxBuf[0] == CMD_ACK[0])
                    {
                        Array.Clear(rxBuf, 0, rxBuf.Length);
                        spt.DiscardInBuffer();
                        for (curTimes = 0; curTimes < 8; curTimes++)
                        {
                            data[curTimes] = CMD_PRG[curTimes];
                        }
                        spt.Write(data, 0x0, 8);
                        wantLen = 1;
                        for (readedLen = spt.Read(rxBuf, 0x0, wantLen); readedLen < wantLen; readedLen += spt.Read(rxBuf, readedLen, wantLen - readedLen))
                        {
                            Thread.Sleep(20);
                        }
                        if (rxBuf[0] == CMD_ACK[0])
                        {
                            int check_sum = 0;
                            curTimes = 80;
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
                                        wantLen = 1;
                                        readedLen = spt.Read(rxBuf, 0, wantLen);
                                        spt.Close();
                                        return;
                                    }
                                    remainder = dataAddr % 1024;
                                    realLen = ((dataAddr + 1024 <= dataEndAddr) ? (1024 - remainder) : (dataEndAddr - dataAddr));
                                    byte[] realData = new byte[realLen + 5];
                                    Array.Copy(Global.EEROM, curTimes, data, 0, realLen);
                                    realData[0] = (byte)(dataAddr >> 24);
                                    realData[1] = (byte)(dataAddr >> 16);
                                    realData[2] = (byte)(dataAddr >> 8);
                                    realData[3] = (byte)dataAddr;
                                    realData[4] = 0;
                                    curTimes += realLen;
                                    for (i = 0; i < realLen; i++)
                                    {
                                        realData[i + 5] = data[i];
                                        check_sum += data[i] & 0xFF;
                                    }
                                    Array.Clear(rxBuf, 0, rxBuf.Length);
                                    spt.DiscardInBuffer();
                                    spt.Write(realData, 0, 5);
                                    spt.Write(realData, 5, realLen);
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
                                data[0] = (byte)'E'; //E
                                data[1] = (byte)'N'; //N
                                data[2] = (byte)'D'; //D
                                data[3] = byte.MaxValue;
                                data[4] = byte.MaxValue;
                                data[5] = (byte)((check_sum >> 24) & 0xFF);
                                data[6] = (byte)((check_sum >> 16) & 0xFF);
                                data[7] = (byte)((check_sum >> 8) & 0xFF);
                                data[8] = (byte)(check_sum & 0xFF);
                                Array.Clear(rxBuf, 0, rxBuf.Length);
                                spt.DiscardInBuffer();
                                spt.Write(data, 0, 9);
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
                        }
                    }
                }
                goto IL_061f;
            IL_061f:
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
