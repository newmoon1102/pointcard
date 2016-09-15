namespace _wp_point.Rest.Reissue
{
    partial class frmNR_NewQRCodeInput
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
            this.btnReturn = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.timerReadQRcode = new System.Windows.Forms.Timer(this.components);
            this.timer_Wait = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbtitle_Card = new System.Windows.Forms.Label();
            this.lbCardid = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReturn.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnReturn.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(550, 525);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(250, 67);
            this.btnReturn.TabIndex = 5;
            this.btnReturn.Text = "戻る";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(400, 167);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(510, 343);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // timerReadQRcode
            // 
            this.timerReadQRcode.Tick += new System.EventHandler(this.timerReadQRcode_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(594, 530);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(364, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(519, 41);
            this.label3.TabIndex = 14;
            this.label3.Text = "新しいカードのQRコードを読取ります。";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(364, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(627, 41);
            this.label4.TabIndex = 15;
            this.label4.Text = "新しいカードのQRをカメラにかざしてください。";
            // 
            // lbtitle_Card
            // 
            this.lbtitle_Card.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle_Card.AutoSize = true;
            this.lbtitle_Card.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbtitle_Card.Location = new System.Drawing.Point(456, 123);
            this.lbtitle_Card.Name = "lbtitle_Card";
            this.lbtitle_Card.Size = new System.Drawing.Size(180, 41);
            this.lbtitle_Card.TabIndex = 17;
            this.lbtitle_Card.Text = "カード番号：";
            // 
            // lbCardid
            // 
            this.lbCardid.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbCardid.AutoSize = true;
            this.lbCardid.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbCardid.Location = new System.Drawing.Point(636, 123);
            this.lbCardid.Name = "lbCardid";
            this.lbCardid.Size = new System.Drawing.Size(211, 41);
            this.lbCardid.TabIndex = 16;
            this.lbCardid.Text = "0000-000000";
            // 
            // frmNR_NewQRCodeInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1314, 622);
            this.ControlBox = false;
            this.Controls.Add(this.lbtitle_Card);
            this.Controls.Add(this.lbCardid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNR_NewQRCodeInput";
            this.ShowInTaskbar = false;
            this.Text = "frmQRCodeInput";
            this.Load += new System.EventHandler(this.frmQRCodeInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Timer timerReadQRcode;
        private System.Windows.Forms.Timer timer_Wait;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbtitle_Card;
        private System.Windows.Forms.Label lbCardid;
    }
}