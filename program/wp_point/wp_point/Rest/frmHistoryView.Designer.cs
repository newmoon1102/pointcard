namespace _wp_point.Rest
{
    partial class frmHistoryView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MembersNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CordName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Furigana = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountSolid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TradePoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CumulativePoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemainingAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriCordFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GrayText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MembersNo,
            this.CordName,
            this.Furigana,
            this.datetime,
            this.Tempo,
            this.AmountSolid,
            this.AmountPoint,
            this.NewPoint,
            this.TradePoint,
            this.CumulativePoint,
            this.InPrice,
            this.RemainingAmount,
            this.PriCordFlag});
            this.dataGridView1.Location = new System.Drawing.Point(49, 121);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.Size = new System.Drawing.Size(1123, 398);
            this.dataGridView1.TabIndex = 0;
            // 
            // MembersNo
            // 
            this.MembersNo.HeaderText = "カード番号";
            this.MembersNo.Name = "MembersNo";
            this.MembersNo.ReadOnly = true;
            // 
            // CordName
            // 
            this.CordName.HeaderText = "名前";
            this.CordName.Name = "CordName";
            this.CordName.ReadOnly = true;
            // 
            // Furigana
            // 
            this.Furigana.HeaderText = "ふりがな";
            this.Furigana.Name = "Furigana";
            this.Furigana.ReadOnly = true;
            // 
            // datetime
            // 
            this.datetime.HeaderText = "利用日時";
            this.datetime.Name = "datetime";
            this.datetime.ReadOnly = true;
            this.datetime.Width = 120;
            // 
            // Tempo
            // 
            this.Tempo.HeaderText = "利用店舗";
            this.Tempo.Name = "Tempo";
            this.Tempo.ReadOnly = true;
            this.Tempo.Width = 150;
            // 
            // AmountSolid
            // 
            this.AmountSolid.HeaderText = "売上額";
            this.AmountSolid.Name = "AmountSolid";
            this.AmountSolid.ReadOnly = true;
            this.AmountSolid.Width = 110;
            // 
            // AmountPoint
            // 
            this.AmountPoint.HeaderText = "売上  ポイント";
            this.AmountPoint.Name = "AmountPoint";
            this.AmountPoint.ReadOnly = true;
            this.AmountPoint.Width = 110;
            // 
            // NewPoint
            // 
            this.NewPoint.HeaderText = "新規   ポイント";
            this.NewPoint.Name = "NewPoint";
            this.NewPoint.ReadOnly = true;
            this.NewPoint.Width = 110;
            // 
            // TradePoint
            // 
            this.TradePoint.HeaderText = "交換   ポイント";
            this.TradePoint.Name = "TradePoint";
            this.TradePoint.ReadOnly = true;
            this.TradePoint.Width = 110;
            // 
            // CumulativePoint
            // 
            this.CumulativePoint.HeaderText = "累計   ポイント";
            this.CumulativePoint.Name = "CumulativePoint";
            this.CumulativePoint.ReadOnly = true;
            this.CumulativePoint.Width = 110;
            // 
            // InPrice
            // 
            this.InPrice.HeaderText = "プリペイド入金額";
            this.InPrice.Name = "InPrice";
            this.InPrice.ReadOnly = true;
            this.InPrice.Width = 110;
            // 
            // RemainingAmount
            // 
            this.RemainingAmount.HeaderText = "プリペイド残高";
            this.RemainingAmount.Name = "RemainingAmount";
            this.RemainingAmount.ReadOnly = true;
            this.RemainingAmount.Width = 110;
            // 
            // PriCordFlag
            // 
            this.PriCordFlag.HeaderText = "プリカ処理区分";
            this.PriCordFlag.Name = "PriCordFlag";
            this.PriCordFlag.ReadOnly = true;
            this.PriCordFlag.Width = 120;
            // 
            // btnTop
            // 
            this.btnTop.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnTop.Font = new System.Drawing.Font("Meiryo", 24F, System.Drawing.FontStyle.Bold);
            this.btnTop.ForeColor = System.Drawing.Color.White;
            this.btnTop.Location = new System.Drawing.Point(48, 56);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(149, 55);
            this.btnTop.TabIndex = 1;
            this.btnTop.Text = "TOPへ";
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // frmHistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1232, 580);
            this.ControlBox = false;
            this.Controls.Add(this.btnTop);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHistoryView";
            this.ShowInTaskbar = false;
            this.Text = "frmHistoryView";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn MembersNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CordName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Furigana;
        private System.Windows.Forms.DataGridViewTextBoxColumn datetime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tempo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountSolid;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn TradePoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn CumulativePoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn InPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemainingAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriCordFlag;
    }
}