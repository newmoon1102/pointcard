using log4net;
using System;
using _wp_point.Rest;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using _wp_point.Rest.Class;
using System.Runtime.InteropServices;

namespace _wp_point
{
    class Program
    {
        public static ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                String thisprocessname = Process.GetCurrentProcess().ProcessName;

                if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
                {
                    MessageBox.Show("アプリがすでに実行されています", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
            catch(Exception ex)
            {
                _logger.Fatal("Application Finish with Error", ex);
            }
        }
    }
}
