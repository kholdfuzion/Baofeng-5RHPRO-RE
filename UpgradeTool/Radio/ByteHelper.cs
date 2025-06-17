namespace Radio;

internal static class ByteHelper
{
    public static void Reset<T>(this T[] data, T value) where T : struct
    {
        int i = 0;
        for (i = 0; i < data.Length; i++)
        {
            data[i] = value;
        }
    }
}
