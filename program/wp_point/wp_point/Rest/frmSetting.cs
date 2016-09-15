using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using _wp_point.Rest.Class;
using System.Configuration;

namespace _wp_point.Rest
{
    public partial class frmSetting : Form
    {
        private readonly frmMain _frm;
        private FilterInfoCollection captureDevice;
        Dictionary<string, int> port = new Dictionary<string, int>
            {
                { "COM1",1},
                { "COM2",2},
                { "COM3",3},
                { "COM4",4},
                { "COM5",5},
                { "COM6",6},
                { "COM7",7},
                { "COM8",8},
                { "COM9",9},
                { "COM10",10}
            };

        public frmSetting(frmMain fr)
        {
            InitializeComponent();
            this._frm = fr;
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            cbbport.DataSource = new BindingSource(port, null);
            cbbport.DisplayMember = "Key";
            cbbport.ValueMember = "Value";

            Common cm = new Common();
            captureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (cm.CheckCamera(captureDevice))
            {
                foreach (FilterInfo Device in captureDevice)
                {

                    cbbSelectCamera.Items.Add(Device.Name);
                }
                cbbSelectCamera.SelectedIndex = Common.GetSetting<int>("device");
            }
            else
            {
                if (captureDevice.Count > 0)
                {
                    foreach (FilterInfo Device in captureDevice)
                    {

                        cbbSelectCamera.Items.Add(Device.Name);
                    }
                }
            }

            cbbport.Text = ConfigurationManager.AppSettings["portname"] ?? "";
            cbbbaudrate.Text = Common.GetSetting<int>("baud").ToString();
            chkbirth.Checked = Common.GetSetting<bool>("chkbirthday");
            chkAddress.Checked = Common.GetSetting<bool>("chkaddress");
            chkPhonenum.Checked = Common.GetSetting<bool>("chkphoneNum");

            if (Common.GetSetting<int>("sendCardName") == 1)
            {
                rdname.Checked = true;
                rdhurigana.Checked = false;
            }
            else
            {
                rdname.Checked = false;
                rdhurigana.Checked = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["chkphoneNum"].Value = chkPhonenum.Checked.ToString();
                config.AppSettings.Settings["chkaddress"].Value = chkAddress.Checked.ToString();
                config.AppSettings.Settings["chkbirthday"].Value = chkbirth.Checked.ToString();
                config.AppSettings.Settings["device"].Value = cbbSelectCamera.SelectedIndex.ToString();
                config.AppSettings.Settings["baud"].Value = cbbbaudrate.Text;
                config.AppSettings.Settings["sendCardName"].Value = rdname.Checked ? "1" : "2";

                KeyValuePair<string, int> item = (KeyValuePair<string, int>)cbbport.SelectedItem;
                config.AppSettings.Settings["portname"].Value = item.Key;
                config.AppSettings.Settings["portno"].Value = item.Value.ToString();

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                btnCancel.Text = "戻る";
                MsgBox.Show("設定を保存しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this._frm.ResetOpenWindow("frmSelect"))
            {
                frmSelect childForm = new frmSelect(_frm);
                childForm.MdiParent = _frm;
                childForm.WindowState = FormWindowState.Maximized;
                childForm.Show();
            }
        }

        private void rdname_CheckedChanged(object sender, EventArgs e)
        {
            if (rdname.Checked)
            {
                rdhurigana.Checked = false;
            }
        }

        private void rdhurigana_CheckedChanged(object sender, EventArgs e)
        {
            if (rdhurigana.Checked)
            {
                rdname.Checked = false;
            }
        }
    }
}
