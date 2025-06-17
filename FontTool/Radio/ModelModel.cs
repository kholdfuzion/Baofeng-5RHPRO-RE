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
		tmp = BitConverter.GetBytes(Common.DecToBcd32(MinFreq));
		Array.Copy(tmp, 0, data, index, tmp.Length);
		index += tmp.Length;
		tmp = BitConverter.GetBytes(Common.DecToBcd32(MaxFreq));
		Array.Copy(tmp, 0, data, index, tmp.Length);
		index += tmp.Length;
		return data;
	}

	public void BytesToData(byte[] data)
	{
		int index = 0;
		MinFreq = Common.BcdToDec32(BitConverter.ToInt32(data, index));
		MaxFreq = Common.BcdToDec32(BitConverter.ToInt32(data, index + 4));
		MinFreq = 40000000;
		MaxFreq = 52000000;
	}
}
