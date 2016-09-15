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
    public partial class frmGuide_001 : Form
    {
        private readonly frmMain _frm;
        private CardNo cardNo;

        public frmGuide_001(frmMain frm, CardNo cardno)
        {
            InitializeComponent();
            this._frm = frm;
            cardNo = cardno;
            btnBack.Enabled = false;
            UserInitialize();
        }

        private void UserInitialize()
        {
            lbTitle.Text = lbTitle.Text.Replace("newcardId", cardNo.NewCardNo);
            lbContent.Text = lbContent.Text.Replace("newcardId", cardNo.NewCardNo);
            lbContent.Text = lbContent.Text.Replace("oldcardId", cardNo.OldCardNo);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmGuide_002"))
            {
                frmGuide_002 childForm = new frmGuide_002(_frm, cardNo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
