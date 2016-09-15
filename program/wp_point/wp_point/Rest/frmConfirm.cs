using System;
using System.Text;
using System.Windows.Forms;
using _wp_point.Rest.Class;
using System.Drawing;
using log4net;
using System.Reflection;
using AForge.Video.DirectShow;
using System.Text.RegularExpressions;
using System.Json;
using System.IO;
using _wp_point.Rest.Request;
using System.Configuration;
using System.Data.SqlClient;

namespace _wp_point.Rest
{
    public partial class frmConfirm : Form
    {
        private readonly frmMain _frm;
        private FilterInfoCollection captureDevice;
        private readonly string _frmName;
        private MemberImportRequest RegisterData;
        private ResponseData_MemberInfo_Reference Res_MemberInfo;
        private ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static Boolean Status_finish_flag; //ステータス要求中フラグ
        public static Boolean Check_card_flag; //true: 新しいカード；false：データの入ったカード

        public static Boolean Status_event_flag; //ステータス要求中フラグ
        public static Boolean Card_event_flag;   //カードデータ要求中フラグ
        public static Boolean Name_event_flag;   //名前データ要求中フラグ
        public static Boolean MemoryInitialFlg;  //ICメモリデータ要求フラグ

        public static Boolean SpCardFlg; //特殊カード挿入中フラグ(True:特殊カードが入っている)
        public static Boolean CardDataReadFlg; //カードデータ読み込みフラグ(True:読み込み済み)
        public static Boolean PortOpenFlg; //ポートが開いているかどうか
        public static Boolean PortCloseFlg; //ポートクローズフラグ(データ送受信中はクローズしない)
        public static Boolean EndFlg; //終了フラグ
        public static int PreErrCode; //前回のエラーコード
        public static int CardCheckWait; //ウェイト(100ms)
        public static string CardId;
        public string CardName;
        public string inputdata;
        public static string password;

        #region スクロール対応
        private bool MoveVectorFlag;
        private float MoveVector;

        private int StartMouseX;
        private int StartMouseY;
        private int EndMouseX;
        private int EndMouseY;
        private int StartScrollNum;

        private DateTime StartTime;
        private DateTime EndTime;
        #endregion

        public frmConfirm(frmMain fr, string frmName, MemberImportRequest registerData)
        {
            InitializeComponent();
            this._frm = fr;
            this._frmName = frmName;
            RegisterData = registerData;
            UserInitialize();
            MoveVectorFlag = false;
            Res_MemberInfo = new ResponseData_MemberInfo_Reference();
        }
        private void frmConfirm_Load(object sender, EventArgs e)
        {
            panelGuide.Visible = false;
            Check_card_flag = false;
            //フラグ初期化
            CardName = "";
            inputdata = "";
            Status_finish_flag = false;
            CardDataReadFlg = false;
            SpCardFlg = false;
            PortOpenFlg = false;
            PortCloseFlg = false;
            Status_event_flag = false;
            Card_event_flag = false;
            Name_event_flag = false;
            MemoryInitialFlg = false;
            EndFlg = false;

            //ウェイトクリア
            CardCheckWait = 0;

            //エラーコード初期化
            PreErrCode = 0;

            timerScrollUpdate.Start();

            //申込一覧へデータを入れる。
            InputDB_RequestList();

        }

        private void InputDB_RequestList()
        {
            try
            {
                if (RegisterData.receipt_id == "0")
                {
                    SqlDataReader rd = (null);
                    SqlConnection con = new SqlConnection();

                    if (Common.SqlCon(con))
                    {
                        SqlCommand cmdReceipt = new SqlCommand("SELECT NEXT VALUE FOR SEQ_RECEIPT_ID AS RECEIPT_ID", con);
                        rd = cmdReceipt.ExecuteReader();
                        while (rd.Read())
                        {
                            RegisterData.receipt_id = rd["RECEIPT_ID"].ToString();
                        }
                        rd.Close();
                    }

                    if (!Data.insertMemberData(RegisterData))
                    {
                        MsgBox.Show("データベースエラー(申込一覧 追加エラー)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    if (!Data.updateMemberData(RegisterData))
                    {
                        MsgBox.Show("データベースエラー(申込一覧 更新エラー)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserInitialize()
        {
            if (!String.IsNullOrEmpty(RegisterData.member_id))
            {
                RegisterData.password = ""; //btnExistCard.Visible = true;
            }else {}

            if (!String.IsNullOrEmpty(RegisterData.password))
            {
                password = RegisterData.password;
                string display_pass = "";
                display_pass = Common.Mid(password, 1, 2);
                for (int i = 0; i < password.Length - 2; i++)
                {
                    display_pass = display_pass + "*";
                }
                lbpass.Text = display_pass;
            }
            else
            {
                lbpass.Text = "変更なし";
            }

            lbfullName.Text = RegisterData.last_name + ' ' + RegisterData.first_name;
            lbfullName_kana.Text = RegisterData.last_name_y + ' ' + RegisterData.first_name_y;
            lbname.Text = RegisterData.call_name;
            lbpostCode.Text = "〒" + RegisterData.zip_1 + '-' + RegisterData.zip_2;
            lbAddress.Text = RegisterData.pref_name + RegisterData.city_name + RegisterData.area_name + RegisterData.block;
            lbbuilname.Text = RegisterData.building;
            lbhomePhone.Text = RegisterData.tel_number_1 + '-' + RegisterData.tel_number_2 + '-' + RegisterData.tel_number_3;
            lbmobilePhone.Text = RegisterData.mobile_number_1 + '-' + RegisterData.mobile_number_2 + '-' + RegisterData.mobile_number_3;
            lbprePhone.Text = RegisterData.other_tel_number_1 + '-' + RegisterData.other_tel_number_2 + '-' + RegisterData.other_tel_number_3;

            if (RegisterData.sex == "1")
            {
                lbgender.Text = "男性";
            }
            else
            {
                lbgender.Text = "女性";
            }

            if (String.IsNullOrEmpty(RegisterData.birth))
            {
                lbbirthday.Text = "--";
            }
            else
            {
                lbbirthday.Text = RegisterData.birth;
            }

            if (RegisterData.mail_address != "")
            {
                lbEmail.Text = RegisterData.mail_address;
            }
            else
            {
                lbEmail.Text = "カード番号@waitingpass.com";
            }

            if (RegisterData.mailmaga_disable_flag == "2")
            {
                lbmz.Text = "希望する";
            }
            else
            {
                lbmz.Text = "希望しない";
            }

            if (RegisterData.dm_disable_flag == "2")
            {
                lbdr.Text = "希望する";
            }
            else
            {
                lbdr.Text = "希望しない";
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //カメラ　チェック
            if (!CheckCamera())
            {
                return;
            }

            if (PortOpenFlg)
            {
                //ポートクローズ
                PortCloseCheck();
            }
            else
            {
                try
                {
                    TcsPc100.port_hndl = new IntPtr(0);
                    short portNo = Convert.ToInt16(Common.GetSetting<int>("portno"));
                    int baud = Common.GetSetting<int>("baud");
                    PreErrCode = 0;

                    TcsPc100.ret = TcsPc100.OpenPort(ref TcsPc100.port_hndl, portNo, baud);
                    if (TcsPc100.ret != 0)
                    {
                        _logger.Error("カード機との通信に失敗しました。");
                        DispErrorMessage(TcsPc100.ret);
                        return;
                    }
                    else
                    {
                        PortOpenFlg = true;
                        timer_Card.Enabled = true;

                        panelGuide.Visible = true;
                        lbguide_001.Visible = false;
                        lbguide_002.Text = "カード機の「F4：新規」ボタンを押して、";
                        lbguide_002.Location = new Point(34, 124);
                        lbguide_003.Text = "新しいカードを挿入してください。";
                        lbguide_003.Location = new Point(72, 173);
                        lbguide_004.Visible = false;
                        CardName = "";
                        panelButton.Enabled = false;
                        btnExistCard.Enabled = false;
                    }
                }
                catch
                {
                    _logger.Error("ポイントカード機器との通信に失敗しました。");
                    MsgBox.Show("ポイントカード機器との通信に失敗しました。", "通信ポートエラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

            }
        }

        private void timer_Card_Tick(object sender, EventArgs e)
        {
            TcsPc100.ret = TcsPc100.RmGetStatus(ref TcsPc100.port_hndl, ref TcsPc100.card, ref TcsPc100.rwstatus, ref TcsPc100.mvmode, ref TcsPc100.icsstatus, ref TcsPc100.datanum, ref TcsPc100.datamax, ref TcsPc100.errcode);
            if (TcsPc100.ret != 0)
            {
                DispErrorMessage(TcsPc100.ret);
            }
            else
            {
                //PreErrCode = 0;
                DispErrorMessage(TcsPc100.errcode);
            }
            if (PortCloseFlg) PortCloseCheck();

            //ｶｰﾄﾞが挿入された？
            if ((TcsPc100.rwstatus == 1) && !CardDataReadFlg && !SpCardFlg)
            {
                //ウェイト(800ms)
                timer_Wait.Enabled = true;
                do
                {
                    CardCheckWait = CardCheckWait + 1;
                } while (CardCheckWait < 8);

                timer_Wait.Enabled = false;
                CardCheckWait = 0;

                TcsPc100.ret = TcsPc100.RmGetStatus(ref TcsPc100.port_hndl, ref TcsPc100.card, ref TcsPc100.rwstatus, ref TcsPc100.mvmode, ref TcsPc100.icsstatus, ref TcsPc100.datanum, ref TcsPc100.datamax, ref TcsPc100.errcode);
                if (TcsPc100.ret != 0)
                {
                    DispErrorMessage(TcsPc100.ret);
                }
                else
                {
                    //PreErrCode = 0;
                    DispErrorMessage(TcsPc100.errcode);
                }
                if (PortCloseFlg) PortCloseCheck();

                //違うｶｰﾄﾞが挿入されたか？
                if (TcsPc100.rwstatus == 1) //正しいカード
                {
                    //カードデータ要求
                    Card_event_flag = true;

                    TcsPc100.ret = TcsPc100.RmGetCardData(ref TcsPc100.port_hndl, TcsPc100.cardid, ref TcsPc100.ownpoint, ref TcsPc100.usecount, TcsPc100.usedate, ref TcsPc100.salevalue, ref TcsPc100.addpoint, TcsPc100.limit,
                        ref TcsPc100.expired, ref TcsPc100.namedata, TcsPc100.birthday, ref TcsPc100.memlist, ref TcsPc100.ptype);
                    if (TcsPc100.ret != 0)
                    {
                        DispErrorMessage(TcsPc100.ret);
                    }
                    else
                    {
                        //エラーコードクリア
                        PreErrCode = 0;

                        //特殊カードフラグクリア
                        SpCardFlg = false;

                        //カードデータを読み込む
                        CardId = Encoding.GetEncoding("shift_jis").GetString(TcsPc100.cardid);
                        if (CardId.Length > 10) { CardId = CardId.Substring(0, 10); }

                        CardDataReadFlg = true; //すでにカードデータを読み込んだ

                        //名前データ要求
                        Name_event_flag = true;
                        TcsPc100.ret = TcsPc100.RmGetName(ref TcsPc100.port_hndl, TcsPc100.namedata2);

                        if (TcsPc100.ret != 0)
                        {
                            DispErrorMessage(TcsPc100.ret);
                        }
                        else
                        {
                            PreErrCode = 0;
                        }
                        Name_event_flag = false;
                        if (PortCloseFlg) PortCloseCheck(); //ポートクローズ
                        //名前データ
                        CardName = Encoding.GetEncoding("shift_jis").GetString(TcsPc100.namedata2);
                        CardName = CardName.Replace(" ", "");
                        CardName = CardName.Replace("\0", "");
                        if (String.IsNullOrEmpty(CardName) == false)
                        {
                            MsgBox.Show("新しいカードを挿入してください。","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            lbguide_002.Text = "引き続きカード機で";
                            lbguide_002.Location = new Point(172, 124);
                            lbguide_003.Text = "発行処理を進めてください。";
                            lbguide_003.Location = new Point(124, 173);
                            lbguide_001.Visible = true;
                            lbguide_004.Visible = true;
                            Check_card_flag = true;
                        }

                        /* 引数設定 */
                        if (Common.GetSetting<int>("sendCardName") == 1)
                        {
                            inputdata = lbfullName.Text;
                            //全角を半角に変換する
                            inputdata = Microsoft.VisualBasic.Strings.StrConv(inputdata, Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
                            //頭から16文字まで
                            if (inputdata.Length > 16)
                            {
                                inputdata = Common.Mid(inputdata, 1, 16);
                            }
                        }
                        else if (Common.GetSetting<int>("sendCardName") == 2)
                        {
                            inputdata = lbfullName_kana.Text;
                            //ひらがなをカタカナに変換し、全角を半角に変換する
                            inputdata = Microsoft.VisualBasic.Strings.StrConv(inputdata, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
                            //頭から16文字まで
                            if (inputdata.Length > 16)
                            {
                                inputdata = Common.Mid(inputdata, 1, 16);
                            }
                        }

                        TcsPc100.namedata3 = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(inputdata); /* 名前データ */
                        TcsPc100.ret = TcsPc100.RmSendName(ref TcsPc100.port_hndl, TcsPc100.namedata3);
                        if (TcsPc100.ret != 0)
                        {
                            DispErrorMessage(TcsPc100.ret);
                        }
                        else
                        {
                            Status_finish_flag = true;
                        }
                    }

                    Card_event_flag = false;
                    if (PortCloseFlg) PortCloseCheck();
                }
                else
                {
                    //ウェイト(1s)
                    timer_Wait.Enabled = true;
                    do
                    {
                        CardCheckWait = CardCheckWait + 1;
                    }
                    while (CardCheckWait < 10);

                    timer_Wait.Enabled = false;
                    CardCheckWait = 0;
                }
            }

            if (TcsPc100.rwstatus != 1)
            {

                SpCardFlg = false;
                CardDataReadFlg = false;
            }

            if ((TcsPc100.card == 0) && Status_finish_flag && (String.IsNullOrEmpty(CardName) == true))
            {
                PortCloseCheck(); //ポートクローズ
                Status_finish_flag = false;

                if (this._frm.ResetOpenWindow("frmQRCode"))
                {
                    frmQRCode childForm = new frmQRCode(_frm, this.Name.ToString(), CardId, RegisterData);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            if (TcsPc100.rwstatus != 1 && !Status_finish_flag && !String.IsNullOrEmpty(CardName))
            {
                CardName = "";
            }
        }
        //機能     : ポートクローズチェック
        private void PortCloseCheck()
        {
            if (Card_event_flag || Name_event_flag)
            {
                PortCloseFlg = true;
            }
            else
            {
                timer_Card.Enabled = false;
                PortCloseFlg = false;
                SpCardFlg = false;
                CardDataReadFlg = false;
                TcsPc100.rwstatus = 0;

                TcsPc100.ret = TcsPc100.ClosePort(ref TcsPc100.port_hndl);
                if (TcsPc100.ret != 0)
                {
                    DispErrorMessage(TcsPc100.ret);
                    EndFlg = false;
                    timer_Card.Enabled = true;
                    return;
                }
                else
                {
                    if (EndFlg)
                    {
                        return;
                    }
                    else
                    {
                        PortOpenFlg = false;
                    }
                }
            }
        }

        private void DispErrorMessage(int Errcode)
        {
            const string ErrTitle = "ステータスエラー";
            string errString = "";

            if (PreErrCode != Errcode)
            {
                switch (Errcode)
                {
                    case 4000:
                        errString = "通信ポートエラー";
                        break;
                    case 4001:
                        errString = "パラメータが違います";
                        break;
                    case 4002:
                        errString = "通信タイムアウト";
                        break;
                    case 4003:
                        if (MemoryInitialFlg)
                        {
                            errString = "メモリカードデータ要求できません";
                        }
                        else if (TcsPc100.rwstatus != 1)
                        {
                            errString = "NAK受信";
                        }
                        else
                        {
                            errString = "特殊カードが挿入されています";
                            SpCardFlg = true;
                        }
                        break;
                    case 4004:
                        errString = "ホスト交信不可です";
                        break;
                    case 4005:
                        errString = "ディスクが異常です";
                        break;
                    case 1:
                        errString = "R/Wセンサー異常 [01]";
                        break;
                    case 2:
                        errString = "カード詰り [02]";
                        break;
                    case 3:
                        errString = "カード誤挿入 [03]";
                        break;
                    case 4:
                        errString = "カード搬送異常 [04]";
                        break;
                    case 5:
                        errString = "カード誤挿入 [05]";
                        break;
                    case 6:
                        errString = "カードリードエラー [06]";
                        break;
                    case 7:
                        errString = "カードリードエラー  [07]";
                        break;
                    case 8:
                        errString = "カードリードエラー  [08]";
                        break;
                    case 9:
                        errString = "カードリードエラー  [09]";
                        break;
                    case 10:
                        errString = "カードライトエラー [0A]";
                        break;
                    case 11:
                        errString = "カードライトエラー [0B]";
                        break;
                    case 12:
                        errString = "カードライトエラー [0C]";
                        break;
                    case 13:
                        errString = "カードライトエラー [0D]";
                        break;
                    case 14:
                        errString = "カード搬送異常 [0E]";
                        break;
                    case 15:
                        errString = "カード搬送異常 [0F]";
                        break;
                    case 22:
                        errString = "R/Wメカユニット開放 [16]";
                        break;
                    case 24:
                        errString = "サーマルヘッド・サーミスタ異常 [18]";
                        break;
                    case 25:
                        errString = "イレーズバー・サーミスタ異常 [19]";
                        break;
                    case 32:
                        errString = "イレーズバー・温度異常 [20]";
                        break;
                    case 256:
                        errString = "カードが違います";
                        break;
                    case 257:
                        errString = "無効カードです";
                        break;
                    case 258:
                        errString = "メモリカード未挿入";
                        break;
                    case 261:
                        errString = "メモリカードが違います";
                        break;
                    case 262:
                        errString = "メモリカードが違います";
                        break;
                    case 263:
                        errString = "メモリカードのデータが満杯で";
                        break;
                    case 265:
                        errString = "メモリカードの関連ファイルがありません";
                        break;
                    case 266:
                        errString = "メモリカード書き込み不良";
                        break;
                    case 267:
                        errString = "エラーカードです 回収してください";
                        break;
                    case 514:
                        errString = "バックアップメモリ異常";
                        break;
                    default:
                        PreErrCode = Errcode;
                        break;
                }

                PreErrCode = Errcode;

                if(errString != "")
                {
                    _logger.Error(errString);
                    MsgBox.Show(errString, ErrTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //ポートクローズ
            if (PortOpenFlg) PortCloseCheck();
            CardName = "";
            if (this._frm.ResetOpenWindow(_frmName))
            {
                switch (_frmName)
                {
                    case "frmQRCode":
                        //frmSelect childForm = new frmSelect(_frm);
                        frmRegister childForm = new frmRegister(_frm, this.Name.ToString(), RegisterData);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                        break;
                    case "frmList":
                        frmList childForm1 = new frmList(_frm);
                        childForm1.MdiParent = _frm;
                        childForm1.WindowState = FormWindowState.Maximized;
                        childForm1.Show();
                        break;
                    case "frmRegister":
                        frmRegister childForm2 = new frmRegister(_frm, this.Name.ToString(), RegisterData);
                        childForm2.MdiParent = _frm;
                        childForm2.WindowState = FormWindowState.Maximized;
                        childForm2.Show();
                        break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Status_finish_flag = false;
            TcsPc100.ret = TcsPc100.RmGetStatus(ref TcsPc100.port_hndl, ref TcsPc100.card, ref TcsPc100.rwstatus, ref TcsPc100.mvmode, ref TcsPc100.icsstatus, ref TcsPc100.datanum, ref TcsPc100.datamax, ref TcsPc100.errcode);
            if (TcsPc100.ret != 0)
            {
                DispErrorMessage(TcsPc100.ret);
            }
            //Clear card name

            if (TcsPc100.rwstatus == 1 && Check_card_flag)
            {
                Check_card_flag = false;
                string inputdata = " ";
                inputdata = Microsoft.VisualBasic.Strings.StrConv(inputdata, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
                TcsPc100.namedata3 = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(inputdata); /* 名前データ */
                TcsPc100.ret = TcsPc100.RmSendName(ref TcsPc100.port_hndl, TcsPc100.namedata3);
                if (TcsPc100.ret != 0)
                {
                    DispErrorMessage(TcsPc100.ret);
                }
                MsgBox.Show("カード機で[取消]ボタンを押して、カードを抜いてください。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (PortOpenFlg) PortCloseCheck();
            panelGuide.Visible = false;
            panelButton.Enabled = true;
            btnExistCard.Enabled = true;
        }

        private bool CheckCamera()
        {
            Common cm = new Common();
            captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (!cm.CheckCamera(captureDevice))
            {
                _logger.Error("カメラとの接続に失敗しました。");
                MsgBox.Show("カメラとの接続に失敗しました。", "接続エラー", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void frmConfirm_Scroll(object sender, ScrollEventArgs e)
        {
            //if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            //{
            //    if (e.NewValue - e.OldValue >= 0)
            //    {
            //        this.VerticalScroll.Value = e.NewValue + 8;
            //        this.Refresh();
            //    }
            //    else
            //    {
            //        if (e.NewValue - 8 >= 0)
            //        {
            //            this.VerticalScroll.Value = e.NewValue - 8;
            //            this.Refresh();
            //        }
            //    }
            //}
        }
        private void frmConfirm_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //MoveVectorFlag = true;
                StartMouseX = Cursor.Position.X;
                StartMouseY = Cursor.Position.Y;
                //StartScrollNum = this.VerticalScroll.Value;

                StartTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void frmConfirm_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                //移動中フラグをオフにする
                MoveVectorFlag = false;

                ////移動量を計算する
                //int a = OldMouseY - Cursor.Position.Y;
                //MoveVector = a;

                ////リセット
                //OldMouseX = 0;
                //OldMouseY = 0;
                EndMouseX = Cursor.Position.X;
                EndMouseY = Cursor.Position.Y;

                //StartMouseX = Cursor.Position.X;
                //StartMouseY = Cursor.Position.Y;
                StartScrollNum = this.VerticalScroll.Value;
                MoveVector = 100;
                if ((StartMouseY - EndMouseY) < 0)
                {
                    MoveVector *= -1;
                }
                else
                {
                    MoveVector *= 1;
                }

                //終了時間
                EndTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void timerScrollUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!MoveVectorFlag)
                {
                    if (MoveVector != 0)
                    {

                        //移動量を減らす
                        if (MoveVector > 1.0) MoveVector = MoveVector * (float)0.8;
                        if (MoveVector < -1.0) MoveVector = MoveVector * (float)0.8;
                        if (MoveVector > -1.0 && MoveVector < 1.0) MoveVector = 0;

                        //慣性で動かす
                        int AddNum = StartScrollNum + (int)MoveVector;
                        if (AddNum < this.VerticalScroll.Maximum
                            && AddNum > this.VerticalScroll.Minimum)
                        {
                            this.VerticalScroll.Value = AddNum;
                            StartScrollNum = AddNum;
                        }

                        //画面を更新
                        //this.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void _KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnRegister.PerformClick();
        }

        private void btnExistCard_Click(object sender, EventArgs e)
        {
            try
            {
                CardId = Common.ShowDialog("カード番号を入力して下さい。", "カード番号入力");

                if (CardId == "-")
                {
                    return;
                }
                else
                {
                    if (!Regex.IsMatch(CardId.Replace("-", ""), "[^0-9]"))
                    {
                        string[] card = CardId.Split('-');
                        if (card[0].Length != 4) CardId = card[0].PadLeft(4, '0');
                        else CardId = card[0];
                        if (card[1].Length != 6) CardId = CardId + card[1].PadLeft(6, '0');
                        else CardId = CardId + card[1];

                        //------------------------------
                        // メンバー情報参照
                        //------------------------------
                        string ShopAuthCode = shopAuthCode.shop_auth_code;
                        ApiClient ac = new ApiClient(_logger);
                        JsonValue JVresult = ac.MemberInfo_Reference(ShopAuthCode, null, CardId, null, null);
                        int code = (int)JVresult["code"];
                        if (code != 200)
                        {
                            if (code == 404)
                            {
                                if (this._frm.ResetOpenWindow("frmQRCode"))
                                {
                                    frmQRCode childForm = new frmQRCode(_frm, "frmCardInput", CardId, RegisterData);
                                    childForm.MdiParent = _frm;
                                    childForm.WindowState = FormWindowState.Maximized;
                                    childForm.Show();
                                }
                            }
                            if (code == 400) MsgBox.Show("リクエストが不正です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            if (code == 401) MsgBox.Show("認証に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (code == 500) MsgBox.Show("システムエラー", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (code == 503) MsgBox.Show("WEBシステムがメンテナンス中です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            return;
                        }
                        else
                        {
                            Res_MemberInfo.GetJsonData(JVresult);
                            if(Res_MemberInfo.member_id == RegisterData.member_id)
                            {
                                if (this._frm.ResetOpenWindow("frmQRCode"))
                                {
                                    frmQRCode childForm = new frmQRCode(_frm, "frmCardInput", CardId, RegisterData);
                                    childForm.MdiParent = _frm;
                                    childForm.WindowState = FormWindowState.Maximized;
                                    childForm.Show();
                                }
                            }
                            else
                            {
                                MsgBox.Show("既に会員に紐づいたカード番号です。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    { MsgBox.Show("カード番号の入力に誤りがあります。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                }
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
