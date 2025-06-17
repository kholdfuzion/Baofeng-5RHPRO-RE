namespace CPS_5RH.Data;

public class DatMdcPara
{
	private byte ctrlsw = 1;

	private byte dectone = 1;

	private ushort encid = 1234;

	private ushort pretime = 250;

	private ushort sqldly = 250;

	private ushort decrst = 50;

	private byte encsync = 0;

	private byte decsync = 3;

	public byte CtrlSw
	{
		get
		{
			return ctrlsw;
		}
		set
		{
			ctrlsw = value;
		}
	}

	public byte DecTone
	{
		get
		{
			return dectone;
		}
		set
		{
			dectone = value;
		}
	}

	public ushort EncID
	{
		get
		{
			return encid;
		}
		set
		{
			encid = value;
		}
	}

	public ushort PreTim
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

	public ushort SqlDly
	{
		get
		{
			return sqldly;
		}
		set
		{
			sqldly = value;
		}
	}

	public ushort DecRst
	{
		get
		{
			return decrst;
		}
		set
		{
			decrst = value;
		}
	}

	public byte EncSync
	{
		get
		{
			return encsync;
		}
		set
		{
			encsync = value;
		}
	}

	public byte DecSync
	{
		get
		{
			return decsync;
		}
		set
		{
			decsync = value;
		}
	}
}
