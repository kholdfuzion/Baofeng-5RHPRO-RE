namespace CPS_5RH.Data;

public class DatChannelInfo
{
	public static int Data_Get_Offset = 0;

	public static int Max_Chn_Num = 640;

	public static int Chn_Name_Len = 0;

	public static int Chn_Info_Size = 48;

	public static int Chn_Name_Size = 0;

	public static int ChnTotal = 0;

	private string name = "";

	private string rxFreq;

	private string txFreq;

	private string rxCtsDcs = "OFF";

	private string txCtsDcs = "OFF";

	private string divFreq;

	private byte nPower = 2;

	private byte wideth = 0;

	private byte offsetdir = 0;

	private byte freqinvert = 0;

	private byte talkaround = 0;

	private byte dtmfptt;

	private byte fivetoneptt;

	private byte sqtype;

	private byte signaltype;

	private byte busylock;

	private byte txdis;

	private byte jumpfreq;

	private byte freqstep;

	private byte dtmfIdx;

	private byte tone5Idx;

	private byte tone2Idx;

	private byte mdcIdx;

	private byte scanlist;

	private byte emerglist;

	private byte scanadd;

	private byte cepin24bit = 0;

	private byte cepindcs = 0;

	private byte compand = 0;

	private byte scram = 0;

	private byte useFlg = 0;

	public string ChnName
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	public string RxFreq
	{
		get
		{
			return rxFreq;
		}
		set
		{
			rxFreq = value;
		}
	}

	public string TxFreq
	{
		get
		{
			return txFreq;
		}
		set
		{
			txFreq = value;
		}
	}

	public string RxCtsDcs
	{
		get
		{
			return rxCtsDcs;
		}
		set
		{
			rxCtsDcs = value;
		}
	}

	public string TxCtsDcs
	{
		get
		{
			return txCtsDcs;
		}
		set
		{
			txCtsDcs = value;
		}
	}

	public string DivFreq
	{
		get
		{
			return divFreq;
		}
		set
		{
			divFreq = value;
		}
	}

	public byte Power
	{
		get
		{
			return nPower;
		}
		set
		{
			nPower = value;
		}
	}

	public byte Wideth
	{
		get
		{
			return wideth;
		}
		set
		{
			wideth = value;
		}
	}

	public byte Offsetdir
	{
		get
		{
			return offsetdir;
		}
		set
		{
			offsetdir = value;
		}
	}

	public byte FreqStep
	{
		get
		{
			return freqstep;
		}
		set
		{
			freqstep = value;
		}
	}

	public byte FreqInvert
	{
		get
		{
			return freqinvert;
		}
		set
		{
			freqinvert = value;
		}
	}

	public byte TalkAround
	{
		get
		{
			return talkaround;
		}
		set
		{
			talkaround = value;
		}
	}

	public byte DtmfPtt
	{
		get
		{
			return dtmfptt;
		}
		set
		{
			dtmfptt = value;
		}
	}

	public byte FivetonePtt
	{
		get
		{
			return fivetoneptt;
		}
		set
		{
			fivetoneptt = value;
		}
	}

	public byte SqType
	{
		get
		{
			return sqtype;
		}
		set
		{
			sqtype = value;
		}
	}

	public byte SignalType
	{
		get
		{
			return signaltype;
		}
		set
		{
			signaltype = value;
		}
	}

	public byte DtmfIdx
	{
		get
		{
			return dtmfIdx;
		}
		set
		{
			dtmfIdx = value;
		}
	}

	public byte TwotoneIdx
	{
		get
		{
			return tone2Idx;
		}
		set
		{
			tone2Idx = value;
		}
	}

	public byte FivetoneIdx
	{
		get
		{
			return tone5Idx;
		}
		set
		{
			tone5Idx = value;
		}
	}

	public byte MdcIdx
	{
		get
		{
			return mdcIdx;
		}
		set
		{
			mdcIdx = value;
		}
	}

	public byte Scanlist
	{
		get
		{
			return scanlist;
		}
		set
		{
			scanlist = value;
		}
	}

	public byte Emerglist
	{
		get
		{
			return emerglist;
		}
		set
		{
			emerglist = value;
		}
	}

	public byte BusyLock
	{
		get
		{
			return busylock;
		}
		set
		{
			busylock = value;
		}
	}

	public byte TxDis
	{
		get
		{
			return txdis;
		}
		set
		{
			txdis = value;
		}
	}

	public byte JumpFreq
	{
		get
		{
			return jumpfreq;
		}
		set
		{
			jumpfreq = value;
		}
	}

	public byte ScanAdd
	{
		get
		{
			return scanadd;
		}
		set
		{
			scanadd = value;
		}
	}

	public byte Cepin24bit
	{
		get
		{
			return cepin24bit;
		}
		set
		{
			cepin24bit = value;
		}
	}

	public byte CepinDcs
	{
		get
		{
			return cepindcs;
		}
		set
		{
			cepindcs = value;
		}
	}

	public byte Compand
	{
		get
		{
			return compand;
		}
		set
		{
			compand = value;
		}
	}

	public byte Scram
	{
		get
		{
			return scram;
		}
		set
		{
			scram = value;
		}
	}

	public byte UseFlg
	{
		get
		{
			return useFlg;
		}
		set
		{
			useFlg = value;
		}
	}
}
