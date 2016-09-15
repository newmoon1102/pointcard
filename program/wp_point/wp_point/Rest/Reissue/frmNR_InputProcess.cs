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
    public partial class frmNR_InputProcess : Form
    {
        private readonly frmMain _frm;
        private string NewCardNo;
        private string OldCardNo;


        #region コンストラクタ・デストラクタ
        public frmNR_InputProcess()
        {
            InitializeComponent();
        }
        public frmNR_InputProcess(frmMain fr, string newcardno, string oldcardno)
        {
            InitializeComponent();
            this._frm = fr;
            NewCardNo = newcardno;
            OldCardNo = oldcardno;
            
            this.label1.Text = this.label1.Text.Replace("newcardId", NewCardNo);
            this.label6.Text = this.label6.Text.Replace("newcardId", NewCardNo);
            this.label2.Text = this.label2.Text.Replace("oldcardId", OldCardNo);
        }

        #endregion

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._frm.ResetOpenWindow("frmNR_InputProcess2"))
                {
                    frmNR_InputProcess2 childForm = new frmNR_InputProcess2(_frm, NewCardNo, OldCardNo);
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
