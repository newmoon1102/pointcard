namespace _wp_point.Rest.Replace
{
    partial class frmCardNoRead
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
            this.label2 = new System.Windows.Forms.Label();
            this.textCard_H = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.textCard_B = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnReturn.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(48, 61);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(159, 57);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "戻る";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 21.75F);
            this.label2.Location = new System.Drawing.Point(533, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 44);
            this.label2.TabIndex = 11;
            this.label2.Text = "-";
            // 
            // textCard_H
            // 
            this.textCard_H.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textCard_H.Font = new System.Drawing.Font("メイリオ", 21.75F);
            this.textCard_H.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textCard_H.Location = new System.Drawing.Point(393, 240);
            this.textCard_H.Name = "textCard_H";
            this.textCard_H.Size = new System.Drawing.Size(134, 51);
            this.textCard_H.TabIndex = 1;
            this.textCard_H.KeyDown += new System.Windows.Forms.KeyEventHandler(this._KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(222, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 44);
            this.label1.TabIndex = 9;
            this.label1.Text = "カード番号";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.Orange;
            this.btnSearch.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(501, 345);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(243, 55);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "確認";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // textCard_B
            // 
            this.textCard_B.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textCard_B.Font = new System.Drawing.Font("メイリオ", 21.75F);
            this.textCard_B.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textCard_B.Location = new System.Drawing.Point(569, 240);
            this.textCard_B.Name = "textCard_B";
            this.textCard_B.Size = new System.Drawing.Size(349, 51);
            this.textCard_B.TabIndex = 2;
            this.textCard_B.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcard_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(222, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(724, 48);
            this.label3.TabIndex = 13;
            this.label3.Text = "譲渡元ポイントカードの番号を入力して下さい。";
            // 
            // frmCardNoRead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1232, 580);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textCard_H);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textCard_B);
            this.Controls.Add(this.btnReturn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCardNoRead";
            this.ShowInTaskbar = false;
            this.Text = "frmCardNo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textCard_H;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox textCard_B;
        private System.Windows.Forms.Label label3;
    }
}