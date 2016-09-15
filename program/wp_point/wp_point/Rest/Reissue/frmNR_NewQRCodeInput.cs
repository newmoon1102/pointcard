using _wp_point.Rest.Class;
using _wp_point.Rest.Request;
using AForge.Video;
using AForge.Video.DirectShow;
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
using ZXing;

namespace _wp_point.Rest.Reissue
{
    public partial class frmNR_NewQRCodeInput : Form
    {
        private readonly frmMain _frm;
        private static readonly ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private VideoCaptureDevice FinalFrame;
        private FilterInfoCollection captureDevice;
        ResponseData_MemberInfo_Reference Res_MemberInfo;
        private ReissueQRCodeData OldReissueQRData;

        #region コンストラクタ・デストラクタ
        public frmNR_NewQRCodeInput() { InitializeComponent(); }
        public frmNR_NewQRCodeInput(frmMain fr, ResponseData_MemberInfo_Reference data, ReissueQRCodeData olddata)
        {
            InitializeComponent();
            this._frm = fr;
            Res_MemberInfo = data;
            this.lbCardid.Text = data.card_no.Substring(0, 4) + " - " + data.card_no.Substring(4, 6);
            OldReissueQRData = olddata;
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
                            //タイマ停止
                            timerReadQRcode.Stop();

                            //カメラの写真を読み込んだ時点の画像に
                            if (FinalFrame.IsRunning == true) FinalFrame.Stop();

                            //------------------------------
                            // QRコードの新旧重複チェック
                            //------------------------------
                            if (OldReissueQRData.CardQRCode == decode)
                            {
                                MsgBox.Show("破棄カードのQRコードと新規カードのQRコードが同じです", "QRコード不正", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                pictureBox.Image = null;
                                FinalFrame.Start();
                                timerReadQRcode.Start();
                                return;
                            }


                            //------------------------------
                            // QRコードのカード固定文字列チェック
                            //------------------------------
                            if (!Common.CheckQRCode_CardMachine(decode))
                            {
                                MsgBox.Show("不正なQRコードです。", "QRコード不正", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                pictureBox.Image = null;
                                FinalFrame.Start();
                                timerReadQRcode.Start();
                                return;
                            }


                            //------------------------------
                            // カードデータの紐付け
                            //------------------------------
                            Requestdata_MemberInfo_InputOrUpdate RD_MIIOU = new Requestdata_MemberInfo_InputOrUpdate();
                            RD_MIIOU.SetReferenceData(ref Res_MemberInfo);

                            //------------------------------
                            // 成功画面へ遷移
                            //------------------------------
                            if (this._frm.ResetOpenWindow("frmSuccess"))
                            {
                                frmSuccess childForm = new frmSuccess(_frm, decode, RD_MIIOU, Res_MemberInfo, 2, OldReissueQRData);
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
                FinalFrame = new VideoCaptureDevice();

                FinalFrame = new VideoCaptureDevice(captureDevice[device].MonikerString);
                FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
                FinalFrame.Start();

                // start time
                timerReadQRcode.Enabled = true;
                timerReadQRcode.Start();
            }
            catch (Exception)
            {
                MsgBox.Show("カメラとの接続に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                pictureBox.Image = (Bitmap)eventArgs.Frame.Clone();
            }
            catch {}
        }
    }
}
