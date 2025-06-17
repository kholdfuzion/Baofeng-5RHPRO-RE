namespace Radio;

public class ChannelIndexModel : IData
{
    public byte[] DataToBytes()
    {
        int index = 0;
        byte[] data = new byte[16];
        for (int i = 0; i < 100; i++)
        {
            string rxFreq = Cps.GetInstance().ChannelModel.Channels[i].Items[0];
            index = i;
            if (string.IsNullOrEmpty(rxFreq))
            {
                data[index / 8] = data[index / 8].SetBit(index % 8, 1, 1);
            }
            else
            {
                data[index / 8] = data[index / 8].SetBit(index % 8, 1, 0);
            }
        }
        return data;
    }

    public void BytesToData(byte[] data)
    {
    }
}
