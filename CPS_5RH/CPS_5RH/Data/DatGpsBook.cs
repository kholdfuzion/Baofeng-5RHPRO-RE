namespace CPS_5RH.Data;

public class DatGpsBook
{
	public static int MAX_GpsBook_Num = 80;

	public static int GpsBook_Total = 0;

	private byte codeID = 0;

	private byte useFlg = 0;

	private string codename = "";

	public byte CodeID
	{
		get
		{
			return codeID;
		}
		set
		{
			codeID = value;
		}
	}

	public byte UseFlg
	{
		get
		{
			return useFlg;
		}
		set
		{
			useFlg = value;
		}
	}

	public string CodeName
	{
		get
		{
			return codename;
		}
		set
		{
			codename = value;
		}
	}
}
