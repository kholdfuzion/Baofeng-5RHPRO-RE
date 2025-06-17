using System;

namespace Radio;

internal class ButtonHelper
{
	public static string DispButtonKey(int index)
	{
		if (index < Lang.SZ_BUTTON_KEY.Length)
		{
			return Lang.SZ_BUTTON_KEY[index];
		}
		if (index == 7 || index == 8)
		{
			return Lang.SZ_BUTTON_KEY[0];
		}
		return Lang.SZ_BUTTON_KEY[0];
	}

	public static int SaveButtonKey(string key)
	{
		return Array.IndexOf(Lang.SZ_BUTTON_KEY, key);
	}
}
