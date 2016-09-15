using _wp_point.Rest.Class;
using _wp_point.Rest.Request;
using AForge.Video;
using AForge.Video.DirectShow;
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
using System.Windows.Forms;
using ZXing;

namespace _wp_point.Rest.Reissue
{
    public partial class frmNR_QRCodeInput : Form
    {
        private readonly frmMain _frm;
        private static readonly ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private VideoCaptureDevice FinalFrame;
        private FilterInfoCollection captureDevice;
        private ResponseData_MemberInfo_Reference Res_MemberInfo;
        private ReissueQRCodeData OldReissueQRData;

        #region コンストラクタ・デストラクタ
        public frmNR_QRCodeInput() { InitializeComponent(); }
        public frmNR_QRCodeInput(frmMain fr)
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
            //タイマ停止
            timerReadQRcode.Stop();
            //カメラの写真を読み込んだ時点の画像に
            FinalFrame.Stop();
            //選択画面へ戻る
            if (this._frm.ResetOpenWindow("frmNotRead"))
            {
                frmNotRead childForm = new frmNotRead(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
        
        private void timerReadQRcode_Tick(object sender, EventArgs e)
        {
            try
            {
                BarcodeReader Reader = new BarcodeReader();
                Image picture = null;
                pictureBox.Invoke(new MethodInvoker(delegate { picture = pictureBox.Image; }));

                if (picture != null)
                {

                    Result result = Reader.Decode((Bitmap)picture);
                    if (result != null)
                    {
                        string decode = result.ToString().Trim();
                        if (decode != "")
                        {
                            //------------------------------
                            // 成功処理
                            //------------------------------
                            //タイマ停止
                            timerReadQRcode.Stop();

                            //カメラの写真を読み込んだ時点の画像に
                            if (FinalFrame.IsRunning == true) FinalFrame.Stop();

                            //------------------------------
                            // メンバー情報参照
                            //------------------------------
                            string ShopAuthCode = shopAuthCode.shop_auth_code;
                            ApiClient ac = new ApiClient(_logger);
                            JsonValue JVresult = ac.MemberInfo_Reference(ShopAuthCode, null, null, decode, null);
                            int code = (int)JVresult["code"];
                            if (code != 200)
                            {
                                if (code == 400) MsgBox.Show("リクエストが不正です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                if (code == 401) MsgBox.Show("認証に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (code == 404) MsgBox.Show("会員情報がありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                if (code == 500) MsgBox.Show("システムエラー", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (code == 503) MsgBox.Show("WEBシステムがメンテナンス中です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    
                                //タイマ再開
                                timerReadQRcode.Start();
                                //カメラの写真の読み込みを再開
                                if (FinalFrame.IsRunning == false) FinalFrame.Start();
                                //画像データのクリア
                                pictureBox.Image = null;

                                return;
                            }
                            Res_MemberInfo.GetJsonData(JVresult);
                            //パスワードはクリア
                            Res_MemberInfo.password = "";
                            //旧データを取得
                            OldReissueQRData.CardNo = Res_MemberInfo.card_no;
                            OldReissueQRData.CardQRCode = Res_MemberInfo.card_qr;

                            //------------------------------
                            // 成功画面へ遷移
                            //------------------------------
                            if (this._frm.ResetOpenWindow("frmNR_Confirm"))
                            {
                                frmNR_Confirm childForm = new frmNR_Confirm(_frm, Res_MemberInfo, OldReissueQRData);
                                childForm.MdiParent = _frm;
                                childForm.WindowState = FormWindowState.Maximized;
                                childForm.Show();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                timerReadQRcode.Stop();
            }
        }

        private void frmQRCodeInput_Load(object sender, EventArgs e)
        {
            try
            {
                captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                int device = Common.GetSetting<int>("device");
                if (device < 0) device = 0;
                FinalFrame = new VideoCaptureDevice();

                FinalFrame = new VideoCaptureDevice(captureDevice[device].MonikerString);
                FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
                FinalFrame.Start();

                // start time
                timerReadQRcode.Enabled = true;
                timerReadQRcode.Start();
            }
            catch
            {
                MsgBox.Show("カメラに接続が失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                if ((Bitmap)eventArgs.Frame.Clone() != null)
                {
                    pictureBox.Image = (Bitmap)eventArgs.Frame.Clone();
                }
            }
            catch { }
        }
    }
}
