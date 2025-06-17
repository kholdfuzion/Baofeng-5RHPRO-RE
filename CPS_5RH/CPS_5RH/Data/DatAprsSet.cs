namespace CPS_5RH.Data;

public class DatAprsSet
{
	public static int CallSign_Total = 0;

	private byte passall = 0;

	private byte position = 0;

	private byte mice = 0;

	private byte objec_t = 0;

	private byte item = 0;

	private byte message = 0;

	private byte wxreport = 0;

	private byte nmeareport = 0;

	private byte statusreport = 0;

	private byte other = 0;

	private byte power = 2;

	private byte band = 1;

	private byte beeptone = 0;

	private byte londir = 0;

	private byte latdir = 0;

	private string ctdcs = "OFF";

	private string postable = "";

	private string posicon = "";

	private byte srcid = 0;

	private byte desid = 0;

	private int pretime = 25;

	private int codeDly = 25;

	private string srcno = "";

	private string desno = "";

	private byte rxcallsignnum = 0;

	private int sendinterval = 15;

	private byte regularlysend = 0;

	private byte aprsdistim = 0;

	private byte beacon = 0;

	private byte heighttype = 0;

	private byte txtlength = 0;

	private byte txfreqindex = 0;

	private byte pttid = 0;

	private byte micetype = 7;

	private string[] callsignno = new string[8] { "", "", "", "", "", "", "", "" };

	private byte[] callsignid = new byte[8];

	private string[] rxcallsignno = new string[32]
	{
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", "", "", "", "", "", "", "", "", "",
		"", ""
	};

	private byte[] rxcallsignid = new byte[32];

	private double lat = 0.0;

	private double lon = 0.0;

	private int height = 0;

	private string freq1 = "";

	private string freq2 = "";

	private string freq3 = "";

	private string freq4 = "";

	private string freq5 = "";

	private string freq6 = "";

	private string freq7 = "";

	private string freq8 = "";

	private string txt = "";

	private byte[] rxfilter = new byte[32];

	public byte TxFreqIndex
	{
		get
		{
			return txfreqindex;
		}
		set
		{
			txfreqindex = value;
		}
	}

	public byte PttId
	{
		get
		{
			return pttid;
		}
		set
		{
			pttid = value;
		}
	}

	public byte MicEType
	{
		get
		{
			return micetype;
		}
		set
		{
			micetype = value;
		}
	}

	public byte PassAll
	{
		get
		{
			return passall;
		}
		set
		{
			passall = value;
		}
	}

	public byte Position
	{
		get
		{
			return position;
		}
		set
		{
			position = value;
		}
	}

	public byte MicE
	{
		get
		{
			return mice;
		}
		set
		{
			mice = value;
		}
	}

	public byte Object
	{
		get
		{
			return objec_t;
		}
		set
		{
			objec_t = value;
		}
	}

	public byte Item
	{
		get
		{
			return item;
		}
		set
		{
			item = value;
		}
	}

	public byte Message
	{
		get
		{
			return message;
		}
		set
		{
			message = value;
		}
	}

	public byte WxReport
	{
		get
		{
			return wxreport;
		}
		set
		{
			wxreport = value;
		}
	}

	public byte NMEAReport
	{
		get
		{
			return nmeareport;
		}
		set
		{
			nmeareport = value;
		}
	}

	public byte StatusReport
	{
		get
		{
			return statusreport;
		}
		set
		{
			statusreport = value;
		}
	}

	public byte Other
	{
		get
		{
			return other;
		}
		set
		{
			other = value;
		}
	}

	public byte Power
	{
		get
		{
			return power;
		}
		set
		{
			power = value;
		}
	}

	public byte Band
	{
		get
		{
			return band;
		}
		set
		{
			band = value;
		}
	}

	public byte BeepTone
	{
		get
		{
			return beeptone;
		}
		set
		{
			beeptone = value;
		}
	}

	public byte LongDir
	{
		get
		{
			return londir;
		}
		set
		{
			londir = value;
		}
	}

	public byte LatDir
	{
		get
		{
			return latdir;
		}
		set
		{
			latdir = value;
		}
	}

	public string Ctdcs
	{
		get
		{
			return ctdcs;
		}
		set
		{
			ctdcs = value;
		}
	}

	public string PostionTable
	{
		get
		{
			return postable;
		}
		set
		{
			postable = value;
		}
	}

	public string PostionIcon
	{
		get
		{
			return posicon;
		}
		set
		{
			posicon = value;
		}
	}

	public byte RxCallSignNum
	{
		get
		{
			return rxcallsignnum;
		}
		set
		{
			rxcallsignnum = value;
		}
	}

	public int SendInterval
	{
		get
		{
			return sendinterval;
		}
		set
		{
			sendinterval = value;
		}
	}

	public byte RegularlySend
	{
		get
		{
			return regularlysend;
		}
		set
		{
			regularlysend = value;
		}
	}

	public byte APRSDisplayTime
	{
		get
		{
			return aprsdistim;
		}
		set
		{
			aprsdistim = value;
		}
	}

	public byte Beacon
	{
		get
		{
			return beacon;
		}
		set
		{
			beacon = value;
		}
	}

	public byte HeightType
	{
		get
		{
			return heighttype;
		}
		set
		{
			heighttype = value;
		}
	}

	public byte TxtLength
	{
		get
		{
			return txtlength;
		}
		set
		{
			txtlength = value;
		}
	}

	public double Lat
	{
		get
		{
			return lat;
		}
		set
		{
			lat = value;
		}
	}

	public double Long
	{
		get
		{
			return lon;
		}
		set
		{
			lon = value;
		}
	}

	public int Height
	{
		get
		{
			return height;
		}
		set
		{
			height = value;
		}
	}

	public string Freq1
	{
		get
		{
			return freq1;
		}
		set
		{
			freq1 = value;
		}
	}

	public string Freq2
	{
		get
		{
			return freq2;
		}
		set
		{
			freq2 = value;
		}
	}

	public string Freq3
	{
		get
		{
			return freq3;
		}
		set
		{
			freq3 = value;
		}
	}

	public string Freq4
	{
		get
		{
			return freq4;
		}
		set
		{
			freq4 = value;
		}
	}

	public string Freq5
	{
		get
		{
			return freq5;
		}
		set
		{
			freq5 = value;
		}
	}

	public string Freq6
	{
		get
		{
			return freq6;
		}
		set
		{
			freq6 = value;
		}
	}

	public string Freq7
	{
		get
		{
			return freq7;
		}
		set
		{
			freq7 = value;
		}
	}

	public string Freq8
	{
		get
		{
			return freq8;
		}
		set
		{
			freq8 = value;
		}
	}

	public string Txt
	{
		get
		{
			return txt;
		}
		set
		{
			txt = value;
		}
	}

	public byte SrcID
	{
		get
		{
			return srcid;
		}
		set
		{
			srcid = value;
		}
	}

	public byte DesID
	{
		get
		{
			return desid;
		}
		set
		{
			desid = value;
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

	public string SrcNo
	{
		get
		{
			return srcno;
		}
		set
		{
			srcno = value;
		}
	}

	public string DesNo
	{
		get
		{
			return desno;
		}
		set
		{
			desno = value;
		}
	}

	public void SetCallSignNo(int Idx, string val)
	{
		callsignno[Idx] = val;
	}

	public string GetCallSignNo(int Idx)
	{
		return callsignno[Idx];
	}

	public void SetCallSignID(int Idx, byte val)
	{
		callsignid[Idx] = val;
	}

	public byte GetCallSignID(int Idx)
	{
		return callsignid[Idx];
	}

	public void SetRxCallSignNo(int Idx, string val)
	{
		rxcallsignno[Idx] = val;
	}

	public string GetRxCallSignNo(int Idx)
	{
		return rxcallsignno[Idx];
	}

	public void SetRxCallSignID(int Idx, byte val)
	{
		rxcallsignid[Idx] = val;
	}

	public byte GetRxCallSignID(int Idx)
	{
		return rxcallsignid[Idx];
	}

	public void SetRxFilter(int Idx, byte val)
	{
		rxfilter[Idx] = val;
	}

	public byte GetRxFilter(int Idx)
	{
		return rxfilter[Idx];
	}
}
