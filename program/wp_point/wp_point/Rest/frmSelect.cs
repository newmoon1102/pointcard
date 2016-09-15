using _wp_point.Rest.Class;
using AForge.Video.DirectShow;
using log4net;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using _wp_point.Rest.Request;
using _wp_point.Rest.Replace;
using System.Configuration;

namespace _wp_point.Rest
{
    public partial class frmSelect : Form
    {
        private readonly frmMain _frm;
        private FilterInfoCollection captureDevice;
        MemberImportRequest RegisterData;
        /// <summary>
        /// 元会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Original_MemberInfo;
        /// <summary>
        /// 先会員の情報
        /// </summary>
        private ResponseData_MemberInfo_Reference Former_MemberInfo; 

        private ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public frmSelect(frmMain frm)
        {
            InitializeComponent();
            this._frm = frm;
            Original_MemberInfo = new ResponseData_MemberInfo_Reference();
            Former_MemberInfo = new ResponseData_MemberInfo_Reference();
            RegisterData = new MemberImportRequest();
        }
        private void btnGeneral_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmRegister"))
            {
                frmRegister childForm = new frmRegister(_frm, this.Name.ToString(), RegisterData);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnWP_Click(object sender, EventArgs e)
        {
            Common cm = new Common();
            captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (cm.CheckCamera(captureDevice))
            {
                if (this._frm.ResetOpenWindow("frmQRCode"))
                {
                    frmQRCode childForm = new frmQRCode(_frm, this.Name.ToString(), "", RegisterData);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            else
            {
                MsgBox.Show("カメラとの接続に失敗しました。", "接続エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void btnCreatCard_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmSearch"))
            {
                frmSearch childForm = new frmSearch(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnsetting_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmSetting"))
            {
                frmSetting childForm = new frmSetting(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmLogin"))
            {
                frmLogin childForm = new frmLogin(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();

            if (Common.SqlCon(con))
            {
                if (this._frm.ResetOpenWindow("frmList"))
                {
                    frmList childForm = new frmList(_frm);
                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            else
            {
                MsgBox.Show("データベースへの接続に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void frmSelect_Load(object sender, EventArgs e)
        {
            btnGeneral.Text = "新規発行\r\n(WP会員でない方)";
            btnWP.Text = "追加発行\r\n(WP会員の方)";
            btnSearch.Text = "カード取引履歴照会\r\n(当日分)";
        }

        private void btnReissue_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmReissue"))
            {
                frmReissue childForm = new frmReissue(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            //2016.5.30 バックアップ処理追加
            DialogResult Result = MsgBox.Show("カード取引履歴をアップロードし、バックアップを作成します。\r\n処理に時間がかかりますが、よろしいでしょうか？", "確認", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (Result == DialogResult.OK)
            {
                //マウスカーソルを待機状態
                Cursor.Current = Cursors.WaitCursor;

                bool errorFlag = false;

                _logger.Info("日次処理開始");

                try
                {
                    using (frmPrgogress progress_form = new frmPrgogress())
                    {
                        progress_form.Left = this.Left + (this.Width - progress_form.Width) / 2;
                        progress_form.Top = this.Top + (this.Height - progress_form.Height) / 2;
                        progress_form.Show();

                        //------------------------------
                        //カード機連動
                        //------------------------------
                        string RetFilePath = MovPrinterMemoryData();
                        if (RetFilePath == "") throw new Exception("カード取引履歴のアップロード処理中にエラーが発生しました。\r\n(カード機からのデータ取得に失敗しました。)");

                        //------------------------------
                        //バッチ処理
                        //------------------------------
                        //カード機からデータを取得し、LocalDBへデータを登録
                        ProcessStartInfo WP_PCARDIFpsInfo = new ProcessStartInfo();
                        WP_PCARDIFpsInfo.FileName = Properties.Settings.Default.WP_PCARDIF_Path;
                        WP_PCARDIFpsInfo.CreateNoWindow = true;
                        WP_PCARDIFpsInfo.UseShellExecute = false;
                        Process WP_PCARDIF = Process.Start(WP_PCARDIFpsInfo);
                        WP_PCARDIF.WaitForExit();
                        if (WP_PCARDIF.ExitCode != 0) throw new Exception("カード取引履歴のアップロード処理中にエラーが発生しました。\r\n(WP_PCARDIFの実行中にエラーが発生しました。)");

                        //LocalDBからデータをアップする。
                        ProcessStartInfo WP_UPLOAD_POINTpsInfo = new ProcessStartInfo();
                        WP_UPLOAD_POINTpsInfo.FileName = Properties.Settings.Default.WP_UPLOAD_POINT_Path;
                        WP_UPLOAD_POINTpsInfo.CreateNoWindow = true;
                        WP_UPLOAD_POINTpsInfo.UseShellExecute = false;
                        Process WP_UPLOAD_POINT = Process.Start(WP_UPLOAD_POINTpsInfo);
                        WP_UPLOAD_POINT.WaitForExit();
                        if (WP_UPLOAD_POINT.ExitCode != 0) throw new Exception("カード取引履歴のアップロード処理中にエラーが発生しました。\r\n(wp_upload_pointの実行中にエラーが発生しました。)");
                    }
                }
                catch (Exception ex)
                {
                    errorFlag = true;
                    MsgBox.Show(ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    _logger.Fatal("日次処理 エラー: " + ex);
                }

                try
                {
                    if (Properties.Settings.Default.WpPointCardBackUp_Path != "")
                    {
                        using (frmPrgogress progress_form = new frmPrgogress())
                        {
                            progress_form.Left = this.Left + (this.Width - progress_form.Width) / 2;
                            progress_form.Top = this.Top + (this.Height - progress_form.Height) / 2;
                            progress_form.Show();

                            ProcessStartInfo WpPointCardBackUppsInfo = new ProcessStartInfo();
                            WpPointCardBackUppsInfo.FileName = Properties.Settings.Default.WpPointCardBackUp_Path;
                            WpPointCardBackUppsInfo.CreateNoWindow = true;
                            WpPointCardBackUppsInfo.UseShellExecute = false;
                            Process WpPointCardBackUp = Process.Start(WpPointCardBackUppsInfo);
                            WpPointCardBackUp.WaitForExit();
                            if (WpPointCardBackUp.ExitCode != 0) throw new Exception("バックアップ処理中にエラーが発生しました。");
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorFlag = true;
                    MsgBox.Show(ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    _logger.Fatal("日次処理 エラー: " + ex);
                }

                //正常終了アラート
                if (!errorFlag)
                {
                    MsgBox.Show("日次処理が正常に完了しました。","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    _logger.Info("日次処理が正常に完了しました。");
                }
                else
                {
                    MsgBox.Show("日次処理が終了しました。エラー項目を確認してください。","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    _logger.Info("日次処理が終了しました。エラー項目を確認してください。");
                }
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// プリンターメモリーのデータをファイルへ移動、プリンター機からはデータが無くなる
        /// </summary>
        /// <returns>ファイルパス(拡張子抜き)</returns>
        private string MovPrinterMemoryData()
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

                //オープン出来ない場合は終了
                if (TcsPc100.ret != 0) throw new Exception("カード機との通信に失敗しました。");

                //------------------------------
                // プリンターのメモリーからデータを抜き出す
                //------------------------------
                //ファイルパスを用意

                //RetFilePath = Properties.Settings.Default.PointCardMemoryDataFilePath + @"\" + Properties.Settings.Default.PointCardMemoryDataFileName;
                for (int serial = 1; serial <= 999; serial++)
                {
                    if (!System.IO.File.Exists(Properties.Settings.Default.PointCardMemoryDataFilePath + @"\" + Properties.Settings.Default.PointCardMemoryDataFileName + "_" + serial.ToString("d3") + ".txt"))
                    {
                        RetFilePath = Properties.Settings.Default.PointCardMemoryDataFilePath + @"\" + Properties.Settings.Default.PointCardMemoryDataFileName + "_" + serial.ToString("d3");
                        break;
                    }
                }
                if (RetFilePath == "") throw new Exception("メモリーデータファイルの作成に失敗しました。");
                TcsPc100.filename = Encoding.GetEncoding("shift_jis").GetBytes(RetFilePath);

                //メモリーデータの読込（ファイル作成・新規上書きで作成したファイルへプリンターAPIから書き込む）
                TcsPc100.ret = TcsPc100.RmGetTrade(ref TcsPc100.port_hndl, TcsPc100.filename);
                if (TcsPc100.ret != 0) throw new Exception("メモリーデータの読込に失敗しました。");
            }
            catch (Exception ex)
            {
                RetFilePath = "";
                MsgBox.Show(ex.Message,"エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                _logger.Fatal(ex);
            }
            finally
            {
                //------------------------------
                // プリンター機器通信ポートが開いて要る場合、クローズ処理
                //------------------------------
                if (TcsPc100.port_hndl != null)
                {
                    TcsPc100.ret = TcsPc100.ClosePort(ref TcsPc100.port_hndl);
                    if (TcsPc100.ret != 0) MsgBox.Show("カード機との通信クローズに失敗しました。","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    _logger.Fatal("カード機との通信クローズに失敗しました。");
                }
            }

            return RetFilePath;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmOldMemeber"))
            {
                frmOldMemeber childForm = new frmOldMemeber(_frm, Original_MemberInfo, Former_MemberInfo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
