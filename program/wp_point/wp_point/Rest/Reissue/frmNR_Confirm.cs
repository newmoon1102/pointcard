using System;
using System.Text;
using System.Windows.Forms;
using _wp_point.Rest.Class;
using System.Drawing;
using log4net;
using System.Reflection;
using _wp_point.Rest.Request;
using _wp_point.Rest.Reissue;
using AForge.Video.DirectShow;
using System.Text.RegularExpressions;
using System.Json;

namespace _wp_point.Rest
{
    public partial class frmNR_Confirm : Form
    {
        private readonly frmMain _frm;
        private FilterInfoCollection captureDevice;
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

        public static ResponseData_MemberInfo_Reference Res_MemberInfo;
        public ReissueQRCodeData OldReissueQRData;

        public static string password;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fr"></param>
        /// <param name="processno">1:QRコード 2:カードNo</param>
        /// <param name="code"></param>
        public frmNR_Confirm(frmMain fr, ResponseData_MemberInfo_Reference data, ReissueQRCodeData olddata)
        {
            InitializeComponent();
            this._frm = fr;
            Res_MemberInfo = data;
            UserInitialize();
            OldReissueQRData = olddata;
        }



        private void frmRegister_Load(object sender, EventArgs e)
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
        }

        private void UserInitialize()
        {
            lbCard_no.Text = Res_MemberInfo.card_no.Substring(0,4) + "-" + Res_MemberInfo.card_no.Substring(4, 6);
            lbPoint.Text = Res_MemberInfo.point + " P";
            lbfullName.Text = Res_MemberInfo.last_name + ' ' + Res_MemberInfo.first_name;
            lbfullName_kana.Text = Res_MemberInfo.last_name_y + ' ' + Res_MemberInfo.first_name_y;
            lbname.Text = Res_MemberInfo.call_name;
            lbpostCode.Text = "〒" + Res_MemberInfo.zip_1 + '-' + Res_MemberInfo.zip_2;
            lbAddress.Text = Res_MemberInfo.pref_name + Res_MemberInfo.area_name + Res_MemberInfo.city_name + Res_MemberInfo.block;
            lbbuilname.Text = Res_MemberInfo.building;
            lbhomePhone.Text = Res_MemberInfo.tel_number_1 + '-' + Res_MemberInfo.tel_number_2 + '-' + Res_MemberInfo.tel_number_3;
            lbmobilePhone.Text = Res_MemberInfo.mobile_number_1 + '-' + Res_MemberInfo .mobile_number_2+ '-' + Res_MemberInfo.mobile_number_3;
            lbprePhone.Text = Res_MemberInfo.other_tel_number_1 + '-' + Res_MemberInfo.other_tel_number_2 + '-' + Res_MemberInfo.other_tel_number_3;

            if (Res_MemberInfo.sex == "1")
            {
                lbgender.Text = "男性";
            }
            else
            {
                lbgender.Text = "女性";
            }

            if (!String.IsNullOrEmpty(Res_MemberInfo.birth))
            {
                lbbirthday.Text = Res_MemberInfo.birth.Replace("-","/");
            }
            else
            {
                lbbirthday.Text = "--";
            }

            lbEmail.Text = Res_MemberInfo.mail_address;
            lbpass.Text = "変更なし";

            if (Res_MemberInfo.mailmaga_disable_flag == "2")
            {
                lbmz.Text = "希望する";
            }
            else
            {
                lbmz.Text = "希望しない";
            }

            if (Res_MemberInfo.dm_disable_flag == "2")
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
                        lbguide_004.Visible = false;
                        CardName = "";
                        panelButton.Enabled = false;
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
                PreErrCode = 0;
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
                    PreErrCode = 0;
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
                            MsgBox.Show("新しいカードを挿入してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                if (this._frm.ResetOpenWindow("frmNR_NewQRCodeInput"))
                {
                    //新しカード番号を交換
                    Res_MemberInfo.card_no = CardId.Substring(0,10);

                    frmNR_NewQRCodeInput childForm = new frmNR_NewQRCodeInput(_frm, Res_MemberInfo, OldReissueQRData);
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

                if (errString != "")
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
            if (this._frm.ResetOpenWindow("frmNotRead"))
            {
                frmNotRead childForm2 = new frmNotRead(_frm);
                childForm2.MdiParent = _frm;
                childForm2.WindowState = FormWindowState.Maximized;
                childForm2.Show();
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
        }

        private bool CheckCamera()
        {
            Common cm = new Common();
            captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (!cm.CheckCamera(captureDevice))
            {
                _logger.Error("カメラとの接続に失敗しました");
                MsgBox.Show("カメラとの接続に失敗しました。", "接続エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void frmNR_Confirm_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (e.NewValue - e.OldValue >= 0)
                {
                    this.VerticalScroll.Value = e.NewValue + 8;
                    this.Refresh();
                }
                else
                {
                    if (e.NewValue - 8 >= 0)
                    {
                        this.VerticalScroll.Value = e.NewValue - 8;
                        this.Refresh();
                    }
                }
            }
        }

        private void btnInCardNo_Click(object sender, EventArgs e)
        {
            try
            {
                //--------------------------------------------------
                // カード番号入力POPUPを出す
                //--------------------------------------------------
                CardId = Common.ShowDialog("新しいカードのカード番号を入力して下さい。", "カード番号入力");

                //結果番号が無い場合、抜ける
                if (CardId == "-") return;

                //--------------------------------------------------
                // 番号が入力された場合
                //--------------------------------------------------
                //------------------------------
                // 入力文字列のチェック
                // 数値以外の文字があった場合、抜ける
                //------------------------------
                if (Regex.IsMatch(CardId.Replace("-", ""), "[^0-9]"))
                {
                    MsgBox.Show("カード番号の入力に誤りがあります。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //------------------------------
                // カード番号文字列の補正（ゼロ埋め）
                //------------------------------
                string[] card = CardId.Split('-');
                if (card[0].Length != 4) CardId = card[0].PadLeft(4, '0');
                else CardId = card[0];
                if (card[1].Length != 6) CardId = CardId + card[1].PadLeft(6, '0');
                else CardId = CardId + card[1];

                //------------------------------
                // メンバー情報参照
                //------------------------------
                //ショップAuthコード取得
                string ShopAuthCode = shopAuthCode.shop_auth_code;
                //通信Class取得
                ApiClient ac = new ApiClient(_logger);
                //メンバー情報参照
                JsonValue JVresult = ac.MemberInfo_Reference(ShopAuthCode, null, CardId, null, null);
                //参照結果
                int code = (int)JVresult["code"];

                //------------------------------
                // 応答コードにより処理分岐
                //------------------------------
                //該当データが無い場合
                if(code == 404)
                {
                    if (this._frm.ResetOpenWindow("frmNR_NewQRCodeInput"))
                    {
                        //新しカード番号を交換
                        Res_MemberInfo.card_no = CardId.Substring(0, 10);

                        frmNR_NewQRCodeInput childForm = new frmNR_NewQRCodeInput(_frm, Res_MemberInfo, OldReissueQRData);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    
                    return;
                }
                //該当データが有る場合
                if (code == 200)
                {
                    MsgBox.Show("既に会員に紐づいたカード番号です。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
                else
                {
                    //その他エラーコード
                    if (code == 400) MsgBox.Show("リクエストが不正です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (code == 401) MsgBox.Show("認証に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (code == 500) MsgBox.Show("システムエラー", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (code == 503) MsgBox.Show("WEBシステムがメンテナンス中です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
