namespace CPS_5RH.Data;

public class DatDtmfSysInfo
{
	public static int Dtmf_Info_Size = 88;

	private int resetTime = 100;

	private int firstCodetime = 50;

	private int pretime = 200;

	private int codeDly = 200;

	private byte pttpause = 0;

	private byte sepcode = 4;

	private byte dtmfAni = 0;

	private byte dtmfstone = 1;

	private byte grpCode = 1;

	private byte codespeed = 0;

	private byte decrsp = 0;

	private string did = "123";

	private string bot = "";

	private string eot = "";

	private string stun = "";

	private string kill = "";

	public string Did
	{
		get
		{
			return did;
		}
		set
		{
			did = value;
		}
	}

	public string Bot
	{
		get
		{
			return bot;
		}
		set
		{
			bot = value;
		}
	}

	public string Eot
	{
		get
		{
			return eot;
		}
		set
		{
			eot = value;
		}
	}

	public string Stun
	{
		get
		{
			return stun;
		}
		set
		{
			stun = value;
		}
	}

	public string Kill
	{
		get
		{
			return kill;
		}
		set
		{
			kill = value;
		}
	}

	public int ResetTime
	{
		get
		{
			return resetTime;
		}
		set
		{
			resetTime = value;
		}
	}

	public int FirstCodeTim
	{
		get
		{
			return firstCodetime;
		}
		set
		{
			firstCodetime = value;
		}
	}

	public int PreTime
	{
		get
		{
			return pretime;
		}
		set
		{
			pretime = value;
		}
	}

	public int CodeDly
	{
		get
		{
			return codeDly;
		}
		set
		{
			codeDly = value;
		}
	}

	public byte GrpCode
	{
		get
		{
			return grpCode;
		}
		set
		{
			grpCode = value;
		}
	}

	public byte CodeSpeed
	{
		get
		{
			return codespeed;
		}
		set
		{
			codespeed = value;
		}
	}

	public byte PttIDPause
	{
		get
		{
			return pttpause;
		}
		set
		{
			pttpause = value;
		}
	}

	public byte SepCode
	{
		get
		{
			return sepcode;
		}
		set
		{
			sepcode = value;
		}
	}

	public byte DtmfSw
	{
		get
		{
			return dtmfAni;
		}
		set
		{
			dtmfAni = value;
		}
	}

	public byte DtmfTone
	{
		get
		{
			return dtmfstone;
		}
		set
		{
			dtmfstone = value;
		}
	}

	public byte DecRsp
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
}
