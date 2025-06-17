namespace CPS_5RH.Data;

public class DatGenSetInfo
{
	private byte chamode = 1;

	private byte chbmode = 1;

	private ushort chanum = 0;

	private ushort chbnum = 0;

	private byte chazone = 0;

	private byte chbzone = 0;

	private byte blighttime = 6;

	private byte blightlv = 4;

	private byte chAdisply = 0;

	private byte chBdisply = 0;

	private byte dualmode = 1;

	private byte mainband = 0;

	private byte powoface = 0;

	private byte sqlv = 3;

	private byte voxsw = 0;

	private byte voxlv = 4;

	private byte voxdettime = 4;

	private byte save = 3;

	private byte savedly = 0;

	private byte loneworkrsp = 0;

	private byte loneworktime = 0;

	private byte apo = 0;

	private byte tot = 8;

	private byte pretot = 3;

	private byte hz1750 = 0;

	private byte aprs = 0;

	private byte lonework = 0;

	private byte daodi = 0;

	private byte voice = 2;

	private byte busylock = 0;

	private byte keylock = 0;

	private byte autokey = 0;

	private byte tone = 1;

	private byte endtone = 0;

	private byte langsel = 0;

	private byte tailFreq = 1;

	private byte noaach = 0;

	private byte gpsid = 1;

	private byte gps = 0;

	private byte gpsmode = 0;

	private byte gpsreq = 1;

	private byte gpsshare = 1;

	private byte gpszone = 20;

	private byte bluet = 0;

	private byte bluetapp = 0;

	private byte bluetpair = 0;

	private byte bluethold = 0;

	private byte bluerxdly = 0;

	private byte bluetmic = 0;

	private byte bluetspk = 0;

	private byte record = 0;

	private byte recordmode = 2;

	private byte engineering = 0;

	private byte noaa = 0;

	private byte tianqi = 0;

	private byte fminter = 0;

	private byte dispdir = 0;

	private byte enhancefunc = 0;

	private byte noisecancel = 0;

	private byte skey1 = 2;

	private byte skey2 = 4;

	private byte lkey1 = 0;

	private byte lkey2 = 0;

	private byte skey3 = 2;

	private byte skey4 = 4;

	private byte lkey3 = 0;

	private byte lkey4 = 0;

	private string powPassword = "";

	private string wrpassword = "";

	private string btpassword = "";

	private string radioname = "welcome";

	private string paitname = "";

	private string btname = "";

	public byte ChAMode
	{
		get
		{
			return chamode;
		}
		set
		{
			chamode = value;
		}
	}

	public byte ChBMode
	{
		get
		{
			return chbmode;
		}
		set
		{
			chbmode = value;
		}
	}

	public ushort ChANum
	{
		get
		{
			return chanum;
		}
		set
		{
			chanum = value;
		}
	}

	public ushort ChBNum
	{
		get
		{
			return chbnum;
		}
		set
		{
			chbnum = value;
		}
	}

	public byte ChAZone
	{
		get
		{
			return chazone;
		}
		set
		{
			chazone = value;
		}
	}

	public byte ChBZone
	{
		get
		{
			return chbzone;
		}
		set
		{
			chbzone = value;
		}
	}

	public byte BlightTime
	{
		get
		{
			return blighttime;
		}
		set
		{
			blighttime = value;
		}
	}

	public byte BlightLv
	{
		get
		{
			return blightlv;
		}
		set
		{
			blightlv = value;
		}
	}

	public byte DualMode
	{
		get
		{
			return dualmode;
		}
		set
		{
			dualmode = value;
		}
	}

	public byte ChADispMode
	{
		get
		{
			return chAdisply;
		}
		set
		{
			chAdisply = value;
		}
	}

	public byte ChBDispMode
	{
		get
		{
			return chBdisply;
		}
		set
		{
			chBdisply = value;
		}
	}

	public byte PownFace
	{
		get
		{
			return powoface;
		}
		set
		{
			powoface = value;
		}
	}

	public byte MainBand
	{
		get
		{
			return mainband;
		}
		set
		{
			mainband = value;
		}
	}

	public byte Sqlv
	{
		get
		{
			return sqlv;
		}
		set
		{
			sqlv = value;
		}
	}

	public byte PoSave
	{
		get
		{
			return save;
		}
		set
		{
			save = value;
		}
	}

	public byte PoSaveDly
	{
		get
		{
			return savedly;
		}
		set
		{
			savedly = value;
		}
	}

	public byte VoxSW
	{
		get
		{
			return voxsw;
		}
		set
		{
			voxsw = value;
		}
	}

	public byte VoxLv
	{
		get
		{
			return voxlv;
		}
		set
		{
			voxlv = value;
		}
	}

	public byte Voxdettime
	{
		get
		{
			return voxdettime;
		}
		set
		{
			voxdettime = value;
		}
	}

	public byte LoneWorkRsp
	{
		get
		{
			return loneworkrsp;
		}
		set
		{
			loneworkrsp = value;
		}
	}

	public byte LoneWorkTim
	{
		get
		{
			return loneworktime;
		}
		set
		{
			loneworktime = value;
		}
	}

	public byte APO
	{
		get
		{
			return apo;
		}
		set
		{
			apo = value;
		}
	}

	public byte TOT
	{
		get
		{
			return tot;
		}
		set
		{
			tot = value;
		}
	}

	public byte PreTot
	{
		get
		{
			return pretot;
		}
		set
		{
			pretot = value;
		}
	}

	public byte HzTo1750
	{
		get
		{
			return hz1750;
		}
		set
		{
			hz1750 = value;
		}
	}

	public byte AprsSw
	{
		get
		{
			return aprs;
		}
		set
		{
			aprs = value;
		}
	}

	public byte LoneWork
	{
		get
		{
			return lonework;
		}
		set
		{
			lonework = value;
		}
	}

	public byte DaoDi
	{
		get
		{
			return daodi;
		}
		set
		{
			daodi = value;
		}
	}

	public byte Voice
	{
		get
		{
			return voice;
		}
		set
		{
			voice = value;
		}
	}

	public byte BusyLock
	{
		get
		{
			return busylock;
		}
		set
		{
			busylock = value;
		}
	}

	public byte KeyLock
	{
		get
		{
			return keylock;
		}
		set
		{
			keylock = value;
		}
	}

	public byte AutoKey
	{
		get
		{
			return autokey;
		}
		set
		{
			autokey = value;
		}
	}

	public byte Tone
	{
		get
		{
			return tone;
		}
		set
		{
			tone = value;
		}
	}

	public byte EndTone
	{
		get
		{
			return endtone;
		}
		set
		{
			endtone = value;
		}
	}

	public byte LangSel
	{
		get
		{
			return langsel;
		}
		set
		{
			langsel = value;
		}
	}

	public byte TailFreq
	{
		get
		{
			return tailFreq;
		}
		set
		{
			tailFreq = value;
		}
	}

	public byte NoaaCh
	{
		get
		{
			return noaach;
		}
		set
		{
			noaach = value;
		}
	}

	public byte GpsID
	{
		get
		{
			return gpsid;
		}
		set
		{
			gpsid = value;
		}
	}

	public byte GpsSW
	{
		get
		{
			return gps;
		}
		set
		{
			gps = value;
		}
	}

	public byte GpsMode
	{
		get
		{
			return gpsmode;
		}
		set
		{
			gpsmode = value;
		}
	}

	public byte GpsReq
	{
		get
		{
			return gpsreq;
		}
		set
		{
			gpsreq = value;
		}
	}

	public byte GpsShare
	{
		get
		{
			return gpsshare;
		}
		set
		{
			gpsshare = value;
		}
	}

	public byte GpsZone
	{
		get
		{
			return gpszone;
		}
		set
		{
			gpszone = value;
		}
	}

	public byte BlueT
	{
		get
		{
			return bluet;
		}
		set
		{
			bluet = value;
		}
	}

	public byte BluetAPP
	{
		get
		{
			return bluetapp;
		}
		set
		{
			bluetapp = value;
		}
	}

	public byte BTpair
	{
		get
		{
			return bluetpair;
		}
		set
		{
			bluetpair = value;
		}
	}

	public byte BThold
	{
		get
		{
			return bluethold;
		}
		set
		{
			bluethold = value;
		}
	}

	public byte BTrxdly
	{
		get
		{
			return bluerxdly;
		}
		set
		{
			bluerxdly = value;
		}
	}

	public byte BTmic
	{
		get
		{
			return bluetmic;
		}
		set
		{
			bluetmic = value;
		}
	}

	public byte BTspk
	{
		get
		{
			return bluetspk;
		}
		set
		{
			bluetspk = value;
		}
	}

	public byte Reord
	{
		get
		{
			return record;
		}
		set
		{
			record = value;
		}
	}

	public byte RecordMode
	{
		get
		{
			return recordmode;
		}
		set
		{
			recordmode = value;
		}
	}

	public byte Engineering
	{
		get
		{
			return engineering;
		}
		set
		{
			engineering = value;
		}
	}

	public byte NOAA
	{
		get
		{
			return noaa;
		}
		set
		{
			noaa = value;
		}
	}

	public byte TianQi
	{
		get
		{
			return tianqi;
		}
		set
		{
			tianqi = value;
		}
	}

	public byte FmInter
	{
		get
		{
			return fminter;
		}
		set
		{
			fminter = value;
		}
	}

	public byte DispDir
	{
		get
		{
			return dispdir;
		}
		set
		{
			dispdir = value;
		}
	}

	public byte NoiseCancel
	{
		get
		{
			return noisecancel;
		}
		set
		{
			noisecancel = value;
		}
	}

	public byte EnhanceFunc
	{
		get
		{
			return enhancefunc;
		}
		set
		{
			enhancefunc = value;
		}
	}

	public byte Skey1
	{
		get
		{
			return skey1;
		}
		set
		{
			skey1 = value;
		}
	}

	public byte Lkey1
	{
		get
		{
			return lkey1;
		}
		set
		{
			lkey1 = value;
		}
	}

	public byte Skey2
	{
		get
		{
			return skey2;
		}
		set
		{
			skey2 = value;
		}
	}

	public byte Lkey2
	{
		get
		{
			return lkey2;
		}
		set
		{
			lkey2 = value;
		}
	}

	public byte Skey3
	{
		get
		{
			return skey3;
		}
		set
		{
			skey3 = value;
		}
	}

	public byte Lkey3
	{
		get
		{
			return lkey3;
		}
		set
		{
			lkey3 = value;
		}
	}

	public byte Skey4
	{
		get
		{
			return skey4;
		}
		set
		{
			skey4 = value;
		}
	}

	public byte Lkey4
	{
		get
		{
			return lkey4;
		}
		set
		{
			lkey4 = value;
		}
	}

	public string PowPassword
	{
		get
		{
			return powPassword;
		}
		set
		{
			powPassword = value;
		}
	}

	public string WrPassword
	{
		get
		{
			return wrpassword;
		}
		set
		{
			wrpassword = value;
		}
	}

	public string BTpassword
	{
		get
		{
			return btpassword;
		}
		set
		{
			btpassword = value;
		}
	}

	public string Radioname
	{
		get
		{
			return radioname;
		}
		set
		{
			radioname = value;
		}
	}

	public string PairName
	{
		get
		{
			return paitname;
		}
		set
		{
			paitname = value;
		}
	}

	public string BlueTName
	{
		get
		{
			return btname;
		}
		set
		{
			btname = value;
		}
	}
}
