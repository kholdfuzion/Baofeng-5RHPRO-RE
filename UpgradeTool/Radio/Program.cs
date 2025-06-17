using System;
using System.Threading;
using System.Windows.Forms;

namespace Radio;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs e)
        {
            FatalExceptionObject(e.ExceptionObject);
        };
        Application.ThreadException += delegate (object sender, ThreadExceptionEventArgs e)
        {
            FatalExceptionObject(e.Exception);
        };
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(defaultValue: false);
        Application.Run(new MainForm());
    }

    private static void FatalExceptionObject(object e)
    {
        Exception ex = e as Exception;
        MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        if (ex.InnerException != null)
        {
            MessageBox.Show(ex.InnerException.Message + "\r\n" + ex.InnerException.StackTrace, "System Information", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}
