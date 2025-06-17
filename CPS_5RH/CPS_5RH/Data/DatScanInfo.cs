namespace CPS_5RH.Data;

public class DatScanInfo
{
	public static int Data_Get_Offset = 0;

	public static int Scan_List_Size = 0;

	public static int Max_Scan_List_Num = 10;

	public static int Scan_Para_Size = 32;

	public static int ScanTotal = 0;

	private byte mode = 1;

	private byte rtnch;

	private byte priosacn;

	private byte scanrange;

	private ushort backtime = 19;

	private ushort rxresume = 2;

	private ushort txresume = 2;

	private ushort priochannel;

	private string sName = "";

	private int[] upFreq = new int[10] { 40000000, 40000000, 40000000, 40000000, 40000000, 40000000, 40000000, 40000000, 40000000, 40000000 };

	private int[] dwFreq = new int[10] { 40000000, 40000000, 40000000, 40000000, 40000000, 40000000, 40000000, 40000000, 40000000, 40000000 };

	private byte[] scanflg = new byte[80]
	{
		255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
		255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
		255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
		255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
		255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
		255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
		255, 255, 255, 255, 255, 255, 255, 255, 255, 255,
		255, 255, 255, 255, 255, 255, 255, 255, 255, 255
	};

	public byte ScanMode
	{
		get
		{
			return mode;
		}
		set
		{
			mode = value;
		}
	}

	public byte RtnChType
	{
		get
		{
			return rtnch;
		}
		set
		{
			rtnch = value;
		}
	}

	public byte PrioScan
	{
		get
		{
			return priosacn;
		}
		set
		{
			priosacn = value;
		}
	}

	public ushort RxResume
	{
		get
		{
			return rxresume;
		}
		set
		{
			rxresume = value;
		}
	}

	public ushort TxResume
	{
		get
		{
			return txresume;
		}
		set
		{
			txresume = value;
		}
	}

	public ushort BackScanTim
	{
		get
		{
			return backtime;
		}
		set
		{
			backtime = value;
		}
	}

	public byte ScanRange
	{
		get
		{
			return scanrange;
		}
		set
		{
			scanrange = value;
		}
	}

	public ushort PrioChannel
	{
		get
		{
			return priochannel;
		}
		set
		{
			priochannel = value;
		}
	}

	public string ScanName
	{
		get
		{
			return sName;
		}
		set
		{
			sName = value;
		}
	}

	public void SetScanFlg(int Idx, byte val)
	{
		scanflg[Idx] = val;
	}

	public byte GetScanFlg(int Idx)
	{
		return scanflg[Idx];
	}

	public void SetUpFreq(int Idx, int val)
	{
		upFreq[Idx] = val;
	}

	public int GetUpFreq(int Idx)
	{
		return upFreq[Idx];
	}

	public void SetDwFreq(int Idx, int val)
	{
		dwFreq[Idx] = val;
	}

	public int GetDwFreq(int Idx)
	{
		return dwFreq[Idx];
	}
}
