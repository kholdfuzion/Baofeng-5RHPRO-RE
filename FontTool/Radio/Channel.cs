using System;

namespace Radio;

public class Channel : IData
{
	public int ChIndex { get; set; }

	public string[] Items { get; set; }

	public Channel()
	{
		Items = new string[8];
	}

	public byte[] DataToBytes()
	{
		int index = 0;
		byte[] tmp = null;
		byte[] data = new byte[16];
		Array.Copy(Global.EEROM, 128 + ChIndex * 16, data, 0, data.Length);
		if (string.IsNullOrEmpty(Items[0]))
		{
			data.Reset(byte.MaxValue);
		}
		else
		{
			tmp = FrequencyHelper.FreqToBytes(Items[0]);
			Array.Copy(tmp, 0, data, index, tmp.Length);
			index += tmp.Length;
			tmp = FrequencyHelper.FreqToBytes(Items[1]);
			Array.Copy(tmp, 0, data, index, tmp.Length);
			index += tmp.Length;
			tmp = FrequencyHelper.ToneToBytes(Items[2]);
			Array.Copy(tmp, 0, data, index, tmp.Length);
			index += tmp.Length;
			tmp = FrequencyHelper.ToneToBytes(Items[3]);
			Array.Copy(tmp, 0, data, index, tmp.Length);
			index += tmp.Length;
			index = 12;
			if (Items[4] == Lang.SZ_POWER[0])
			{
				data[index] = data[index].SetBit(6, 2, 0);
			}
			else if (Items[4] == Lang.SZ_POWER[1])
			{
				data[index] = data[index].SetBit(6, 2, 1);
			}
			if (Items[5] == Lang.SZ_BANDWIDTH[0])
			{
				data[index] = data[index].SetBit(0, 2, 0);
			}
			else if (Items[5] == Lang.SZ_BANDWIDTH[1])
			{
				data[index] = data[index].SetBit(0, 2, 2);
			}
			index = index + 1 + 1;
			data[index] = data[index].SetBit(0, 4, Array.IndexOf(Lang.SZ_SQL_MODE, Items[7]));
			index++;
		}
		return data;
	}

	public void BytesToData(byte[] data)
	{
		int index = 0;
		byte[] tmp = null;
		byte value = 0;
		tmp = new byte[4];
		Array.Copy(data, index, tmp, 0, tmp.Length);
		Items[0] = FrequencyHelper.BytesToFreq(tmp);
		index += tmp.Length;
		if (string.IsNullOrEmpty(Items[0]))
		{
			for (int i = 1; i < Items.Length; i++)
			{
				Items[i] = "";
			}
			return;
		}
		Array.Copy(data, index, tmp, 0, tmp.Length);
		Items[1] = FrequencyHelper.BytesToFreq(tmp);
		index += 4;
		tmp = new byte[2];
		Array.Copy(data, index, tmp, 0, tmp.Length);
		Items[2] = FrequencyHelper.BytesToTone(tmp);
		index += tmp.Length;
		tmp = new byte[2];
		Array.Copy(data, index, tmp, 0, tmp.Length);
		Items[3] = FrequencyHelper.BytesToTone(tmp);
		index += tmp.Length;
		index = 12;
		Items[4] = Common.GetString(Lang.SZ_POWER, data[index].GetBit(6, 2));
		if (data[index].GetBit(0, 2) == 2)
		{
			Items[5] = Lang.SZ_BANDWIDTH[1];
		}
		else
		{
			Items[5] = Lang.SZ_BANDWIDTH[0];
		}
		index = index + 1 + 1;
		Items[7] = Common.GetString(Lang.SZ_SQL_MODE, data[index].GetBit(0, 4));
		index++;
	}
}
