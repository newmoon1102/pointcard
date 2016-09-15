using AForge.Video.DirectShow;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace _wp_point.Rest.Class
{
    public struct ReissueQRCodeData
    {
        public string CardNo;
        public string CardQRCode;
    }

    public struct ReplaceData
    {
        public string memberId;
        public string memberName;
    }

    public static class shopAuthCode
    {
        public static string shop_auth_code { get; set; }
    }

    public struct CardNo
    {
        public string OldCardNo;
        public string NewCardNo;
        public string NewCardName;
    }

    public class Common
    {
        #region TCSPC100関連

        //構造体
        public struct TCSPC100TradeData_Header
        {
            public string TerminalCode;     //端末コード(企業コード4桁＋店舗コード4桁＋端末番号2桁)
            public int RecordNum;           //レコード数：６バイト
            public string Version;          //バージョン(取引記述コード1桁＋アプリケーション4桁＋運用モード2桁＋設定モード3桁)
        }

        public struct TCSPC100TradeData_DataRecord_TypeC { }

        public struct TCSPC100TradeData_DataRecord_TypeD
        {
            public DateTime datetime;           //利用日＋利用時間
            public string MembersNo;            //会員番号(発行店舗コード4桁＋会員コード6桁＋チェックマーク2桁)
            public string ServiceCode;          //サービスコード(入力されたサービスコード数値または@+数値、@@@@@@、%%%%%%、空白)
            public int AmountSolid;             //売上金額（符号＋基本売上金額、マイナスは返品）
            public int AmountPoint;             //売上ポイント（符号＋基本演算ポイント、マイナスは返品）
            public int DoublePoint;             //倍ポイント
            public int CountPoint;              //回数ポイント
            public int SpecialPoint;            //特別ポイント
            public int NewPoint;                //新規、新規プレミアム、入会または任意ポイント
            public int CumulativePoint;         //累計ポイント
            public int TradePoint;              //交換ポイント
            public int UseCount;                //利用回数
            public DateTime LastDate;           //前回利用日
            public DateTime ExpirationDate;     //有効期限
            public int AddUpAmountSolid;        //積算売上金額
            public int TaxIncludedKbn;          //内税区分
            public int CorrectionKbn;           //訂正区分
            public int TradeKbn;                //取引区分
            public int LimitDateUpdateKbn;      //期限更新区分
            public int RankKbn;                 //ランク区分
            public int MemberCollationKbn;      //会員照合区分
            public int NameCollationKbn;        //名前照合区分
            public string RecordNumber;         //レコード番号（000001～999999）
            public string ResponsibleCode;      //担当者コード
            public int InPrice;                 //入金額
            public int PremiumPrice;            //付加プレミアム額：符号＋プレミアム額（マイナスは返金処理）
            public int RemainingAmount;         //残額
            public int PriCordFlag;             //プリカ処理フラグ
            public string BarcodeData;          //バーコードデータ（機能予約のみ）
            public string TakeoverData;         //引継発行番号データ
            public int SettlementPreAmount;     //決済プレミアム額

            public DateTime ReservationDate1;   //予約日付１
            public DateTime ReservationDate2;   //予約日付１
            public string Name;                 //名前
            public int NameWriteFlag;           //名前書込フラグ
            public string Furigana;             //名前入力かなデータ
            public DateTime PriExpirationDate;  //プリカ有効期限
            public string Dummy1;               //予備1
            public string Dummy2;               //予備2
        }

        public struct CorpCodeData
        {
            public string CorpCode;
            public string CorpName;
        }
        public struct TempoCodeData
        {
            public string TempoCode;
            public string TempoName;
        }

        #endregion

        #region QRコード関連
        public static bool CheckQRCode_CardMachine(string qr_code)
        {
            if (Properties.Settings.Default.QRCodeFixedString != "")
            {
                if (qr_code.Substring(0, 7) != Properties.Settings.Default.QRCodeFixedString)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region キーボード関連
        //define vitual keyboard
        static string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
        static string onScreenKeyboardPath = System.IO.Path.Combine(progFiles, "TabTip.exe");

        internal static void startKeyboard()
        {
            Process.Start(onScreenKeyboardPath);
        }

        internal static void closeKeyboard()
        {
            //Kill all on screen keyboards
            Process[] oskProcessArray = Process.GetProcessesByName("TabTip");
            foreach (Process onscreenProcess in oskProcessArray)
            {
                onscreenProcess.Kill();
            }
        }
        #endregion


        static Regex validEmailRegex = CreateValidEmailRegex();

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        internal static bool EmailIsValid(string emailAddress)
        {
            bool isValid = validEmailRegex.IsMatch(emailAddress);

            return isValid;
        }

        internal static bool SqlCon(SqlConnection con)
        {
            try
            {
                con.ConnectionString = Properties.Settings.Default.POINTCARDConnString;
                con.Open();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        internal static bool checkTextLenght(string strText, int maxLeng, int minLeng)
        {
            string strHan = strText;
            bool flg_check_max_lenght = false;
            bool flg_check_min_lenght = false;
            strHan = Microsoft.VisualBasic.Strings.StrConv(strHan, Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);

            if (maxLeng != 0 && !String.IsNullOrEmpty(strHan))
            {
                if (strHan.Length > maxLeng) flg_check_max_lenght = false;
                else
                {
                    flg_check_max_lenght = true;
                }
            }
            else
            {
                flg_check_max_lenght = true;
            }

            if (minLeng != 0 && !String.IsNullOrEmpty(strHan))
            {
                if (strHan.Length < minLeng) flg_check_min_lenght = false;
                else
                {
                    flg_check_min_lenght = true;
                }
            }
            else
            {
                flg_check_min_lenght = true;
            }

            return flg_check_max_lenght & flg_check_min_lenght;
        }

        internal static bool IsSafeChar(string str)
        {
            try
            {
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                byte[] bytes;
                string chkCode = "";
                string new_str = "";
                int num16 = 0;
                int num32 = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    //全角を半角に変換する
                    new_str = Microsoft.VisualBasic.Strings.StrConv(str[i].ToString(), Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);

                    bytes = Encoding.ASCII.GetBytes(new_str);
                    chkCode = "";
                    foreach (byte item in bytes)
                    {
                        chkCode = chkCode + Convert.ToString(item, 16);
                    }

                    //10進数の整数に直す
                    num32 = Convert.ToInt32(chkCode, 16);
                    if (num32 == 34 || num32 == 44)
                    {
                        return false;
                    }

                    bytes = sjisEnc.GetBytes(str[i].ToString());
                    chkCode = "";
                    foreach (byte item in bytes)
                    {
                        chkCode = chkCode + Convert.ToString(item, 16);
                    }

                    //10進数の整数に直す
                    num16 = Convert.ToInt32(chkCode, 16);

                    //半角カナ(00A0～00FF)を含むか
                    if (160 <= num16 && num16 <= 255)
                    {
                        return false;
                    }
                    //特殊文字(8540～889E)を含むか
                    if (34112 <= num16 && num16 <= 34974)
                    {
                        return false;
                    }
                    //縦文字（EB40～EFFC）を含むか
                    if (60224 <= num16 && num16 <= 61436)
                    {
                        return false;
                    }
                    //外字（F040～F9FC）を含むか
                    if (60224 <= num16 && num16 <= 61436)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static bool checkPhoneNumFormat(string str)
        {
            try
            {
                Encoding en = Encoding.ASCII;
                byte[] bytes;
                string chkCode = "";
                string new_str = "";
                int num32 = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    //全角を半角に変換する
                    new_str = Microsoft.VisualBasic.Strings.StrConv(str[i].ToString(), Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);

                    bytes = en.GetBytes(new_str);
                    chkCode = "";
                    foreach (byte item in bytes)
                    {
                        chkCode = chkCode + Convert.ToString(item, 16);
                    }

                    //10進数の整数に直す
                    num32 = Convert.ToInt32(chkCode, 16);
                    if (num32 == 34 || num32 == 44)
                    {
                        return false;
                    }
                }

                return !Regex.IsMatch(str, "[^0-9]");
            }
            catch
            {
                return false;
            }
        }

        internal static bool checkInput(string str)
        {
            try
            {
                Encoding en = Encoding.ASCII;
                byte[] bytes;
                string chkCode = "";
                string new_str = "";
                int num32 = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    //全角を半角に変換する
                    new_str = Microsoft.VisualBasic.Strings.StrConv(str[i].ToString(), Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);

                    bytes = en.GetBytes(new_str);
                    chkCode = "";
                    foreach (byte item in bytes)
                    {
                        chkCode = chkCode + Convert.ToString(item, 16);
                    }

                    //10進数の整数に直す
                    num32 = Convert.ToInt32(chkCode, 16);
                    if (num32 == 34 || num32 == 44)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static bool IsDateTime(string txtDate)
        {
            DateTime tempDate;

            return DateTime.TryParse(txtDate, out tempDate) ? true : false;
        }

        public static bool IsHiragana(char srcChar)
        {
            return ('\u3001' <= srcChar && srcChar <= '\u3002') || ('\u3041' <= srcChar && srcChar <= '\u3096') || ('\u309D' <= srcChar && srcChar <= '\u309F') || srcChar == '\u30FC';
        }

        internal static bool IsHiragana(string srcString)
        {
            if (System.String.IsNullOrEmpty(srcString))
            {
                return false;
            }

            foreach (char c in srcString)
            {
                if (!IsHiragana(c))
                {
                    return false;
                }
            }

            return true;
        }

        static Regex validPassRegex = CreateValidPassRegex();

        private static Regex CreateValidPassRegex()
        {
            string validPassPattern = @"^[A-Za-z0-9]+$";

            return new Regex(validPassPattern, RegexOptions.IgnoreCase);
        }

        internal static bool PassIsValid(string pass)
        {
            bool isValid = validPassRegex.IsMatch(pass);

            return isValid;
        }

        public static string Mid(string stTarget, int iStart, int iLength)
        {
            if (iStart <= stTarget.Length)
            {
                if (iStart + iLength - 1 <= stTarget.Length)
                {
                    return stTarget.Substring(iStart - 1, iLength);
                }

                return stTarget.Substring(iStart - 1);
            }

            return string.Empty;
        }

        #region カメラ　チェック
        private VideoCaptureDevice FinalFrame;
        int deviceNo;

        internal bool CheckCamera(FilterInfoCollection captureDevice)
        {
            try
            {
                deviceNo = Common.GetSetting<int>("device");
                if (captureDevice.Count > 0)
                {
                    FinalFrame = new VideoCaptureDevice();
                    FinalFrame = new VideoCaptureDevice(captureDevice[deviceNo].MonikerString);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }           
        }
        #endregion

        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 250,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label lbTitle = new Label() { Left = 50, Top = 25, Text = text, AutoSize = true, Font = new Font("Meiryo", 16f) };
            TextBox txtCard_H = new TextBox() { Left = 50, Top = 60, Width = 100, Height = 300, MaxLength = 4,Font = new Font("Meiryo", 18f, FontStyle.Bold), ImeMode = ImeMode.Disable };
            Label lb_ = new Label() { Left = 160, Top = 60, Text = "-", AutoSize = true, Font = new Font("Meiryo", 18f) };
            TextBox txtCard_B = new TextBox() { Left = 190, Top = 60, Width = 250, Height = 300,MaxLength = 6, Font = new Font("Meiryo", 18f, FontStyle.Bold), ImeMode = ImeMode.Disable };

            Button cancel = new Button() { Text = "キャンセル", Left = 60, Width = 150, Height = 50, Top = 120, Font = new Font("Meiryo", 18f, FontStyle.Bold), ForeColor = Color.White, BackColor = SystemColors.Highlight, DialogResult = DialogResult.Cancel };
            Button confirm = new Button() { Text = "OK", Left = 275, Width = 150, Height = 50, Top = 120, Font = new Font("Meiryo", 18f, FontStyle.Bold), ForeColor = Color.White, BackColor = Color.Orange, DialogResult = DialogResult.OK };

            cancel.Click += (sender, e) => { prompt.Close(); };
            confirm.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(lbTitle);
            prompt.Controls.Add(txtCard_H);
            prompt.Controls.Add(lb_);
            prompt.Controls.Add(txtCard_B);
            prompt.Controls.Add(confirm);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirm;

            return prompt.ShowDialog() == DialogResult.OK ? txtCard_H.Text +"-"+ txtCard_B.Text : "-";
        }

        public static T GetSetting<T>(string key, T defaultValue = default(T)) where T : IConvertible
        {
            string val = ConfigurationManager.AppSettings[key] ?? "";
            T result = defaultValue;
            if (!string.IsNullOrEmpty(val))
            {
                T typeDefault = default(T);
                result = (T)Convert.ChangeType(val, typeDefault.GetTypeCode());
            }
            return result;
        }
    }
}
