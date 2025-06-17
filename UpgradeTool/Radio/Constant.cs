namespace Radio;

internal class Constant
{
    public const string SZ_SUPER_PWD = "ZWSG18";

    public const string SZ_DTMF_CODE = "0123456789ABCD*#";

    public const int EEROM_SPACE = 0x80000;

    public const int SCL_FREQ = 100000;

    public const int LEN_FREQ = 9;

    public const int FREQ_STEP1 = 500;

    public const int FREQ_STEP2 = 625;

    public const int SCL_TONE = 10;

    public const int CNT_CHANNEL = 100;

    public const int CNT_CHANNEL_ITEMS = 8;

    public const int LEN_CH_NAME = 0;

    public const int SPACE_PER_CH = 16;

    public const int CNT_DTMF_CONTACT = 16;

    public const int SPACE_PER_CONTACT = 16;

    public const int SPACE_ALL_CONTACT = 256;

    public const int LEN_DTMF_CODE = 16;

    public const int LEN_UP_CODE = 16;

    public const int LEN_DOWN_CODE = 16;

    public const int LEN_SUTN_CODE = 16;

    public const int LEN_KILL_CODE = 16;

    public const int CNT_SKIP_FREQ_GROUP = 16;

    public const int CNT_SKIP_FREQ_PER_GROUP = 41;

    public static readonly string[] SZ_FREQ_RANGE = new string[6] { "136-174MHz", "180-240MHz", "220-280MHz", "350-399MHz", "400-480MHz", "100-999MHz" };
}
