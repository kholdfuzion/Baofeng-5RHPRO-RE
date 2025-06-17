namespace CPS_5RH.Data;

public class DatTwoToneInfo
{
	public static int Enc_List_Num = 16;

	public static int One_Enc_List_Size = 16;

	public static int Dec_Para_Size = 16;

	private int firsttone = 0;

	private int secondtone = 0;

	private int tonedur = 1;

	private int toneint = 10;

	private int stonesw = 1;

	private int decrsp = 0;

	private int resetime = 100;

	private int decformat = 1;

	private string atone = "1000";

	private string btone = "1800";

	private string ctone = "2000";

	private string dtone = "2200";

	private string[] encFreq1 = new string[32]
	{
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", ""
	};

	private string[] encFreq2 = new string[32]
	{
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", ""
	};

	private string[] encName = new string[32]
	{
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", ""
	};

	public int FirstTone
	{
		get
		{
			return firsttone;
		}
		set
		{
			firsttone = value;
		}
	}

	public int SecondTone
	{
		get
		{
			return secondtone;
		}
		set
		{
			secondtone = value;
		}
	}

	public int ToneDur
	{
		get
		{
			return tonedur;
		}
		set
		{
			tonedur = value;
		}
	}

	public int ToneInt
	{
		get
		{
			return toneint;
		}
		set
		{
			toneint = value;
		}
	}

	public int SToneSW
	{
		get
		{
			return stonesw;
		}
		set
		{
			stonesw = value;
		}
	}

	public int DecodeRsp
	{
		get
		{
			return decrsp;
		}
		set
		{
			decrsp = value;
		}
	}

	public int ReseTim
	{
		get
		{
			return resetime;
		}
		set
		{
			resetime = value;
		}
	}

	public int DecFormat
	{
		get
		{
			return decformat;
		}
		set
		{
			decformat = value;
		}
	}

	public string Atone
	{
		get
		{
			return atone;
		}
		set
		{
			atone = value;
		}
	}

	public string Btone
	{
		get
		{
			return btone;
		}
		set
		{
			btone = value;
		}
	}

	public string Ctone
	{
		get
		{
			return ctone;
		}
		set
		{
			ctone = value;
		}
	}

	public string Dtone
	{
		get
		{
			return dtone;
		}
		set
		{
			dtone = value;
		}
	}

	public void SetEncFreq1(int Idx, string val)
	{
		encFreq1[Idx] = val;
	}

	public string GetFreq1(int Idx)
	{
		return encFreq1[Idx];
	}

	public void SetEncFreq2(int Idx, string val)
	{
		encFreq2[Idx] = val;
	}

	public string GetFreq2(int Idx)
	{
		return encFreq2[Idx];
	}

	public void SetEncName(int Idx, string val)
	{
		encName[Idx] = val;
	}

	public string GetEncName(int Idx)
	{
		return encName[Idx];
	}
}
