using _wp_point.Rest.Class;
using _wp_point.Rest.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _wp_point.Rest.Reissue
{
    public partial class frmNR_InputProcess2 : Form
    {
        private readonly frmMain _frm;
        private string NewCardNo;
        private string OldCardNo;


        #region コンストラクタ・デストラクタ
        public frmNR_InputProcess2()
        {
            InitializeComponent();
        }
        public frmNR_InputProcess2(frmMain fr, string newcardno, string oldcardno)
        {
            InitializeComponent();
            this._frm = fr;
            NewCardNo = newcardno;
            OldCardNo = oldcardno;
            
            this.label1.Text = this.label1.Text.Replace("newcardId", NewCardNo);
            this.label6.Text = this.label6.Text.Replace("newcardId", NewCardNo);
            this.label4.Text = this.label4.Text.Replace("oldcardId", OldCardNo);
        }

        #endregion

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._frm.ResetOpenWindow("frmNR_InputProcess"))
                {
                    frmNR_InputProcess childForm = new frmNR_InputProcess(_frm, NewCardNo, OldCardNo);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._frm.ResetOpenWindow("frmSuccess"))
                {
                    frmSuccess childForm = new frmSuccess(_frm, 3);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
