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

namespace _wp_point.Rest.Reissue
{
    public partial class frmNR_CardNo : Form
    {
        private readonly frmMain _frm;
        ResponseData_MemberInfo_Reference Res_MemberInfo;
        private static readonly ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private ReissueQRCodeData OldReissueQRData;

        #region コンストラクタ・デストラクタ
        public frmNR_CardNo() { InitializeComponent(); }
        public frmNR_CardNo(frmMain fr)
        {
            InitializeComponent();
            this._frm = fr;
            Res_MemberInfo = new ResponseData_MemberInfo_Reference();
        }

        #endregion

        /// <summary>
        /// 戻るボタン押した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                //QR、CardNo選択画面へ遷移
                if (this._frm.ResetOpenWindow("frmNotRead"))
                {
                    frmNotRead childForm = new frmNotRead(_frm);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 検索ボタン押した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------------------
                // 入力チェック
                //------------------------------
                string SearchText = "";
                if (txtMemberID.Text == "" || textTempoCode.Text == "")
                {
                    MsgBox.Show("カード番号を入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    if (textTempoCode.Text.Length != 4) SearchText = textTempoCode.Text.PadLeft(4, '0');
                    else SearchText = textTempoCode.Text;
                    if (txtMemberID.Text.Length != 6) SearchText = SearchText + txtMemberID.Text.PadLeft(6, '0');
                    else SearchText = SearchText + txtMemberID.Text;
                    if (Regex.IsMatch(SearchText, "[^0-9]"))
                    {
                        MsgBox.Show("カード番号の入力に誤りがあります。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                //------------------------------
                // メンバー情報参照
                //------------------------------
                string ShopAuthCode = shopAuthCode.shop_auth_code;
                ApiClient ac = new ApiClient(_logger);
                JsonValue result = ac.MemberInfo_Reference(ShopAuthCode, null, SearchText, null, null);
                int code = (int)result["code"];
                if (code != 200)
                {
                    if (code == 400) MsgBox.Show("リクエストが不正です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (code == 401) MsgBox.Show("認証に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (code == 404) MsgBox.Show("会員情報がありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (code == 500) MsgBox.Show("システムエラー", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (code == 503) MsgBox.Show("WEBシステムがメンテナンス中です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Res_MemberInfo.GetJsonData(result);
                //パスワードはクリア
                Res_MemberInfo.password = "";
                //旧データを取得
                OldReissueQRData.CardNo = Res_MemberInfo.card_no;
                OldReissueQRData.CardQRCode = Res_MemberInfo.card_qr;

                //------------------------------
                // 次の画面へ
                //------------------------------
                if (this._frm.ResetOpenWindow("frmNR_Confirm"))
                {
                    frmNR_Confirm childForm = new frmNR_Confirm(_frm, Res_MemberInfo, OldReissueQRData);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textTempoCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar < '0' || '9' < e.KeyChar)
                {
                    if (e.KeyChar != '\b')
                    {
                        //押されたキーが 0～9、Backspace、でない場合は、イベントをキャンセルする
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
