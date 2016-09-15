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
    public partial class frmCardNotExist : Form
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
        private static readonly ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public frmCardNotExist(frmMain frm, ResponseData_MemberInfo_Reference original_MemberInfo, ResponseData_MemberInfo_Reference former_MemberInfo, string frmname, int index)
        {
            InitializeComponent();
            this._frm = frm;
            transitionNo = index;
            Original_MemberInfo = original_MemberInfo;
            Former_MemberInfo = former_MemberInfo;
        }

        private void btnWebQRCode_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmWebQRCode"))
            {
                frmWebQRCode childForm = new frmWebQRCode(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnWebId_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmWebID"))
            {
                frmMemberID childForm = new frmMemberID(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmNewMember"))
            {
                frmNewMember childForm = new frmNewMember(_frm, Original_MemberInfo, Former_MemberInfo, 2);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
