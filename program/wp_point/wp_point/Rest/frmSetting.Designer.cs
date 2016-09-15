namespace _wp_point.Rest
{
    partial class frmSetting
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
            this.chkbirth = new System.Windows.Forms.CheckBox();
            this.cbbport = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbbaudrate = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbSelectCamera = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkPhonenum = new System.Windows.Forms.CheckBox();
            this.chkAddress = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rdhurigana = new System.Windows.Forms.RadioButton();
            this.rdname = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkbirth
            // 
            this.chkbirth.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkbirth.AutoSize = true;
            this.chkbirth.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkbirth.Location = new System.Drawing.Point(466, 49);
            this.chkbirth.Margin = new System.Windows.Forms.Padding(0);
            this.chkbirth.Name = "chkbirth";
            this.chkbirth.Size = new System.Drawing.Size(106, 40);
            this.chkbirth.TabIndex = 0;
            this.chkbirth.Text = "誕生日";
            this.chkbirth.UseVisualStyleBackColor = true;
            // 
            // cbbport
            // 
            this.cbbport.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbbport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbport.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cbbport.FormattingEnabled = true;
            this.cbbport.Location = new System.Drawing.Point(330, 80);
            this.cbbport.Name = "cbbport";
            this.cbbport.Size = new System.Drawing.Size(492, 44);
            this.cbbport.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(149, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 36);
            this.label2.TabIndex = 3;
            this.label2.Text = "ポート番号";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(149, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 36);
            this.label3.TabIndex = 4;
            this.label3.Text = "通信速度";
            // 
            // cbbbaudrate
            // 
            this.cbbbaudrate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbbbaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbbaudrate.Font = new System.Drawing.Font("メイリオ", 18F);
            this.cbbbaudrate.FormattingEnabled = true;
            this.cbbbaudrate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600"});
            this.cbbbaudrate.Location = new System.Drawing.Point(330, 133);
            this.cbbbaudrate.Name = "cbbbaudrate";
            this.cbbbaudrate.Size = new System.Drawing.Size(492, 44);
            this.cbbbaudrate.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Location = new System.Drawing.Point(-10, 454);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(0, 0);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "チェックボス";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(178, 504);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(241, 60);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.BackColor = System.Drawing.Color.Orange;
            this.btnSave.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(577, 504);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(235, 60);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(149, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 36);
            this.label5.TabIndex = 11;
            this.label5.Text = "カメラ";
            // 
            // cbbSelectCamera
            // 
            this.cbbSelectCamera.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbbSelectCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSelectCamera.Font = new System.Drawing.Font("メイリオ", 18F);
            this.cbbSelectCamera.FormattingEnabled = true;
            this.cbbSelectCamera.Location = new System.Drawing.Point(330, 189);
            this.cbbSelectCamera.Name = "cbbSelectCamera";
            this.cbbSelectCamera.Size = new System.Drawing.Size(492, 44);
            this.cbbSelectCamera.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.chkPhonenum);
            this.groupBox2.Controls.Add(this.chkAddress);
            this.groupBox2.Controls.Add(this.chkbirth);
            this.groupBox2.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(153, 341);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(669, 106);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "必須チェック設定";
            // 
            // chkPhonenum
            // 
            this.chkPhonenum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkPhonenum.AutoSize = true;
            this.chkPhonenum.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkPhonenum.Location = new System.Drawing.Point(234, 49);
            this.chkPhonenum.Margin = new System.Windows.Forms.Padding(0);
            this.chkPhonenum.Name = "chkPhonenum";
            this.chkPhonenum.Size = new System.Drawing.Size(130, 40);
            this.chkPhonenum.TabIndex = 5;
            this.chkPhonenum.Text = "電話番号";
            this.chkPhonenum.UseVisualStyleBackColor = true;
            // 
            // chkAddress
            // 
            this.chkAddress.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkAddress.AutoSize = true;
            this.chkAddress.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkAddress.Location = new System.Drawing.Point(49, 49);
            this.chkAddress.Margin = new System.Windows.Forms.Padding(0);
            this.chkAddress.Name = "chkAddress";
            this.chkAddress.Size = new System.Drawing.Size(82, 40);
            this.chkAddress.TabIndex = 3;
            this.chkAddress.Text = "住所";
            this.chkAddress.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(149, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(255, 36);
            this.label4.TabIndex = 15;
            this.label4.Text = "カードに印字する名前";
            // 
            // rdhurigana
            // 
            this.rdhurigana.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rdhurigana.AutoSize = true;
            this.rdhurigana.Font = new System.Drawing.Font("メイリオ", 18F);
            this.rdhurigana.Location = new System.Drawing.Point(525, 287);
            this.rdhurigana.Name = "rdhurigana";
            this.rdhurigana.Size = new System.Drawing.Size(187, 40);
            this.rdhurigana.TabIndex = 14;
            this.rdhurigana.TabStop = true;
            this.rdhurigana.Text = "ﾌﾘｶﾞﾅ(ｾｲ＆ﾒｲ)";
            this.rdhurigana.UseVisualStyleBackColor = true;
            this.rdhurigana.CheckedChanged += new System.EventHandler(this.rdhurigana_CheckedChanged);
            // 
            // rdname
            // 
            this.rdname.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rdname.AutoSize = true;
            this.rdname.Font = new System.Drawing.Font("メイリオ", 18F);
            this.rdname.Location = new System.Drawing.Point(331, 287);
            this.rdname.Name = "rdname";
            this.rdname.Size = new System.Drawing.Size(175, 40);
            this.rdname.TabIndex = 13;
            this.rdname.TabStop = true;
            this.rdname.Text = "漢字(姓＆名)";
            this.rdname.UseVisualStyleBackColor = true;
            this.rdname.CheckedChanged += new System.EventHandler(this.rdname_CheckedChanged);
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1006, 650);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rdhurigana);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rdname);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbbSelectCamera);
            this.Controls.Add(this.cbbbaudrate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkbirth;
        private System.Windows.Forms.ComboBox cbbport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbbaudrate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbSelectCamera;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdhurigana;
        private System.Windows.Forms.RadioButton rdname;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkAddress;
        private System.Windows.Forms.CheckBox chkPhonenum;
    }
}