using System;
using System.Globalization;

namespace Radio;

internal class FrequencyHelper
{
	static FrequencyHelper()
	{
	}

	public static int FreqToDec(string freq)
	{
		return Convert.ToInt32(decimal.Parse(freq) * 100000m);
	}

	public static int FreqToBcd(string freq)
	{
		return Common.DecToBcd32(FreqToDec(freq));
	}

	public static byte[] FreqToBytes(string freq)
	{
		if (string.IsNullOrEmpty(freq))
		{
			return BitConverter.GetBytes(-1);
		}
		return BitConverter.GetBytes(FreqToBcd(freq));
	}

	public static string BytesToFreq(byte[] data)
	{
		try
		{
			int freqDec = Common.BcdToDec32(BitConverter.ToInt32(data, 0));
			if (freqDec >= Cps.GetInstance().ModelModel.MinFreq && freqDec <= Cps.GetInstance().ModelModel.MaxFreq)
			{
				return Common.DivDecimal(freqDec, 100000).ToString("f5");
			}
			return "";
		}
		catch (Exception)
		{
			return "";
		}
	}

	public static string DecToFreq(int freq)
	{
		return Common.DivDecimal(freq, 100000).ToString("f5");
	}

	public static bool FreqIsValid(string freq)
	{
		if (string.IsNullOrEmpty(freq))
		{
			return false;
		}
		double freqDouble = 0.0;
		double.TryParse(freq, out freqDouble);
		int freqInt = 0;
		try
		{
			freqInt = Convert.ToInt32(freqDouble * 100000.0);
		}
		catch
		{
			return false;
		}
		ModelModel model = Cps.GetInstance().ModelModel;
		if (freqInt >= model.MinFreq && freqInt <= model.MaxFreq)
		{
			return true;
		}
		return false;
	}

	public static void AdjustFreq(ref int freq, int freqStep1, int freqStep2)
	{
		int remain1 = freq % freqStep1;
		int remain2 = freq % freqStep2;
		if (remain1 != 0 && remain2 != 0)
		{
			int upperRemain1 = freqStep1 - remain1;
			int upperRemain2 = freqStep2 - remain2;
			if (upperRemain1 < upperRemain2)
			{
				freq += upperRemain1;
			}
			else
			{
				freq += upperRemain2;
			}
		}
	}

	public static byte[] ToneToBytes(string tone)
	{
		short toneShort = 0;
		short toneBcd = 0;
		double toneDouble = 0.0;
		string value = "";
		byte[] data = new byte[2];
		if (string.IsNullOrEmpty(tone))
		{
			return BitConverter.GetBytes((short)(-1));
		}
		if (tone == Lang.SZ_NONE)
		{
			data[0] = byte.MaxValue;
			data[1] = byte.MaxValue;
		}
		else if (tone.IndexOf('N') >= 0)
		{
			short.TryParse(tone.Substring(1, 3), NumberStyles.HexNumber, null, out toneShort);
			data[0] = (byte)(toneShort >> 8);
			data[0] |= 128;
			data[1] = (byte)toneShort;
		}
		else if (tone.IndexOf('I') >= 0)
		{
			short.TryParse(tone.Substring(1, 3), NumberStyles.HexNumber, null, out toneShort);
			data[0] = (byte)(toneShort >> 8);
			data[0] |= 192;
			data[1] = (byte)toneShort;
		}
		else if (tone.IndexOf('.') >= 0)
		{
			double.TryParse(tone, out toneDouble);
			toneBcd = Common.DecToBcd16((short)Common.MulDecimal(toneDouble, 10.0));
			data[0] = (byte)(toneBcd >> 8);
			data[1] = (byte)toneBcd;
		}
		else
		{
			data[0] = byte.MaxValue;
			data[1] = byte.MaxValue;
		}
		return data;
	}

	public static string BytesToTone(byte[] data)
	{
		short toneDec = 0;
		short toneBcd = 0;
		double toneDouble = 0.0;
		try
		{
			if (data[0] == byte.MaxValue && data[1] == byte.MaxValue)
			{
				return Lang.SZ_NONE;
			}
			if (data[0] == 0 && data[1] == 0)
			{
				return Lang.SZ_NONE;
			}
			if (data[0] >= 192)
			{
				return $"D{(short)((data[0] & 0x3F) * 256 + data[1]):X3}I";
			}
			if (data[0] >= 128)
			{
				return $"D{(short)((data[0] & 0x3F) * 256 + data[1]):X3}N";
			}
			return Common.DivDecimal(Common.BcdToDec16((short)(data[0] * 256 + data[1])), 10).ToString("f1");
		}
		catch (Exception)
		{
			return "";
		}
	}

	public static bool FreqIs5kStep(string freq)
	{
		return FreqToDec(freq) % 500 == 0;
	}
}
