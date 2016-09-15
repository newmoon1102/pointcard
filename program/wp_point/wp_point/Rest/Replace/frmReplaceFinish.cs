using _wp_point.Rest.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _wp_point.Rest.Replace
{
    public partial class frmReplaceFinish : Form
    {
        private readonly frmMain _frm;
        private CardNo cardNo;

        public frmReplaceFinish(frmMain frm, CardNo cardno)
        {
            InitializeComponent();
            this._frm = frm;
            cardNo = cardno;
            if (!String.IsNullOrEmpty(cardNo.NewCardNo)) { lbContent.Text = lbContent.Text.Replace("newcardId", cardNo.NewCardNo); }
            else { lbContent.Text = lbContent.Text.Replace("newcardId", cardNo.OldCardNo); }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmSelect"))
            {
                frmSelect childForm = new frmSelect(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
