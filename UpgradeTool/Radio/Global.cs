using System.Collections;
using System.Windows.Forms;

namespace Radio;

internal class Global
{
    public static IniHelper SetupIniHelper = new IniHelper(Application.StartupPath + "\\Setup.ini");

    public static Hashtable SkipFreqHash = new Hashtable();

    public static string CurLang { get; set; }

    public static byte[] EEROM { get; set; }

    public static string[] SZ_FREQ_RANGE { get; set; }
}
