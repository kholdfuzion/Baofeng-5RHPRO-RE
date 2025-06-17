namespace Radio;

public class ScanIndexModel : IData
{
    public byte[] DataToBytes()
    {
        int index = 0;
        byte[] data = new byte[16];
        for (int i = 0; i < 100; i++)
        {
            Channel ch = Cps.GetInstance().ChannelModel.Channels[i];
            string rxFreq = ch.Items[0];
            string scan = ch.Items[6];
            index = i;
            if (string.IsNullOrEmpty(rxFreq))
            {
                data[index / 8] = data[index / 8].SetBit(index % 8, 1, 1);
            }
            else if (scan == Lang.SZ_SCAN[0])
            {
                data[index / 8] = data[index / 8].SetBit(index % 8, 1, 0);
            }
            else
            {
                data[index / 8] = data[index / 8].SetBit(index % 8, 1, 1);
            }
        }
        return data;
    }

    public void BytesToData(byte[] data)
    {
        int index = 0;
        for (int i = 0; i < 100; i++)
        {
            Channel ch = Cps.GetInstance().ChannelModel.Channels[i];
            string rxFreq = ch.Items[0];
            index = i;
            if (string.IsNullOrEmpty(rxFreq))
            {
                ch.Items[6] = "";
            }
            else if (data[index / 8].GetBit(index % 8, 1) == 0)
            {
                ch.Items[6] = Lang.SZ_SCAN[0];
            }
            else
            {
                ch.Items[6] = Lang.SZ_SCAN[1];
            }
        }
    }
}
