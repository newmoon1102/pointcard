namespace _wp_point.Rest.Reissue
{
    partial class frmNotRead
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
            this.btnCardNo = new System.Windows.Forms.Button();
            this.btnQRCode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnReturn.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(48, 61);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(159, 57);
            this.btnReturn.TabIndex = 3;
            this.btnReturn.Text = "戻る";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnCardNo
            // 
            this.btnCardNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCardNo.BackColor = System.Drawing.Color.White;
            this.btnCardNo.BackgroundImage = global::_wp_point.Properties.Resources._02;
            this.btnCardNo.Font = new System.Drawing.Font("MS PGothic", 20.25F);
            this.btnCardNo.ForeColor = System.Drawing.Color.White;
            this.btnCardNo.Location = new System.Drawing.Point(669, 206);
            this.btnCardNo.Name = "btnCardNo";
            this.btnCardNo.Size = new System.Drawing.Size(400, 200);
            this.btnCardNo.TabIndex = 11;
            this.btnCardNo.UseVisualStyleBackColor = false;
            this.btnCardNo.Click += new System.EventHandler(this.btnCardNo_Click);
            // 
            // btnQRCode
            // 
            this.btnQRCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnQRCode.BackColor = System.Drawing.Color.White;
            this.btnQRCode.Font = new System.Drawing.Font("MS PGothic", 20.25F);
            this.btnQRCode.ForeColor = System.Drawing.Color.White;
            this.btnQRCode.Image = global::_wp_point.Properties.Resources._01;
            this.btnQRCode.Location = new System.Drawing.Point(163, 206);
            this.btnQRCode.Name = "btnQRCode";
            this.btnQRCode.Size = new System.Drawing.Size(400, 200);
            this.btnQRCode.TabIndex = 10;
            this.btnQRCode.UseVisualStyleBackColor = false;
            this.btnQRCode.Click += new System.EventHandler(this.btnQRCode_Click);
            // 
            // frmNotRead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1232, 580);
            this.ControlBox = false;
            this.Controls.Add(this.btnCardNo);
            this.Controls.Add(this.btnQRCode);
            this.Controls.Add(this.btnReturn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNotRead";
            this.ShowInTaskbar = false;
            this.Text = "frmNotRead";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnCardNo;
        private System.Windows.Forms.Button btnQRCode;
    }
}