namespace Radio;

public class DtmfContactIndexModel : IData
{
	public byte[] DataToBytes()
	{
		int index = 0;
		byte[] data = new byte[8];
		string[] contacts = Cps.GetInstance().DtmfContactModel.Contacts;
		for (int i = 0; i < contacts.Length; i++)
		{
			if (string.IsNullOrEmpty(contacts[i]))
			{
				data[index / 8] = data[index / 8].SetBit(index % 8, 1, 1);
			}
			else
			{
				data[index / 8] = data[index / 8].SetBit(index % 8, 1, 0);
			}
			index++;
		}
		return data;
	}

	public void BytesToData(byte[] data)
	{
	}
}
