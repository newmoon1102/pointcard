using _wp_point.Rest.Class;
using _wp_point.Rest.Request;
using log4net;
using System;
using System.Configuration;
using System.Drawing;
using System.Json;
using System.Reflection;
using System.Windows.Forms;

namespace _wp_point.Rest.Replace
{
    public partial class frmDeleteConfirm : Form
    {
        private readonly frmMain _frm;
        private int transitionNo;
        private bool FormerCardflg;
        private MemberMergeRequest MergeData;
        private CardNo cardNo;
        /// <summary>
        /// 元会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Original_MemberInfo;
        /// <summary>
        /// 先会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Former_MemberInfo;

        private ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public frmDeleteConfirm(frmMain frm, ResponseData_MemberInfo_Reference original_MemberInfo, ResponseData_MemberInfo_Reference former_MemberInfo, int index)
        {
            InitializeComponent();
            this._frm = frm;
            transitionNo = index;
            Original_MemberInfo = original_MemberInfo;
            Former_MemberInfo = former_MemberInfo;
            MergeData = new MemberMergeRequest();
            cardNo = new CardNo();
            UserInitialize();
        }

        private void UserInitialize()
        {
            lbOriginalCardId.Text = Original_MemberInfo.card_no.Substring(0, 4) + "-" + Original_MemberInfo.card_no.Substring(4, 6);
            cardNo.OldCardNo = lbOriginalCardId.Text;

            lbOriginalMemberName.Text = Original_MemberInfo.last_name + " " + Original_MemberInfo.first_name;
            if (!String.IsNullOrEmpty(Former_MemberInfo.card_no))
            {
                lbFormerCardId.Text = Former_MemberInfo.card_no.Substring(0,4) + "-" + Former_MemberInfo.card_no.Substring(4, 6);
                FormerCardflg = true;
                cardNo.NewCardNo = lbFormerCardId.Text;
            }
            else { lbFormerCardId.Text = "なし"; FormerCardflg = false; }
            cardNo.NewCardName = Former_MemberInfo.last_name_y + " " + Former_MemberInfo.first_name_y;
            lbFormerMemberName.Text = Former_MemberInfo.last_name + " " + Former_MemberInfo.first_name;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string om_delete_flag = "0";
            if (chkDelete.Checked == true)
            {
                DialogResult result = MsgBox.Show("譲渡後、元会員の会員情報を削除します。","確認",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (result == DialogResult.Cancel) return;
                om_delete_flag = "1";
            }

            MergeData.force_flag = ConfigurationManager.AppSettings["force_flag"] ?? "";

            if (Original_MemberInfo.member_type == "1" && MergeData.force_flag == "0")
            {
                MsgBox.Show("カード譲渡処理に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Original_MemberInfo.member_type == "2" || (Original_MemberInfo.member_type == "1" && MergeData.force_flag == "1"))
            {
                MergeData.shop_auth_code = shopAuthCode.shop_auth_code;
                MergeData.old_member_id = Original_MemberInfo.member_id;
                MergeData.new_member_id = Former_MemberInfo.member_id;
                //MergeData.force_flag = "1";
                MergeData.om_delete_flag = om_delete_flag;

                ApiClient ac = new ApiClient(_logger);
                JsonValue Jsonresult = ac.MemberMerge(MergeData);
                int code = (int)Jsonresult["code"];
                if (code != 200)
                {
                    if (code == 400) MsgBox.Show("リクエストが不正です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (code == 401) MsgBox.Show("認証に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (code == 404) MsgBox.Show("会員情報がありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (code == 500) MsgBox.Show("システムエラー", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (code == 503) MsgBox.Show("WEBシステムがメンテナンス中です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //画面へ遷移
                if (FormerCardflg)
                {
                    if (this._frm.ResetOpenWindow("frmGuide_001"))
                    {
                        frmGuide_001 childForm = new frmGuide_001(_frm, cardNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                }
                else
                {
                    if (this._frm.ResetOpenWindow("frmChangeName"))
                    {
                        frmChangeName childForm = new frmChangeName(_frm, cardNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                }
            }
            else
            {
                MsgBox.Show("カード譲渡処理に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmInfoConfirm"))
            {
                frmInfoConfirm childForm = new frmInfoConfirm(_frm, Original_MemberInfo, Former_MemberInfo, this.Name.ToString(), transitionNo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDelete.Checked) pnlOriginalMember.BackColor = Color.Red;
            else pnlOriginalMember.BackColor = Color.Gainsboro;
        }
    }
}
