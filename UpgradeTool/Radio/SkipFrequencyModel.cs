using System;

namespace Radio;

public class SkipFrequencyModel : IData
{
    public string[,] Frequencys { get; set; }

    public SkipFrequencyModel()
    {
        Frequencys = new string[41, 16];
    }

    public byte[] DataToBytes()
    {
        byte[] data = new byte[2816];
        int index = 0;
        for (int row = 0; row < Frequencys.GetLength(1); row++)
        {
            index = row * 176;
            for (int col = 0; col < Frequencys.GetLength(0); col++)
            {
                byte[] tmp = FrequencyHelper.FreqToBytes(Frequencys[col, row]);
                Array.Copy(tmp, 0, data, index, 4);
                index += 4;
            }
        }
        return data;
    }

    public void BytesToData(byte[] data)
    {
        byte[] tmp = new byte[4];
        int index = 0;
        for (int row = 0; row < Frequencys.GetLength(1); row++)
        {
            index = row * 176;
            for (int col = 0; col < Frequencys.GetLength(0); col++)
            {
                Array.Copy(data, index, tmp, 0, 4);
                Frequencys[col, row] = FrequencyHelper.BytesToFreq(tmp);
                index += 4;
            }
        }
    }

    public void Import(string[] freq)
    {
        int row = 0;
        int col = 0;
        if (freq.Length == Frequencys.Length)
        {
            for (int index = 0; index < freq.Length; index++)
            {
                row = index % 41;
                col = index / 41;
                Frequencys[row, col] = freq[index];
            }
        }
    }

    public string[] Export()
    {
        int row = 0;
        int col = 0;
        string[] freq = new string[Frequencys.Length];
        for (int index = 0; index < freq.Length; index++)
        {
            row = index % 41;
            col = index / 41;
            freq[index] = Frequencys[row, col];
        }
        return freq;
    }

    public void SetFreq(int groupIndex, int baseFreq, int step)
    {
        for (int i = 0; i < 20; i++)
        {
            Frequencys[41 - i - 1, groupIndex] = (Frequencys[i, groupIndex] = FrequencyHelper.DecToFreq(baseFreq + (i + 1) * 10000));
        }
        Frequencys[20, groupIndex] = FrequencyHelper.DecToFreq(baseFreq + 210000);
    }
}
