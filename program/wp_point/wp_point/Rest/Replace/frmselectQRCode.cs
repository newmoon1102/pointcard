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
using System.Json;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ZXing;

namespace _wp_point.Rest.Replace
{
    public partial class frmselectQRCode : Form
    {
        private readonly frmMain _frm;
        private int transitionNo;
        private MemberExportRepuest ResData;
        private FilterInfoCollection captureDevice;
        private VideoCaptureDevice FinalFrame;
        private bool webQrflg;
        /// <summary>
        /// 元会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Original_MemberInfo;
        /// <summary>
        /// 先会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Former_MemberInfo;
        private static readonly ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public frmselectQRCode(frmMain frm, ResponseData_MemberInfo_Reference original_MemberInfo, ResponseData_MemberInfo_Reference former_MemberInfo, int index)
        {
            InitializeComponent();
            this._frm = frm;
            transitionNo = index;
            Original_MemberInfo = original_MemberInfo;
            Former_MemberInfo = former_MemberInfo;
            ResData = new MemberExportRepuest();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (FinalFrame.IsRunning == true) { FinalFrame.Stop(); }

            if (this._frm.ResetOpenWindow("frmNewMember"))
            {
                frmNewMember childForm = new frmNewMember(_frm, Original_MemberInfo, Former_MemberInfo, transitionNo);
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
                            timerReadQRcode.Stop();
                            if (FinalFrame.IsRunning == true) { FinalFrame.Stop(); }
                            //------------------------------
                            // メンバー情報参照
                            //------------------------------
                            string ShopAuthCode = shopAuthCode.shop_auth_code;
                            ApiClient ac = new ApiClient(_logger);

                            // カードQRコード
                            JsonValue JVresult = ac.MemberInfo_Reference(ShopAuthCode, null, null, decode, null);
                            int code = (int)JVresult["code"];
                            if (code != 200)
                            {
                                if (code == 404)
                                {
                                    // WebQRコード
                                    webQrflg = true;
                                    ResData.shop_auth_code = ShopAuthCode;
                                    ResData.qr_code = decode;
                                    ResData.with_data_flag = 1;
                                    JVresult = ac.PostMemberEx(ResData);
                                    code = (int)JVresult["code"];
                                    if (code != 200)
                                    {
                                            if (code == 400) MsgBox.Show("リクエストが不正です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            if (code == 401) MsgBox.Show("認証に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            if (code == 404) MsgBox.Show("会員情報がありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            if (code == 500) MsgBox.Show("システムエラー", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            if (code == 503) MsgBox.Show("WEBシステムがメンテナンス中です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                            //タイマ再開
                                            runCamera();

                                            return;
                                    }
                                }
                                else
                                {
                                    if (code == 400) MsgBox.Show("リクエストが不正です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    if (code == 401) MsgBox.Show("認証に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    if (code == 500) MsgBox.Show("システムエラー", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    if (code == 503) MsgBox.Show("WEBシステムがメンテナンス中です。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                    //タイマ再開
                                    runCamera();

                                    return;
                                }
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

                                    //タイマ再開
                                    runCamera();

                                    return;
                                }

                                if (!String.IsNullOrEmpty(Former_MemberInfo.card_no) && webQrflg)
                                {
                                    MsgBox.Show("譲渡先会員にすでにカードが存在しています。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                    //タイマ再開
                                    runCamera();

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
                    }
                }
            }
            catch (Exception ex)
            {
                if (FinalFrame.IsRunning == true) { FinalFrame.Stop(); }
                timerReadQRcode.Stop();
                MsgBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmselectQRCode_Load(object sender, EventArgs e)
        {
            Common cmm = new Common();
            captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            int device = Common.GetSetting<int>("device");

            if (cmm.CheckCamera(captureDevice))
            {
                FinalFrame = new VideoCaptureDevice();
                FinalFrame = new VideoCaptureDevice(captureDevice[device].MonikerString);
                FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);

                runCamera();
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

        private void runCamera()
        {
            pictureBox.Image = null;
            if (FinalFrame.IsRunning == false) FinalFrame.Start();
            FinalFrame.Start();

            // start time
            timerReadQRcode.Enabled = true;
            timerReadQRcode.Start();
            //　reset WebQrCode flag
            webQrflg = false;
        }
    }
}
