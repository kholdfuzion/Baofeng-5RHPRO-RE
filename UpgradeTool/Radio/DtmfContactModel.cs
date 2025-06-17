using System;

namespace Radio;

public class DtmfContactModel : IData
{
    public string[] Contacts { get; set; }

    public DtmfContactModel()
    {
        Contacts = new string[16];
    }

    public byte[] DataToBytes()
    {
        int index = 0;
        byte[] data = new byte[256];
        string[] contacts = Contacts;
        foreach (string contact in contacts)
        {
            byte[] tmp = DtmfHelper.SaveDtmfCode(contact);
            Array.Copy(tmp, 0, data, index, Math.Min(16, tmp.Length));
            index += 16;
        }
        return data;
    }

    public void BytesToData(byte[] data)
    {
        int index = 0;
        byte[] tmp = new byte[16];
        for (int i = 0; i < 16; i++)
        {
            Array.Copy(data, index, tmp, 0, 16);
            Contacts[i] = DtmfHelper.DispDtmfCode(tmp);
            index += 16;
        }
    }

    public bool IndexIsValid(int index)
    {
        if (index < 0 || index >= 16)
        {
            return false;
        }
        return !string.IsNullOrEmpty(Contacts[index]);
    }
}
