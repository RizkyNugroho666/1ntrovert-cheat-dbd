using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Introvert_Launcher
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(WIN32_EXCEPTION);
            Application.Run(new Form1());
        }

        static void WIN32_EXCEPTION(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "1ntrovert Launcher Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            WIN32_ADVANCEDEXCEPTION(e);
        }

        [Conditional("DEBUG")]
        static void WIN32_ADVANCEDEXCEPTION(ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "1ntrovert Launcher Debug Error Information", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
    }
}
