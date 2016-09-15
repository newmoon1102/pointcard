using _wp_point.Rest.Class;
using System;
using System.Windows.Forms;
using _wp_point.Rest.Request;
using log4net;
using System.Reflection;
using System.Drawing;
using System.Data.SqlClient;

namespace _wp_point.Rest
{
    public partial class frmList : Form
    {
        private readonly frmMain _frm;
        MemberImportRequest RegisterData;
        private ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string[] arrData;

        public frmList(frmMain fr)
        {
            InitializeComponent();
            this._frm = fr;
            RegisterData = new MemberImportRequest();
        }

        private void frmList_Load(object sender, EventArgs e)
        {
            dataNewMember_display();
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

        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DataGridViewRow row = this.dataNewMember.Rows[e.RowIndex];
                int columnindex = dataNewMember.CurrentCell.ColumnIndex;
                int id = Convert.ToInt32(row.Cells[columnindex].Value);

                RegisterData = Data.geMemberdata(id);

                if (this._frm.ResetOpenWindow("frmRegister"))
                {
                    frmRegister childForm = new frmRegister(_frm, this.Name.ToString(), RegisterData);

                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                }
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 7)
            {
                DataGridViewRow row = this.dataNewMember.Rows[e.RowIndex];
                int recipt_id = Convert.ToInt32(row.Cells[0].Value);

                DialogResult result = MsgBox.Show("受付番号 " + recipt_id + " の申込を削除しますか？", "確認", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    if (Data.deleteRecipt(recipt_id))
                    {
                        _logger.InfoFormat("受付番号 {0}　を削除しました。", recipt_id);
                        MsgBox.Show("申込を削除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // refresh datagridview
                        dataNewMember.Rows.Clear();
                        dataNewMember.Refresh();
                        // redisplay data
                        dataNewMember_display();
                    }
                    else
                    {
                        _logger.Error("申込の削除に失敗しました。");
                        MsgBox.Show("申込の削除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void dataNewMember_display()
        {
            try
            {
                SqlConnection con = new SqlConnection();

                if (Common.SqlCon(con))
                {
                    SqlDataReader rd = (null);
                    try
                    {
                        SqlCommand cmd = new SqlCommand("SELECT * FROM NEW_RECEIPT_LIST WHERE CARD_FLG = 'FALSE'", con);
                        rd = cmd.ExecuteReader();
                        int RowNo;
                        while (rd.Read())
                        {
                            RowNo = this.dataNewMember.Rows.Add();
                            this.dataNewMember.Rows[RowNo].Cells["RECEIPT_ID"].Value = rd["RECEIPT_ID"].ToString();
                            this.dataNewMember.Rows[RowNo].Cells["RECEIPT_DATE"].Value = !String.IsNullOrEmpty(rd["RECEIPT_DATE"].ToString())?rd["RECEIPT_DATE"].ToString().Substring(0, rd["RECEIPT_DATE"].ToString().Length - 3):"";
                            this.dataNewMember.Rows[RowNo].Cells["LAST_NAME"].Value = rd["LAST_NAME"].ToString();
                            this.dataNewMember.Rows[RowNo].Cells["FIRT_NAME"].Value = rd["FIRT_NAME"].ToString();
                            this.dataNewMember.Rows[RowNo].Cells["LAST_NAME_Y"].Value = rd["LAST_NAME_Y"].ToString();
                            this.dataNewMember.Rows[RowNo].Cells["FIRT_NAME_Y"].Value = rd["FIRT_NAME_Y"].ToString();
                            string sex = "";
                            switch (rd["SEX"].ToString())
                            {
                                case "1": sex = "男性"; break;
                                case "2": sex = "女性"; break;
                                default: sex = ""; break;
                            }
                            this.dataNewMember.Rows[RowNo].Cells["SEX"].Value = sex;
                            this.dataNewMember.Rows[RowNo].Cells["DELETE"].Value = "削除";
                        }

                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                    }
                }
                else
                {
                    MsgBox.Show("データベースへの接続に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // TODO: This line of code loads data into the 'pOINTCARDDataSet.NEW_RECEIPT_LIST' table. You can move, or remove it, as needed.
                //this.nEW_RECEIPT_LISTTableAdapter.Fill(this.pOINTCARDDataSet.NEW_RECEIPT_LIST);
                this.dataNewMember.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 18, FontStyle.Bold);
                this.dataNewMember.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                this.dataNewMember.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                this.dataNewMember.EnableHeadersVisualStyles = false;
                this.dataNewMember.DefaultCellStyle.Font = new Font("Tahoma", 20);
            }
            catch
            {
                _logger.Error("データベースへの接続に失敗しました。");
                MsgBox.Show("データベースへの接続に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
