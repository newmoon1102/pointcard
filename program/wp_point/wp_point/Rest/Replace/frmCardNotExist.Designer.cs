namespace _wp_point.Rest.Replace
{
    partial class frmCardNotExist
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
            this.btnWebQRCode = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.lbtitle3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnWebId = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWebQRCode
            // 
            this.btnWebQRCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnWebQRCode.BackColor = System.Drawing.Color.White;
            this.btnWebQRCode.BackgroundImage = global::_wp_point.Properties.Resources._07;
            this.btnWebQRCode.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.btnWebQRCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnWebQRCode.Location = new System.Drawing.Point(163, 250);
            this.btnWebQRCode.Name = "btnWebQRCode";
            this.btnWebQRCode.Size = new System.Drawing.Size(400, 200);
            this.btnWebQRCode.TabIndex = 1;
            this.btnWebQRCode.UseVisualStyleBackColor = false;
            this.btnWebQRCode.Click += new System.EventHandler(this.btnWebQRCode_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnReturn.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(48, 61);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(159, 57);
            this.btnReturn.TabIndex = 5;
            this.btnReturn.Text = "戻る";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lbtitle3
            // 
            this.lbtitle3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle3.AutoSize = true;
            this.lbtitle3.Font = new System.Drawing.Font("メイリオ", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbtitle3.Location = new System.Drawing.Point(368, 63);
            this.lbtitle3.Name = "lbtitle3";
            this.lbtitle3.Size = new System.Drawing.Size(468, 55);
            this.lbtitle3.TabIndex = 15;
            this.lbtitle3.Text = "譲渡先会員情報の取得方法";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(372, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 33);
            this.label1.TabIndex = 16;
            this.label1.Text = "※ポイントカードが無い場合";
            // 
            // btnWebId
            // 
            this.btnWebId.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnWebId.BackColor = System.Drawing.Color.White;
            this.btnWebId.BackgroundImage = global::_wp_point.Properties.Resources._08;
            this.btnWebId.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.btnWebId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnWebId.Location = new System.Drawing.Point(669, 250);
            this.btnWebId.Name = "btnWebId";
            this.btnWebId.Size = new System.Drawing.Size(400, 200);
            this.btnWebId.TabIndex = 4;
            this.btnWebId.UseVisualStyleBackColor = false;
            this.btnWebId.Click += new System.EventHandler(this.btnWebId_Click);
            // 
            // frmCardNotExist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1232, 580);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbtitle3);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnWebId);
            this.Controls.Add(this.btnWebQRCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCardNotExist";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnWebQRCode;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label lbtitle3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnWebId;
    }
}