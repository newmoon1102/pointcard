namespace _wp_point.Rest.Replace
{
    partial class frmNewMember
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
            this.btnCardNotExist = new System.Windows.Forms.Button();
            this.btnQrInput = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnCardInput = new System.Windows.Forms.Button();
            this.lbtitle3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCardNotExist
            // 
            this.btnCardNotExist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCardNotExist.BackColor = System.Drawing.Color.Gray;
            this.btnCardNotExist.Font = new System.Drawing.Font("メイリオ", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCardNotExist.ForeColor = System.Drawing.Color.White;
            this.btnCardNotExist.Location = new System.Drawing.Point(998, 61);
            this.btnCardNotExist.Name = "btnCardNotExist";
            this.btnCardNotExist.Size = new System.Drawing.Size(179, 57);
            this.btnCardNotExist.TabIndex = 4;
            this.btnCardNotExist.Text = "カード(無)";
            this.btnCardNotExist.UseVisualStyleBackColor = false;
            this.btnCardNotExist.Click += new System.EventHandler(this.btnCardNotExist_Click);
            // 
            // btnQrInput
            // 
            this.btnQrInput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnQrInput.BackColor = System.Drawing.Color.White;
            this.btnQrInput.BackgroundImage = global::_wp_point.Properties.Resources._09;
            this.btnQrInput.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.btnQrInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnQrInput.Location = new System.Drawing.Point(163, 250);
            this.btnQrInput.Name = "btnQrInput";
            this.btnQrInput.Size = new System.Drawing.Size(400, 200);
            this.btnQrInput.TabIndex = 1;
            this.btnQrInput.UseVisualStyleBackColor = false;
            this.btnQrInput.Click += new System.EventHandler(this.btnQrInput_Click);
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
            // btnCardInput
            // 
            this.btnCardInput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCardInput.BackColor = System.Drawing.Color.White;
            this.btnCardInput.BackgroundImage = global::_wp_point.Properties.Resources._10;
            this.btnCardInput.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.btnCardInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnCardInput.Location = new System.Drawing.Point(669, 250);
            this.btnCardInput.Name = "btnCardInput";
            this.btnCardInput.Size = new System.Drawing.Size(400, 200);
            this.btnCardInput.TabIndex = 6;
            this.btnCardInput.UseVisualStyleBackColor = false;
            this.btnCardInput.Click += new System.EventHandler(this.btnCardInput_Click);
            // 
            // lbtitle3
            // 
            this.lbtitle3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle3.AutoSize = true;
            this.lbtitle3.Font = new System.Drawing.Font("メイリオ", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbtitle3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbtitle3.Location = new System.Drawing.Point(368, 63);
            this.lbtitle3.Name = "lbtitle3";
            this.lbtitle3.Size = new System.Drawing.Size(468, 55);
            this.lbtitle3.TabIndex = 14;
            this.lbtitle3.Text = "譲渡先会員情報の取得方法";
            // 
            // frmNewMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1232, 580);
            this.ControlBox = false;
            this.Controls.Add(this.lbtitle3);
            this.Controls.Add(this.btnCardInput);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnCardNotExist);
            this.Controls.Add(this.btnQrInput);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNewMember";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCardNotExist;
        private System.Windows.Forms.Button btnQrInput;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnCardInput;
        private System.Windows.Forms.Label lbtitle3;
    }
}