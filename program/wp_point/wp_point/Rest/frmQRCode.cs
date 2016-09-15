using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using _wp_point.Rest.Request;
using log4net;
using System.Reflection;
using System.Json;
using _wp_point.Rest.Class;
using System.Configuration;

namespace _wp_point.Rest
{
    public partial class frmQRCode : Form
    {
        private readonly frmMain _frm;

        private FilterInfoCollection captureDevice;
        private VideoCaptureDevice FinalFrame;
        public static string cardid;
        public static string qr_;
        public static int recipt_id;

        private static string frmName;
        private static string shop_auth_code;
        private ResponseData_MemberInfo_InputOrUpdate resData;
        private MemberImportRequest ReturnData;
        private MemberImportRequest RegisterData;

        private ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public frmQRCode(frmMain fr, string fr_name, string card, MemberImportRequest registerData)
        {
            InitializeComponent();
            this._frm = fr;
            cardid = card;
            frmName = fr_name;
            RegisterData = registerData;
            recipt_id = Convert.ToInt32(RegisterData.receipt_id );
            resData = new ResponseData_MemberInfo_InputOrUpdate();
            UserInitialize();
        }

        private void UserInitialize()
        {
            ReturnData = new MemberImportRequest();
            ReturnData.shop_auth_code = RegisterData.shop_auth_code;
            ReturnData.receipt_id = RegisterData.receipt_id;
            ReturnData.member_id = RegisterData.member_id;
            ReturnData.receipt_date = RegisterData.receipt_date;
            ReturnData.card_no = RegisterData.card_no;
            ReturnData.qr_code = RegisterData.qr_code;
            ReturnData.point = RegisterData.point;
            ReturnData.last_name = RegisterData.last_name;
            ReturnData.first_name = RegisterData.first_name;
            ReturnData.last_name_y = RegisterData.last_name_y;
            ReturnData.first_name_y = RegisterData.first_name_y;
            ReturnData.zip_1 = RegisterData.zip_1;
            ReturnData.zip_2 = RegisterData.zip_2;
            ReturnData.pref_name = RegisterData.pref_name;
            ReturnData.area_name = RegisterData.area_name;
            ReturnData.city_name = RegisterData.city_name;
            ReturnData.block = RegisterData.block;
            ReturnData.building = RegisterData.building;
            ReturnData.tel_number_1 = RegisterData.tel_number_1;
            ReturnData.tel_number_2 = RegisterData.tel_number_2;
            ReturnData.tel_number_3 = RegisterData.tel_number_3;
            ReturnData.mobile_number_1 = RegisterData.mobile_number_1;
            ReturnData.mobile_number_2 = RegisterData.mobile_number_2;
            ReturnData.mobile_number_3 = RegisterData.mobile_number_3;
            ReturnData.other_tel_number_1 = RegisterData.other_tel_number_1;
            ReturnData.other_tel_number_2 = RegisterData.other_tel_number_2;
            ReturnData.other_tel_number_3 = RegisterData.other_tel_number_3;
            ReturnData.mail_address = RegisterData.mail_address;
            ReturnData.password = RegisterData.password;
            ReturnData.call_name = RegisterData.call_name;
            ReturnData.mailmaga_disable_flag = RegisterData.mailmaga_disable_flag;
            ReturnData.dm_disable_flag = RegisterData.dm_disable_flag;
            ReturnData.sex = RegisterData.sex;
            ReturnData.birth = RegisterData.birth;
            ReturnData.member_type = RegisterData.member_type;
            ReturnData.card_issue_date = RegisterData.card_issue_date;
            ReturnData.card_flg = RegisterData.card_flg;
        }

        private void frmQRCode1_Load(object sender, EventArgs e)
        {
            shop_auth_code = shopAuthCode.shop_auth_code;
            if (cardid != "")
            {
                lbtitle_Card.Visible = true;
                lbCardid.Visible = true;
                lbtitle_Card.Visible = true;
                lbtitle1.Visible = true;
                lbtitle2.Visible = true;
                lbtitle3.Visible = false;
                lbCardid.Text = cardid.Substring(0,4) + "-" + cardid.Substring(4,6);
            }
            else
            {
                lbtitle_Card.Visible = false;
                lbCardid.Visible = false;
                lbtitle_Card.Visible = false;
                lbtitle1.Visible = false;
                lbtitle2.Visible = false;
                lbtitle3.Visible = true;
            }

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

        private void timerReadQRcode_Tick(object sender, EventArgs e)
        {
            try
            {
                BarcodeReader Reader = new BarcodeReader();
                Image picture = null;

                //if (InvokeRequired)
                //{
                    pictureBox.Invoke(new MethodInvoker(delegate { picture = pictureBox.Image; }));
                //}
                if (picture != null)
                {
                    Result result = Reader.Decode((Bitmap)picture);
                    if (result != null)
                    {
                        string decode = result.ToString().Trim();
                        if (decode != "")
                        {
                            qr_ = decode;
                            timerReadQRcode.Stop();

                            if (FinalFrame.IsRunning == true) { FinalFrame.Stop(); }
                            switch (frmName)
                            {
                                case "frmSelect":
                                    //data = new string[31];
                                    RegisterData = new MemberImportRequest();
                                    if (RunClient_PostData_QR(_logger))
                                    {
                                        if (!String.IsNullOrEmpty(RegisterData.card_no))
                                        {
                                            _logger.Warn("QR会員証には既にポイントカードが作成済み");
                                            string cardId = Common.Mid(RegisterData.card_no, 1, 4) + "-" + Common.Mid(RegisterData.card_no, 5, 6);
                                            DialogResult dt = MsgBox.Show("この会員には既にポイントカードが作成済みです。カード番号は「" + cardId + "]です。新しいカードを再発行しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                                            switch (dt)
                                            {
                                                case DialogResult.OK:
                                                    if (this._frm.ResetOpenWindow("frmReissue"))
                                                    {
                                                        frmReissue childForm = new frmReissue(_frm);
                                                        childForm.MdiParent = _frm;
                                                        childForm.WindowState = FormWindowState.Maximized;
                                                        childForm.Show();
                                                    }
                                                    break;
                                                case DialogResult.Cancel:
                                                    runCamera();
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            _logger.Info("QR会員証を読み成功");
                                            if (this._frm.ResetOpenWindow("frmRegister"))
                                            {
                                                frmRegister childForm = new frmRegister(_frm, this.Name.ToString(), RegisterData);
                                                childForm.MdiParent = _frm;
                                                childForm.WindowState = FormWindowState.Maximized;
                                                childForm.Show();
                                            }
                                        }
                                    }
                                    break;
                                case "frmConfirm":
                                case "frmCardInput":

                                    if (!Common.CheckQRCode_CardMachine(qr_))
                                    {
                                        _logger.Error("不正なQRコードです。");
                                        DialogResult result1 = MsgBox.Show("不正なQRコードです。", "不正QRコード", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        if (result1 == DialogResult.OK)
                                        {
                                            runCamera();
                                        }
                                    }
                                    else
                                    {
                                        if (RunClient_PostData_Register(_logger))
                                        {
                                            if (recipt_id != 0)
                                            {
                                                if (Data.updateMemberData(RegisterData))
                                                {
                                                    _logger.Info("会員情報の登録に成功しました。会員番号 : "+ RegisterData.member_id);
                                                    if (this._frm.ResetOpenWindow("frmFinish"))
                                                    {
                                                        frmFinish childForm = new frmFinish(_frm);
                                                        childForm.MdiParent = _frm;
                                                        childForm.WindowState = FormWindowState.Maximized;
                                                        childForm.Show();
                                                    }
                                                }
                                                else
                                                {
                                                    MsgBox.Show("データベースエラー(申込一覧 削除エラー)。受付番号　: " + recipt_id, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            else
                                            {
                                                if (Data.insertMemberData(RegisterData))
                                                {
                                                    _logger.Info("会員情報の登録に成功しました。会員番号 : " + RegisterData.member_id);
                                                    if (this._frm.ResetOpenWindow("frmFinish"))
                                                    {
                                                        frmFinish childForm = new frmFinish(_frm);
                                                        childForm.MdiParent = _frm;
                                                        childForm.WindowState = FormWindowState.Maximized;
                                                        childForm.Show();
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    break;
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

        public bool RunClient_PostData_QR(ILog logger)
        {
            ApiClient client = new ApiClient(logger);
            MemberExportRepuest dtMember = new MemberExportRepuest();
            dtMember.shop_auth_code = shop_auth_code;
            dtMember.qr_code = qr_;
            dtMember.with_data_flag = 1;

            JsonValue jsonRes = client.PostMemberEx(dtMember);

            if ((string)jsonRes["code"] == "200")
            {
                RegisterData.GetJsonData(jsonRes);
                return true;
            }
            else
            {
                logger.ErrorFormat("API Error code: {0} with note: {1}", jsonRes["code"], jsonRes["note"]);
                DialogResult result = MsgBox.Show("会員情報の取得に失敗しました。再度、QRコードをかざしてください。", "会員情報の取得に失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    runCamera();
                }
                return false;
            }
        }

        public bool RunClient_PostData_Register(ILog logger)
        {
            ApiClient client = new ApiClient(logger);
            MemberImportRequest dtPost = new MemberImportRequest();
            dtPost = RegisterData;
            dtPost.shop_auth_code = shop_auth_code;
            dtPost.card_no = cardid;
            dtPost.qr_code = qr_;
            if (String.IsNullOrEmpty(dtPost.zip_1) && String.IsNullOrEmpty(dtPost.zip_2))
            { dtPost.zip_1 = "000"; dtPost.zip_2 = "0000"; }

            if (String.IsNullOrEmpty(dtPost.pref_name)) dtPost.pref_name = "x";

            if (String.IsNullOrEmpty(dtPost.area_name)) dtPost.area_name = "x";

            if (String.IsNullOrEmpty(dtPost.city_name)) dtPost.city_name = "x";

            if (String.IsNullOrEmpty(dtPost.block)) dtPost.block = "x";

            if (String.IsNullOrEmpty(dtPost.tel_number_1) && String.IsNullOrEmpty(dtPost.tel_number_2) && String.IsNullOrEmpty(dtPost.tel_number_3) && String.IsNullOrEmpty(dtPost.mobile_number_1) && String.IsNullOrEmpty(dtPost.mobile_number_2)
                && String.IsNullOrEmpty(dtPost.mobile_number_3) && String.IsNullOrEmpty(dtPost.other_tel_number_1) && String.IsNullOrEmpty(dtPost.other_tel_number_2) && String.IsNullOrEmpty(dtPost.other_tel_number_3))
            {

                dtPost.tel_number_1 = "0000"; dtPost.tel_number_2 = "0000"; dtPost.tel_number_3 = "0000";
            }

            if (String.IsNullOrEmpty(dtPost.mail_address))
            {
                dtPost.mail_address = cardid.Substring(0, 4) + "-" + cardid.Substring(4, 6) + "@waitingpass.com";
            }

            if (!String.IsNullOrEmpty(dtPost.member_id )) { dtPost.password = ""; }

            JsonValue jsonRes = client.PostMemberIm(dtPost);

            if ((string)jsonRes["code"] != "200")
            {
                if ((string)jsonRes["code"] == "405")
                {
                    logger.Error("カードのQRコードは既に利用中");
                    string strMesg = null;
                    if (frmName == "frmCardInput") strMesg = "不正なQRコードです。";
                    else { strMesg = "このカードのQRコードは既に利用中です。他の新しいカードで再発行をやり直してください。"; }

                    DialogResult result1 = MsgBox.Show(strMesg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (result1 == DialogResult.OK)
                    {
                        runCamera();
                    }
                }
                else
                {
                    logger.ErrorFormat("API Error code: {0} with note: {1}", jsonRes["code"], jsonRes["note"]);
                    displayMess(jsonRes["code"].ToString(), jsonRes["note"]);
                    return false;
                }

                return false;
            }
            else
            {
                resData.GetJsonData(jsonRes);
                dtPost.member_id = resData.member_id.ToString();
                dtPost.mail_address = resData.mail_address;
                dtPost.receipt_date = !String.IsNullOrEmpty(dtPost.receipt_date) ? dtPost.receipt_date : DateTime.Now.ToString();
                dtPost.receipt_id = recipt_id.ToString()??"";
                dtPost.card_issue_date = DateTime.Now.ToString();
                dtPost.card_flg = "TRUE";
            }
            RegisterData = dtPost;
            return true;
        }

        private void btn_qr_cancel_Click(object sender, EventArgs e)
        {
            if (FinalFrame.IsRunning == true)
            {
                FinalFrame.Stop();
            }

            switch (frmName)
            {
                case "frmConfirm":
                case "frmCardInput":
                    if (this._frm.ResetOpenWindow("frmConfirm"))
                    {
                        frmConfirm childForm = new frmConfirm(_frm, this.Name.ToString(), ReturnData);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    break;
                case "frmSelect":

                    if (this._frm.ResetOpenWindow("frmSelect"))
                    {
                        frmSelect childForm = new frmSelect(_frm);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    break;
            }
        }

        private void displayMess(string code, JsonValue jsonError)
        {
            if (code == "400")
            {
                string errorMsg = null;

                foreach (var err in jsonError)
                {
                    errorMsg += Environment.NewLine + jsonError[err.Key].ToString().Substring(2, jsonError[err.Key].ToString().Length - 4);
                }

                DialogResult result = MsgBox.Show("会員情報の登録に失敗しました。" + errorMsg + Environment.NewLine + "カードを確認してください。", "会員情報の登録に失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                if(result == DialogResult.OK)
                {
                    if (this._frm.ResetOpenWindow("frmConfirm"))
                    {
                        frmConfirm childForm = new frmConfirm(_frm, this.Name.ToString(), ReturnData);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                }
            }
            else
            {
                DialogResult result = MsgBox.Show("会員情報の登録に失敗しました。" + jsonError.ToString() + "。再度スキャンしてください。", "会員情報の登録に失敗", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                switch (result)
                {
                    case DialogResult.OK:
                        runCamera();
                        break;
                    case DialogResult.Cancel:
                        if (this._frm.ResetOpenWindow("frmSelect"))
                        {
                            frmSelect childForm = new frmSelect(_frm);
                            childForm.MdiParent = _frm;
                            childForm.WindowState = FormWindowState.Maximized;
                            childForm.Show();
                        }
                        break;
                }
            }
        }
        private void runCamera()
        {
            pictureBox.Image = null;
            if (FinalFrame.IsRunning == false) FinalFrame.Start();
            FinalFrame.Start();

            // start time
            timerReadQRcode.Enabled = false;
            timerReadQRcode.Enabled = true;
            timerReadQRcode.Start();
        }
    }
}
