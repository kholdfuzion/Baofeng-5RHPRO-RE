namespace Radio;

internal class Lang
{
    public static string SZ_PROMPT = "Prompt";

    public static string SZ_READ_DATA = "Read data";

    public static string SZ_READ_COMPLETED = "Reading completed";

    public static string SZ_WRITE_DATA = "Write data";

    public static string SZ_WRITE_COMPLETED = "Write completed";

    public static string SZ_ERR_COMM = "Communication error";

    public static string SZ_ERR_OPEN_PORT = "Failed to open the serial port";

    public static string SZ_ERR_MODEL = "Model mismatch";

    public static string SZ_ERR_FREQ_RANGE = "Frequency band mismatch";

    public static string SZ_INIT_DATA = "Data Initialization";

    public static string SZ_INIT_FREQ = "Clear frequency";

    public static string SZ_SAVE_DONE = "Save successfully";

    public static string SZ_SOFTWARE_EXIT = "Whether to exit the software";

    public static string SZ_NONE = "None";

    public static string SZ_OFF = "Off";

    public static string SZ_ON = "On";

    public static string SZ_CLOSE = "Close";

    public static string SZ_GROUP = "Group";

    public static string[] SZ_POWER = new string[2] { "Lo", "Hi" };

    public static string[] SZ_BANDWIDTH = new string[2] { "12.5", "25" };

    public static string[] SZ_CH_TYPE = new string[3] { "Digital", "Analog", "Hybrid" };

    public static string[] SZ_TALKAROUND = new string[2] { SZ_OFF, SZ_ON };

    public static string[] SZ_SIGNAL_TYPE = new string[2] { SZ_OFF, "DTMF" };

    public static string[] SZ_PTTID_TYPE = new string[4] { SZ_OFF, "Online code", "Offline code", "Both" };

    public static string[] SZ_REVERSE = new string[2] { SZ_OFF, SZ_ON };

    public static string[] SZ_SCAN = new string[2] { "Allow", "Prohibit" };

    public static string[] SZ_SQL_MODE = new string[5] { "None", "CTDCS", "Optional Signaling", "CTDCS or Optional Signaling", "CTDCS and Optional Signaling" };

    public static string[] SZ_SKIP_FREQ = new string[2] { "Close", "Open" };

    public static string[] SZ_BEEP = new string[2] { SZ_OFF, SZ_ON };

    public static string[] SZ_VOICE_LANG = new string[3] { SZ_CLOSE, "Chinese", "English" };

    public static string[] SZ_BUSY_LOCK = new string[3] { SZ_OFF, "Repeater", "Busy" };

    public static string[] SZ_KEY_LOCK = new string[2] { SZ_OFF, SZ_ON };

    public static string[] SZ_SAVING = new string[5] { SZ_OFF, "1:1", "1:2", "1:4", "1:8" };

    public static string[] SZ_APO = new string[5] { SZ_OFF, "10M", "30M", "1H", "2H" };

    public static string[] SZ_VOX_SWITCH = new string[2] { SZ_OFF, SZ_ON };

    public static string[] SZ_SCAN_MODE = new string[3] { "Time", "Carrier", "Search" };

    public static string[] SZ_PRIORITY_SCAN = new string[2] { "Close", "Open" };

    public static string[] SZ_REVERT_CH = new string[6] { "Selected", "Select + Current Call", "Last Received Call Channel", "Last Used Channel", "Priority Channel", "Priority Channel + Current Call" };

    public static string[] SZ_RATE = new string[5] { "50", "100", "200", "300", "500" };

    public static string[] SZ_SIDE_TONE = new string[2] { SZ_OFF, SZ_ON };

    public static string[] SZ_BUTTON_KEY = new string[16]
    {
        "None", "Monitor key", "Scan key", "Voice control", "Power detection", "Power control", "One-key call", "Alarm", "Flashlight", "Inverted frequency",
        "Offnet", "Bandwidth", "1750Hz", "2100Hz", "1000Hz", "1450Hz"
    };
}
