namespace _wp_point.Rest
{
    partial class frmSelect
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
            this.btnBatch = new System.Windows.Forms.Button();
            this.btnReissue = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnsetting = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnList = new System.Windows.Forms.Button();
            this.btnWP = new System.Windows.Forms.Button();
            this.btnGeneral = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBatch
            // 
            this.btnBatch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBatch.BackColor = System.Drawing.Color.White;
            this.btnBatch.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnBatch.Location = new System.Drawing.Point(106, 588);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(431, 123);
            this.btnBatch.TabIndex = 6;
            this.btnBatch.Text = "日次処理";
            this.btnBatch.UseVisualStyleBackColor = false;
            this.btnBatch.Click += new System.EventHandler(this.btnBatch_Click);
            // 
            // btnReissue
            // 
            this.btnReissue.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReissue.BackColor = System.Drawing.Color.White;
            this.btnReissue.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnReissue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnReissue.Location = new System.Drawing.Point(631, 308);
            this.btnReissue.Name = "btnReissue";
            this.btnReissue.Size = new System.Drawing.Size(431, 123);
            this.btnReissue.TabIndex = 5;
            this.btnReissue.Text = "カード再発行";
            this.btnReissue.UseVisualStyleBackColor = false;
            this.btnReissue.Click += new System.EventHandler(this.btnReissue_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.BackColor = System.Drawing.Color.Blue;
            this.btnClose.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(48, 67);
            this.btnClose.MaximumSize = new System.Drawing.Size(431, 81);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(181, 52);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "ログアウト";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnsetting
            // 
            this.btnsetting.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnsetting.BackColor = System.Drawing.Color.Gray;
            this.btnsetting.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 20.25F);
            this.btnsetting.ForeColor = System.Drawing.Color.White;
            this.btnsetting.Location = new System.Drawing.Point(972, 720);
            this.btnsetting.MaximumSize = new System.Drawing.Size(431, 81);
            this.btnsetting.Name = "btnsetting";
            this.btnsetting.Size = new System.Drawing.Size(163, 52);
            this.btnsetting.TabIndex = 8;
            this.btnsetting.Text = "設定";
            this.btnsetting.UseVisualStyleBackColor = false;
            this.btnsetting.Click += new System.EventHandler(this.btnsetting_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnSearch.Location = new System.Drawing.Point(631, 168);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(431, 123);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "カード取引履歴照会\r\n(当日分)";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnCreatCard_Click);
            // 
            // btnList
            // 
            this.btnList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnList.BackColor = System.Drawing.Color.White;
            this.btnList.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnList.Location = new System.Drawing.Point(106, 168);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(431, 123);
            this.btnList.TabIndex = 1;
            this.btnList.Text = "申込一覧";
            this.btnList.UseVisualStyleBackColor = false;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // btnWP
            // 
            this.btnWP.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnWP.BackColor = System.Drawing.Color.White;
            this.btnWP.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnWP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnWP.Location = new System.Drawing.Point(106, 448);
            this.btnWP.Name = "btnWP";
            this.btnWP.Size = new System.Drawing.Size(431, 123);
            this.btnWP.TabIndex = 3;
            this.btnWP.Text = "追加発行\r\n(WP会員の方)";
            this.btnWP.UseVisualStyleBackColor = false;
            this.btnWP.Click += new System.EventHandler(this.btnWP_Click);
            // 
            // btnGeneral
            // 
            this.btnGeneral.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGeneral.BackColor = System.Drawing.Color.White;
            this.btnGeneral.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnGeneral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnGeneral.Location = new System.Drawing.Point(106, 308);
            this.btnGeneral.Name = "btnGeneral";
            this.btnGeneral.Size = new System.Drawing.Size(431, 123);
            this.btnGeneral.TabIndex = 2;
            this.btnGeneral.Text = "新規発行\r\n(WP会員でない方)";
            this.btnGeneral.UseVisualStyleBackColor = false;
            this.btnGeneral.Click += new System.EventHandler(this.btnGeneral_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReplace.BackColor = System.Drawing.Color.White;
            this.btnReplace.Font = new System.Drawing.Font("メイリオ", 24F, System.Drawing.FontStyle.Bold);
            this.btnReplace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(201)))));
            this.btnReplace.Location = new System.Drawing.Point(631, 448);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(431, 123);
            this.btnReplace.TabIndex = 7;
            this.btnReplace.Text = "カード譲渡";
            this.btnReplace.UseVisualStyleBackColor = false;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // frmSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1168, 780);
            this.ControlBox = false;
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnBatch);
            this.Controls.Add(this.btnReissue);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGeneral);
            this.Controls.Add(this.btnsetting);
            this.Controls.Add(this.btnWP);
            this.Controls.Add(this.btnList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmSelect_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnsetting;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Button btnWP;
        private System.Windows.Forms.Button btnGeneral;
        private System.Windows.Forms.Button btnReissue;
        private System.Windows.Forms.Button btnBatch;
        private System.Windows.Forms.Button btnReplace;
    }
}