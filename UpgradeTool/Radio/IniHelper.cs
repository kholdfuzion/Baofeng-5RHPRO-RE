using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Radio;

internal class IniHelper
{
    private string iniPath = Application.StartupPath + "\\Setup.ini";

    public string IniPath
    {
        get
        {
            return iniPath;
        }
        set
        {
            iniPath = value;
        }
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    [DllImport("kernel32.DLL ", CharSet = CharSet.Ansi)]
    private static extern int GetPrivateProfileInt(string section, string key, int def, string filePath);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
    public static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, int nSize, string filePath);

    [DllImport("kernel32.DLL ", CharSet = CharSet.Ansi)]
    private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string filePath);

    public IniHelper(string path)
    {
        IniPath = path;
    }

    public int ReadInt(string section, string key, int def)
    {
        return GetPrivateProfileInt(section, key, def, IniPath);
    }

    public void WriteInt(string section, string key, int val)
    {
        WritePrivateProfileString(section, key, val.ToString(), IniPath);
    }

    public string ReadString(string section, string key, string def)
    {
        StringBuilder temp = new StringBuilder(1024);
        GetPrivateProfileString(section, key, def, temp, 1024, IniPath);
        return temp.ToString();
    }

    public string ReadString(string section, string key, string def, int size)
    {
        StringBuilder temp = new StringBuilder();
        GetPrivateProfileString(section, key, def, temp, size, IniPath);
        return temp.ToString();
    }

    public void WriteString(string section, string key, string val)
    {
        WritePrivateProfileString(section, key, val, IniPath);
    }

    public void DelKey(string section, string key)
    {
        WritePrivateProfileString(section, key, null, IniPath);
    }

    public void DelSection(string section)
    {
        WritePrivateProfileString(section, null, null, IniPath);
    }

    public int GetAllSectionNames(out string[] sections)
    {
        int MAX_BUFFER = 32767;
        IntPtr pReturnedString = Marshal.AllocCoTaskMem(MAX_BUFFER);
        int bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, IniPath);
        if (bytesReturned == 0)
        {
            sections = null;
            return -1;
        }
        string local = Marshal.PtrToStringAnsi(pReturnedString, bytesReturned).ToString();
        Marshal.FreeCoTaskMem(pReturnedString);
        string text = local.Substring(0, local.Length - 1);
        char[] separator = new char[1];
        sections = text.Split(separator);
        return 0;
    }

    public int GetAllKeyValues(string section, out string[] keys, out string[] values)
    {
        byte[] b = new byte[65535];
        GetPrivateProfileSection(section, b, b.Length, IniPath);
        string s = Encoding.Default.GetString(b);
        char[] separator = new char[1];
        string[] tmp = s.Split(separator);
        ArrayList result = new ArrayList();
        string[] array = tmp;
        foreach (string r in array)
        {
            if (r != string.Empty)
            {
                result.Add(r);
            }
        }
        keys = new string[result.Count];
        values = new string[result.Count];
        for (int j = 0; j < result.Count; j++)
        {
            string[] item = result[j].ToString().Split('=');
            if (item.Length == 2)
            {
                keys[j] = item[0].Trim();
                values[j] = item[1].Trim();
            }
            else if (item.Length == 1)
            {
                keys[j] = item[0].Trim();
                values[j] = "";
            }
            else if (item.Length == 0)
            {
                keys[j] = "";
                values[j] = "";
            }
        }
        return 0;
    }
}
