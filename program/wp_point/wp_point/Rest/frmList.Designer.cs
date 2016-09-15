namespace _wp_point.Rest
{
    partial class frmList
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnTop = new System.Windows.Forms.Button();
            this.dataNewMember = new System.Windows.Forms.DataGridView();
            this.nEWRECEIPTLISTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pOINTCARDDataSet = new _wp_point.POINTCARDDataSet();
            this.nEW_RECEIPT_LISTTableAdapter = new _wp_point.POINTCARDDataSetTableAdapters.NEW_RECEIPT_LISTTableAdapter();
            this.RECEIPT_ID = new System.Windows.Forms.DataGridViewButtonColumn();
            this.RECEIPT_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAST_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIRT_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAST_NAME_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIRT_NAME_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DELETE = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataNewMember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nEWRECEIPTLISTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pOINTCARDDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTop
            // 
            this.btnTop.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnTop.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnTop.ForeColor = System.Drawing.Color.White;
            this.btnTop.Location = new System.Drawing.Point(48, 56);
            this.btnTop.Margin = new System.Windows.Forms.Padding(5);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(149, 55);
            this.btnTop.TabIndex = 0;
            this.btnTop.Text = "TOPへ";
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // dataNewMember
            // 
            this.dataNewMember.AllowUserToAddRows = false;
            this.dataNewMember.AllowUserToDeleteRows = false;
            this.dataNewMember.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataNewMember.BackgroundColor = System.Drawing.Color.White;
            this.dataNewMember.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataNewMember.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataNewMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataNewMember.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RECEIPT_ID,
            this.RECEIPT_DATE,
            this.LAST_NAME,
            this.FIRT_NAME,
            this.LAST_NAME_Y,
            this.FIRT_NAME_Y,
            this.SEX,
            this.DELETE});
            this.dataNewMember.GridColor = System.Drawing.Color.Black;
            this.dataNewMember.Location = new System.Drawing.Point(20, 144);
            this.dataNewMember.Margin = new System.Windows.Forms.Padding(0);
            this.dataNewMember.Name = "dataNewMember";
            this.dataNewMember.ReadOnly = true;
            this.dataNewMember.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataNewMember.RowHeadersWidth = 15;
            this.dataNewMember.RowTemplate.Height = 50;
            this.dataNewMember.Size = new System.Drawing.Size(1304, 415);
            this.dataNewMember.TabIndex = 1;
            this.dataNewMember.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseClick);
            // 
            // nEWRECEIPTLISTBindingSource
            // 
            this.nEWRECEIPTLISTBindingSource.DataMember = "NEW_RECEIPT_LIST";
            this.nEWRECEIPTLISTBindingSource.DataSource = this.pOINTCARDDataSet;
            // 
            // pOINTCARDDataSet
            // 
            this.pOINTCARDDataSet.DataSetName = "POINTCARDDataSet";
            this.pOINTCARDDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // nEW_RECEIPT_LISTTableAdapter
            // 
            this.nEW_RECEIPT_LISTTableAdapter.ClearBeforeFill = true;
            // 
            // RECEIPT_ID
            // 
            this.RECEIPT_ID.HeaderText = "受付   番号";
            this.RECEIPT_ID.Name = "RECEIPT_ID";
            this.RECEIPT_ID.ReadOnly = true;
            this.RECEIPT_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.RECEIPT_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.RECEIPT_ID.Width = 120;
            // 
            // RECEIPT_DATE
            // 
            this.RECEIPT_DATE.HeaderText = "受付日時";
            this.RECEIPT_DATE.Name = "RECEIPT_DATE";
            this.RECEIPT_DATE.ReadOnly = true;
            this.RECEIPT_DATE.Width = 250;
            // 
            // LAST_NAME
            // 
            this.LAST_NAME.HeaderText = "姓";
            this.LAST_NAME.Name = "LAST_NAME";
            this.LAST_NAME.ReadOnly = true;
            this.LAST_NAME.Width = 180;
            // 
            // FIRT_NAME
            // 
            this.FIRT_NAME.HeaderText = "名";
            this.FIRT_NAME.Name = "FIRT_NAME";
            this.FIRT_NAME.ReadOnly = true;
            this.FIRT_NAME.Width = 180;
            // 
            // LAST_NAME_Y
            // 
            this.LAST_NAME_Y.HeaderText = "せい";
            this.LAST_NAME_Y.Name = "LAST_NAME_Y";
            this.LAST_NAME_Y.ReadOnly = true;
            this.LAST_NAME_Y.Width = 180;
            // 
            // FIRT_NAME_Y
            // 
            this.FIRT_NAME_Y.HeaderText = "めい";
            this.FIRT_NAME_Y.Name = "FIRT_NAME_Y";
            this.FIRT_NAME_Y.ReadOnly = true;
            this.FIRT_NAME_Y.Width = 180;
            // 
            // SEX
            // 
            this.SEX.FillWeight = 80F;
            this.SEX.HeaderText = "性別";
            this.SEX.Name = "SEX";
            this.SEX.ReadOnly = true;
            this.SEX.Width = 110;
            // 
            // DELETE
            // 
            this.DELETE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DELETE.HeaderText = "";
            this.DELETE.Name = "DELETE";
            this.DELETE.ReadOnly = true;
            this.DELETE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DELETE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // frmList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1344, 573);
            this.ControlBox = false;
            this.Controls.Add(this.dataNewMember);
            this.Controls.Add(this.btnTop);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmList";
            this.ShowInTaskbar = false;
            this.Text = "frmList";
            this.Load += new System.EventHandler(this.frmList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataNewMember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nEWRECEIPTLISTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pOINTCARDDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnTop;
        private POINTCARDDataSet pOINTCARDDataSet;
        private System.Windows.Forms.BindingSource nEWRECEIPTLISTBindingSource;
        private POINTCARDDataSetTableAdapters.NEW_RECEIPT_LISTTableAdapter nEW_RECEIPT_LISTTableAdapter;
        private System.Windows.Forms.DataGridView dataNewMember;
        private System.Windows.Forms.DataGridViewButtonColumn RECEIPT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECEIPT_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAST_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIRT_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAST_NAME_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIRT_NAME_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEX;
        private System.Windows.Forms.DataGridViewButtonColumn DELETE;
    }
}