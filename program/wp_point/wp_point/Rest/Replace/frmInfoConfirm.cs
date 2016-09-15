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
    public partial class frmInfoConfirm : Form
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

        private string frmName;
        private ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public frmInfoConfirm(frmMain frm, ResponseData_MemberInfo_Reference original_MemberInfo, ResponseData_MemberInfo_Reference former_MemberInfo, string frmname, int index)
        {
            InitializeComponent();
            this._frm = frm;
            transitionNo = index;
            frmName = frmname;
            Original_MemberInfo = original_MemberInfo;
            Former_MemberInfo = former_MemberInfo;
            UserInitialize();
        }

        private void UserInitialize()
        {
            if (transitionNo == 1)
            {
                //譲渡元
                displaydata(Original_MemberInfo);
                this.label3.Text = "譲渡元会員情報の確認";
            }
            else if (transitionNo == 2 || transitionNo == 3)
            {
                //譲渡先
                displaydata(Former_MemberInfo);
                this.label3.Text = "譲渡元会員情報の確認";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            switch (frmName)
            {
                case "frmQRCodeRead":
                    if (this._frm.ResetOpenWindow(frmName))
                    {
                        frmQRCodeRead childForm = new frmQRCodeRead(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    break;
                case "frmCardNoRead":
                    if (this._frm.ResetOpenWindow(frmName))
                    {
                        frmCardNoRead childForm = new frmCardNoRead(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    break;
                case "frmWebQRCode":
                    if (this._frm.ResetOpenWindow(frmName))
                    {
                        frmWebQRCode childForm = new frmWebQRCode(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    break;

                case "frmselectQRCode":
                    if (this._frm.ResetOpenWindow(frmName))
                    {
                        frmselectQRCode childForm = new frmselectQRCode(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    break;
                case "frmMemberID":
                    if (this._frm.ResetOpenWindow(frmName))
                    {
                        frmMemberID childForm = new frmMemberID(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    break;
                case "frmDeleteConfirm":
                    if (this._frm.ResetOpenWindow("frmNewMember"))
                    {
                        frmNewMember childForm = new frmNewMember(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    break;
                default:
                    if (this._frm.ResetOpenWindow(frmName))
                    {
                        frmOldMemeber childForm = new frmOldMemeber(_frm, Original_MemberInfo, Former_MemberInfo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    break;
            }
        }

        private void displaydata(ResponseData_MemberInfo_Reference MemberInfo)
        {
            lbmemberId.Text = MemberInfo.member_id;
            lbcardId.Text = !String.IsNullOrEmpty(MemberInfo.card_no)? MemberInfo.card_no.Substring(0, 4) + "-" + MemberInfo.card_no.Substring(4, 6):"なし";
            lbpoint.Text = !String.IsNullOrEmpty(MemberInfo.card_no)?MemberInfo.point + " P":"";
            lbfullName.Text = MemberInfo.last_name + ' ' + MemberInfo.first_name;
            lbfullName_kana.Text = MemberInfo.last_name_y + ' ' + MemberInfo.first_name_y;
            lbname.Text = MemberInfo.call_name;
            if (MemberInfo.zip_1 == "000" && MemberInfo.zip_2 == "0000") lbpostCode.Text = "--";
            else lbpostCode.Text = "〒" + MemberInfo.zip_1 + '-' + MemberInfo.zip_2;
            string pref_name = ""; string area_name = ""; string city_name = ""; string block = "";
            if (MemberInfo.pref_name == "x") pref_name = ""; else pref_name = MemberInfo.pref_name;
            if (MemberInfo.area_name == "x") area_name = ""; else area_name = MemberInfo.area_name;
            if (MemberInfo.city_name == "x") city_name = ""; else city_name = MemberInfo.city_name;
            if (MemberInfo.block == "x") block = ""; else block = MemberInfo.block;
            lbAddress.Text = pref_name + area_name + city_name + block;
            lbbuilname.Text = MemberInfo.building;
            if (MemberInfo.tel_number_1 == "0000") lbhomePhone.Text = "--";
            else lbhomePhone.Text = MemberInfo.tel_number_1 + '-' + MemberInfo.tel_number_2 + '-' + MemberInfo.tel_number_3;
            lbmobilePhone.Text = MemberInfo.mobile_number_1 + '-' + MemberInfo.mobile_number_2 + '-' + MemberInfo.mobile_number_3;
            lbprePhone.Text = MemberInfo.other_tel_number_1 + '-' + MemberInfo.other_tel_number_2 + '-' + MemberInfo.other_tel_number_3;

            if (MemberInfo.sex == "1") { lbgender.Text = "男性"; }
            else { lbgender.Text = "女性"; }

            if (!String.IsNullOrEmpty(MemberInfo.birth))
            { lbbirthday.Text = MemberInfo.birth.Replace("-", "/"); }
            else { lbbirthday.Text = "--"; }

            lbEmail.Text = MemberInfo.mail_address;
            lbpass.Text = "変更なし";

            if (MemberInfo.mailmaga_disable_flag == "2")
            { lbmz.Text = "希望する"; }
            else { lbmz.Text = "希望しない"; }

            if (MemberInfo.dm_disable_flag == "2")
            { lbdr.Text = "希望する"; }
            else { lbdr.Text = "希望しない"; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (transitionNo == 1)
            {
                if (this._frm.ResetOpenWindow("frmNewMember"))
                {
                    frmNewMember childForm = new frmNewMember(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            else if (transitionNo == 2 || transitionNo == 3)
            {
                if (this._frm.ResetOpenWindow("frmDeleteConfirm"))
                {
                    frmDeleteConfirm childForm = new frmDeleteConfirm(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
        }
    }
}
