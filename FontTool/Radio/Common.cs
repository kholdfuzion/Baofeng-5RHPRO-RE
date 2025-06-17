using System;
using System.Text;

namespace Radio;

internal class Common
{
	public static double MulDecimal(double data1, double data2)
	{
		return Convert.ToDouble(Convert.ToDecimal(data1) * Convert.ToDecimal(data2));
	}

	public static double DivDecimal(int data1, int data2)
	{
		return Convert.ToDouble(Convert.ToDecimal(data1) / Convert.ToDecimal(data2));
	}

	public static int DecToBcd32(int dec)
	{
		int i = 0;
		int tmp = 0;
		int result = 0;
		for (i = 0; i < 8; i++)
		{
			tmp = dec % 10;
			dec /= 10;
			result += tmp * (int)Math.Pow(16.0, i);
		}
		return result;
	}

	public static int BcdToDec32(int bcd)
	{
		int i = 0;
		int tmp = 0;
		int result = 0;
		for (i = 0; i < 8; i++)
		{
			tmp = bcd & 0xF;
			bcd >>= 4;
			result += tmp * (int)Math.Pow(10.0, i);
		}
		return result;
	}

	public static short BcdToDec16(short bcd)
	{
		int i = 0;
		int tmp = 0;
		short result = 0;
		for (i = 0; i < 4; i++)
		{
			tmp = bcd & 0xF;
			bcd >>= 4;
			result = (short)((double)result + (double)tmp * Math.Pow(10.0, i));
		}
		return result;
	}

	public static short DecToBcd16(short dec)
	{
		int i = 0;
		int tmp = 0;
		short result = 0;
		for (i = 0; i < 4; i++)
		{
			tmp = dec % 10;
			dec /= 10;
			result = (short)((double)result + (double)tmp * Math.Pow(16.0, i));
		}
		return result;
	}

	public static string GetString(string[] items, int index)
	{
		if (index < items.Length)
		{
			return items[index];
		}
		return items[0];
	}

	public static byte[] SaveName(string name, int length)
	{
		byte[] data = new byte[length];
		data.Reset(byte.MaxValue);
		byte[] tmp = Encoding.ASCII.GetBytes(name);
		Array.Copy(tmp, 0, data, 0, Math.Min(length, tmp.Length));
		return data;
	}

	public static string DispName(byte[] data)
	{
		int index = Array.IndexOf(data, byte.MaxValue);
		if (index == -1)
		{
			index = Array.IndexOf(data, (byte)0);
			if (index == -1)
			{
				index = data.Length;
			}
		}
		return Encoding.ASCII.GetString(data, 0, index);
	}
}
