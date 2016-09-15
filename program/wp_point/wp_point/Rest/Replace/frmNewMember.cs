using _wp_point.Rest.Class;
using _wp_point.Rest.Request;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace _wp_point.Rest.Replace
{
    public partial class frmNewMember : Form
    {
        private readonly frmMain _frm;
        private int transitionNo;
        /// <summary>
        /// 元会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Original_MemberInfo;
        /// <summary>
        /// 先会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Former_MemberInfo;

        private ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public frmNewMember(frmMain frm, ResponseData_MemberInfo_Reference original_MemberInfo, ResponseData_MemberInfo_Reference former_MemberInfo, int index)
        {
            InitializeComponent();
            this._frm = frm;
            transitionNo = index;
            Original_MemberInfo = original_MemberInfo;
            Former_MemberInfo = former_MemberInfo;
        }

        private void btnCardNotExist_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmCardNotExist"))
            {
                frmCardNotExist childForm = new frmCardNotExist(_frm, Original_MemberInfo, Former_MemberInfo, this.Name.ToString(), 3);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            //------------------------------
            // 画面へ戻る
            //------------------------------
            if (this._frm.ResetOpenWindow("frmInfoConfirm"))
            {
                frmInfoConfirm childForm = new frmInfoConfirm(_frm, Original_MemberInfo, Former_MemberInfo, this.Name.ToString(), 1);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnCardInput_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmCardNoRead"))
            {
                frmCardNoRead childForm = new frmCardNoRead(_frm, Original_MemberInfo, Former_MemberInfo, 2);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnQrInput_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmselectQRCode"))
            {
                frmselectQRCode childForm = new frmselectQRCode(_frm, Original_MemberInfo, Former_MemberInfo, 2);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
