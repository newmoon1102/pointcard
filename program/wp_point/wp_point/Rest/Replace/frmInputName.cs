using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _wp_point.Rest.Class;

namespace _wp_point.Rest.Replace
{
    public partial class frmInputName : Form
    {
        private readonly frmMain _frm;
        private CardNo cardNo;

        public frmInputName(frmMain frm, CardNo cardno)
        {
            InitializeComponent();
            this._frm = frm;
            cardNo = cardno;
            UserInitialize();
        }
        private void UserInitialize()
        {
            lbTitle.Text = lbTitle.Text.Replace("oldcardId", cardNo.OldCardNo);
            lbContent.Text = lbContent.Text.Replace("oldcardId", cardNo.OldCardNo);
            //ひらがなをカタカナに変換し、全角を半角に変換する
            string newMemberName = Microsoft.VisualBasic.Strings.StrConv(cardNo.NewCardName, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            lbContent.Text = lbContent.Text.Replace("newName", newMemberName);
            btnBack.Enabled = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //完了画面へ遷移
            if (this._frm.ResetOpenWindow("frmReplaceFinish"))
            {
                frmReplaceFinish childForm = new frmReplaceFinish(_frm, cardNo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
