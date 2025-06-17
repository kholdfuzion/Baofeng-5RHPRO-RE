namespace Radio;

internal interface IData
{
	byte[] DataToBytes();

	void BytesToData(byte[] data);
}
