namespace CPS_5RH.Data;

public class DatFiveToneEncInfo
{
	public static int Enc_List_Num = 100;

	public static int One_Enc_List_Size = 32;

	public static int Enc_PTTID_Size = 32;

	private byte pidstds = 0;

	private byte pidstde = 0;

	private byte pidcodetimes = 0;

	private byte pidcodetimee = 0;

	private byte pidcodelens = 0;

	private byte pidcodelene = 0;

	private string pidst = "";

	private string pided = "";

	private byte[] encStd = new byte[100];

	private byte[] encCodetim = new byte[100];

	private byte[] encCodelen = new byte[100];

	private byte[] encSpecall = new byte[100];

	private byte[] tblEn = new byte[104];

	private string[] encId = new string[100]
	{
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", ""
	};

	private string[] encName = new string[100]
	{
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", ""
	};

	public byte PidStandS
	{
		get
		{
			return pidstds;
		}
		set
		{
			pidstds = value;
		}
	}

	public byte PidStandE
	{
		get
		{
			return pidstde;
		}
		set
		{
			pidstde = value;
		}
	}

	public byte PidCodeTimS
	{
		get
		{
			return pidcodetimes;
		}
		set
		{
			pidcodetimes = value;
		}
	}

	public byte PidCodeTimE
	{
		get
		{
			return pidcodetimee;
		}
		set
		{
			pidcodetimee = value;
		}
	}

	public byte PidCodeLenS
	{
		get
		{
			return pidcodelens;
		}
		set
		{
			pidcodelens = value;
		}
	}

	public byte PidCodeLenE
	{
		get
		{
			return pidcodelene;
		}
		set
		{
			pidcodelene = value;
		}
	}

	public string PidStart
	{
		get
		{
			return pidst;
		}
		set
		{
			pidst = value;
		}
	}

	public string PidEnd
	{
		get
		{
			return pided;
		}
		set
		{
			pided = value;
		}
	}

	public void SetTblEn(int Idx, byte val)
	{
		tblEn[Idx] = val;
	}

	public byte GetTblEn(int Idx)
	{
		return tblEn[Idx];
	}

	public void SetEncStand(int Idx, byte val)
	{
		encStd[Idx] = val;
	}

	public byte GetEncStand(int Idx)
	{
		return encStd[Idx];
	}

	public void SetEncCodeTim(int Idx, byte val)
	{
		encCodetim[Idx] = val;
	}

	public byte GetEncCodeTim(int Idx)
	{
		return encCodetim[Idx];
	}

	public void SetEncCodeLen(int Idx, byte val)
	{
		encCodelen[Idx] = val;
	}

	public byte GetEncCodeLen(int Idx)
	{
		return encCodelen[Idx];
	}

	public void SetEncScall(int Idx, byte val)
	{
		encSpecall[Idx] = val;
	}

	public byte GetEncScall(int Idx)
	{
		return encSpecall[Idx];
	}

	public void SetEncID(int Idx, string val)
	{
		encId[Idx] = val;
	}

	public string GetEncID(int Idx)
	{
		return encId[Idx];
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
