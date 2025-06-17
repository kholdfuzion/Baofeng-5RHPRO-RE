using System;
using System.Text.RegularExpressions;

namespace CPS_5RH.Data;

public class DatDeviceInfo
{
	private byte freqRpoint = 4;

	private byte freqTpoint = 4;

	private byte stand = 0;

	private byte scramble = 0;

	private byte dis200m = 0;

	private byte dis350m = 0;

	private byte dis500m = 0;

	private byte endtmf = 0;

	private byte en2tone = 0;

	private byte en5tone = 0;

	private byte enmdc1200 = 0;

	private byte enbdc1200 = 0;

	private byte engps = 0;

	private byte enaprs = 0;

	private byte enbt = 0;

	private byte ennoaa = 0;

	private byte enfalldn = 0;

	private byte ennoise = 0;

	private byte enflashlight = 0;

	private byte enzone = 0;

	private byte pkeyCnt = 1;

	private string devname = "";

	private string softVer = "";

	private string hardVer = "";

	private string pdate = "";

	private int[] rxFreqs = new int[4] { 13600000, 22000000, 35000000, 40000000 };

	private int[] rxFreqe = new int[4] { 17400000, 26000000, 39500000, 52000000 };

	private int[] txFreqs = new int[4] { 13600000, 22000000, 35000000, 40000000 };

	private int[] txFreqe = new int[4] { 17400000, 26000000, 39500000, 52000000 };

	private byte[] pkey = new byte[16];

	public byte FreqRxPt
	{
		get
		{
			return freqRpoint;
		}
		set
		{
			freqRpoint = value;
		}
	}

	public byte FreqTxPt
	{
		get
		{
			return freqTpoint;
		}
		set
		{
			freqTpoint = value;
		}
	}

	public byte En2Tone
	{
		get
		{
			return en2tone;
		}
		set
		{
			en2tone = value;
		}
	}

	public byte En5Tone
	{
		get
		{
			return en5tone;
		}
		set
		{
			en5tone = value;
		}
	}

	public byte EnDTMF
	{
		get
		{
			return endtmf;
		}
		set
		{
			endtmf = value;
		}
	}

	public byte EnMdc1200
	{
		get
		{
			return enmdc1200;
		}
		set
		{
			enmdc1200 = value;
		}
	}

	public byte EnBdc1200
	{
		get
		{
			return enbdc1200;
		}
		set
		{
			enbdc1200 = value;
		}
	}

	public byte EnGps
	{
		get
		{
			return engps;
		}
		set
		{
			engps = value;
		}
	}

	public byte EnZone
	{
		get
		{
			return enzone;
		}
		set
		{
			enzone = value;
		}
	}

	public byte EnBT
	{
		get
		{
			return enbt;
		}
		set
		{
			enbt = value;
		}
	}

	public byte EnFlight
	{
		get
		{
			return enflashlight;
		}
		set
		{
			enflashlight = value;
		}
	}

	public byte EnNoise
	{
		get
		{
			return ennoise;
		}
		set
		{
			ennoise = value;
		}
	}

	public byte EnFalldn
	{
		get
		{
			return enfalldn;
		}
		set
		{
			enfalldn = value;
		}
	}

	public byte Stand
	{
		get
		{
			return stand;
		}
		set
		{
			stand = value;
		}
	}

	public byte Scramble
	{
		get
		{
			return scramble;
		}
		set
		{
			scramble = value;
		}
	}

	public byte Dis200m
	{
		get
		{
			return dis200m;
		}
		set
		{
			dis200m = value;
		}
	}

	public byte Dis350m
	{
		get
		{
			return dis350m;
		}
		set
		{
			dis350m = value;
		}
	}

	public byte Dis500m
	{
		get
		{
			return dis500m;
		}
		set
		{
			dis500m = value;
		}
	}

	public byte PkeyCnt
	{
		get
		{
			return pkeyCnt;
		}
		set
		{
			pkeyCnt = value;
		}
	}

	public string DevName
	{
		get
		{
			return devname;
		}
		set
		{
			devname = value;
		}
	}

	public string SoftVer
	{
		get
		{
			return softVer;
		}
		set
		{
			softVer = value;
		}
	}

	public string HardVer
	{
		get
		{
			return hardVer;
		}
		set
		{
			hardVer = value;
		}
	}

	public string Pdate
	{
		get
		{
			return pdate;
		}
		set
		{
			pdate = value;
		}
	}

	public void SetProKey(int Idx, int val)
	{
		pkey[Idx] = (byte)val;
	}

	public int GetProKey(int Idx)
	{
		return pkey[Idx];
	}

	public void SetRxStartFreq(int Idx, int val)
	{
		rxFreqs[Idx] = val;
	}

	public int GetRxStartFreq(int Idx)
	{
		return rxFreqs[Idx];
	}

	public void SetRxEndFreq(int Idx, int val)
	{
		rxFreqe[Idx] = val;
	}

	public int GetRxEndFreq(int Idx)
	{
		return rxFreqe[Idx];
	}

	public void SetTxStartFreq(int Idx, int val)
	{
		txFreqs[Idx] = val;
	}

	public int GetTxStartFreq(int Idx)
	{
		return txFreqs[Idx];
	}

	public void SetTxEndFreq(int Idx, int val)
	{
		txFreqe[Idx] = val;
	}

	public int GetTxEndFreq(int Idx)
	{
		return txFreqe[Idx];
	}

	public double Adjust_Freq(string val)
	{
		int Freq2P5 = 250;
		int Freq6P25 = 625;
		bool matchFlg = false;
		string result = "";
		try
		{
			result = Regex.Replace(val, "[^0-9]+", "");
			if (result.Length > 8)
			{
				result = result.Remove(8);
			}
			for (int i = result.Length; i < 8; i++)
			{
				result += "0";
			}
		}
		catch
		{
			result = GetRxStartFreq(0).ToString();
		}
		int intFreq = Convert.ToInt32(result);
		if (intFreq % Freq2P5 != 0 && intFreq % Freq6P25 != 0)
		{
			intFreq = intFreq / Freq2P5 * Freq2P5;
		}
		for (int i = 0; i < 4; i++)
		{
			int intFreqS = GetRxStartFreq(i);
			int intFreqE = GetRxEndFreq(i);
			if (intFreqS > 100000 && intFreqE > intFreqS && intFreq >= intFreqS && intFreq <= intFreqE)
			{
				matchFlg = true;
				break;
			}
		}
		if (!matchFlg)
		{
			if (intFreq < GetTxStartFreq(0))
			{
				intFreq = GetTxStartFreq(0);
			}
			if (intFreq > GetTxEndFreq(0))
			{
				intFreq = GetTxEndFreq(0);
			}
		}
		return Convert.ToDouble(intFreq) / 100000.0;
	}

	public double Adjust_TxFreq(string val, string matchFreq)
	{
		int Freq2P5 = 250;
		int Freq6P25 = 625;
		int Tfreq = 0;
		string result = "";
		try
		{
			int mFreq = Convert.ToInt32(Regex.Replace(matchFreq, "[^0-9]+", ""));
			Tfreq = ((mFreq > GetTxEndFreq(0)) ? ((mFreq <= GetTxEndFreq(1)) ? 1 : ((mFreq > GetTxEndFreq(2)) ? 3 : 2)) : 0);
			result = Regex.Replace(val, "[^0-9]+", "");
			if (result.Length > 8)
			{
				result = result.Remove(8);
			}
			for (int i = result.Length; i < 8; i++)
			{
				result += "0";
			}
		}
		catch
		{
			result = matchFreq;
		}
		int intFreq = Convert.ToInt32(result);
		if (intFreq % Freq2P5 != 0 && intFreq % Freq6P25 != 0)
		{
			intFreq = intFreq / Freq2P5 * Freq2P5;
		}
		switch (Tfreq)
		{
		case 0:
			if (intFreq < GetTxStartFreq(0) || intFreq > GetTxEndFreq(0))
			{
				intFreq = GetTxStartFreq(0);
			}
			break;
		case 1:
			if (intFreq < GetTxStartFreq(1) || intFreq > GetTxEndFreq(1))
			{
				intFreq = GetTxStartFreq(1);
			}
			break;
		default:
			if (FreqTxPt == 2)
			{
				if (intFreq < GetTxStartFreq(2) || intFreq > GetTxEndFreq(2))
				{
					intFreq = GetTxStartFreq(2);
				}
				break;
			}
			if (intFreq < GetTxStartFreq(3))
			{
				intFreq = GetTxStartFreq(3);
			}
			if (intFreq > GetTxEndFreq(3))
			{
				intFreq = GetTxEndFreq(3);
			}
			break;
		}
		return Convert.ToDouble(intFreq) / 100000.0;
	}
}
