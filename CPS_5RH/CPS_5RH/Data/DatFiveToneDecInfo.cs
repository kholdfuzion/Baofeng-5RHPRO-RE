namespace CPS_5RH.Data;

public class DatFiveToneDecInfo
{
	public static int Dec_Size = 24;

	private byte decrsp = 0;

	private byte decstand = 0;

	private byte dectonetime = 0;

	private byte pttpause = 0;

	private byte resetTime = 100;

	private byte fiveAni = 1;

	private byte stopcode = 0;

	private int pretime = 500;

	private int codeDly = 200;

	private int firstCodetime = 70;

	private int stopCodetime = 10;

	private int decCodetime = 0;

	private string did = "12345";

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

	public int StopCodetime
	{
		get
		{
			return stopCodetime;
		}
		set
		{
			stopCodetime = value;
		}
	}

	public int DecCodetime
	{
		get
		{
			return decCodetime;
		}
		set
		{
			decCodetime = value;
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

	public byte DecStand
	{
		get
		{
			return decstand;
		}
		set
		{
			decstand = value;
		}
	}

	public byte DecToneTim
	{
		get
		{
			return dectonetime;
		}
		set
		{
			dectonetime = value;
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

	public byte ResetTime
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

	public byte FiveAni
	{
		get
		{
			return fiveAni;
		}
		set
		{
			fiveAni = value;
		}
	}

	public byte StopCode
	{
		get
		{
			return stopcode;
		}
		set
		{
			stopcode = value;
		}
	}
}
