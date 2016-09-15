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
    public partial class frmCardNoRead : Form
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

        public frmCardNoRead(frmMain frm, ResponseData_MemberInfo_Reference original_MemberInfo, ResponseData_MemberInfo_Reference former_MemberInfo, int index)
        {
            InitializeComponent();
            this._frm = frm;
            transitionNo = index;
            Original_MemberInfo = original_MemberInfo;
            Former_MemberInfo = former_MemberInfo;

            if(transitionNo == 1)
            {
                //譲渡元
                this.label3.Text = "譲渡元カードのカード番号を入力して下さい。";
            }
            if (transitionNo == 2)
            {
                //譲渡先
                this.label3.Text = "譲渡先カードのカード番号を入力して下さい。";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //------------------------------
            // 入力チェック
            //------------------------------
            string cardNo = "";
            if (textCard_H.Text == "" || textCard_B.Text == "")
            {
                MsgBox.Show("カード番号を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (textCard_H.Text.Length != 4) cardNo = textCard_H.Text.PadLeft(4, '0');
                else cardNo = textCard_H.Text;
                if (textCard_B.Text.Length != 6) cardNo = cardNo + textCard_B.Text.PadLeft(6, '0');
                else cardNo = cardNo + textCard_B.Text;

                if (Regex.IsMatch(cardNo, "[^0-9]")) { MsgBox.Show("カード番号の入力に誤りがあります。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            }

            //------------------------------
            // メンバー情報参照
            //------------------------------
            string ShopAuthCode = shopAuthCode.shop_auth_code;
            ApiClient ac = new ApiClient(_logger);
            JsonValue JVresult = ac.MemberInfo_Reference(ShopAuthCode, null, cardNo, null, null);
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

            if (transitionNo == 1)
            {
                Original_MemberInfo.GetJsonData(JVresult);
                //パスワードはクリア
                Original_MemberInfo.password = "";
            }
            else if (transitionNo == 2)
            {
                Former_MemberInfo.GetJsonData(JVresult);
                //パスワードはクリア
                Former_MemberInfo.password = "";

                if (Original_MemberInfo.member_id == Former_MemberInfo.member_id)
                {
                    MsgBox.Show("譲渡元会員と譲渡先会員が同じです。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            //------------------------------
            // 成功画面へ遷移
            //------------------------------
            if (this._frm.ResetOpenWindow("frmInfoConfirm"))
            {
                frmInfoConfirm childForm = new frmInfoConfirm(_frm, Original_MemberInfo, Former_MemberInfo, this.Name.ToString(), transitionNo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (transitionNo == 1)
            {
                if (this._frm.ResetOpenWindow("frmOldMemeber"))
                {
                    frmOldMemeber childForm = new frmOldMemeber(_frm, Original_MemberInfo, Former_MemberInfo);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            else
            {
                if (this._frm.ResetOpenWindow("frmNewMember"))
                {
                    frmNewMember childForm = new frmNewMember(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
        }

        /// <summary>
        /// Next control when Enter key import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.SelectNextControl((Control)sender, true, true, true, true);
        }
        /// <summary>
        /// Login when Enter key import
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtcard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
    }
}
