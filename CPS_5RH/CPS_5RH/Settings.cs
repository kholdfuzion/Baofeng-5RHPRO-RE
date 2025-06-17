using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Windows.Forms;
using SharpConfig;

namespace CPS_5RH;

public class Settings
{
	private const string ConfigFile = "Setting.ini";

	public static string LangDef = "Chinese";

	public static string PortName { get; set; }

	public static int Baudrate { get; set; }

	public static byte FreqBand { get; set; }

	[DllImport("kernel32")]
	private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

	public static void Load()
	{
		try
		{
			string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Setting.ini");
			if (File.Exists(path))
			{
				SetSettings(path);
				return;
			}
			WritePrivateProfileString("Config", "com", "COM1", path);
			WritePrivateProfileString("Config", "lang", "Chinese", path);
			WritePrivateProfileString("Config", "freqband", "0", path);
			WritePrivateProfileString("Config", "baudrate", "19200", path);
			AddSecurityControll2File(path);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private static void AddSecurityControll2File(string filePath)
	{
		FileInfo fileInfo = new FileInfo(filePath);
		FileSecurity fileSecurity = fileInfo.GetAccessControl();
		fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
		fileSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
		fileInfo.SetAccessControl(fileSecurity);
	}

	public static void Save()
	{
		Configuration config = new Configuration();
		config["Config"]["com"].StringValue = PortName;
		config["Config"]["lang"].StringValue = LangDef;
		config["Config"]["freqband"].ByteValue = FreqBand;
		config["Config"]["baudrate"].IntValue = Baudrate;
		config.SaveToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Setting.ini"));
	}

	private static void SetSettings(string path)
	{
		try
		{
			Section section = Configuration.LoadFromFile(path)["Config"];
			PortName = section["com"].StringValue;
			LangDef = section["lang"].StringValue;
			FreqBand = section["freqband"].ByteValue;
			Baudrate = section["baudrate"].IntValue;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
}
