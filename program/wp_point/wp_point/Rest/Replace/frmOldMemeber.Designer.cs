namespace _wp_point.Rest.Replace
{
    partial class frmOldMemeber
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
            this.btnCardNo = new System.Windows.Forms.Button();
            this.btnQRCode = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.lbtitle3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCardNo
            // 
            this.btnCardNo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCardNo.BackColor = System.Drawing.Color.White;
            this.btnCardNo.BackgroundImage = global::_wp_point.Properties.Resources._06;
            this.btnCardNo.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.btnCardNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnCardNo.Location = new System.Drawing.Point(669, 250);
            this.btnCardNo.Name = "btnCardNo";
            this.btnCardNo.Size = new System.Drawing.Size(400, 200);
            this.btnCardNo.TabIndex = 4;
            this.btnCardNo.UseVisualStyleBackColor = false;
            this.btnCardNo.Click += new System.EventHandler(this.btnCardNo_Click);
            // 
            // btnQRCode
            // 
            this.btnQRCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnQRCode.BackColor = System.Drawing.Color.White;
            this.btnQRCode.BackgroundImage = global::_wp_point.Properties.Resources._05;
            this.btnQRCode.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnQRCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnQRCode.Location = new System.Drawing.Point(163, 250);
            this.btnQRCode.Name = "btnQRCode";
            this.btnQRCode.Size = new System.Drawing.Size(400, 200);
            this.btnQRCode.TabIndex = 1;
            this.btnQRCode.UseVisualStyleBackColor = false;
            this.btnQRCode.Click += new System.EventHandler(this.btnQRCode_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnReturn.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(48, 61);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(159, 57);
            this.btnReturn.TabIndex = 6;
            this.btnReturn.Text = "戻る";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lbtitle3
            // 
            this.lbtitle3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle3.AutoSize = true;
            this.lbtitle3.Font = new System.Drawing.Font("メイリオ", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lbtitle3.Location = new System.Drawing.Point(368, 63);
            this.lbtitle3.Name = "lbtitle3";
            this.lbtitle3.Size = new System.Drawing.Size(468, 55);
            this.lbtitle3.TabIndex = 13;
            this.lbtitle3.Text = "譲渡元会員情報の取得方法";
            // 
            // frmOldMemeber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1232, 580);
            this.ControlBox = false;
            this.Controls.Add(this.lbtitle3);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnCardNo);
            this.Controls.Add(this.btnQRCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmOldMemeber";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCardNo;
        private System.Windows.Forms.Button btnQRCode;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label lbtitle3;
    }
}