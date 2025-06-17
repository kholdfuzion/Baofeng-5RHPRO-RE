using System;

namespace Radio;

public class ButtonModel : IData
{
	public string Sk1Short { get; set; }

	public string Sk1Long { get; set; }

	public string Sk2Short { get; set; }

	public string Sk2Long { get; set; }

	public byte[] DataToBytes()
	{
		return new byte[4]
		{
			(byte)Array.IndexOf(Lang.SZ_BUTTON_KEY, Sk1Short),
			(byte)Array.IndexOf(Lang.SZ_BUTTON_KEY, Sk1Long),
			(byte)Array.IndexOf(Lang.SZ_BUTTON_KEY, Sk2Short),
			(byte)Array.IndexOf(Lang.SZ_BUTTON_KEY, Sk2Long)
		};
	}

	public void BytesToData(byte[] data)
	{
		Sk1Short = ButtonHelper.DispButtonKey(data[0]);
		Sk1Long = ButtonHelper.DispButtonKey(data[1]);
		Sk2Short = ButtonHelper.DispButtonKey(data[2]);
		Sk2Long = ButtonHelper.DispButtonKey(data[3]);
	}
}
