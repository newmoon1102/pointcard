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

namespace _wp_point.Rest.Replace
{
    public partial class frmOldMemeber : Form
    {
        private readonly frmMain _frm;
        /// <summary>
        /// 元会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Original_MemberInfo;
        /// <summary>
        /// 先会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Former_MemberInfo;

        public frmOldMemeber(frmMain frm, ResponseData_MemberInfo_Reference original_MemberInfo, ResponseData_MemberInfo_Reference former_MemberInfo)
        {
            InitializeComponent();
            this._frm = frm;
            Original_MemberInfo = original_MemberInfo;
            Former_MemberInfo = former_MemberInfo;
        }

        private void btnQRCode_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmQRCodeRead"))
            {
                frmQRCodeRead childForm = new frmQRCodeRead(_frm, Original_MemberInfo, Former_MemberInfo, 1);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnCardNo_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmCardNoRead"))
            {
                frmCardNoRead childForm = new frmCardNoRead(_frm, Original_MemberInfo, Former_MemberInfo, 1);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
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
