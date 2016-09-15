using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _wp_point.Rest.Class;

namespace _wp_point.Rest.Replace
{
    public partial class frmChangeName : Form
    {
        private readonly frmMain _frm;
        private CardNo cardNo;
        private string CardId;
        private string newMemberName;
        private bool msgFlg;
        private bool PortOpenFlg; //ポートが開いているかどうか

        public frmChangeName(frmMain frm, CardNo cardno)
        {
            InitializeComponent();
            this._frm = frm;
            cardNo = cardno;
            UserInitialize();
        }

        private void UserInitialize()
        {
            btnBack.Enabled = false;
            lbTitle.Text = lbTitle.Text.Replace("oldcardId", cardNo.OldCardNo);
            lbContent.Text = lbContent.Text.Replace("oldcardId", cardNo.OldCardNo);
            msgFlg = false;
        }

        private void frmChangeName_Load(object sender, EventArgs e)
        {
            try
            {
                //カード機通信オープン
                PortOpenFlg = TCSPC100Open();

                //タイマスタート
                timer_Card.Start();
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer_Card_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!PortOpenFlg)
                {
                    //タイマ停止
                    timer_Card.Stop();

                    //カード機通信オープン失敗メッセージ
                    MsgBox.Show("カード機との通信に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MsgBox.Show("譲渡元カード(カードNo："+ cardNo.OldCardNo + ")に名前を変更する処理に失敗しました。/r/nカード機で手入力して下さい。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    //完了画面へ遷移
                    if (this._frm.ResetOpenWindow("frmInputName"))
                    {
                        frmInputName childForm = new frmInputName(_frm, cardNo);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                    }
                    return;
                }

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

                if (TcsPc100.ret != 0)
                {
                    // message
                }
                else
                {
                    //message
                }

                if (TcsPc100.rwstatus == 1)
                {

                    TcsPc100.ret = TcsPc100.RmGetCardData(ref TcsPc100.port_hndl, TcsPc100.cardid, ref TcsPc100.ownpoint, ref TcsPc100.usecount, TcsPc100.usedate, ref TcsPc100.salevalue, ref TcsPc100.addpoint, TcsPc100.limit,
                        ref TcsPc100.expired, ref TcsPc100.namedata, TcsPc100.birthday, ref TcsPc100.memlist, ref TcsPc100.ptype);
                    if (TcsPc100.ret != 0)
                    {
                        //DispErrorMessage(TcsPc100.ret);
                    }
                    else
                    {
                        //カードデータを読み込む
                        CardId = Encoding.GetEncoding("shift_jis").GetString(TcsPc100.cardid);
                        if (CardId.Length > 10) { CardId = CardId.Substring(0, 10); }

                        if (CardId != cardNo.OldCardNo.Replace("-", ""))
                        {
                            if (!msgFlg)
                            {
                                msgFlg = true;
                                MsgBox.Show("間違ったカードが挿入されました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            //ひらがなをカタカナに変換し、全角を半角に変換する
                            newMemberName = Microsoft.VisualBasic.Strings.StrConv(cardNo.NewCardName, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
                            //頭から16文字まで
                            if (newMemberName.Length > 16)
                            {
                                newMemberName = Common.Mid(newMemberName, 1, 16);
                            }
                            TcsPc100.namedata3 = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(newMemberName); /* 名前データ */
                            TcsPc100.ret = TcsPc100.RmSendName(ref TcsPc100.port_hndl, TcsPc100.namedata3);

                            //if (TcsPc100.ret != 0)
                            //{
                            //    // message
                            //}
                            //else
                            //{
                            //    //message
                            //}
                        }
                    }

                    if (TcsPc100.rwstatus != 1 && msgFlg)
                    {
                        msgFlg = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //タイマ停止
                timer_Card.Stop();
            }
        }

        private bool TCSPC100Open()
        {
            try
            {
                //------------------------------
                // カード機との通信処理
                //------------------------------
                //カード機との通信開始
                TcsPc100.port_hndl = new IntPtr(0);
                short portNo = Convert.ToInt16(Common.GetSetting<int>("portno"));
                int baud = Common.GetSetting<int>("baud");
                TcsPc100.ret = TcsPc100.OpenPort(ref TcsPc100.port_hndl, portNo, baud);

                if (TcsPc100.ret != 0) return false;
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void TCSPC100Close()
        {
            try
            {
                //------------------------------
                // カード機通信ポートが開いて要る場合、クローズ処理
                //------------------------------
                if (TcsPc100.port_hndl != null && PortOpenFlg)
                {
                    TcsPc100.ret = TcsPc100.ClosePort(ref TcsPc100.port_hndl);
                    if (TcsPc100.ret != 0) MsgBox.Show("カード機との通信クローズに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (PortOpenFlg)
            {
                //カード通信クローズ
                TCSPC100Close();

                //タイマ停止
                timer_Card.Stop();
            }

            //完了画面へ遷移
            if (this._frm.ResetOpenWindow("frmReplaceFinish"))
            {
                frmReplaceFinish childForm = new frmReplaceFinish(_frm, cardNo);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
