using System;
using System.Windows.Forms;

namespace CPS_5RH;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		if (Environment.OSVersion.Version.Major != 5)
		{
			Application.EnableVisualStyles();
		}
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Settings.Load();
		Application.Run(new FormMain());
	}
}
