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
    public partial class frmFinish : Form
    {
        private readonly frmMain _frm;
        public frmFinish(frmMain frm)
        {
            InitializeComponent();
            this._frm = frm;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmSelect"))
            {
                frmSelect childForm = new frmSelect(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }
    }
}
