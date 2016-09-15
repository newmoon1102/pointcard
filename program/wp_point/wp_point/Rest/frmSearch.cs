using System;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using _wp_point.Rest.Class;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using static _wp_point.Rest.Class.Common;
using System.Text.RegularExpressions;

namespace _wp_point.Rest
{
    public struct HistoryRefineCondition
    {
        /// <summary>カード絞込フラグ</summary>
        public bool CardNoFlag;
        /// <summary>カード番号</summary>
        public string CardNo;
        /// <summary>名前絞込フラグ</summary>
        public bool NameFlag;
        /// <summary>名前</summary>
        public string Name;
        /// <summary>区分絞込フラグ</summary>
        public bool KbnFlag;
        /// <summary>プリカ決済なし</summary>
        public bool PlicaSettlement_None;
        /// <summary>プリカ決済あり</summary>
        public bool PlicaSettlement_Yes;
        /// <summary>プリカ販売</summary>
        public bool PlicaSale;
        /// <summary>現金精算</summary>
        public bool CashPayOff;
        /// <summary>返品</summary>
        public bool ReturnedGoods;
        /// <summary>引継</summary>
        public bool Takeover;
        /// <summary>発行</summary>
        public bool Issue;
        /// <summary>追加入金</summary>
        public bool AddPayment;
        /// <summary>再発行</summary>
        public bool Reissue;
        /// <summary>返金</summary>
        public bool Refund;
    }

    public partial class frmSearch : Form
    {
        private readonly frmMain _frm;
        /// <summary>ポートオーペンフラグ</summary>
        public bool openPortFlag;

        public frmSearch(frmMain fr)
        {
            InitializeComponent();
            this._frm = fr;
            openPortFlag = false;
            lbStatus.Visible = false;
        }
        
        /// <summary>
        /// 検索ボタンを押された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                HistoryRefineCondition HRC = new HistoryRefineCondition();

                //------------------------------
                // 入力チェック
                //------------------------------
                //カード番号絞込チェック
                if (txtSearch.Text == "" || textTempoCode.Text == "")
                {
                    HRC.CardNo = "";
                    HRC.CardNoFlag = false;
                }
                else
                {
                    if (textTempoCode.Text.Length != 4) HRC.CardNo = textTempoCode.Text.PadLeft(4, '0');
                    else HRC.CardNo = textTempoCode.Text;
                    if (txtSearch.Text.Length != 6) HRC.CardNo = HRC.CardNo + txtSearch.Text.PadLeft(6, '0');
                    else HRC.CardNo = HRC.CardNo + txtSearch.Text;
                    if (Regex.IsMatch(HRC.CardNo, "[^0-9]"))
                    {
                        MsgBox.Show("カード番号の入力に誤りがあります。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    HRC.CardNoFlag = true;
                }
                //名前絞込チェック
                if (textBox1.Text != "")
                {
                    foreach (char s in textBox1.Text)
                    {
                        if (!(IsKatakana(s) || IsHiragana(s)))
                        {
                            MsgBox.Show("名前(カナ)は「ひらがな」か「カタカナ」で入力してください。", "入力チェック", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    HRC.Name = textBox1.Text;
                    HRC.NameFlag = true;
                }
                else
                {
                    HRC.NameFlag = false;
                }

                //カード区分絞込チェック
                if (checkBox1.Checked == true) { HRC.PlicaSettlement_None = true; }
                if (checkBox2.Checked == true) { HRC.PlicaSettlement_Yes = true; }
                if (checkBox3.Checked == true) { HRC.PlicaSale = true; }
                if (checkBox4.Checked == true) { HRC.CashPayOff = true; }
                if (checkBox5.Checked == true) { HRC.ReturnedGoods = true; }
                if (checkBox6.Checked == true) { HRC.Takeover = true; }
                if (checkBox7.Checked == true) { HRC.Issue = true; }
                if (checkBox8.Checked == true) { HRC.AddPayment = true; }
                if (checkBox9.Checked == true) { HRC.Reissue = true; }
                if (checkBox10.Checked == true) { HRC.Refund = true; }
                if (HRC.PlicaSettlement_None ||
                    HRC.PlicaSettlement_Yes ||
                    HRC.PlicaSale ||
                    HRC.CashPayOff ||
                    HRC.ReturnedGoods ||
                    HRC.Takeover ||
                    HRC.Issue ||
                    HRC.AddPayment ||
                    HRC.Reissue ||
                    HRC.Refund
                    )
                {
                    HRC.KbnFlag = true;
                }

                lbStatus.Visible = true;
                lbStatus.Text  = "処理中.";
                lbStatus.Refresh();
                //------------------------------
                // データの取得開始
                //------------------------------
                //プリンター機器からデータファイルを取得
                string FilePath = GetPrinterMemoryData();
                if (FilePath == "") return; //ファイルパスが無い場合抜ける

                //ファイルから構造体リストに変換したデータを取得
                List<TCSPC100TradeData_DataRecord_TypeD> ListData = new List<TCSPC100TradeData_DataRecord_TypeD>();
                TCSPC100TradeData_Header HeaderData = new TCSPC100TradeData_Header();
                GetPrinterData(FilePath + ".txt", ref ListData, ref HeaderData, HRC);

                lbStatus.Text = "";
                //------------------------------
                // 画面遷移、リスト画面へ
                //------------------------------
                if (this._frm.ResetOpenWindow("frmHistoryView"))
                {
                    frmHistoryView childForm = new frmHistoryView(_frm, ListData, HeaderData);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            catch (Exception ex)
            {

                MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// トップ画面へ戻る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTop_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmSelect"))
            {
                frmSelect childForm = new frmSelect(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
        
        /// <summary>
        /// プリンターメモリーのデータをファイルへ読込
        /// </summary>
        /// <returns>ファイルパス(拡張子抜き)</returns>
        private string GetPrinterMemoryData()
        {
            string RetFilePath = "";

            try
            {
                //------------------------------
                // プリンター機器との通信処理
                //------------------------------
                //プリンター機器との通信開始
                TcsPc100.port_hndl = new IntPtr(0);
                short portNo = Convert.ToInt16(Common.GetSetting<int>("portno"));
                int baud = Common.GetSetting<int>("baud");
                TcsPc100.ret = TcsPc100.OpenPort(ref TcsPc100.port_hndl, portNo, baud);
                lbStatus.Text = "処理中..";
                lbStatus.Refresh();
                //オープン出来ない場合は終了
                if (TcsPc100.ret != 0) throw new Exception("カード機との通信に失敗しました。");
                openPortFlag = true;
                //------------------------------
                // プリンターのメモリーからデータを抜き出す
                //------------------------------
                //ファイルパスを用意
                RetFilePath = AppDomain.CurrentDomain.BaseDirectory;
                if (Strings.Right(RetFilePath, 1) != @"\") RetFilePath = RetFilePath + @"\";
                TcsPc100.filename = Encoding.GetEncoding("shift_jis").GetBytes(RetFilePath + "ICMData");
                lbStatus.Text = "処理中...";
                lbStatus.Refresh();
                //メモリーデータの読込（ファイル作成・新規上書きで作成したファイルへプリンターAPIから書き込む）
                TcsPc100.ret = TcsPc100.RmGetICM(ref TcsPc100.port_hndl, TcsPc100.filename);
                if (TcsPc100.ret != 0) throw new Exception("メモリーデータの読込に失敗しました。");
                RetFilePath = RetFilePath + "ICMData";
                lbStatus.Text = "処理中....";
                lbStatus.Refresh();
            }
            catch(Exception ex)
            {
                RetFilePath = "";
                MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //------------------------------
                // プリンター機器通信ポートが開いて要る場合、クローズ処理
                //------------------------------
                if (TcsPc100.port_hndl != null)
                {
                    if (openPortFlag)
                    {
                        TcsPc100.ret = TcsPc100.ClosePort(ref TcsPc100.port_hndl);
                        if (TcsPc100.ret != 0) MsgBox.Show("カード機との通信クローズに失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        openPortFlag = false;
                    }
                }
            }

            return RetFilePath;
        }
        
        /// <summary>
        /// プリンターデータファイルを構造体のリストデータに変換
        /// </summary>
        /// <param name="FilePath">プリンターデータファイルまでのパス(拡張子必要)</param>
        /// <param name="ListData">参照型ボディデータ</param>
        /// <param name="HeaderData">参照型ヘッダデータ</param>
        /// <param name="SearchNo">絞込用カード番号</param>
        private void GetPrinterData(string FilePath, ref List<TCSPC100TradeData_DataRecord_TypeD> ListData, ref TCSPC100TradeData_Header HeaderData, HistoryRefineCondition Refine)
        {
            try
            {
                //ファイルの読込
                int FileNo;
                FileNo = FileSystem.FreeFile();

                //ファイルが無い場合は抜ける
                if (FileNo == 0) return;

                //--------------------------------------------------
                // ファイルの中身を構造体に変換
                //--------------------------------------------------
                StreamReader reader = new StreamReader(FilePath, Encoding.GetEncoding("shift_JIS"));
                string[] data;
                while (reader.Peek() >= 0)
                {
                    //------------------------------
                    // １行読込
                    //------------------------------
                    data = reader.ReadLine().Replace(" ","").Split(',');

                    //------------------------------
                    // ヘッダ判定
                    //------------------------------
                    if (data[0] == "#")
                    {
                        HeaderData.TerminalCode = data[1];
                        HeaderData.RecordNum = int.Parse(data[2]);
                        HeaderData.Version = data[3];
                        if (HeaderData.Version.Substring(0, 1) != "D") return;
                        continue;
                    }

                    //------------------------------
                    // データの絞込
                    //------------------------------
                    //カードNoによる絞込
                    if (Refine.CardNoFlag)
                    {
                        if (data[1].Substring(0, 10) != Refine.CardNo) continue;
                    }
                    //名前による絞込
                    if (Refine.NameFlag)
                    {
                        //部分一致
                        string s1 = ChangeHalfKatakana(data[33]);
                        string s2 = ChangeHalfKatakana(Refine.Name);
                        if (s1.IndexOf(s2) == -1) continue;
                    }
                    //カード区分による絞込
                    if (Refine.KbnFlag)
                    {
                        bool continueflag = true;

                        if (Refine.PlicaSettlement_None) { if (data[27] == "0") continueflag = false; }
                        if (Refine.PlicaSettlement_Yes) { if (data[27] == "1") continueflag = false; }
                        if (Refine.PlicaSale) { if (data[27] == "2") continueflag = false; }
                        if (Refine.CashPayOff) { if (data[27] == "3") continueflag = false; }
                        if (Refine.ReturnedGoods) { if (data[27] == "4") continueflag = false; }
                        if (Refine.Takeover) { if (data[27] == "5") continueflag = false; }
                        if (Refine.Issue) { if (data[27] == "6") continueflag = false; }
                        if (Refine.AddPayment) { if (data[27] == "7") continueflag = false; }
                        if (Refine.Reissue) { if (data[27] == "8") continueflag = false; }
                        if (Refine.Refund) { if (data[27] == "9") continueflag = false; }

                        if (continueflag) continue;
                    }

                    //------------------------------
                    // 通常レコード
                    //------------------------------
                    TCSPC100TradeData_DataRecord_TypeD Item = new TCSPC100TradeData_DataRecord_TypeD();
                    Item.datetime = DateTime.ParseExact(data[0], "yyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None);
                    Item.MembersNo = data[1];
                    Item.ServiceCode = data[2];
                    if (data[3] != "") Item.AmountSolid = int.Parse(data[3]);
                    if (data[4] != "") Item.AmountPoint = int.Parse(data[4]);
                    if (data[5] != "") Item.DoublePoint = int.Parse(data[5]);
                    if (data[6] != "") Item.CountPoint = int.Parse(data[6]);
                    if (data[7] != "") Item.SpecialPoint = int.Parse(data[7]);
                    if (data[8] != "") Item.NewPoint = int.Parse(data[8]);
                    if (data[9] != "") Item.CumulativePoint = int.Parse(data[9]);
                    if (data[10] != "") Item.TradePoint = int.Parse(data[10]);
                    if (data[11] != "") Item.UseCount = int.Parse(data[11]);
                    if (data[12] != "") Item.LastDate = DateTime.ParseExact(data[12], "yyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None);
                    if (data[13] != "") Item.ExpirationDate = DateTime.ParseExact(data[13], "yyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None);
                    if (data[14] != "") Item.AddUpAmountSolid = int.Parse(data[14]);
                    if (data[15] != "") Item.TaxIncludedKbn = int.Parse(data[15]);
                    if (data[16] != "") Item.CorrectionKbn = int.Parse(data[16]);
                    if (data[17] != "") Item.TradeKbn = int.Parse(data[17]);
                    if (data[18] != "") Item.LimitDateUpdateKbn = int.Parse(data[18]);
                    if (data[19] != "") Item.RankKbn = int.Parse(data[19]);
                    if (data[20] != "") Item.MemberCollationKbn = int.Parse(data[20]);
                    if (data[21] != "") Item.NameCollationKbn = int.Parse(data[21]);
                    Item.RecordNumber = data[22];
                    Item.ResponsibleCode = data[23];
                    if (data[24] != "") Item.InPrice = int.Parse(data[24]);
                    if (data[25] != "") Item.PremiumPrice = int.Parse(data[25]);
                    if (data[26] != "") Item.RemainingAmount = int.Parse(data[26]);
                    if (data[27] != "") Item.PriCordFlag = int.Parse(data[27]);
                    Item.BarcodeData = data[28];
                    Item.TakeoverData = data[29];
                    if (data[30] != "") Item.SettlementPreAmount = int.Parse(data[30]);

                    if (data[31] != "" && data[31] != "000000") Item.ReservationDate1 = DateTime.ParseExact(data[31], "yyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None);
                    if (data[32] != "" && data[32] != "000000") Item.ReservationDate2 = DateTime.ParseExact(data[32], "yyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None);
                    Item.Name = data[33];
                    if (data[34] != "") Item.NameWriteFlag = int.Parse(data[34]);
                    Item.Furigana = data[35];
                    if (data[36] != "" && data[36] != "000000") Item.PriExpirationDate = DateTime.ParseExact(data[36], "yyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None);
                    Item.Dummy1 = data[37];
                    Item.Dummy2 = data[38];
                    ListData.Add(Item);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region ひらがな・カタカナチェック
        /// <summary>
        /// 指定した Unicode 文字が、カタカナかどうかを示します。
        /// </summary>
        /// <param name="c">評価する Unicode 文字。</param>
        /// <returns>c がカタカナである場合は true。それ以外の場合は false。</returns>
        public static bool IsKatakana(string s)
        {
            bool ret = false;

            try
            {
                //「ダブルハイフン」から「コト」までと、カタカナフリガナ拡張と、
                //濁点と半濁点と、半角カタカナをカタカナとする
                //中点と長音記号も含む
                ret = System.Text.RegularExpressions.Regex.IsMatch(s, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }
        public static bool IsKatakana(char s)
        {
            bool ret = false;

            try
            {
                ret = IsKatakana(s.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }
        /// <summary>
        /// 指定した文字列に含まれる文字がすべてひらがなかどうかを示します。
        /// </summary>
        /// <param name="s">評価する文字列。</param>
        /// <returns>c に含まれる文字がすべてひらがなである場合は true。
        /// それ以外の場合は false。</returns>
        public static bool IsHiragana(string s)
        {
            bool ret = false;

            try
            {
                ret = System.Text.RegularExpressions.Regex.IsMatch(s, @"^\p{IsHiragana}+$");
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return ret;
        }
        public static bool IsHiragana(char s)
        {
            bool ret = false;

            try
            {
                ret = IsHiragana(s.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        /// <summary>
        /// ひらがなをカタカナに変換し、全角を半角に変換する
        /// </summary>
        /// <param name="s">文字列</param>
        /// <returns></returns>
        public string ChangeHalfKatakana(string s)
        {
            string ret = "";

            try
            {
                ret = Microsoft.VisualBasic.Strings.StrConv(
                    s,
                    Microsoft.VisualBasic.VbStrConv.Katakana |
                         Microsoft.VisualBasic.VbStrConv.Narrow,
                    0x411);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }
        #endregion
    }
}
