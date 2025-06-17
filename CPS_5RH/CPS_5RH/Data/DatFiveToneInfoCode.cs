namespace CPS_5RH.Data;

public class DatFiveToneInfoCode
{
	private byte[] func = new byte[8];

	private byte[] RspInfo = new byte[8];

	private byte[] cdlen = new byte[8];

	private string[] decId = new string[8] { "1234", "1234", "1234", "1234", "1234", "1234", "1234", "1234" };

	private string[] decName = new string[8] { "List 1", "List 2", "List 3", "List 4", "List 5", "List 6", "List 7", "List 8" };

	public void SetFunc(int Idx, byte val)
	{
		func[Idx] = val;
	}

	public byte GetFunc(int Idx)
	{
		return func[Idx];
	}

	public void SetCdLen(int Idx, byte val)
	{
		cdlen[Idx] = val;
	}

	public byte GetCdLen(int Idx)
	{
		return cdlen[Idx];
	}

	public void SetRspInfo(int Idx, byte val)
	{
		RspInfo[Idx] = val;
	}

	public byte GetRspInfo(int Idx)
	{
		return RspInfo[Idx];
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
}
