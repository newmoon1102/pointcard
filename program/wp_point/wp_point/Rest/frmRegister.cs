using log4net;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using _wp_point.Rest.Class;
using System.Json;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Configuration;
using _wp_point.Rest.Request;
using System.Data.SqlClient;

namespace _wp_point.Rest
{
    public partial class frmRegister : Form
    {
        private readonly frmMain _frm;
        public readonly string _frmName;
        private static string shop_auth_code;
        private MemberImportRequest RegisterData;
        private static readonly ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string gender;
        public static string mailaddress;
        public static string member_id;
        public int receipt_id;
        public static string birth;
        private Color errColor = Color.Yellow;
        private Color succColor = Color.White;

        #region スクロール対応
        private bool MoveVectorFlag;
        private float MoveVector;

        //private int OldMouseX;
        //private int OldMouseY;
        private int StartMouseX;
        private int StartMouseY;
        private int EndMouseX;
        private int EndMouseY;
        private int StartScrollNum;

        private DateTime StartTime;
        private DateTime EndTime;
        #endregion

        public frmRegister(frmMain fr, string frmName, MemberImportRequest registerData)
        {
            InitializeComponent();
            this._frm = fr;
            this._frmName = frmName;
            receipt_id = 0;
            RegisterData = registerData;
            userInitialize();
            MoveVectorFlag = false;
            //RegisterData = new MemberImportRequest();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!CheckDataImport())
            {
                MsgBox.Show("入力内容に誤りがあります。ご確認ください。","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                RegisterData.shop_auth_code = shop_auth_code;
                RegisterData.last_name = txtLastName.Text;
                RegisterData.first_name = txtFirstName.Text;
                RegisterData.last_name_y = txtLastName_Kana.Text;
                RegisterData.first_name_y = txtFirstName_Kana.Text;
                RegisterData.zip_1 = txtPostalCode1.Text;
                RegisterData.zip_2 = txtPostalCode2.Text;
                RegisterData.pref_name = cbbPref.Text;
                RegisterData.city_name = txtCity.Text;
                RegisterData.area_name = txtArea.Text;
                RegisterData.block = txtBlock.Text;
                RegisterData.building = txtBdname.Text;
                RegisterData.tel_number_1 = txthomePhone1.Text;
                RegisterData.tel_number_2 = txthomePhone2.Text;
                RegisterData.tel_number_3 = txthomePhone3.Text;
                RegisterData.mobile_number_1 = txtmobilePhone1.Text;
                RegisterData.mobile_number_2 = txtmobilePhone2.Text;
                RegisterData.mobile_number_3 = txtmobilePhone3.Text;
                RegisterData.other_tel_number_1 = txtprePhone1.Text;
                RegisterData.other_tel_number_2 = txtprePhone2.Text;
                RegisterData.other_tel_number_3 = txtprePhone3.Text;
                RegisterData.sex = gender;
                RegisterData.birth = birth;
                RegisterData.mail_address = txtEmail.Text;
                if (!String.IsNullOrEmpty(RegisterData.member_id)) RegisterData.password = ""; else RegisterData.password = txtPass.Text;
                RegisterData.call_name = txtname.Text;
                if (chkmagazin.Checked) { RegisterData.mailmaga_disable_flag = "2"; } else { RegisterData.mailmaga_disable_flag = "1"; }
                if (chkdirect.Checked) { RegisterData.dm_disable_flag = "2"; } else { RegisterData.dm_disable_flag = "1"; }
                RegisterData.member_id = member_id;
                RegisterData.receipt_id = receipt_id.ToString();
                RegisterData.receipt_date = !String.IsNullOrEmpty(RegisterData.receipt_date) ? RegisterData.receipt_date : DateTime.Now.ToString();
                RegisterData.card_flg = "FALSE";

                //確認画面に遷移
                if (this._frm.ResetOpenWindow("frmConfirm"))
                {
                    frmConfirm childForm = new frmConfirm(_frm, this.Name.ToString(), RegisterData);

                    childForm.MdiParent = _frm;
                    childForm.WindowState = FormWindowState.Maximized;
                    childForm.Show();
                    //MsgBox.Show("既にカードが発行済の場合、カード番号を入力して下さい。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //}
                //catch { }
            }
        }

        private void btnsearchAdd_Click(object sender, EventArgs e)
        {
            if (txtPostalCode1.Text == "" & txtPostalCode2.Text == "")
            {
                MsgBox.Show("郵便番号を入力してください。","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else
            {
                if (txtPostalCode1.TextLength + txtPostalCode2.TextLength < 7)
                {
                    lbpostCode_error.Text = "※郵便番号の入力に誤りがあります。";
                    txtPostalCode1.BackColor = errColor;
                    txtPostalCode2.BackColor = errColor;
                    return;
                }
                try
                {
                    string code = txtPostalCode1.Text + txtPostalCode2.Text;
                    if (RunClient_AddressSearch(_logger, code))
                    {
                        lbpostCode_error.Text = "";
                        lbpref_error.Text = "";
                        lbarea_error.Text = "";
                        lbcity_error.Text = "";
                        txtPostalCode1.BackColor = succColor;
                        txtPostalCode2.BackColor = succColor;
                        cbbPref.BackColor = succColor;
                        txtCity.BackColor = succColor;
                        txtArea.BackColor = succColor;
                    }
                }
                catch
                {
                    MsgBox.Show("該当する住所は見つかりませんでした。","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }
        private void frmRegister_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtLastName;

            //read shop auth code
            shop_auth_code = shopAuthCode.shop_auth_code;
            // 住所　必須　チェック
            if (Common.GetSetting<bool>("chkaddress"))
            {
                lb_req_address1.Visible = true;
                lb_req_address2.Visible = true;
                lb_req_address3.Visible = true;
                lb_req_address4.Visible = true;
                lb_req_address5.Visible = true;
            }
            else
            {
                lb_req_address1.Visible = false;
                lb_req_address2.Visible = false;
                lb_req_address3.Visible = false;
                lb_req_address4.Visible = false;
                lb_req_address5.Visible = false;
            }
            // 電話番号　必須　チェック
            if (Common.GetSetting<bool>("chkphoneNum"))
            {
                lb_req_tell.Visible = true;
                lb_req_mobile.Visible = true;
                lb_req_other_tell.Visible = true;
            }
            else
            {
                lb_req_tell.Visible = false;
                lb_req_mobile.Visible = false;
                lb_req_other_tell.Visible = false;
            }
            // 誕生日　必須　チェック
            if (Common.GetSetting<bool>("chkbirthday"))
            {
                lb_req_birth.Visible = true;
            }
            else
            {
                lb_req_birth.Visible = false;
            }
            cbbbirth_y.Items.Add("----");
            int year = DateTime.Now.Year;
            for (int i = year; i >= year - 150; i--)
            {
                cbbbirth_y.Items.Add(i);
            }

            timerScrollUpdate.Start();
        }

        private void userInitialize()
        {
            //会員ID
            if (!String.IsNullOrEmpty(RegisterData.member_id))
            {
                member_id = RegisterData.member_id;
                txtPass.Visible = false;
                lbpass_kaiin.Visible = true;
            }
            else
            {
                member_id = "";
                txtPass.Visible = true;
                lbpass_kaiin.Visible = false;
                RegisterData.member_id = null;
            }
            //受付番号
            if (!String.IsNullOrEmpty(RegisterData.receipt_id))
            {
                receipt_id = Convert.ToInt32(RegisterData.receipt_id);
            }

            txtLastName.Text = RegisterData.last_name;
            txtFirstName.Text = RegisterData.first_name;
            txtLastName_Kana.Text = RegisterData.last_name_y;
            txtFirstName_Kana.Text = RegisterData.first_name_y;
            txtPostalCode1.Text = RegisterData.zip_1;
            txtPostalCode2.Text = RegisterData.zip_2;
            if (!String.IsNullOrEmpty(RegisterData.pref_name))
            {
                cbbPref.Text = RegisterData.pref_name;
            }
            txtCity.Text = RegisterData.city_name;
            txtArea.Text = RegisterData.area_name;
            txtBlock.Text = RegisterData.block;
            txtBdname.Text = RegisterData.building;
            txthomePhone1.Text = RegisterData.tel_number_1;
            txthomePhone2.Text = RegisterData.tel_number_2;
            txthomePhone3.Text = RegisterData.tel_number_3;
            txtmobilePhone1.Text = RegisterData.mobile_number_1;
            txtmobilePhone2.Text = RegisterData.mobile_number_2;
            txtmobilePhone3.Text = RegisterData.mobile_number_3;
            txtprePhone1.Text = RegisterData.other_tel_number_1;
            txtprePhone2.Text = RegisterData.other_tel_number_2;
            txtprePhone3.Text = RegisterData.other_tel_number_3;
            //性別
            if (!String.IsNullOrEmpty(RegisterData.sex))
            {
                if (RegisterData.sex == "2")
                {
                    gender = "2";
                    rdmale.Checked = false;
                    rdfemale.Checked = true;
                }
                else
                {
                    gender = "1";
                    rdmale.Checked = true;
                    rdfemale.Checked = false;
                }
            }
            else
            {
                rdmale.Checked = true;
                rdfemale.Checked = false;
                gender = "1";
            }

            //生年月日
            if (RegisterData.birth != "" && Common.IsDateTime(RegisterData.birth) == true)
            {
                cbbbirth_y.Text = DateTime.Parse(RegisterData.birth, new CultureInfo("ja-JP")).Year.ToString();
                cbbbirth_m.Text = DateTime.Parse(RegisterData.birth, new CultureInfo("ja-JP")).Month.ToString().PadLeft(2, '0');
                cbbbirth_d.Text = DateTime.Parse(RegisterData.birth, new CultureInfo("ja-JP")).Day.ToString().PadLeft(2, '0');
            }

            if (!String.IsNullOrEmpty(RegisterData.mail_address))
            {
                txtEmail.Text = RegisterData.mail_address;
                if (!_frmName.Equals("frmConfirm")) mailaddress = RegisterData.mail_address;
            }

            txtPass.Text = RegisterData.password;
            txtname.Text = RegisterData.call_name;

            if (!String.IsNullOrEmpty(RegisterData.mailmaga_disable_flag))
            {
                if (RegisterData.mailmaga_disable_flag == "1") { chkmagazin.Checked = false; } else { chkmagazin.Checked = true; }

            }
            else
            {
                chkmagazin.Checked = true;
            }

            if (!String.IsNullOrEmpty(RegisterData.dm_disable_flag))
            {
                if (RegisterData.dm_disable_flag == "1") { chkdirect.Checked = false; } else { chkdirect.Checked = true; }
            }
            else
            {
                chkdirect.Checked = true;
            }

        }

        private bool CheckDataImport()
        {
            // 名前（姓）チェック
            bool flg_check_lastname = false;
            flg_check_lastname = checkName(txtLastName, lblname_error, 50, "名前（姓）");

            //名前（名）チェック
            bool flg_check_firtname = false;
            flg_check_firtname = checkName(txtFirstName, lbfname_error, 50, "名前（名）");

            // 名前ふりがな（せい） チェック
            bool flg_check_lastname_y = false;
            flg_check_lastname_y = checkName(txtLastName_Kana, lblname_kana_error, 50, "名前ふりがな（せい）");

            //名前ふりがな（めい) チェック
            bool flg_check_firtname_y = false;
            flg_check_firtname_y = checkName(txtFirstName_Kana, lbfname_kana_error, 50, "名前ふりがな（めい）");

            //お呼出名 チェック
            bool flg_check_name = false;
            flg_check_name = checkName(txtname, lbname_error, 20, "お呼出名");

            //住所　チェック
            bool flg_check_postcode = false;
            bool flg_check_pref = false;
            bool flg_check_area = false;
            bool flg_check_city = false;
            bool flg_check_block = false;
            bool flg_check_building = false;

            if (Common.GetSetting<bool>("chkaddress"))
            {
                //郵便番号　チェック
                if (txtPostalCode2.Text == "" || txtPostalCode1.Text == "")
                {
                    //lbpostCode_error.Text = "※郵便番号を入力してください。";
                    txtPostalCode1.BackColor = errColor;
                    txtPostalCode2.BackColor = errColor;
                    flg_check_postcode = true;
                }
                else
                {
                    flg_check_postcode = checkPostCode();
                }
                //都道府県名 チェック
                if (String.IsNullOrEmpty(cbbPref.Text) || cbbPref.Text == "--")
                {
                    lbpref_error.Text = "※都道府県を選択してください。";
                    cbbPref.BackColor = errColor;
                    flg_check_pref = true;
                }
                else
                {
                    lbpref_error.Text = "";
                    cbbPref.BackColor = succColor;
                    flg_check_pref = false;
                }

                //市区町村名 チェック
                if (!String.IsNullOrEmpty(txtCity.Text))
                {
                    flg_check_area = checkAdress(txtCity, lbarea_error, "市区町村名");
                }
                else
                {
                    //lbarea_error.Text = "※市区町村名を入力してください。";
                    txtCity.BackColor = errColor;
                    flg_check_area = true;
                }

                //町域名 チェック
                if (!String.IsNullOrEmpty(txtArea.Text))
                {
                    flg_check_city = checkAdress(txtArea, lbcity_error, "町域名");
                }
                else
                {
                    //lbcity_error.Text = "※町域名を入力してください。";
                    txtArea.BackColor = errColor;
                    flg_check_city = true;
                }

                //番地　チェック
                if (!String.IsNullOrEmpty(txtBlock.Text))
                {
                    flg_check_block = checkAdress(txtBlock, lbblock_error, "番地");
                }
                else
                {
                    //lbblock_error.Text = "※番地を入力してください。";
                    txtBlock.BackColor = errColor;
                    flg_check_block = true;
                }

                //建物名～号室 チェック
                flg_check_building = checkAdress(txtBdname, lbbuiiding_error, "建物名～号室");
            }
            else
            {
                //郵便番号　チェック
                if (txtPostalCode2.Text == "" || txtPostalCode1.Text == "")
                {
                    //lbpostCode_error.Text = "";
                    txtPostalCode1.BackColor = succColor;
                    txtPostalCode2.BackColor = succColor;
                    flg_check_postcode = false;
                }
                else
                {
                    flg_check_postcode = checkPostCode();
                }
                //都道府県名 チェック
                if (String.IsNullOrEmpty(cbbPref.Text) || cbbPref.Text == "--")
                {
                    lbpref_error.Text = "";
                    cbbPref.BackColor = succColor;
                    flg_check_pref = false;
                }

                //市区町村名 チェック
                if (!String.IsNullOrEmpty(txtCity.Text))
                {
                    flg_check_area = checkAdress(txtCity, lbarea_error, "市区町村名");
                }
                else
                {
                    //lbarea_error.Text = "";
                    txtCity.BackColor = succColor;
                    flg_check_city = false;
                }

                //町域名 チェック
                if (!String.IsNullOrEmpty(txtArea.Text))
                {
                    flg_check_city = checkAdress(txtArea, lbcity_error, "町域名");
                }
                else
                {
                    //lbcity_error.Text = "";
                    txtArea.BackColor = succColor;
                    flg_check_city = false;
                }

                //番地　チェック
                if (!String.IsNullOrEmpty(txtBlock.Text))
                {
                    flg_check_block = checkAdress(txtBlock, lbblock_error, "番地");
                }
                else
                {
                    //lbblock_error.Text = "";
                    txtBlock.BackColor = succColor;
                    flg_check_block = false;
                }

                //建物名～号室 チェック
                flg_check_building = checkAdress(txtBdname, lbbuiiding_error, "建物名～号室");
            }

            //電話番号　チェック
            bool flg_check_allNum = false;
            bool flg_check_tell = false;
            bool flg_check_mobile = false;
            bool flg_check_other_tell = false;

            if (Common.GetSetting<bool>("chkphoneNum"))
            {
                flg_check_allNum = checkAllPhoneNum(1);
                if (!flg_check_allNum)
                {
                    flg_check_tell = checkPhoneNumber(txthomePhone1, txthomePhone2, txthomePhone3, lbtel_error, "自宅電話番号");
                    flg_check_mobile = checkPhoneNumber(txtmobilePhone1, txtmobilePhone2, txtmobilePhone3, lbmobile_error, "携帯電話番号");
                    flg_check_other_tell = checkPhoneNumber(txtprePhone1, txtprePhone2, txtprePhone3, lbothertel_error, "予備電話番号");
                }
            }
            else
            {
                flg_check_allNum = checkAllPhoneNum(2);
                if (!flg_check_allNum)
                {
                    flg_check_tell = checkPhoneNumber(txthomePhone1, txthomePhone2, txthomePhone3, lbtel_error, "自宅電話番号");
                    flg_check_mobile = checkPhoneNumber(txtmobilePhone1, txtmobilePhone2, txtmobilePhone3, lbmobile_error, "携帯電話番号");
                    flg_check_other_tell = checkPhoneNumber(txtprePhone1, txtprePhone2, txtprePhone3, lbothertel_error, "予備電話番号");
                }
            }

            //生年月日　チェック
            bool flg_check_birth = false;
            if (Common.GetSetting<bool>("chkbirthday"))
            {
                if ((cbbbirth_y.Text == "----") || (cbbbirth_m.Text == "--") || (cbbbirth_d.Text == "--") || (cbbbirth_y.Text == "") || (cbbbirth_m.Text == "") || (cbbbirth_d.Text == ""))
                {
                    //lbbirth_error.Text = "※生年月日を入力してください。";
                    cbbbirth_y.BackColor = errColor;
                    cbbbirth_m.BackColor = errColor;
                    cbbbirth_d.BackColor = errColor;
                    flg_check_birth = true;
                }
                else
                {
                    flg_check_birth = checkBirth();
                }
            }
            else
            {
                flg_check_birth = checkBirth();
            }

            //メール　チェック
            bool flg_check_mail = false;
            if (txtEmail.Text != "")
            {
                if (String.IsNullOrEmpty(member_id))
                {
                    flg_check_mail = checkEmail();
                }
                else
                {
                    if (!txtEmail.Text.Equals(mailaddress))
                    {
                        flg_check_mail = checkEmail();
                    }
                }
            }
            else
            {
                //lbmail_error.Text = "";
                txtEmail.BackColor = succColor;
            }

            //パスワード　チェック
            bool flg_check_pass = false;
            if (String.IsNullOrEmpty(member_id))
            {
                if (txtPass.Text == "")
                {
                    lbpass_error.Text = "※パスワードを入力してください。";
                    txtPass.BackColor = errColor;
                    flg_check_pass = true;
                }
                else
                {
                    if (!Common.checkTextLenght(txtPass.Text, 20, 8))
                    {
                        lbpass_error.Text = "※パスワードは8文字以上20文字以内で入力してください。";
                        txtPass.BackColor = errColor;
                        flg_check_pass = true;
                    }
                    else
                    {
                        if (!Common.PassIsValid(txtPass.Text))
                        {
                            lbpass_error.Text = "※パスワードの入力に誤りがあります。";
                            txtPass.BackColor = errColor;
                            flg_check_pass = true;
                        }
                        else
                        {
                            lbpass_error.Text = "";
                            txtPass.BackColor = succColor;
                        }
                    }
                }
            }

            if (flg_check_lastname || flg_check_firtname || flg_check_lastname_y || flg_check_firtname_y ||
                flg_check_name || flg_check_postcode || flg_check_pref || flg_check_area || flg_check_city || flg_check_block || flg_check_building ||
                flg_check_allNum || flg_check_tell || flg_check_mobile || flg_check_other_tell ||
                flg_check_birth || flg_check_mail || flg_check_pass)
            {
                return false;
            }
            return true;
        }

        // Check name
        private bool checkName(TextBox txt, Label lbErr, int lenght, String str)
        {
            bool flg_check = false;
            if (txt.Text == "")
            {
                //lbErr.Text = "※" + str + "を入力してください。";
                txt.BackColor = errColor;
                flg_check = true;
            }
            else
            {
                if (!Common.IsSafeChar(txt.Text))
                {
                    //lbErr.Text = "※" + str + "の入力に誤りがあります。";
                    txt.BackColor = errColor;
                    flg_check = true;
                }
                else
                {
                    if (Common.checkTextLenght(txt.Text, lenght, 0))
                    {
                        if (!str.Equals("名前（姓）") && !str.Equals("名前（名）"))
                        {
                            if (!Common.IsHiragana(txt.Text))
                            {
                                //lbErr.Text = "※" + str + "はひらがなで入力してください。";
                                txt.BackColor = errColor;
                                flg_check = true;
                            }
                            else
                            {
                                lbErr.Text = "";
                                txt.BackColor = succColor;
                                flg_check = false;
                            }
                        }
                        else
                        {
                            lbErr.Text = "";
                            txt.BackColor = succColor;
                            flg_check = false;
                        }
                    }
                    else
                    {
                        //lbErr.Text = "※" + str + "は" + lenght + "文字以下で入力してください。";
                        txt.BackColor = errColor;
                        flg_check = true;
                    }
                }
            }

            return flg_check;
        }

        // Check postCode when not null
        private bool checkPostCode()
        {
            bool flg_check = false;
            if (txtPostalCode1.Text != "" && txtPostalCode2.Text != "")
            {
                if (!Common.checkTextLenght(txtPostalCode1.Text, 0, 3))
                {
                    //lbpostCode_error.Text = "※郵便番号上位3桁は半角数字3桁で入力してください。";
                    txtPostalCode1.BackColor = errColor;
                    flg_check = true;
                }
                if (!Common.checkTextLenght(txtPostalCode2.Text, 0, 4))
                {
                    //lbpostCode_error.Text = "※郵便番号上位4桁は半角数字4桁で入力してください。";
                    txtPostalCode2.BackColor = errColor;
                    flg_check = true;
                }

                if (!flg_check)
                {
                    //lbpostCode_error.Text = "";
                    txtPostalCode1.BackColor = succColor;
                    txtPostalCode2.BackColor = succColor;
                    flg_check = false;
                }
            }
            else if (txtPostalCode1.Text != "" && txtPostalCode2.Text == "")
            {
                //lbpostCode_error.Text = "※郵便番号上位4桁は半角数字4桁で入力してください。";
                txtPostalCode1.BackColor = errColor;
                txtPostalCode2.BackColor = errColor;
                flg_check = true;
            }
            else if (txtPostalCode2.Text != "" && txtPostalCode1.Text == "")
            {
                //lbpostCode_error.Text = "※郵便番号上位3桁は半角数字3桁で入力してください。";
                txtPostalCode1.BackColor = errColor;
                txtPostalCode2.BackColor = errColor;
                flg_check = true;
            }

            return flg_check;
        }

        //check addresswhen not null
        private bool checkAdress(TextBox txt, Label lbErr, String str)
        {
            bool flg_check = false;
            if (!String.IsNullOrEmpty(txt.Text))
            {
                if (Common.IsSafeChar(txt.Text))
                {
                    if (!Common.checkTextLenght(txt.Text, 100, 0))
                    {
                        //lbErr.Text = "※" + str + "は100文字以下で入力してください。";
                        txt.BackColor = errColor;
                        flg_check = true;
                    }
                    else
                    {
                        //lbErr.Text = "";
                        txt.BackColor = succColor;
                        flg_check = false;
                    }
                }
                else
                {
                    //lbErr.Text = "※" + str + "に機種依存文字が含まれています。";
                    txt.BackColor = errColor;
                    flg_check = true;
                }
            }
            return flg_check;
        }

        //check phone1,2,3 all is null?
        private bool checkAllPhoneNum(int mode)
        {
            bool flg_check = false;
            if (mode == 1)
            {
                if (txthomePhone1.Text == "" && txthomePhone2.Text == "" && txthomePhone3.Text == "" && txtmobilePhone1.Text == "" && txtmobilePhone2.Text == "" &&
                            txtmobilePhone3.Text == "" && txtprePhone1.Text == "" && txtprePhone2.Text == "" && txtprePhone3.Text == "")
                {
                    lbtel_error.Text = "※電話番号を入力してください。";
                    txthomePhone1.BackColor = errColor;
                    txthomePhone2.BackColor = errColor;
                    txthomePhone3.BackColor = errColor;

                    //lbmobile_error.Text = "※いずれかの電話番号を入力してください。";
                    txtmobilePhone1.BackColor = errColor;
                    txtmobilePhone2.BackColor = errColor;
                    txtmobilePhone3.BackColor = errColor;

                    //lbothertel_error.Text = "※いずれかの電話番号を入力してください。";
                    txtprePhone1.BackColor = errColor;
                    txtprePhone2.BackColor = errColor;
                    txtprePhone3.BackColor = errColor;
                    flg_check = true;
                }
                else
                {
                    lbtel_error.Text = "";
                }
            }
            else if (mode == 2)
            {
                if (txthomePhone1.Text == "" && txthomePhone2.Text == "" && txthomePhone3.Text == "" && txtmobilePhone1.Text == "" && txtmobilePhone2.Text == "" &&
                         txtmobilePhone3.Text == "" && txtprePhone1.Text == "" && txtprePhone2.Text == "" && txtprePhone3.Text == "")
                {
                    lbtel_error.Text = "";
                    txthomePhone1.BackColor = succColor;
                    txthomePhone2.BackColor = succColor;
                    txthomePhone3.BackColor = succColor;

                    //lbmobile_error.Text = "";
                    txtmobilePhone1.BackColor = succColor;
                    txtmobilePhone2.BackColor = succColor;
                    txtmobilePhone3.BackColor = succColor;

                    //lbothertel_error.Text = "";
                    txtprePhone1.BackColor = succColor;
                    txtprePhone2.BackColor = succColor;
                    txtprePhone3.BackColor = succColor;
                    flg_check = false;
                }

            }
            return flg_check;
        }
        //Check phone number when not null
        private bool checkPhoneNumber(TextBox txt1, TextBox txt2, TextBox txt3, Label lbErr, String str)
        {
            bool flg_check = false;
            if (txt1.Text != "" && txt2.Text != "" && txt3.Text != "")
            {
                if (Common.Mid(txt1.Text, 1, 1) != "0")
                {
                    //lbErr.Text = "※" + str + "の入力に誤りがあります";
                    txt1.BackColor = errColor;
                    txt2.BackColor = errColor;
                    txt3.BackColor = errColor;
                    flg_check = true;
                }
                else
                {
                    if (!Common.checkTextLenght(txt1.Text, 4, 0) || !Common.checkTextLenght(txt2.Text, 4, 0) || !Common.checkTextLenght(txt3.Text, 5, 0))
                    {
                        //lbErr.Text = "※" + str + "の入力に誤りがあります";
                        txt1.BackColor = errColor;
                        txt2.BackColor = errColor;
                        txt3.BackColor = errColor;
                        flg_check = true;
                    }
                    else
                    {
                        if (!Common.checkPhoneNumFormat(txt1.Text) || !Common.checkPhoneNumFormat(txt2.Text) || !Common.checkPhoneNumFormat(txt3.Text))
                        {
                            //lbErr.Text = "※" + str + "の入力に誤りがあります";
                            txt1.BackColor = errColor;
                            txt2.BackColor = errColor;
                            txt3.BackColor = errColor;
                            flg_check = true;
                        }
                        else
                        {
                            //lbErr.Text = "";
                            txt1.BackColor = succColor;
                            txt2.BackColor = succColor;
                            txt3.BackColor = succColor;
                            flg_check = false;
                        }
                    }
                }
            }
            else if (txt1.Text != "" && (txt2.Text == "" || txt3.Text == ""))
            {
                //lbErr.Text = "※" + str + "の入力に誤りがあります";
                txt1.BackColor = errColor;
                txt2.BackColor = errColor;
                txt3.BackColor = errColor;
                flg_check = true;
            }
            else if (txt2.Text != "" && (txt1.Text == "" || txt3.Text == ""))
            {
                //lbErr.Text = "※" + str + "の入力に誤りがあります";
                txt1.BackColor = errColor;
                txt2.BackColor = errColor;
                txt3.BackColor = errColor;
                flg_check = true;
            }
            else if (txt3.Text != "" && (txt1.Text == "" || txt2.Text == ""))
            {
                //lbErr.Text = "※" + str + "の入力に誤りがあります";
                txt1.BackColor = errColor;
                txt2.BackColor = errColor;
                txt3.BackColor = errColor;
                flg_check = true;
            }
            else
            {
                //lbErr.Text = "";
                txt1.BackColor = succColor;
                txt2.BackColor = succColor;
                txt3.BackColor = succColor;
                flg_check = false;
            }

            return flg_check;
        }

        //check birth datetime
        private bool checkBirth()
        {
            bool flg_check = false;
            string datetime = cbbbirth_y.Text  + cbbbirth_m.Text  + cbbbirth_d.Text;

            if (datetime.Equals("--------") || String.IsNullOrEmpty(datetime))
            {
                //lbbirth_error.Text = "";
                cbbbirth_y.BackColor = succColor;
                cbbbirth_m.BackColor = succColor;
                cbbbirth_d.BackColor = succColor;
                birth = "";
                flg_check = false;
            }
            else
            {
                datetime = cbbbirth_y.Text + "/" + cbbbirth_m.Text.PadLeft(2, '0') + "/" + cbbbirth_d.Text.PadLeft(2, '0');
                if (!Common.IsDateTime(datetime))
              {
                    //lbbirth_error.Text = "※生年月日の入力に誤りがあります。";
                    cbbbirth_y.BackColor = errColor;
                    cbbbirth_m.BackColor = errColor;
                    cbbbirth_d.BackColor = errColor;
                    flg_check = true;
                }
                else
                {
                    //lbbirth_error.Text = "";
                    cbbbirth_y.BackColor = succColor;
                    cbbbirth_m.BackColor = succColor;
                    cbbbirth_d.BackColor = succColor;
                    birth = datetime;
                    flg_check = false;
                }
            }

            return flg_check;
        }
        //check email when not null
        private bool checkEmail()
        {
            bool flg_check = false;
            if (Common.checkTextLenght(txtEmail.Text, 256, 0))
            {
                if (Common.EmailIsValid(txtEmail.Text))
                {
                    if (!RunClient_CheckEmail(_logger))
                    {
                        txtEmail.BackColor = errColor;
                        flg_check = true;
                    }
                    else
                    {
                        lbmail_error.Text = "";
                        txtEmail.BackColor = succColor;
                    }
                }
                else
                {
                    lbmail_error.Text = "※メールアドレスの形式が正しくありません。";
                    txtEmail.BackColor = errColor;
                    flg_check = true;
                }
            }
            else
            {
                lbmail_error.Text = "※メールアドレスは256文字以下で入力してください。";
                txtEmail.BackColor = errColor;
                flg_check = true;
            }
            return flg_check;
        }

        // check male
        private void rdmale_CheckedChanged(object sender, EventArgs e)
        {
            if (rdmale.Checked)
            {
                gender = "1";
                rdfemale.Checked = false;
            }
        }

        // check female
        private void rdfemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rdfemale.Checked)
            {
                gender = "2";
                rdmale.Checked = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(RegisterData.receipt_id) || RegisterData.receipt_id == "0")
            {
                if (!checkInputIsNull())
                {
                    DialogResult result = MsgBox.Show("入力中の項目があります。申し込みをキャンセルしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No) return;
                }
            }else
            {
                DialogResult result1 = MsgBox.Show("入力中の項目があります。申し込みを中断しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result1 == DialogResult.Cancel) return;
            }

            if (this._frm.ResetOpenWindow(_frmName))
            {
                switch (_frmName)
                {
                    case "frmQRCode":
                        frmSelect childForm = new frmSelect(_frm);
                        childForm.MdiParent = _frm;
                        childForm.WindowState = FormWindowState.Maximized;
                        childForm.Show();
                        break;

                    case "frmList":
                        frmList childForm1 = new frmList(_frm);
                        childForm1.MdiParent = _frm;
                        childForm1.WindowState = FormWindowState.Maximized;
                        childForm1.Show();
                        break;

                    case "frmSelect":
                        frmSelect childForm2 = new frmSelect(_frm);
                        childForm2.MdiParent = _frm;
                        childForm2.WindowState = FormWindowState.Maximized;
                        childForm2.Show();
                        break;

                    default:
                        frmSelect childForm3 = new frmSelect(_frm);
                        childForm3.MdiParent = _frm;
                        childForm3.WindowState = FormWindowState.Maximized;
                        childForm3.Show();
                        break;
                }
            }
        }

        public bool RunClient_CheckEmail(ILog logger)
        {
            ApiClient client = new ApiClient(logger);
            string email = txtEmail.Text;
            bool flg_mail = false;
            JsonValue jsonRes = client.checkEmail(email, shop_auth_code);

            if (jsonRes != null)
            {
                if ((string)jsonRes["code"] != "200")
                {
                    if ((string)jsonRes["code"] == "404")
                    {
                        logger.Info("メールの確認に成功しました。");
                        flg_mail = true;
                    }
                    else
                    {
                        MsgBox.Show((string)jsonRes["note"],"エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        _logger.ErrorFormat("API Error code: {0} with note: {1}", jsonRes["code"], jsonRes["note"]);
                        flg_mail = false;
                    }
                }
                else
                {
                    logger.Warn("すでにウェイティングパスに登録されているメール");
                    lbmail_error.Text = "※このメールアドレスは、すでに会員登録されています。";
                    txtEmail.BackColor = errColor;
                    flg_mail = false;
                }
                return flg_mail;
            }

            return false;
        }

        public bool RunClient_AddressSearch(ILog logger, string code)
        {
            ApiClient client = new ApiClient(logger);
            bool flg_address = false;
            JsonValue jsonRes = client.AddressSearch(code);

            if (jsonRes != null)
            {
                if ((string)jsonRes["code"] != "200")
                {
                    if((string)jsonRes["code"] == "404")
                    {
                        MsgBox.Show("一致する郵便番号が見つかりませんでした。","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        flg_address = false;
                    }
                    else
                    {
                        MsgBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _logger.ErrorFormat("API Error code: {0} with note: {1}", jsonRes["code"], jsonRes["note"]);
                        flg_address = false;
                    }
                }
                else
                {
                    cbbPref.Text = (string)jsonRes["pref_name"];
                    txtCity.Text = (string)jsonRes["city_name"];
                    txtArea.Text = (string)jsonRes["area_name"];
                    flg_address = true;
                }
                return flg_address;
            }
            return false;
        }

        private void _MouseClick(object sender, MouseEventArgs e)
        {
            string propertyName = ((TextBox)sender).Name;
            int distance = 0;
            switch (propertyName)
            {
                case "txtArea":
                    distance = 120;
                    break;
                case "txtCity":
                    distance = 170;
                    break;
                case "txtBlock":
                    distance = 240;
                    break;
                case "txtBdname":
                    distance = 290;
                    break;
                case "txthomePhone1":
                    distance = 350;
                    break;
                case "txthomePhone2":
                    distance = 350;
                    break;
                case "txthomePhone3":
                    distance = 350;
                    break;
                case "txtmobilePhone1":
                    distance = 410;
                    break;
                case "txtmobilePhone2":
                    distance = 410;
                    break;
                case "txtmobilePhone3":
                    distance = 410;
                    break;
                case "txtprePhone1":
                    distance = 460;
                    break;
                case "txtprePhone2":
                    distance = 460;
                    break;
                case "txtprePhone3":
                    distance = 460;
                    break;
                case "txtEmail":
                    distance = 610;
                    break;
                case "txtPass":
                    distance = 660;
                    break;
                default:
                    distance = 0;
                    break;
            }
            this.AutoScrollPosition = new Point(e.X, e.Y + distance);
            //Common.startKeyboard();
        }


        private void _ComboboxSelect(object sender, MouseEventArgs e)
        {
            this.AutoScrollPosition = new Point(e.X, e.Y + 700);
        }

        private void startKeyboard(object sender, MouseEventArgs e)
        {
            Common.startKeyboard();
        }

        private void frmRegister_Scroll(object sender, ScrollEventArgs e)
        {
            //if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            //{
            //    if (e.NewValue - e.OldValue >= 0)
            //    {
            //        this.VerticalScroll.Value = e.NewValue + 8;
            //        this.Refresh();
            //    }
            //    else
            //    {
            //        if (e.NewValue - 8 >= 0)
            //        {
            //            this.VerticalScroll.Value = e.NewValue - 8;
            //            this.Refresh();
            //        }
            //    }
            //}
        }

        private void frmRegister_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //MoveVectorFlag = true;
                StartMouseX = Cursor.Position.X;
                StartMouseY = Cursor.Position.Y;
                //StartScrollNum = this.VerticalScroll.Value;

                StartTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void frmRegister_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //if (MoveVectorFlag)
                //{
                //    int NowNumber = this.VerticalScroll.Value;
                //    int AddNum = StartScrollNum + (StartMouseY - Cursor.Position.Y);
                //    if (AddNum < this.VerticalScroll.Maximum && AddNum > this.VerticalScroll.Minimum)
                //    {
                //        this.VerticalScroll.Value = AddNum;
                //        System.Diagnostics.Debug.WriteLine("スクロール値：" + AddNum.ToString());
                //    }
                //    else if (AddNum > this.VerticalScroll.Maximum)
                //    {
                //        this.VerticalScroll.Value = this.VerticalScroll.Maximum;
                //        System.Diagnostics.Debug.WriteLine("スクロール値：" + this.VerticalScroll.Value.ToString());
                //    }
                //    else if (AddNum < this.VerticalScroll.Minimum)
                //    {
                //        this.VerticalScroll.Value = this.VerticalScroll.Minimum;
                //        System.Diagnostics.Debug.WriteLine("スクロール値：" + this.VerticalScroll.Value.ToString());
                //    }

                //    //移動量計算用に現在の値を取得
                //    OldMouseX = Cursor.Position.X;
                //    OldMouseY = Cursor.Position.Y;

                //    //画面を更新
                //    this.Refresh();
                //}
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void frmRegister_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                //移動中フラグをオフにする
                MoveVectorFlag = false;

                ////移動量を計算する
                //int a = OldMouseY - Cursor.Position.Y;
                //MoveVector = a;

                ////リセット
                //OldMouseX = 0;
                //OldMouseY = 0;
                EndMouseX = Cursor.Position.X;
                EndMouseY = Cursor.Position.Y;

                //StartMouseX = Cursor.Position.X;
                //StartMouseY = Cursor.Position.Y;
                StartScrollNum = this.VerticalScroll.Value;
                MoveVector = 100;
                if((StartMouseY - EndMouseY) < 0)
                {
                    MoveVector *= -1;
                }else
                {
                    MoveVector *= 1;
                }

                //終了時間
                EndTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void timerScrollUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!MoveVectorFlag)
                {
                    if (MoveVector != 0)
                    {

                        //移動量を減らす
                        if (MoveVector > 1.0) MoveVector = MoveVector * (float)0.8;
                        if (MoveVector < -1.0) MoveVector = MoveVector * (float)0.8;
                        if (MoveVector > -1.0 && MoveVector < 1.0) MoveVector = 0;
                        //if ((this.VerticalScroll.Value + MoveVector) > this.VerticalScroll.Maximum)
                        //{
                        //    this.VerticalScroll.Value = this.VerticalScroll.Maximum;
                        //    MoveVector = 0;
                        //    System.Diagnostics.Debug.WriteLine("スクロール値：" + this.VerticalScroll.Value.ToString());
                        //}

                        //if ((this.VerticalScroll.Value + MoveVector) < this.VerticalScroll.Minimum)
                        //{
                        //    this.VerticalScroll.Value = this.VerticalScroll.Minimum;
                        //    MoveVector = 0;
                        //    System.Diagnostics.Debug.WriteLine("スクロール値：" + this.VerticalScroll.Value.ToString());
                        //}

                        //慣性で動かす
                        int AddNum = StartScrollNum + (int)MoveVector;
                        if (AddNum < this.VerticalScroll.Maximum
                            && AddNum > this.VerticalScroll.Minimum)
                        {
                            this.VerticalScroll.Value = AddNum;
                            StartScrollNum = AddNum;
                            System.Diagnostics.Debug.WriteLine("スクロール値：" + this.VerticalScroll.Value.ToString());
                        }

                        //画面を更新
                        //this.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void nextControl(object sender, EventArgs e)
        {
            this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void _KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private bool checkInputIsNull()
        {
            foreach (Control ctr in this.Controls)
            {
                if (ctr is Panel )
                {
                    foreach (Control pnCtr in ctr.Controls)
                    {
                        if (pnCtr is TextBox)
                        {
                            if (((TextBox)pnCtr).Text != String.Empty) return false;
                        }
                        else if (pnCtr is ComboBox)
                        {
                            string text = "";
                            if (((ComboBox)pnCtr).Text != String.Empty)
                            {
                                text = ((ComboBox)pnCtr).Text.Replace("-", "");
                            }
                            if (text != String.Empty) return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
