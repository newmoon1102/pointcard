namespace _wp_point.Rest.Reissue
{
    partial class frmCardNo
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
            this.label2 = new System.Windows.Forms.Label();
            this.textTempoCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
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
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 21.75F);
            this.label2.Location = new System.Drawing.Point(530, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 44);
            this.label2.TabIndex = 11;
            this.label2.Text = "-";
            // 
            // textTempoCode
            // 
            this.textTempoCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textTempoCode.Font = new System.Drawing.Font("メイリオ", 21.75F);
            this.textTempoCode.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textTempoCode.Location = new System.Drawing.Point(390, 205);
            this.textTempoCode.MaxLength = 4;
            this.textTempoCode.Name = "textTempoCode";
            this.textTempoCode.ReadOnly = true;
            this.textTempoCode.Size = new System.Drawing.Size(134, 51);
            this.textTempoCode.TabIndex = 1;
            this.textTempoCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTempoCode_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(251, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 36);
            this.label1.TabIndex = 9;
            this.label1.Text = "カード番号";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.Orange;
            this.btnSearch.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(470, 312);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(243, 55);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "確認";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtMemberID
            // 
            this.txtMemberID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMemberID.Font = new System.Drawing.Font("メイリオ", 21.75F);
            this.txtMemberID.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtMemberID.Location = new System.Drawing.Point(566, 205);
            this.txtMemberID.MaxLength = 6;
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.ReadOnly = true;
            this.txtMemberID.Size = new System.Drawing.Size(349, 51);
            this.txtMemberID.TabIndex = 2;
            this.txtMemberID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textTempoCode_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(250, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(774, 41);
            this.label3.TabIndex = 13;
            this.label3.Text = "新しいカードの番号と一致していることを確認してください。";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnEdit.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnEdit.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(943, 202);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(159, 57);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "編集";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // frmCardNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1232, 580);
            this.ControlBox = false;
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textTempoCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtMemberID);
            this.Controls.Add(this.btnReturn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCardNo";
            this.ShowInTaskbar = false;
            this.Text = "frmCardNo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textTempoCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEdit;
    }
}