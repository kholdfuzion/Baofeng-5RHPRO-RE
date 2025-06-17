namespace Radio;

internal class Lang
{
	public static string SZ_PROMPT = "Prompt";

	public static string SZ_READ_DATA = "Reading Data";

	public static string SZ_READ_COMPLETED = "Read Completed";

	public static string SZ_WRITE_DATA = "Writing Data";

	public static string SZ_WRITE_COMPLETED = "Write Completed";

	public static string SZ_ERR_COMM = "Comm Error";

	public static string SZ_ERR_OPEN_PORT = "Port Open Error";

	public static string SZ_ERR_MODEL = "Wrong Radio Model";

	public static string SZ_ERR_FREQ_RANGE = "Bad Frequency Range";

	public static string SZ_INIT_DATA = "Initialize Data";

	public static string SZ_INIT_FREQ = "Initialize Frequency";

	public static string SZ_SAVE_DONE = "Save Completed";

	public static string SZ_SOFTWARE_EXIT = "OK to Exit?";

	public static string SZ_NONE = "无";

	public static string SZ_OFF = "关";

	public static string SZ_ON = "开";

	public static string SZ_CLOSE = "关闭";

	public static string SZ_GROUP = "组";

	public static string[] SZ_POWER = new string[2] { "低", "高" };

	public static string[] SZ_BANDWIDTH = new string[2] { "12.5", "25" };

	public static string[] SZ_CH_TYPE = new string[3] { "数字", "模拟", "混合" };

	public static string[] SZ_TALKAROUND = new string[2] { SZ_OFF, SZ_ON };

	public static string[] SZ_SIGNAL_TYPE = new string[2] { SZ_OFF, "DTMF" };

	public static string[] SZ_PTTID_TYPE = new string[4] { SZ_OFF, "上线码", "下线码", "两者" };

	public static string[] SZ_REVERSE = new string[2] { SZ_OFF, SZ_ON };

	public static string[] SZ_SCAN = new string[2] { "允许", "禁止" };

	public static string[] SZ_SQL_MODE = new string[5] { "无", "CTDCS", "可选信令", "CTDCS or 可选信令", "CTDCS and 可选信令" };

	public static string[] SZ_SKIP_FREQ = new string[2] { "关", "开" };

	public static string[] SZ_BEEP = new string[2] { SZ_OFF, SZ_ON };

	public static string[] SZ_VOICE_LANG = new string[3] { SZ_CLOSE, "中文播报", "英文播报" };

	public static string[] SZ_BUSY_LOCK = new string[3] { SZ_OFF, "中继器", "繁忙" };

	public static string[] SZ_KEY_LOCK = new string[2] { SZ_OFF, SZ_ON };

	public static string[] SZ_SAVING = new string[5] { SZ_OFF, "1:1", "1:2", "1:4", "1:8" };

	public static string[] SZ_APO = new string[5] { SZ_OFF, "10M", "30M", "1H", "2H" };

	public static string[] SZ_VOX_SWITCH = new string[2] { SZ_OFF, SZ_ON };

	public static string[] SZ_SCAN_MODE = new string[3] { "时间", "载波", "搜索" };

	public static string[] SZ_PRIORITY_SCAN = new string[2] { "关", "开" };

	public static string[] SZ_REVERT_CH = new string[6] { "选定", "选定+当前通话", "最后接收通话信道", "最后使用信道", "优先信道", "优先信道+当前通话" };

	public static string[] SZ_RATE = new string[5] { "50", "100", "200", "300", "500" };

	public static string[] SZ_SIDE_TONE = new string[2] { SZ_OFF, SZ_ON };

	public static string[] SZ_BUTTON_KEY = new string[16]
	{
		"无", "监听键", "扫描键", "声控控制", "电量检测", "功率控制", "一键呼叫", "报警", "手电筒", "倒频",
		"脱网", "带宽", "1750Hz", "2100Hz", "1000Hz", "1450Hz"
	};
}
