namespace CPS_5RH.Data;

public class DatZoneInfo
{
	public static int ZoneTotal = 0;

	public static ushort Cur_Zone_Idx = 0;

	private int cidx = 0;

	private string zName = "";

	private ushort chnNum = 0;

	private ushort zoneIdx = 0;

	private ushort[] chnID = new ushort[640];

	public int Cidx
	{
		get
		{
			return cidx;
		}
		set
		{
			cidx = value;
		}
	}

	public ushort ChnNum
	{
		get
		{
			return chnNum;
		}
		set
		{
			chnNum = value;
		}
	}

	public ushort ZoneIdx
	{
		get
		{
			return zoneIdx;
		}
		set
		{
			zoneIdx = value;
		}
	}

	public string ZoneName
	{
		get
		{
			return zName;
		}
		set
		{
			zName = value;
		}
	}

	public void SetChnID(int Idx, int val)
	{
		chnID[Idx] = (ushort)val;
	}

	public int GetChnID(int Idx)
	{
		return chnID[Idx];
	}
}
