using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _wp_point.Rest.Reissue
{
    public partial class frmNotRead : Form
    {
        private readonly frmMain _frm;

        #region コンストラクタ・デストラクタ
        public frmNotRead()
        {
            InitializeComponent();
        }
        public frmNotRead(frmMain fr)
        {
            InitializeComponent();
            this._frm = fr;
        }
        #endregion

        /// <summary>
        /// 戻るボタン押した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmReissue"))
            {
                frmReissue childForm = new frmReissue(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        /// <summary>
        /// QRコード読取り画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQRCode_Click(object sender, EventArgs e)
        {
            //QRコード読取り画面へ遷移
            if (this._frm.ResetOpenWindow("frmNR_QRCodeInput"))
            {
                frmNR_QRCodeInput childForm = new frmNR_QRCodeInput(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        /// <summary>
        /// カード番号入力画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCardNo_Click(object sender, EventArgs e)
        {
            //カードNo読取り画面へ遷移
            if (this._frm.ResetOpenWindow("frmNR_CardNo"))
            {
                frmNR_CardNo childForm = new frmNR_CardNo(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
