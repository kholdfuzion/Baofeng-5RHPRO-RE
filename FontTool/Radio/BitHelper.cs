namespace Radio;

internal static class BitHelper
{
	public static byte ClearBit(this byte byt, int index)
	{
		return byt &= (byte)(~(byte)(1 << index));
	}

	public static byte ClearBit(this byte byt, int index, int length)
	{
		return byt &= (byte)(~GetMask(index, length));
	}

	public static int GetMask(int length)
	{
		return (1 << length) - 1;
	}

	public static int GetMask(int index, int length)
	{
		return GetMask(length) << index;
	}

	public static byte GetBit(this byte byt, int index)
	{
		return (byte)((byt >> index) & 1);
	}

	public static byte GetBit(this byte byt, int index, int length)
	{
		return (byte)((byt >> index) & GetMask(length));
	}

	public static byte SetBit(this byte byt, int index, int length, int value)
	{
		int mask = GetMask(length);
		byt = byt.ClearBit(index, length);
		return byt |= (byte)((value & mask) << index);
	}

	public static int GetBit(this int data, int index)
	{
		return data.GetBit(index, 1);
	}

	public static int GetBit(this int data, int index, int length)
	{
		return (data >> index) & GetMask(length);
	}
}
