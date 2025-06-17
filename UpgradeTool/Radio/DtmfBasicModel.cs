using System;

namespace Radio;

public class DtmfBasicModel : IData
{
    public int Rate { get; set; }

    public string FirstDigit { get; set; }

    public string PreCarrier { get; set; }

    public string TxDelay { get; set; }

    public string PttidPause { get; set; }

    public int SideTone { get; set; }

    public string UpCode { get; set; }

    public string DownCode { get; set; }

    public string StunCode { get; set; }

    public string KillCode { get; set; }

    public byte[] DataToBytes()
    {
        int index = 0;
        byte[] tmp = null;
        byte[] data = new byte[88];
        Array.Copy(Global.EEROM, 1952, data, 0, data.Length);
        data[1] = (byte)Rate;
        double value = 0.0;
        double.TryParse(FirstDigit, out value);
        data[2] = (byte)(value / 10.0);
        double.TryParse(PreCarrier, out value);
        data[3] = (byte)(value / 10.0);
        double.TryParse(TxDelay, out value);
        data[4] = (byte)(value / 10.0);
        if (PttidPause == Lang.SZ_OFF)
        {
            data[5] = 0;
        }
        else
        {
            byte.TryParse(PttidPause, out data[5]);
        }
        data[6] = (byte)SideTone;
        index = 24;
        tmp = DtmfHelper.SaveDtmfCode(UpCode);
        Array.Copy(tmp, 0, data, index, tmp.Length);
        index += tmp.Length;
        tmp = DtmfHelper.SaveDtmfCode(DownCode);
        Array.Copy(tmp, 0, data, index, tmp.Length);
        index += tmp.Length;
        tmp = DtmfHelper.SaveDtmfCode(StunCode);
        Array.Copy(tmp, 0, data, index, tmp.Length);
        index += tmp.Length;
        tmp = DtmfHelper.SaveDtmfCode(KillCode);
        Array.Copy(tmp, 0, data, index, tmp.Length);
        index += tmp.Length;
        return data;
    }

    public void BytesToData(byte[] data)
    {
        int index = 0;
        byte[] tmp = new byte[16];
        Rate = data[1];
        FirstDigit = (data[2] * 10).ToString();
        PreCarrier = (data[3] * 10).ToString();
        TxDelay = (data[4] * 10).ToString();
        if (data[5] == 0)
        {
            PttidPause = Lang.SZ_OFF;
        }
        else
        {
            PttidPause = data[5].ToString();
        }
        SideTone = data[6];
        index = 24;
        Array.Copy(data, index, tmp, 0, 16);
        UpCode = DtmfHelper.DispDtmfCode(tmp);
        index += 16;
        Array.Copy(data, index, tmp, 0, 16);
        DownCode = DtmfHelper.DispDtmfCode(tmp);
        index += 16;
        Array.Copy(data, index, tmp, 0, 16);
        StunCode = DtmfHelper.DispDtmfCode(tmp);
        index += 16;
        Array.Copy(data, index, tmp, 0, 16);
        KillCode = DtmfHelper.DispDtmfCode(tmp);
        index += 16;
    }
}
