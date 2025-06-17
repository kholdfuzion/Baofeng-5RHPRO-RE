using System;

namespace Radio;

public class BasicModel : IData
{
    public int Beep { get; set; }

    public int VoiceLang { get; set; }

    public int BusyLock { get; set; }

    public int KeyLock { get; set; }

    public int Sql { get; set; }

    public int Vox { get; set; }

    public string VoxDelay { get; set; }

    public int Saving { get; set; }

    public string SavingDelay { get; set; }

    public int Apo { get; set; }

    public int VoxSwitch { get; set; }

    public byte[] DataToBytes()
    {
        byte[] data = new byte[16];
        Array.Copy(Global.EEROM, 1800, data, 0, data.Length);
        data[8] = data[8].SetBit(1, 1, KeyLock);
        data[8] = data[8].SetBit(2, 2, BusyLock);
        data[8] = data[8].SetBit(4, 2, VoiceLang);
        data[8] = data[8].SetBit(6, 1, Beep);
        data[8] = data[8].SetBit(7, 1, VoxSwitch);
        data[9] = (byte)Sql;
        data[10] = (byte)Vox;
        double voxDelay = 0.1;
        double.TryParse(VoxDelay, out voxDelay);
        data[11] = (byte)(voxDelay * 10.0);
        data[12] = (byte)Saving;
        byte savingDelay = 0;
        byte.TryParse(SavingDelay, out savingDelay);
        data[13] = savingDelay;
        data[14] = (byte)Apo;
        return data;
    }

    public void BytesToData(byte[] data)
    {
        KeyLock = data[8].GetBit(1);
        BusyLock = data[8].GetBit(2, 2);
        VoiceLang = data[8].GetBit(4, 2);
        Beep = data[8].GetBit(6, 1);
        VoxSwitch = data[8].GetBit(7, 1);
        Sql = data[9];
        Vox = data[10];
        VoxDelay = ((double)(int)data[11] * 0.1).ToString("f1");
        Saving = data[12];
        SavingDelay = data[13].ToString();
        Apo = data[14];
    }
}
