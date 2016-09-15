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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace _wp_point.Rest.Reissue
{
    public partial class frmSuccess : Form
    {
        private readonly frmMain _frm;
        Requestdata_MemberInfo_InputOrUpdate RD_MIIOU;
        ResponseData_MemberInfo_Reference InData;
        private static readonly ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private ReissueQRCodeData OldReissueQRData;

        string SuccessText;
        /// <summary>
        /// 帰り先コード、1:frmQRCodeInput、2:frmNR_NewQRCodeInput
        /// </summary>
        int ReturnCode;
        private string NewCardNo;
        private string OldCardNo;

        #region コンストラクタ・デストラクタ
        public frmSuccess() { InitializeComponent();}
        public frmSuccess(frmMain fr, string QRCode, Requestdata_MemberInfo_InputOrUpdate data, ResponseData_MemberInfo_Reference indata, int RetCode)
        {
            InitializeComponent();
            this._frm = fr;
            RD_MIIOU = data;
            RD_MIIOU.qr_code = QRCode;
            InData = indata;
            ReturnCode = RetCode;
        }
        public frmSuccess(frmMain fr, string QRCode, Requestdata_MemberInfo_InputOrUpdate data, ResponseData_MemberInfo_Reference indata, int RetCode, ReissueQRCodeData OldQR )
        {
            InitializeComponent();
            this._frm = fr;
            RD_MIIOU = data;
            RD_MIIOU.qr_code = QRCode;
            InData = indata;
            ReturnCode = RetCode;
            OldReissueQRData = OldQR;
        }
        public frmSuccess(frmMain fr, int RetCode)
        {
            InitializeComponent();
            this._frm = fr;
            ReturnCode = RetCode;
        }

        #endregion

        private void frmSuccess_Load(object sender, EventArgs e)
        {
            try
            {
                //ラベル２は開発用で位置確認のための物なので見えなくする
                this.label2.Hide();
                //特定条件でのみ表示されるメッセージ
                this.label1.Hide();

                //メッセージ表示だけの場合
                if (ReturnCode == 3)
                {
                    //メッセージの変更
                    SuccessText += "プリペイド・ポイントの付与が完了しました。" + Environment.NewLine;
                    SuccessText += "カードをお客様へお渡しください。" + Environment.NewLine;
                    this.labelResult.Text = SuccessText;
                    this.label1.Show();

                    //ボタン名の変更
                    this.btnTop.Text = "TOPへ";

                    //メッセージが長く入りきらないのでサイズと位置を調整
                    this.labelResult.Font = new Font("メイリオ", 20, FontStyle.Bold, GraphicsUnit.Point);
                    int x, y;
                    x = (this.panel1.Size.Width - this.labelResult.Size.Width) / 2;
                    y = this.labelResult.Location.Y;
                    this.labelResult.Location = new Point(x, y);

                    return;
                }

                //成功メッセージ
                SuccessText = "";
                if (ReturnCode == 2)
                {
                    SuccessText += "再発行したカードの登録が完了しました。" + Environment.NewLine;
                    SuccessText += "続いて、プリペイド・ポイントの付与を行います。" + Environment.NewLine;
                    this.btnTop.Text = "次へ";
                }
                else
                {
                    SuccessText += "再発行したカードの登録が完了しました。" + Environment.NewLine;
                    SuccessText += "カードをお客様へお渡しください。" + Environment.NewLine;
                    this.btnTop.Text = "TOPへ";
                    this.label1.Show();
                }

                //処理中メッセージ
                this.labelResult.Text = "QRコード情報を登録中..." + Environment.NewLine + "しばらくお待ち下さい";

                //QRコードを登録
                ResponseData_MemberInfo_InputOrUpdate ResD_MIIOU = new ResponseData_MemberInfo_InputOrUpdate();
                ApiClient ac = new ApiClient(_logger);
                RD_MIIOU.password = "";
                RD_MIIOU.shop_auth_code = shopAuthCode.shop_auth_code;
                ResD_MIIOU.GetJsonData(ac.MemberInfo_InsertUpdate(RD_MIIOU));

                //--------------------------------------------------
                // 戻り値チェック
                //--------------------------------------------------
                #region 登録エラー時
                if (ResD_MIIOU.code != 200)
                {
                    if (ResD_MIIOU.code == 400) MsgBox.Show("リクエストが不正です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (ResD_MIIOU.code == 401) MsgBox.Show("認証に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (ResD_MIIOU.code == 404) MsgBox.Show("該当データ無し", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (ResD_MIIOU.code == 405) MsgBox.Show("QRコード値重複エラー", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (ResD_MIIOU.code == 500) MsgBox.Show("システムエラー", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (ResD_MIIOU.code == 503) MsgBox.Show("WEBシステムがメンテナンス中です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    this.labelResult.Text = "QRコード情報の登録に失敗しました。";

                    //前の画面へ遷移
                    if (ReturnCode == 1)
                    {
                        if (this._frm.ResetOpenWindow("frmQRCodeInput"))
                        {
                            frmQRCodeInput childForm = new frmQRCodeInput(_frm, RD_MIIOU, InData, OldReissueQRData);
                            childForm.MdiParent = _frm;
                            childForm.WindowState = FormWindowState.Maximized;
                            childForm.Show();
                        }
                    }
                    else if (ReturnCode == 2)
                    {
                        if (this._frm.ResetOpenWindow("frmNR_NewQRCodeInput"))
                        {
                            frmNR_NewQRCodeInput childForm = new frmNR_NewQRCodeInput(_frm, InData, OldReissueQRData);
                            childForm.MdiParent = _frm;
                            childForm.WindowState = FormWindowState.Maximized;
                            childForm.Show();
                        }
                    }

                    return;
                }
                #endregion

                #region 登録成功時
                //成功メッセージ
                this.labelResult.Text = SuccessText;

                //メッセージの位置調整
                if (ReturnCode == 2)
                {
                    this.labelResult.Font = new Font("メイリオ", 20, FontStyle.Bold, GraphicsUnit.Point);
                    int x, y;
                    x = (this.panel1.Size.Width - this.labelResult.Size.Width) / 2;
                    y = this.labelResult.Location.Y;
                    this.labelResult.Location = new Point(x, y);
                }

                //次の遷移のためにデータを保持
                NewCardNo = RD_MIIOU.card_no.Substring(0, 4) + " - " + RD_MIIOU.card_no.Substring(4, 6);
                OldCardNo = InData.card_no.Substring(0, 4) + " - " + InData.card_no.Substring(4, 6);

                //成功ポップアップ
                MsgBox.Show("登録が完了しました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 完了後のボタンを押した場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTop_Click(object sender, EventArgs e)
        {
            try
            {
                //QRコードが消えている場合の遷移時 or プリペイド・ポイント手順後
                if (ReturnCode == 1 || ReturnCode == 3)
                {
                    //メニュー画面へ遷移
                    if (this._frm.ResetOpenWindow("frmSelect"))
                    {
                        frmSelect childForm = new frmSelect(_frm);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                }

                //磁気が読めなくなった場合の遷移時
                if (ReturnCode == 2)
                {
                    //プリペイド・ポイントの付加手順画面へ遷移
                    if (this._frm.ResetOpenWindow("frmNR_InputProcess"))
                    {
                        frmNR_InputProcess childForm = new frmNR_InputProcess(_frm, NewCardNo, OldReissueQRData.CardNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
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
