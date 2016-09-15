namespace _wp_point.Rest
{
    partial class frmReissue
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
            this.btnTop = new System.Windows.Forms.Button();
            this.btnNotRead = new System.Windows.Forms.Button();
            this.btnQRCode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTop
            // 
            this.btnTop.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnTop.Font = new System.Drawing.Font("Meiryo", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnTop.ForeColor = System.Drawing.Color.White;
            this.btnTop.Location = new System.Drawing.Point(48, 61);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(159, 57);
            this.btnTop.TabIndex = 2;
            this.btnTop.Text = "TOPへ";
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // btnNotRead
            // 
            this.btnNotRead.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNotRead.BackgroundImage = global::_wp_point.Properties.Resources._04;
            this.btnNotRead.Font = new System.Drawing.Font("MS PGothic", 20.25F);
            this.btnNotRead.ForeColor = System.Drawing.Color.White;
            this.btnNotRead.Location = new System.Drawing.Point(669, 206);
            this.btnNotRead.Name = "btnNotRead";
            this.btnNotRead.Size = new System.Drawing.Size(400, 200);
            this.btnNotRead.TabIndex = 8;
            this.btnNotRead.UseVisualStyleBackColor = false;
            this.btnNotRead.Click += new System.EventHandler(this.btnNotRead_Click);
            // 
            // btnQRCode
            // 
            this.btnQRCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnQRCode.BackgroundImage = global::_wp_point.Properties.Resources._03;
            this.btnQRCode.Font = new System.Drawing.Font("MS PGothic", 20.25F);
            this.btnQRCode.ForeColor = System.Drawing.Color.White;
            this.btnQRCode.Location = new System.Drawing.Point(163, 206);
            this.btnQRCode.Name = "btnQRCode";
            this.btnQRCode.Size = new System.Drawing.Size(400, 200);
            this.btnQRCode.TabIndex = 8;
            this.btnQRCode.UseVisualStyleBackColor = false;
            this.btnQRCode.Click += new System.EventHandler(this.btnQRCode_Click);
            // 
            // frmReissue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1232, 580);
            this.ControlBox = false;
            this.Controls.Add(this.btnNotRead);
            this.Controls.Add(this.btnQRCode);
            this.Controls.Add(this.btnTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReissue";
            this.ShowInTaskbar = false;
            this.Text = "frmReissue";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.Button btnQRCode;
        private System.Windows.Forms.Button btnNotRead;
    }
}