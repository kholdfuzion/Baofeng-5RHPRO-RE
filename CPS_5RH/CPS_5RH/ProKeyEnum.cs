using System.Collections.Generic;

namespace CPS_5RH;

internal class ProKeyEnum
{
	public const int PROKEY_FUNC_MAX = 16;

	public static int KEY_FUNC_NONE = 0;

	public static int KEY_FUNC_SCAN = 1;

	public static int KEY_FUNC_MONI = 2;

	public static int KEY_FUNC_FLASH_LIGHT = 3;

	public static int KEY_FUNC_FM = 4;

	public static int KEY_FUNC_EMERG = 5;

	public static int KEY_FUNC_GPS = 6;

	public static int KEY_FUNC_CEPIN = 7;

	public static int KEY_FUNC_BT = 8;

	public static int KEY_FUNC_1750 = 9;

	public static int KEY_FUNC_FALLING_DW = 10;

	public static int KEY_FUNC_PUSHTOTALK = 11;

	public static int KEY_FUNC_ZONE = 12;

	public static int KEY_FUNC_BATTERY = 13;

	public static int KEY_FUNC_POWERSEL = 14;

	public static int KEY_FUNC_VOX = 15;

	public static string[] KeyFunc_CN = new string[16]
	{
		"无", "扫描键", "监听键", "手电灯", "收音机", "紧急警报", "定位系统", "一键扫频", "蓝牙", "1750Hz",
		"倒地报警", "一键呼叫", "区域", "电池电压", "发射功率", "声控"
	};

	public static string[] KeyFunc_EN = new string[16]
	{
		"None", "Scan On/Off", "Monitor", "FlashLight", "FM Radio", "Emergency", "GPS", "Freq. Measuring", "Bluetooth", "1750Hz",
		"Falling Alarm", "One Touch Call", "Zone Change", "Battery Indicator", "Tx Power", "VOX On/Off"
	};

	public static List<string> Short1Pkey = new List<string> { "" };

	public static List<string> Long1Pkey = new List<string> { "" };

	public static List<string> Short2Pkey = new List<string> { "" };

	public static List<string> Long2Pkey = new List<string> { "" };

	public static List<string> Short3Pkey = new List<string> { "" };

	public static List<string> Long3Pkey = new List<string> { "" };

	public static List<string> Short4Pkey = new List<string> { "" };

	public static List<string> Long4Pkey = new List<string> { "" };

	public static int GetEuumIdx(string str)
	{
		int i = 0;
		if (LanguageSel.LangType == 0)
		{
			for (i = 0; i < 16; i++)
			{
				if (str == KeyFunc_CN[i])
				{
					return i;
				}
			}
		}
		else
		{
			for (i = 0; i < 16; i++)
			{
				if (str == KeyFunc_EN[i])
				{
					return i;
				}
			}
		}
		return 0;
	}

	public static string GetEuumStr(int idx)
	{
		if (idx >= 16)
		{
			idx = 0;
		}
		if (LanguageSel.LangType == 0)
		{
			return KeyFunc_CN[idx];
		}
		return KeyFunc_EN[idx];
	}
}
