using System;

namespace Radio;

public class TotModel : IData, IVerify, ICloneable
{
    private const int MIN_TOT = 0;

    private const int MAX_TOT = 600;

    private const int STEP_TOT = 30;

    private const int MIN_TOT_PRE_ALERT = 0;

    private const int MAX_TOT_PRE_ALERT = 60;

    private const int STEP_TOT_PRE_ALERT = 5;

    private const int MIN_TOT_REKEY = 10;

    private const int MAX_TOT_REKEY = 60;

    private const int STEP_TOT_REKEY = 5;

    public string Tot { get; set; }

    public string TotPreAlert { get; set; }

    public string TotRekey { get; set; }

    public byte[] DataToBytes()
    {
        Verify();
        byte[] data = new byte[4];
        int tot = 0;
        if (Tot == Lang.SZ_OFF || string.IsNullOrEmpty(Tot))
        {
            tot = 0;
        }
        else
        {
            int.TryParse(Tot, out tot);
        }
        data[0] = (byte)(tot >> 8);
        data[1] = (byte)tot;
        if (TotPreAlert == Lang.SZ_OFF || string.IsNullOrEmpty(TotPreAlert))
        {
            data[2] = 0;
        }
        else if (!byte.TryParse(TotPreAlert, out data[2]))
        {
            data[2] = 0;
        }
        if (string.IsNullOrEmpty(TotRekey))
        {
            data[3] = 0;
        }
        else
        {
            byte.TryParse(TotRekey, out data[3]);
        }
        return data;
    }

    public void BytesToData(byte[] data)
    {
        try
        {
            int tot = data[0] * 256 + data[1];
            if (tot == 0)
            {
                Tot = Lang.SZ_OFF;
            }
            else
            {
                Tot = tot.ToString();
            }
            if (data[2] == 0)
            {
                TotPreAlert = Lang.SZ_OFF;
            }
            else
            {
                TotPreAlert = data[2].ToString();
            }
            TotRekey = data[3].ToString();
            Verify();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Verify()
    {
        TotModel model = Cps.GetInstance().TotModelDefault;
        int tot = 0;
        if (Tot != Lang.SZ_OFF)
        {
            if (int.TryParse(Tot, out tot))
            {
                if (tot < 0 || tot > 600 || tot % 30 != 0)
                {
                    Tot = model.Tot;
                }
            }
            else
            {
                Tot = model.Tot;
            }
        }
        int totPreAlert = 0;
        if (TotPreAlert != Lang.SZ_OFF)
        {
            if (int.TryParse(TotPreAlert, out totPreAlert))
            {
                if (totPreAlert < 0 || totPreAlert > 60 || totPreAlert % 5 != 0)
                {
                    TotPreAlert = model.TotPreAlert;
                }
            }
            else
            {
                TotPreAlert = model.TotPreAlert;
            }
        }
        int totRekey = 0;
        if (int.TryParse(TotRekey, out totRekey))
        {
            if (totRekey < 10 || totRekey > 60 || totRekey % 5 != 0)
            {
                TotRekey = model.TotRekey;
            }
        }
        else
        {
            TotRekey = model.TotRekey;
        }
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
