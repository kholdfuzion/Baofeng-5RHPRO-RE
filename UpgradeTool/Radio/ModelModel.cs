using System;

namespace Radio;

public class ModelModel : IData
{
    public int MinFreq { get; set; }

    public int MaxFreq { get; set; }

    public byte[] DataToBytes()
    {
        int index = 0;
        byte[] data = new byte[8];
        byte[] tmp = null;
        int freqBcd = Common.DecToBcd32(MinFreq);
        tmp = BitConverter.GetBytes(freqBcd);
        Array.Copy(tmp, 0, data, index, tmp.Length);
        index += tmp.Length;
        freqBcd = Common.DecToBcd32(MaxFreq);
        tmp = BitConverter.GetBytes(freqBcd);
        Array.Copy(tmp, 0, data, index, tmp.Length);
        index += tmp.Length;
        return data;
    }

    public void BytesToData(byte[] data)
    {
        int index = 0;
        int freqBcd = BitConverter.ToInt32(data, index);
        MinFreq = Common.BcdToDec32(freqBcd);
        index += 4;
        freqBcd = BitConverter.ToInt32(data, index);
        MaxFreq = Common.BcdToDec32(freqBcd);
        MinFreq = 40000000;
        MaxFreq = 52000000;
    }
}
