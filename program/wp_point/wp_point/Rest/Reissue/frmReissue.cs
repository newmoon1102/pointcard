using _wp_point.Rest.Reissue;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _wp_point.Rest
{
    public partial class frmReissue : Form
    {
        private readonly frmMain _frm;

        #region コンストラクタ・デストラクタ
        public frmReissue()
        {
            InitializeComponent();
        }
        public frmReissue(frmMain fr)
        {
            InitializeComponent();
            this._frm = fr;
        }
        #endregion

        /// <summary>
        /// トップボタン押した時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTop_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmSelect"))
            {
                frmSelect childForm = new frmSelect(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        /// <summary>
        /// カード機で読み取れない時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNotRead_Click(object sender, EventArgs e)
        {
            //再発行-磁気カード読めない場合の画面へ遷移
            if (this._frm.ResetOpenWindow("frmNotRead"))
            {
                frmNotRead childForm = new frmNotRead(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        /// <summary>
        /// QRコードの印字が消えた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQRCode_Click(object sender, EventArgs e)
        {
            //再発行手順画面へ遷移
            if (this._frm.ResetOpenWindow("frmReissueProcedure"))
            {
                frmReissueProcedure childForm = new frmReissueProcedure(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
