namespace _wp_point.Rest.Replace
{
    partial class frmMemberID
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
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.cbbTitle = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnReturn.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(48, 61);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(159, 57);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "戻る";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.Orange;
            this.btnSearch.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(497, 339);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(243, 55);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "確認";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtInfo.Font = new System.Drawing.Font("メイリオ", 21.75F);
            this.txtInfo.Location = new System.Drawing.Point(497, 222);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(510, 51);
            this.txtInfo.TabIndex = 2;
            this.txtInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // cbbTitle
            // 
            this.cbbTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbbTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTitle.Font = new System.Drawing.Font("メイリオ", 21.75F);
            this.cbbTitle.FormattingEnabled = true;
            this.cbbTitle.Location = new System.Drawing.Point(249, 222);
            this.cbbTitle.Name = "cbbTitle";
            this.cbbTitle.Size = new System.Drawing.Size(234, 52);
            this.cbbTitle.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(241, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(861, 48);
            this.label1.TabIndex = 13;
            this.label1.Text = "譲渡先会員の会員IDかメールアドレスを入力して下さい。";
            // 
            // frmMemberID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1232, 580);
            this.ControlBox = false;
            this.Controls.Add(this.cbbTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.btnReturn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMemberID";
            this.ShowInTaskbar = false;
            this.Text = "frmWebID";
            this.Load += new System.EventHandler(this.frmMemberID_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.ComboBox cbbTitle;
        private System.Windows.Forms.Label label1;
    }
}