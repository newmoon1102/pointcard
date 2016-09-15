using log4net;
using System;
using System.IO;
using System.Json;
using System.Text;
using System.Windows.Forms;
using _wp_point.Rest.Class;
using System.Configuration;

namespace _wp_point.Rest
{
    public partial class frmLogin : Form
    {
        public static string id;
        public static string pass;
        public readonly frmMain _frm;
        private static readonly ILog logger = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public frmLogin(frmMain frmain)
        {
            InitializeComponent();
            this._frm = frmain;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserId.Text == "" || txtPassWord.Text == "")
            {
                MsgBox.Show("店舗IDとパスワードを入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                id = txtUserId.Text;
                pass = txtPassWord.Text;
                //close vitual keyboard
                //Common.closeKeyboard();
                try
                {
                    if (RunClient(logger))
                    {
                        if (this._frm.ResetOpenWindow("frmSelect"))
                        {
                            frmSelect childForm = new frmSelect(_frm);
                            childForm.MdiParent = _frm;
                            childForm.WindowState = FormWindowState.Maximized;
                            childForm.Show();
                        }
                    }
                    else
                    {
                        MsgBox.Show("店舗IDまたはパスワードが違います。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    logger.Fatal("Application Finish with Error", ex);
                }

            }
        }

        private bool RunClient(ILog logger)
        {
            try
            {
                bool flg = false;
                ApiClient client = new ApiClient(logger);
                JsonValue jsonRes = client.PostAuthStatus(id, pass);
                if ((string)jsonRes["code"] == "200")
                {
                    shopAuthCode.shop_auth_code = (string)jsonRes["shop_auth_code"];                 
                    logger.Info("店舗認証に成功しました。");
                    flg = true;
                }
                else
                {
                    logger.ErrorFormat("API Error code: {0} with note: {1}", jsonRes["code"], jsonRes["note"]);
                    flg = false;
                }
                return flg;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Run Virtual Keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _MouseClick(object sender, MouseEventArgs e)
        {
            //Common.startKeyboard();
        }

        /// <summary>
        /// Next control when Enter key import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.SelectNextControl((Control)sender, true, true, true, true);
        }
        /// <summary>
        /// Login when Enter key import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin.PerformClick();
        }
    }
}
