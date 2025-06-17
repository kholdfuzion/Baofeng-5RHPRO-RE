namespace CPS_5RH.Data;

public class DatEmergInfo
{
	public static int Emerg_Sys_Size = 16;

	public static int Max_Emerg_Sys_Num = 10;

	private byte index;

	private byte type;

	private byte mode;

	private byte grpno;

	private byte exgtime = 5;

	private byte txtime = 5;

	private byte rxtime = 5;

	private byte chsel;

	private byte duration;

	private byte channel = 0;

	private byte zone = 0;

	public byte Type
	{
		get
		{
			return type;
		}
		set
		{
			type = value;
		}
	}

	public byte Mode
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

	public byte GrpNo
	{
		get
		{
			return grpno;
		}
		set
		{
			grpno = value;
		}
	}

	public byte ExgTime
	{
		get
		{
			return exgtime;
		}
		set
		{
			exgtime = value;
		}
	}

	public byte TxTime
	{
		get
		{
			return txtime;
		}
		set
		{
			txtime = value;
		}
	}

	public byte RxTime
	{
		get
		{
			return rxtime;
		}
		set
		{
			rxtime = value;
		}
	}

	public byte ChSel
	{
		get
		{
			return chsel;
		}
		set
		{
			chsel = value;
		}
	}

	public byte Duration
	{
		get
		{
			return duration;
		}
		set
		{
			duration = value;
		}
	}

	public byte Chn
	{
		get
		{
			return channel;
		}
		set
		{
			channel = value;
		}
	}

	public byte Zone
	{
		get
		{
			return zone;
		}
		set
		{
			zone = value;
		}
	}
}
