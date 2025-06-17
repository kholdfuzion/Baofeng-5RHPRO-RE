using System;

namespace Radio;

public class ScanBasicModel : IData
{
    public int ScanMode { get; set; }

    public int PriorityScan { get; set; }

    public string PriorityCh { get; set; }

    public string BackTime { get; set; }

    public string TxDelay { get; set; }

    public string RxDelay { get; set; }

    public int RevertCh { get; set; }

    public byte[] DataToBytes()
    {
        byte[] data = new byte[8];
        Array.Copy(Global.EEROM, 1888, data, 0, data.Length);
        data[0] = (byte)ScanMode;
        data[1] = (byte)PriorityScan;
        int priorityCh = 0;
        int.TryParse(PriorityCh, out priorityCh);
        data[2] = (byte)(priorityCh >> 8);
        data[3] = (byte)priorityCh;
        double value = 0.0;
        double.TryParse(BackTime, out value);
        data[4] = (byte)(value * 10.0);
        double.TryParse(RxDelay, out value);
        data[5] = (byte)(value * 10.0);
        double.TryParse(TxDelay, out value);
        data[6] = (byte)(value * 10.0);
        data[7] = (byte)RevertCh;
        return data;
    }

    public void BytesToData(byte[] data)
    {
        ScanMode = data[0];
        PriorityScan = data[1];
        PriorityCh = (data[2] * 256 + data[3]).ToString();
        BackTime = ((double)(int)data[4] * 0.1).ToString("f1");
        RxDelay = ((double)(int)data[5] * 0.1).ToString("f1");
        TxDelay = ((double)(int)data[6] * 0.1).ToString("f1");
        RevertCh = data[7];
    }
}
