using _wp_point.Rest.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _wp_point.Rest.Reissue
{
    public partial class frmReissueProcedure : Form
    {
        private readonly frmMain _frm;

        private bool OpenFlag;
        
        private static bool FirstFlag;
        private static int OldCardStatus;
        private static int OldCardWriterStatus;
        private static string CardNo;

        private int StatusCounter;

#if DEBUG
        private static string DebugLog;
#endif

        #region コンストラクタ・デストラクタ
        public frmReissueProcedure()
        {
            InitializeComponent();
        }
        public frmReissueProcedure(frmMain fr)
        {
            InitializeComponent();
            this._frm = fr;
            //NextFlag = false;
            StatusCounter = 0;
        }

        #endregion

        /// <summary>
        /// 戻るボタン押した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            //カード通信クローズ
            TCSPC100Close();

            //タイマ停止
            timerReadQRcode.Stop();

            //再発行手順画面へ遷移
            if (this._frm.ResetOpenWindow("frmReissue"))
            {
                frmReissue childForm = new frmReissue(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        /// <summary>
        /// 次ページボタン押した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            //カード通信クローズ
            TCSPC100Close();

            //タイマ停止
            timerReadQRcode.Stop();

            //カード番号入力画面へ遷移
            if (this._frm.ResetOpenWindow("frmCardNo"))
            {
                frmCardNo childForm = new frmCardNo(_frm, CardNo, false);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void frmReissueProcedure_Load(object sender, EventArgs e)
        {
            try
            {
                //カードNo
                CardNo = "";

                //カード機通信オープン
                OpenFlag = TCSPC100Open();

                //タイマスタート
                timerReadQRcode.Start();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TCSPC100Open()
        {
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

                if (TcsPc100.ret != 0) return false;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void TCSPC100Close()
        {
            try
            {
                //------------------------------
                // プリンター機器通信ポートが開いて要る場合、クローズ処理
                //------------------------------
                if (TcsPc100.port_hndl != null && OpenFlag)
                {
                    TcsPc100.ret = TcsPc100.ClosePort(ref TcsPc100.port_hndl);
                    if (TcsPc100.ret != 0) MsgBox.Show("カード機との通信クローズに失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TCSPC100Status(object sender, EventArgs e)
        {
            try
            {
                #region オープン失敗時の処理
                if (!OpenFlag)
                {
                    //タイマ停止
                    timerReadQRcode.Stop();

                    //カード機通信オープン失敗メッセージ
                    MsgBox.Show("カード機との通信に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //再発行手順画面へ遷移
                    if (this._frm.ResetOpenWindow("frmReissue"))
                    {
                        frmReissue childForm = new frmReissue(_frm);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }

                    return;
                }
                #endregion

                //カード受付状態
                TcsPc100.ret = TcsPc100.RmGetStatus(
                        ref TcsPc100.port_hndl,
                        ref TcsPc100.card,
                        ref TcsPc100.rwstatus,
                        ref TcsPc100.mvmode,
                        ref TcsPc100.icsstatus,
                        ref TcsPc100.datanum,
                        ref TcsPc100.datamax,
                        ref TcsPc100.errcode
                );
                if (FirstFlag)
                {
                    FirstFlag = false;
                    OldCardStatus = TcsPc100.card;
                    OldCardWriterStatus = TcsPc100.rwstatus;
                }
                if (OldCardStatus != TcsPc100.card)
                {
                    OldCardStatus = TcsPc100.card;
                    if (TcsPc100.card == 1)
                    {
                        TcsPc100.ret = TcsPc100.RmGetCardData(
                                ref TcsPc100.port_hndl,
                                TcsPc100.cardid,
                                ref TcsPc100.ownpoint,
                                ref TcsPc100.usecount,
                                TcsPc100.usedate,
                                ref TcsPc100.salevalue,
                                ref TcsPc100.addpoint,
                                TcsPc100.limit,
                                ref TcsPc100.expired,
                                ref TcsPc100.namedata,
                                TcsPc100.birthday,
                                ref TcsPc100.memlist,
                                ref TcsPc100.ptype
                        );

                        //カード番号
                        if (TcsPc100.ret == 0)
                        {
                            string TmpCardNo = Encoding.GetEncoding("shift_jis").GetString(TcsPc100.cardid);
                            if (TmpCardNo.Substring(0, 10) != "0000000000")
                            {
                                CardNo = TmpCardNo;
                            }
                        }

                        //label10.Text = "カード受付状態がカード処理中になりました。";
                        StatusCounter++;
                    }
                    if (TcsPc100.card == 0)
                    {
                        //カード未挿入
                        //label10.Text = "カード受付状態がカード未挿入になりました。";
                        StatusCounter++;
                    }
                    if (TcsPc100.card == 2)
                    {
                        //カード抜き取り待ち
                        //label10.Text = "カード受付状態がカード抜き取り待ちになりました。";
                        StatusCounter++;
                    }

                    //文字色の変更
                    TextChange();
                }
                
                #region Debug用
#if DEBUG
                //カード受付状態
                if (OldCardStatus != TcsPc100.card)
                {
                    if (TcsPc100.card == 0)
                    {
                        //カード未挿入
                        DebugLog += "カード受付状態がカード未挿入になりました。" + Environment.NewLine;
                        //label10.Text = "カード受付状態がカード未挿入になりました。";
                    }
                    if (TcsPc100.card == 1)
                    {
                        //カード処理中
                        DebugLog += "カード受付状態がカード処理中になりました。" + Environment.NewLine;
                        //label10.Text = "カード受付状態がカード処理中になりました。";
                    }
                    if (TcsPc100.card == 2)
                    {
                        //カード抜き取り待ち
                        DebugLog += "カード受付状態がカード抜き取り待ちになりました。" + Environment.NewLine;
                        //label10.Text = "カード受付状態がカード抜き取り待ちになりました。";
                    }
                }

                //カードリーダライタ動作状態
                if (OldCardWriterStatus != TcsPc100.rwstatus)
                {
                    OldCardWriterStatus = TcsPc100.rwstatus;
                    if (TcsPc100.rwstatus == 0)
                    {
                        //カード受付可能
                        DebugLog += "カードリーダライタ動作状態がカード受付可能になりました。" + Environment.NewLine;
                    }
                    if (TcsPc100.rwstatus == 1)
                    {
                        //カード受付禁止
                        DebugLog += "カードリーダライタ動作状態がカード受付禁止になりました。" + Environment.NewLine;
                    }
                }
#endif
                #endregion
                
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TextChange()
        {
            //色を全部グレーアウト
            label1.ForeColor = Color.Silver;
            label2.ForeColor = Color.Silver;
            label3.ForeColor = Color.Silver;
            label5.ForeColor = Color.Silver;
            label6.ForeColor = Color.Silver;
            label7.ForeColor = Color.Silver;
            label8.ForeColor = Color.Silver;

            //現在のカウントに応じた色に変更
            switch (StatusCounter)
            {
                case 0:
                    label1.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    label2.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    break;
                case 1:
                    label1.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    label3.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    break;


                case 2:
                    label5.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    label6.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    break;
                case 3:
                    label5.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    label7.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    break;
                case 4:
                    label5.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    label8.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    break;
                default:
                    if(StatusCounter > 4)
                    {
                        label5.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                        label8.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
                    }
                    break;
            }
        }
        
    }
}
