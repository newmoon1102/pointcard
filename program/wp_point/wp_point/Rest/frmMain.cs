using _wp_point.Rest.Class;
using AForge.Video;
using AForge.Video.DirectShow;
using log4net;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace _wp_point.Rest
{
    public partial class frmMain : Form
    {
        private static readonly ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            _logger.Info("アプリケーション - 開始");
            frmLogin childForm = new frmLogin(this);
            childForm.MdiParent = this;
            childForm.BringToFront();
            childForm.WindowState = FormWindowState.Maximized;

            childForm.Show();
        }

        internal bool ResetOpenWindow(string Id)
        {
            bool ResetOpenWindow = false;

            try
            {
                if (this.ActiveMdiChild != null)
                {
                    foreach (Form frm in this.MdiChildren)
                    {
                        if (!this.ActiveMdiChild.Name.Equals(Id))
                        {
                            frm.Close();
                            frm.Dispose();
                            ResetOpenWindow = true;
                        }
                    }
                }
            }
            catch
            {
                ResetOpenWindow = false;
            }
            return ResetOpenWindow;
        }

        public  Form GetFormById(string frmName)
        {
            Form form = null;

            try
            {
                if (this.ActiveMdiChild != null)
                {
                    foreach (Form frm in Application.OpenForms)
                    {
                        if (frm.Name.Equals(frmName))
                        {
                            form = frm;
                        }
                    }
                }
            }
            catch
            {
                form = null;
            }
            return form;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _logger.Info("アプリケーション - 完了");
            String thisprocessname = Process.GetCurrentProcess().ProcessName;
            Process[] runingProcess = Process.GetProcesses();
            for (int i = 0; i < runingProcess.Length; i++)
            {
                // compare equivalent process by their name
                if (runingProcess[i].ProcessName == "pointcard" || runingProcess[i].ProcessName == thisprocessname)
                {
                    // kill  running process
                    runingProcess[i].Kill();
                }

            }
        }
    }
}
