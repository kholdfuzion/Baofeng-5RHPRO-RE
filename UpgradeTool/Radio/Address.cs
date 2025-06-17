namespace Radio;

internal class Address
{
    public const int ADDR_CHANNEL = 0x80;              // 128
    public const int SPACE_CHANNEL = 0x640;            // 1600
    public const int ADDR_CHANNEL_END = 0x6C0;         // 1728

    public const int ADDR_MODEL = 0x700;               // 1792
    public const int SPACE_MODEL = 0x8;                // 8
    public const int ADDR_MODEL_END = 0x708;           // 1800

    public const int ADDR_BASIC = 0x708;               // 1800
    public const int SPACE_BASIC = 0x10;               // 16
    public const int ADDR_BASIC_END = 0x718;           // 1816

    public const int ADDR_TOT = 0x720;                 // 1824
    public const int SPACE_TOT = 0x8;                  // 8
    public const int ADDR_TOT_END = 0x728;             // 1832

    public const int ADDR_BUTTON = 0x730;              // 1840
    public const int SPACE_BUTTON = 0x8;               // 8
    public const int ADDR_BUTTON_END = 0x738;          // 1848

    public const int ADDR_CHANNEL_INDEX = 0x740;       // 1856
    public const int SPACE_CHANNEL_INDEX = 0x10;       // 16
    public const int ADDR_CHANNEL_INDEX_END = 0x750;   // 1872

    public const int ADDR_SCAN_BASIC = 0x760;          // 1888
    public const int SPACE_SCAN_BASIC = 0x8;           // 8
    public const int ADDR_SCAN_BASIC_END = 0x768;      // 1896

    public const int ADDR_SCAN_INDEX = 0x780;          // 1920
    public const int SPACE_SCAIN_INDEX = 0x10;         // 16
    public const int ADDR_SCAN_INDEX_END = 0x790;      // 1936

    public const int ADDR_DTMF_BASIC = 0x7A0;          // 1952
    public const int SPACE_DTMF_BASIC = 0x58;          // 88
    public const int ADDR_DTMF_BASIC_END = 0x7F8;      // 2040

    public const int ADDR_DTMF_CONTACT_INDEX = 0x830;  // 2096
    public const int SPACE_DTMF_CONTACT_IDEX = 0x8;    // 8
    public const int ADDR_DTMF_CONTACT_INDEX_END = 0x838; // 2104

    public const int ADDR_DTMF_CONTACT = 0x840;        // 2112
    public const int SPACE_DTMF_CONTACT = 0x100;       // 256
    public const int ADDR_DTMF_CONTACT_END = 0x940;    // 2368

    public const int ADDR_SKIP_FREQUENCY = 0x950;      // 2384
    public const int SPACE_SKIP_FREQUENCY = 0xB00;     // 2816
    public const int ADDR_SKIP_FREQUENCY_END = 0x1450; // 5200
}
