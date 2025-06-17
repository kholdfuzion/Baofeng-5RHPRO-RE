namespace CPS_5RH.Data;

public class DatDtmfEncTab
{
	private ushort useflg = 0;

	private string[] encCode = new string[16]
	{
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", ""
	};

	private int[] encLen = new int[16];

	public ushort UseFlg
	{
		get
		{
			return useflg;
		}
		set
		{
			useflg = value;
		}
	}

	public void SetEncCode(int Idx, string val)
	{
		encCode[Idx] = val;
	}

	public string GetEncCode(int Idx)
	{
		return encCode[Idx];
	}

	public void SetEncLen(int Idx, int val)
	{
		encLen[Idx] = val;
	}

	public int GetEncLen(int Idx)
	{
		return encLen[Idx];
	}
}
