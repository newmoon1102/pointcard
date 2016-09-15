namespace _wp_point.Rest.Reissue
{
    partial class frmQRCodeInput
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
            this.lbCardid = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.timerReadQRcode = new System.Windows.Forms.Timer(this.components);
            this.timer_Wait = new System.Windows.Forms.Timer(this.components);
            this.lbtitle1 = new System.Windows.Forms.Label();
            this.lbtitle2 = new System.Windows.Forms.Label();
            this.lbtitle_Card = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReturn.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnReturn.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(550, 525);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(250, 67);
            this.btnReturn.TabIndex = 5;
            this.btnReturn.Text = "戻る";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lbCardid
            // 
            this.lbCardid.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbCardid.AutoSize = true;
            this.lbCardid.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbCardid.Location = new System.Drawing.Point(630, 123);
            this.lbCardid.Name = "lbCardid";
            this.lbCardid.Size = new System.Drawing.Size(211, 41);
            this.lbCardid.TabIndex = 11;
            this.lbCardid.Text = "0000-000000";
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
            // lbtitle1
            // 
            this.lbtitle1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle1.AutoSize = true;
            this.lbtitle1.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbtitle1.ForeColor = System.Drawing.Color.Red;
            this.lbtitle1.Location = new System.Drawing.Point(181, 47);
            this.lbtitle1.Name = "lbtitle1";
            this.lbtitle1.Size = new System.Drawing.Size(1017, 41);
            this.lbtitle1.TabIndex = 8;
            this.lbtitle1.Text = "表示されているカード番号と、作成されたカードの番号をチェックしてください！";
            // 
            // lbtitle2
            // 
            this.lbtitle2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle2.AutoSize = true;
            this.lbtitle2.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbtitle2.Location = new System.Drawing.Point(207, 87);
            this.lbtitle2.Name = "lbtitle2";
            this.lbtitle2.Size = new System.Drawing.Size(1005, 41);
            this.lbtitle2.TabIndex = 9;
            this.lbtitle2.Text = "同じ番号であることを確認し、カードのQRコードをカメラにかざしてください。";
            // 
            // lbtitle_Card
            // 
            this.lbtitle_Card.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle_Card.AutoSize = true;
            this.lbtitle_Card.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbtitle_Card.Location = new System.Drawing.Point(450, 123);
            this.lbtitle_Card.Name = "lbtitle_Card";
            this.lbtitle_Card.Size = new System.Drawing.Size(180, 41);
            this.lbtitle_Card.TabIndex = 10;
            this.lbtitle_Card.Text = "カード番号：";
            // 
            // frmQRCodeInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1314, 622);
            this.ControlBox = false;
            this.Controls.Add(this.lbtitle_Card);
            this.Controls.Add(this.lbtitle2);
            this.Controls.Add(this.lbtitle1);
            this.Controls.Add(this.lbCardid);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmQRCodeInput";
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
        private System.Windows.Forms.Label lbCardid;
        private System.Windows.Forms.Timer timerReadQRcode;
        private System.Windows.Forms.Timer timer_Wait;
        private System.Windows.Forms.Label lbtitle1;
        private System.Windows.Forms.Label lbtitle2;
        private System.Windows.Forms.Label lbtitle_Card;
    }
}