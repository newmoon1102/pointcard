namespace _wp_point.Rest
{
    partial class frmQRCode
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
            this.btn_qr_cancel = new System.Windows.Forms.Button();
            this.timerReadQRcode = new System.Windows.Forms.Timer(this.components);
            this.timer_Wait = new System.Windows.Forms.Timer(this.components);
            this.lbtitle1 = new System.Windows.Forms.Label();
            this.lbtitle_Card = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lbtitle2 = new System.Windows.Forms.Label();
            this.lbtitle3 = new System.Windows.Forms.Label();
            this.lbCardid = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_qr_cancel
            // 
            this.btn_qr_cancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_qr_cancel.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_qr_cancel.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.btn_qr_cancel.ForeColor = System.Drawing.Color.White;
            this.btn_qr_cancel.Location = new System.Drawing.Point(550, 525);
            this.btn_qr_cancel.Name = "btn_qr_cancel";
            this.btn_qr_cancel.Size = new System.Drawing.Size(250, 67);
            this.btn_qr_cancel.TabIndex = 2;
            this.btn_qr_cancel.Text = "戻る";
            this.btn_qr_cancel.UseVisualStyleBackColor = false;
            this.btn_qr_cancel.Click += new System.EventHandler(this.btn_qr_cancel_Click);
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
            this.lbtitle1.TabIndex = 6;
            this.lbtitle1.Text = "表示されているカード番号と、作成されたカードの番号をチェックしてください！";
            // 
            // lbtitle_Card
            // 
            this.lbtitle_Card.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle_Card.AutoSize = true;
            this.lbtitle_Card.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbtitle_Card.Location = new System.Drawing.Point(450, 123);
            this.lbtitle_Card.Margin = new System.Windows.Forms.Padding(0);
            this.lbtitle_Card.Name = "lbtitle_Card";
            this.lbtitle_Card.Size = new System.Drawing.Size(180, 41);
            this.lbtitle_Card.TabIndex = 4;
            this.lbtitle_Card.Text = "カード番号：";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(400, 167);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(510, 343);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // lbtitle2
            // 
            this.lbtitle2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle2.AutoSize = true;
            this.lbtitle2.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbtitle2.Location = new System.Drawing.Point(207, 87);
            this.lbtitle2.Name = "lbtitle2";
            this.lbtitle2.Size = new System.Drawing.Size(1005, 41);
            this.lbtitle2.TabIndex = 7;
            this.lbtitle2.Text = "同じ番号であることを確認し、カードのQRコードをカメラにかざしてください。";
            // 
            // lbtitle3
            // 
            this.lbtitle3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle3.AutoSize = true;
            this.lbtitle3.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbtitle3.Location = new System.Drawing.Point(406, 87);
            this.lbtitle3.Name = "lbtitle3";
            this.lbtitle3.Size = new System.Drawing.Size(519, 41);
            this.lbtitle3.TabIndex = 9;
            this.lbtitle3.Text = "カメラにQR会員証をかざしてください。";
            // 
            // lbCardid
            // 
            this.lbCardid.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbCardid.AutoSize = true;
            this.lbCardid.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.lbCardid.Location = new System.Drawing.Point(625, 123);
            this.lbCardid.Margin = new System.Windows.Forms.Padding(0);
            this.lbCardid.Name = "lbCardid";
            this.lbCardid.Size = new System.Drawing.Size(211, 41);
            this.lbCardid.TabIndex = 10;
            this.lbCardid.Text = "0000-000000";
            // 
            // frmQRCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1314, 622);
            this.ControlBox = false;
            this.Controls.Add(this.lbCardid);
            this.Controls.Add(this.lbtitle3);
            this.Controls.Add(this.lbtitle2);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.lbtitle1);
            this.Controls.Add(this.btn_qr_cancel);
            this.Controls.Add(this.lbtitle_Card);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmQRCode";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmQRCode";
            this.Load += new System.EventHandler(this.frmQRCode1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btn_qr_cancel;
        private System.Windows.Forms.Timer timerReadQRcode;
        private System.Windows.Forms.Timer timer_Wait;
        private System.Windows.Forms.Label lbtitle_Card;
        private System.Windows.Forms.Label lbtitle1;
        private System.Windows.Forms.Label lbtitle2;
        private System.Windows.Forms.Label lbtitle3;
        private System.Windows.Forms.Label lbCardid;
    }
}