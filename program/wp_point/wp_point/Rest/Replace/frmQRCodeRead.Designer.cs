namespace _wp_point.Rest.Replace
{
    partial class frmQRCodeRead
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
            this.lbtitle3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReturn.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnReturn.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(551, 517);
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
            this.pictureBox.Location = new System.Drawing.Point(406, 158);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(510, 343);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // timerReadQRcode
            // 
            this.timerReadQRcode.Tick += new System.EventHandler(this.timerReadQRcode_Tick);
            // 
            // lbtitle3
            // 
            this.lbtitle3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbtitle3.AutoSize = true;
            this.lbtitle3.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.lbtitle3.Location = new System.Drawing.Point(237, 107);
            this.lbtitle3.Name = "lbtitle3";
            this.lbtitle3.Size = new System.Drawing.Size(838, 48);
            this.lbtitle3.TabIndex = 12;
            this.lbtitle3.Text = "譲渡元カードのQRコードをカメラにかざしてください。";
            // 
            // frmQRCodeRead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1314, 622);
            this.ControlBox = false;
            this.Controls.Add(this.lbtitle3);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmQRCodeRead";
            this.ShowInTaskbar = false;
            this.Text = "frmQRCodeRead";
            this.Load += new System.EventHandler(this.frmQRCodeRead_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Timer timerReadQRcode;
        private System.Windows.Forms.Label lbtitle3;
    }
}