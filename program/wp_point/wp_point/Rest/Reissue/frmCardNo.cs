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
    public partial class frmCardNo : Form
    {
        private readonly frmMain _frm;
        private static readonly ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region コンストラクタ・デストラクタ
        public frmCardNo() { InitializeComponent(); }
        public frmCardNo(frmMain fr)
        {
            InitializeComponent();
            this._frm = fr;
        }

        public frmCardNo(frmMain fr, string CardNo, bool Skipflag)
        {
            InitializeComponent();
            this._frm = fr;
            if(CardNo != "")
            {
                txtMemberID.Text = CardNo.Substring(4, 6);
                textTempoCode.Text = CardNo.Substring(0, 4);
            }
            if (Skipflag)
            {
                textTempoCode.ReadOnly = false;
                txtMemberID.ReadOnly = false;
                btnEdit.Text = "編集中";
            }
        }

        #endregion

        /// <summary>
        /// 戻るボタン押した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            //説明画面へ遷移
            if (this._frm.ResetOpenWindow("frmReissueProcedure"))
            {
                frmReissueProcedure childForm = new frmReissueProcedure(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        /// <summary>
        /// 検索ボタン押した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
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
            // 会員情報照会APIへデータ照会 APIMemberExportURL
            //------------------------------
            string ShopAuthCode = shopAuthCode.shop_auth_code;
            ApiClient ac = new ApiClient(_logger);
            ResponseData_MemberInfo_Reference RD_MIR = new ResponseData_MemberInfo_Reference();
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
            RD_MIR.GetJsonData(result);

            //旧コード系データを保持
            ReissueQRCodeData OldReissueData = new ReissueQRCodeData();
            OldReissueData.CardNo = RD_MIR.card_no;
            OldReissueData.CardQRCode = RD_MIR.card_qr;

            //------------------------------
            // データを次の画面へ渡す
            //------------------------------
            Requestdata_MemberInfo_InputOrUpdate RD_MIOU = new Requestdata_MemberInfo_InputOrUpdate();
            RD_MIOU.SetReferenceData(ref RD_MIR);
            //QRコード認識画面へ遷移
            if (this._frm.ResetOpenWindow("frmQRCodeInput"))
            {
                frmQRCodeInput childForm = new frmQRCodeInput(_frm, RD_MIOU, RD_MIR, OldReissueData);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        /// <summary>
        /// カード番号の編集可能・不可能を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnEdit.Text == "編集")
                {
                    textTempoCode.ReadOnly = false;
                    txtMemberID.ReadOnly = false;
                    btnEdit.Text = "編集中";
                }
                else
                {
                    textTempoCode.ReadOnly = true;
                    txtMemberID.ReadOnly = true;
                    btnEdit.Text = "編集";
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
