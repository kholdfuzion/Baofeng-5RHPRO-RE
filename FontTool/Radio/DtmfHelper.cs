using System;
using System.Text;

namespace Radio;

internal class DtmfHelper
{
	public static byte[] SaveDtmfCode(string code)
	{
		byte[] data = new byte[16];
		data.Reset(byte.MaxValue);
		if (!string.IsNullOrEmpty(code))
		{
			for (int i = 0; i < Math.Min(code.Length, 16); i++)
			{
				data[i] = (byte)"0123456789ABCD*#".IndexOf(code[i]);
			}
		}
		return data;
	}

	public static string DispDtmfCode(byte[] data)
	{
		StringBuilder sb = new StringBuilder(16);
		for (int i = 0; i < data.Length && data[i] >= 0 && data[i] < "0123456789ABCD*#".Length; i++)
		{
			sb.Append("0123456789ABCD*#"[data[i]]);
		}
		return sb.ToString();
	}
}
