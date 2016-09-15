using _wp_point.Rest.Class;
using _wp_point.Rest.Request;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Json;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace _wp_point.Rest.Replace
{
    public partial class frmMemberID : Form
    {
        private readonly frmMain _frm;
        private int transitionNo;
        private JsonValue JVresult;
        private static readonly ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 元会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Original_MemberInfo;
        /// <summary>
        /// 先会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Former_MemberInfo;
        /// <summary>
        /// 検索情報
        /// </summary>
        Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "会員ID","member_id"},
                { "メールアドレス","mail_address"}
            };

        public frmMemberID(frmMain frm, ResponseData_MemberInfo_Reference original_MemberInfo, ResponseData_MemberInfo_Reference former_MemberInfo, int index)
        {
            InitializeComponent();
            this._frm = frm;
            transitionNo = index;
            Original_MemberInfo = original_MemberInfo;
            Former_MemberInfo = former_MemberInfo;
            JVresult = null;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmCardNotExist"))
            {
                frmCardNotExist childForm = new frmCardNotExist(_frm, Original_MemberInfo,Former_MemberInfo, this.Name.ToString(), transitionNo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void frmMemberID_Load(object sender, EventArgs e)
        {
            cbbTitle.DataSource = new BindingSource(param, null);
            cbbTitle.DisplayMember = "Key";
            cbbTitle.ValueMember = "Value";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                KeyValuePair<string, string> item = (KeyValuePair<string, string>)cbbTitle.SelectedItem;

                switch (item.Value)
                {
                    case "member_id":
                        memberget_Id();
                        break;
                    case "mail_address":
                        memberget_Mail();
                        break;
                }

                if (JVresult != null)
                {
                    int code = (int)JVresult["code"];
                    if (code != 200)
                    {
                        if (code == 400) MsgBox.Show("リクエストが不正です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (code == 401) MsgBox.Show("認証に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (code == 404) MsgBox.Show("会員情報がありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (code == 500) MsgBox.Show("システムエラー", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (code == 503) MsgBox.Show("WEBシステムがメンテナンス中です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return;
                    }

                    Former_MemberInfo.GetJsonData(JVresult);
                    //パスワードはクリア
                    Former_MemberInfo.password = "";

                    if (Original_MemberInfo.member_id == Former_MemberInfo.member_id)
                    {
                        MsgBox.Show("譲渡元会員と譲渡先会員が同じです。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!String.IsNullOrEmpty(Former_MemberInfo.card_no))
                    {
                        MsgBox.Show("譲渡先会員にすでにカードが存在しています。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    //------------------------------
                    // 画面へ遷移
                    //------------------------------
                    if (this._frm.ResetOpenWindow("frmInfoConfirm"))
                    {
                        frmInfoConfirm childForm = new frmInfoConfirm(_frm, Original_MemberInfo, Former_MemberInfo, this.Name.ToString(), transitionNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                }
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void memberget_Id()
        {
            //------------------------------
            // 入力チェック
            //------------------------------
            JVresult = null;
            string memberId = "";
            if (txtInfo.Text == "")
            {
                MsgBox.Show("会員IDを入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                memberId = txtInfo.Text;
                if (Regex.IsMatch(memberId, "[^0-9]")) { MsgBox.Show("会員IDの入力に誤りがあります。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            }

            //------------------------------
            // メンバー情報参照
            //------------------------------
            string ShopAuthCode = shopAuthCode.shop_auth_code;
            ApiClient ac = new ApiClient(_logger);
            JVresult = ac.MemberInfo_Reference(ShopAuthCode, memberId, null, null, null);
        }

        private void memberget_Mail()
        {
            //------------------------------
            // 入力チェック
            //------------------------------
            JVresult = null;
            string mailAddress = "";
            if (txtInfo.Text == "")
            {
                MsgBox.Show("メールを入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                mailAddress = txtInfo.Text;
                if (!Common.EmailIsValid(mailAddress)) { MsgBox.Show("メールの入力に誤りがあります。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            }

            //------------------------------
            // メンバー情報参照
            //------------------------------
            string ShopAuthCode = shopAuthCode.shop_auth_code;
            ApiClient ac = new ApiClient(_logger);
            JVresult = ac.checkEmail(mailAddress,ShopAuthCode);
            int code = (int)JVresult["code"];
            if (code != 200)
            {
                return;
            }

            JsonValue jsonSearchdata = JVresult["data"];
            JsonValue json = jsonSearchdata[0];
            string member_id = (string)json["member_id"];
            JVresult = null;
            JVresult = ac.MemberInfo_Reference(ShopAuthCode, member_id, null, null, null);
        }

        /// <summary>
        /// Login when Enter key import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
    }
}
