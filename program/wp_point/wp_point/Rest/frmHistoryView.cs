using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static _wp_point.Rest.Class.Common;

namespace _wp_point.Rest
{
    public partial class frmHistoryView : Form
    {
        private readonly frmMain _frm;
        public List<TCSPC100TradeData_DataRecord_TypeD> ListData;

        public frmHistoryView(frmMain fr, List<TCSPC100TradeData_DataRecord_TypeD> list, TCSPC100TradeData_Header head)
        {
            InitializeComponent();
            this._frm = fr;
            ListData = list;

            //企業コードの変換リスト作成
            List<CorpCodeData> CCDList = new List<CorpCodeData>();
            string[] tmp = Properties.Settings.Default.PrinterCorpCode.Split(',');
            foreach(string data in tmp)
            {
                string[] sub = data.Split(':');
                CorpCodeData ccd = new CorpCodeData();
                ccd.CorpCode = sub[0];
                ccd.CorpName = sub[1];
                CCDList.Add(ccd);
            }
            //店舗コードの変換リスト作成
            List<TempoCodeData> TCDList = new List<TempoCodeData>();
            tmp = Properties.Settings.Default.PrinterTempoCode.Split(',');
            foreach (string data in tmp)
            {
                string[] sub = data.Split(':');
                TempoCodeData tcd = new TempoCodeData();
                tcd.TempoCode = sub[0];
                tcd.TempoName = sub[1];
                TCDList.Add(tcd);
            }

            //企業コードと店舗コードの取得
            string CorpName = "";
            string TempoName = "";
            string TempoCode = "";
            foreach (CorpCodeData data in CCDList)
            {
                if (data.CorpCode == head.TerminalCode.Substring(0, 4)) CorpName = data.CorpName;
            }
            foreach (TempoCodeData data in TCDList)
            {
                if (data.TempoCode == head.TerminalCode.Substring(4, 4))
                {
                    TempoName = data.TempoName;
                    TempoCode = data.TempoCode;
                }
            }


            int RowNo;
            foreach(TCSPC100TradeData_DataRecord_TypeD data in ListData)
            {
                RowNo = this.dataGridView1.Rows.Add();
                string hosei = data.MembersNo.Substring(0, 4);
                hosei = hosei + "-" + data.MembersNo.Substring(4, 6);
                this.dataGridView1.Rows[RowNo].Cells["MembersNo"].Value = hosei;
                this.dataGridView1.Rows[RowNo].Cells["CordName"].Value = data.Name;
                this.dataGridView1.Rows[RowNo].Cells["Furigana"].Value = data.Furigana;
                this.dataGridView1.Rows[RowNo].Cells["datetime"].Value = data.datetime;
                this.dataGridView1.Rows[RowNo].Cells["Tempo"].Value = TempoCode + Environment.NewLine + " " + CorpName + Environment.NewLine + " " + TempoName;
                this.dataGridView1.Rows[RowNo].Cells["AmountSolid"].Value = data.AmountSolid;
                this.dataGridView1.Rows[RowNo].Cells["AmountPoint"].Value = data.AmountPoint;
                this.dataGridView1.Rows[RowNo].Cells["NewPoint"].Value = data.NewPoint;
                this.dataGridView1.Rows[RowNo].Cells["TradePoint"].Value = data.TradePoint;
                this.dataGridView1.Rows[RowNo].Cells["CumulativePoint"].Value = data.CumulativePoint;
                this.dataGridView1.Rows[RowNo].Cells["InPrice"].Value = data.InPrice;
                this.dataGridView1.Rows[RowNo].Cells["RemainingAmount"].Value = data.RemainingAmount;
                string PriKbnName = "";
                switch (data.PriCordFlag)
                {
                    case 0: PriKbnName = "プリカ決済なし"; break;
                    case 1: PriKbnName = "プリカ決済あり"; break;
                    case 2: PriKbnName = "プリカ販売"; break;
                    case 3: PriKbnName = "現金精算"; break;
                    case 4: PriKbnName = "返品"; break;
                    case 5: PriKbnName = "引継"; break;
                    case 6: PriKbnName = "発行"; break;
                    case 7: PriKbnName = "追加入金"; break;
                    case 8: PriKbnName = "再発行"; break;
                    case 9: PriKbnName = "返金"; break;
                    default: PriKbnName = "不明"; break;
                }
                this.dataGridView1.Rows[RowNo].Cells["PriCordFlag"].Value = PriKbnName;
            }
            //店舗名部分を改行可にする。
            this.dataGridView1.Columns["Tempo"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //数値を3桁カンマ区切りにする
            dataGridView1.Columns["AmountSolid"].DefaultCellStyle.Format = "#,0";
            dataGridView1.Columns["AmountPoint"].DefaultCellStyle.Format = "#,0";
            dataGridView1.Columns["NewPoint"].DefaultCellStyle.Format = "#,0";
            dataGridView1.Columns["TradePoint"].DefaultCellStyle.Format = "#,0";
            dataGridView1.Columns["CumulativePoint"].DefaultCellStyle.Format = "#,0";
            dataGridView1.Columns["InPrice"].DefaultCellStyle.Format = "#,0";
            dataGridView1.Columns["RemainingAmount"].DefaultCellStyle.Format = "#,0";
            //数値を右寄せにする
            dataGridView1.Columns["AmountSolid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["AmountPoint"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["NewPoint"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["TradePoint"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["CumulativePoint"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["InPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["RemainingAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //ソートを利用日時の新しい順にする。
            dataGridView1.Sort(dataGridView1.Columns["datetime"], ListSortDirection.Descending);
            //左２列（カード番号、名前）でロック
            dataGridView1.Columns["CordName"].Frozen = true;

            //ヘッダー　設定
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.EnableHeadersVisualStyles = false;
            AmountPoint.HeaderText = "売上" + Environment.NewLine +"ポイント";
            NewPoint.HeaderText = "新規" + Environment.NewLine + "ポイント";
            TradePoint.HeaderText = "交換" + Environment.NewLine + "ポイント";
            CumulativePoint.HeaderText = "累計" + Environment.NewLine + "ポイント";
        }

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
    }
}
