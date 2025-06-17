namespace CPS_5RH.Data;

public class DatMdcPttID
{
	private byte txtone = 1;

	private byte rxtone = 1;

	private byte encen = 1;

	private byte decen = 1;

	private byte boten = 1;

	private byte eoten = 1;

	private ushort bottime = 3500;

	private ushort eottime = 3500;

	public byte TxTone
	{
		get
		{
			return txtone;
		}
		set
		{
			txtone = value;
		}
	}

	public byte RxTone
	{
		get
		{
			return rxtone;
		}
		set
		{
			rxtone = value;
		}
	}

	public byte EncEn
	{
		get
		{
			return encen;
		}
		set
		{
			encen = value;
		}
	}

	public byte DecEn
	{
		get
		{
			return decen;
		}
		set
		{
			decen = value;
		}
	}

	public byte BotEn
	{
		get
		{
			return boten;
		}
		set
		{
			boten = value;
		}
	}

	public byte EotEn
	{
		get
		{
			return eoten;
		}
		set
		{
			eoten = value;
		}
	}

	public ushort BotTime
	{
		get
		{
			return bottime;
		}
		set
		{
			bottime = value;
		}
	}

	public ushort EotTime
	{
		get
		{
			return eottime;
		}
		set
		{
			eottime = value;
		}
	}
}
