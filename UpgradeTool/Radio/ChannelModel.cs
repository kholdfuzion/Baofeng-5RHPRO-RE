using System;

namespace Radio;

public class ChannelModel : IData
{
    public Channel[] Channels { get; set; }

    public ChannelModel()
    {
        Channels = new Channel[100];
        for (int i = 0; i < 100; i++)
        {
            Channels[i] = new Channel();
            Channels[i].ChIndex = i;
        }
    }

    public byte[] DataToBytes()
    {
        int index = 0;
        byte[] data = new byte[1600];
        byte[] tmp = null;
        for (int i = 0; i < 100; i++)
        {
            tmp = Channels[i].DataToBytes();
            Array.Copy(tmp, 0, data, index, tmp.Length);
            index += tmp.Length;
        }
        return data;
    }

    public void BytesToData(byte[] data)
    {
        int index = 0;
        byte[] tmp = new byte[16];
        for (int i = 0; i < 100; i++)
        {
            Array.Copy(data, index, tmp, 0, tmp.Length);
            Channels[i].BytesToData(tmp);
            index += tmp.Length;
        }
    }

    public void Clear()
    {
        for (int i = 0; i < 100; i++)
        {
            Array.Clear(Channels[i].Items, 0, Channels[i].Items.Length);
        }
    }

    public void InitFirstCh(int freq)
    {
        double freqDouble = Common.DivDecimal(freq, 100000);
        Channels[0].Items[0] = (Channels[0].Items[1] = freqDouble.ToString("f5"));
        Channels[0].Items[2] = Lang.SZ_NONE;
        Channels[0].Items[3] = Lang.SZ_NONE;
        Channels[0].Items[4] = Lang.SZ_POWER[1];
        Channels[0].Items[5] = Lang.SZ_BANDWIDTH[0];
        Channels[0].Items[6] = Lang.SZ_SCAN[0];
        Channels[0].Items[7] = Lang.SZ_SQL_MODE[0];
    }

    public int GetMinIndex()
    {
        for (int i = 0; i < 100; i++)
        {
            if (!string.IsNullOrEmpty(Channels[i].Items[0]))
            {
                return i;
            }
        }
        return 0;
    }

    public bool ChNumIsValid(int chNum)
    {
        return ChIndexIsValid(chNum - 1);
    }

    public bool ChIndexIsValid(int chIndex)
    {
        if (chIndex < 0 || chIndex >= 100)
        {
            return false;
        }
        return string.IsNullOrEmpty(Channels[chIndex].Items[0]);
    }
}
