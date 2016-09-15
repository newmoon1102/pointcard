namespace _wp_point.Rest
{
    partial class frmLogin
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo", 21.75F);
            this.label1.Location = new System.Drawing.Point(195, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 44);
            this.label1.TabIndex = 4;
            this.label1.Text = "店舗ID";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo", 21.75F);
            this.label2.Location = new System.Drawing.Point(195, 344);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 44);
            this.label2.TabIndex = 5;
            this.label2.Text = "パスワード";
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLogin.BackColor = System.Drawing.Color.Orange;
            this.btnLogin.Font = new System.Drawing.Font("Meiryo", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(465, 441);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(346, 70);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "ログイン";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtUserId
            // 
            this.txtUserId.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtUserId.Font = new System.Drawing.Font("Meiryo", 21.75F);
            this.txtUserId.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtUserId.Location = new System.Drawing.Point(366, 259);
            this.txtUserId.MaximumSize = new System.Drawing.Size(600, 50);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(548, 51);
            this.txtUserId.TabIndex = 1;
            this.txtUserId.MouseClick += new System.Windows.Forms.MouseEventHandler(this._MouseClick);
            this.txtUserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this._KeyDown);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPassWord.Font = new System.Drawing.Font("Meiryo", 21.75F);
            this.txtPassWord.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtPassWord.Location = new System.Drawing.Point(366, 344);
            this.txtPassWord.MaximumSize = new System.Drawing.Size(600, 50);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(548, 51);
            this.txtPassWord.TabIndex = 2;
            this.txtPassWord.MouseClick += new System.Windows.Forms.MouseEventHandler(this._MouseClick);
            this.txtPassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassWord_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Meiryo", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(301, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(654, 72);
            this.label3.TabIndex = 6;
            this.label3.Text = "ポイントカード制御用アプリ";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1264, 658);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtUserId;
    }
}