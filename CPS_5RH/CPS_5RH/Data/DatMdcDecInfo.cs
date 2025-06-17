namespace CPS_5RH.Data;

public class DatMdcDecInfo
{
	public static int Max_Num = 100;

	public static int One_List_Size = 16;

	private byte[] SysList = new byte[8];

	private byte[] tblEn = new byte[128];

	private byte[] decRsp = new byte[100];

	private string[] decId = new string[100]
	{
		"1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234",
		"1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234",
		"1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234",
		"1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234",
		"1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234",
		"1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234",
		"1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234",
		"1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234",
		"1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234",
		"1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234"
	};

	private string[] decName = new string[100]
	{
		"List 1", "List 2", "List 3", "List 4", "List 5", "List 6", "List 7", "List 8", "List 9", "List 10",
		"List 11", "List 12", "List 13", "List 14", "List 15", "List 16", "List 17", "List 18", "List 19", "List 20",
		"List 21", "List 22", "List 23", "List 24", "List 25", "List 26", "List 27", "List 28", "List 29", "List 30",
		"List 31", "List 32", "List 33", "List 34", "List 35", "List 36", "List 37", "List 38", "List 39", "List 40",
		"List 41", "List 42", "List 43", "List 44", "List 45", "List 46", "List 47", "List 48", "List 49", "List 50",
		"List 51", "List 52", "List 53", "List 54", "List 65", "List 56", "List 57", "List 58", "List 59", "List 60",
		"List 61", "List 62", "List 63", "List 64", "List 75", "List 66", "List 67", "List 68", "List 69", "List 70",
		"List 71", "List 72", "List 73", "List 74", "List 75", "List 76", "List 77", "List 78", "List 79", "List 80",
		"List 81", "List 82", "List 83", "List 84", "List 85", "List 86", "List 87", "List 88", "List 89", "List 90",
		"List 91", "List 92", "List 93", "List 94", "List 95", "List 96", "List 97", "List 98", "List 99", "List 100"
	};

	public void SetTblEn(int Idx, byte val)
	{
		tblEn[Idx] = val;
	}

	public byte GetTblEn(int Idx)
	{
		return tblEn[Idx];
	}

	public void SetDecRsp(int Idx, byte val)
	{
		decRsp[Idx] = val;
	}

	public byte GetDecRsp(int Idx)
	{
		return decRsp[Idx];
	}

	public void SetDecID(int Idx, string val)
	{
		decId[Idx] = val;
	}

	public string GetDecID(int Idx)
	{
		return decId[Idx];
	}

	public void SetDecName(int Idx, string val)
	{
		decName[Idx] = val;
	}

	public string GetDecName(int Idx)
	{
		return decName[Idx];
	}

	public void SetSysList(int Idx, byte val)
	{
		SysList[Idx] = val;
	}

	public byte GetSysList(int Idx)
	{
		return SysList[Idx];
	}
}
