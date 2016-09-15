namespace _wp_point.Rest.Reissue
{
    partial class frmSuccess
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTop = new System.Windows.Forms.Button();
            this.labelResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnTop);
            this.panel1.Controls.Add(this.labelResult);
            this.panel1.Location = new System.Drawing.Point(285, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 427);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(81, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(532, 48);
            this.label2.TabIndex = 4;
            this.label2.Text = "カードをお客様へお渡しください。";
            this.label2.Visible = false;
            // 
            // btnTop
            // 
            this.btnTop.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTop.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnTop.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnTop.ForeColor = System.Drawing.Color.White;
            this.btnTop.Location = new System.Drawing.Point(211, 232);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(244, 67);
            this.btnTop.TabIndex = 3;
            this.btnTop.Text = "TOPへ";
            this.btnTop.UseVisualStyleBackColor = false;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // labelResult
            // 
            this.labelResult.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelResult.AutoSize = true;
            this.labelResult.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResult.ForeColor = System.Drawing.Color.Black;
            this.labelResult.Location = new System.Drawing.Point(51, 95);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(596, 48);
            this.labelResult.TabIndex = 2;
            this.labelResult.Text = "交換したカードの登録が完了しました。";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(130, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(408, 28);
            this.label1.TabIndex = 5;
            this.label1.Text = "(古いカードは必ず店舗側で預かってください)";
            // 
            // frmSuccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1232, 580);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSuccess";
            this.ShowInTaskbar = false;
            this.Text = "frmSuccess";
            this.Shown += new System.EventHandler(this.frmSuccess_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label label1;
    }
}