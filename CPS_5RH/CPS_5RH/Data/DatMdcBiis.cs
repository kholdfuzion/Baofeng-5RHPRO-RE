namespace CPS_5RH.Data;

public class DatMdcBiis
{
	public static int BIIS_Size = 16;

	private byte swtone = 1;

	private byte zonecode = 16;

	private ushort selfid = 1234;

	private ushort grpid = 1;

	private ushort sync = 46131;

	private ushort pretine = 50;

	public byte ToneSw
	{
		get
		{
			return swtone;
		}
		set
		{
			swtone = value;
		}
	}

	public byte ZoneCode
	{
		get
		{
			return zonecode;
		}
		set
		{
			zonecode = value;
		}
	}

	public ushort SelfID
	{
		get
		{
			return selfid;
		}
		set
		{
			selfid = value;
		}
	}

	public ushort GrpID
	{
		get
		{
			return grpid;
		}
		set
		{
			grpid = value;
		}
	}

	public ushort Sync
	{
		get
		{
			return sync;
		}
		set
		{
			sync = value;
		}
	}

	public ushort PreTime
	{
		get
		{
			return pretine;
		}
		set
		{
			pretine = value;
		}
	}
}
