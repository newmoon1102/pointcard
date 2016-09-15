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
    public partial class frmGuide_002 : Form
    {
        private readonly frmMain _frm;
        private CardNo cardNo;

        public frmGuide_002(frmMain frm, CardNo cardno)
        {
            InitializeComponent();
            this._frm = frm;
            cardNo = cardno;
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
            if (this._frm.ResetOpenWindow("frmGuide_003"))
            {
                frmGuide_003 childForm = new frmGuide_003(_frm, cardNo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmGuide_001"))
            {
                frmGuide_001 childForm = new frmGuide_001(_frm, cardNo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
