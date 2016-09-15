namespace _wp_point.Rest
{
    partial class frmGuide
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
            this.btnCloseGuidefr = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCloseGuidefr
            // 
            this.btnCloseGuidefr.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCloseGuidefr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCloseGuidefr.Font = new System.Drawing.Font("Meiryo", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCloseGuidefr.ForeColor = System.Drawing.Color.White;
            this.btnCloseGuidefr.Location = new System.Drawing.Point(231, 278);
            this.btnCloseGuidefr.MaximumSize = new System.Drawing.Size(200, 50);
            this.btnCloseGuidefr.Name = "btnCloseGuidefr";
            this.btnCloseGuidefr.Size = new System.Drawing.Size(134, 33);
            this.btnCloseGuidefr.TabIndex = 0;
            this.btnCloseGuidefr.Text = "閉じる";
            this.btnCloseGuidefr.UseVisualStyleBackColor = false;
            this.btnCloseGuidefr.Click += new System.EventHandler(this.btnCloseGuidefr_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.label1.Location = new System.Drawing.Point(12, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(562, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "新規キーを押す、新しいカードを挿入してください";
            // 
            // frmGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(603, 323);
            this.ControlBox = false;
            this.Controls.Add(this.btnCloseGuidefr);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(603, 323);
            this.Name = "frmGuide";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGuide";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCloseGuidefr;
        private System.Windows.Forms.Label label1;
    }
}