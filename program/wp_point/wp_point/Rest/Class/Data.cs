using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using _wp_point.Rest.Request;

namespace _wp_point.Rest.Class
{
    class Data
    {
        public static MemberImportRequest memberdata;

        public static MemberImportRequest geMemberdata(int id)
        {
            SqlConnection con = new SqlConnection();

            if (Common.SqlCon(con))
            {
                memberdata = new MemberImportRequest();
                SqlDataReader rd = (null);
                try
                {
                    if (!string.IsNullOrEmpty(id.ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT * FROM NEW_RECEIPT_LIST WHERE RECEIPT_ID = " + id, con);
                        rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            memberdata.receipt_id = id.ToString();
                            memberdata.receipt_date = (rd["RECEIPT_DATE"].ToString());
                            memberdata.last_name = (rd["LAST_NAME"].ToString());
                            memberdata.first_name = (rd["FIRT_NAME"].ToString());
                            memberdata .last_name_y= (rd["LAST_NAME_Y"].ToString());
                            memberdata.first_name_y = (rd["FIRT_NAME_Y"].ToString());
                            memberdata.zip_1 = (rd["ZIP_1"].ToString());
                            memberdata.zip_2 = (rd["ZIP_2"].ToString());
                            memberdata.pref_name = (rd["PREF_NAME"].ToString());
                            memberdata.area_name = (rd["AREA_NAME"].ToString());
                            memberdata.city_name = (rd["CITY_NAME"].ToString());
                            memberdata.block = (rd["BLOCK"].ToString());
                            memberdata.building = (rd["BUILDING"].ToString());
                            memberdata.tel_number_1 = (rd["TEL_NUMBER_1"].ToString());
                            memberdata.tel_number_2 = (rd["TEL_NUMBER_2"].ToString());
                            memberdata.tel_number_3 = (rd["TEL_NUMBER_3"].ToString());
                            memberdata.mobile_number_1 = (rd["MOBILE_NUMBER_1"].ToString());
                            memberdata.mobile_number_2 = (rd["MOBILE_NUMBER_2"].ToString());
                            memberdata.mobile_number_3 = (rd["MOBILE_NUMBER_3"].ToString());
                            memberdata.other_tel_number_1 = (rd["OTHER_NUMBER_1"].ToString());
                            memberdata.other_tel_number_2 = (rd["OTHER_NUMBER_2"].ToString());
                            memberdata.other_tel_number_3 = (rd["OTHER_NUMBER_3"].ToString());
                            memberdata.sex = (rd["SEX"].ToString());
                            memberdata.birth = (rd["BIRTH"].ToString());
                            memberdata.mail_address = (rd["MAIL_ADDRESS"].ToString());
                            memberdata.password = (rd["PASSWORD"].ToString());
                            memberdata.call_name = (rd["CALL_NAME"].ToString());
                            memberdata.mailmaga_disable_flag = (rd["MAILMAGA_DISABLE_FLG"].ToString());
                            memberdata.dm_disable_flag = (rd["DM_DISABLE_FLG"].ToString());
                            memberdata.member_id = (rd["MEMBER_ID"].ToString());
                        }

                        con.Close();
                    }
                    else
                    {
                        MsgBox.Show("データが存在しませんでした。","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception)
                {
                    MsgBox.Show("データベースへの接続に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
            else
            {
                MsgBox.Show("データベースへの接続に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return memberdata;

        }

        public static bool updateMemberData(MemberImportRequest memberdata)
        {
            try
            {
                SqlConnection con = new SqlConnection();

                if (Common.SqlCon(con))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE NEW_RECEIPT_LIST SET" + 
                        " MEMBER_ID = '" + memberdata.member_id + 
                        //"', RECEIPT_DATE = '" + memberdata.receipt_date + 
                        "', CARD_NO = '" + memberdata.card_no + 
                        "', QR_CODE = '" + memberdata.qr_code +
                        "', CARD_POINT = '" + memberdata.point +
                        "', LAST_NAME = '" + memberdata.last_name +
                        "', FIRT_NAME = '" + memberdata.first_name +
                        "', LAST_NAME_Y = '" + memberdata.last_name_y +
                        "', FIRT_NAME_Y = '" + memberdata.first_name_y +
                        "', ZIP_1 = '" + memberdata.zip_1 +
                        "', ZIP_2 = '" + memberdata.zip_2 +
                        "', PREF_NAME = '" + memberdata.pref_name +
                        "', AREA_NAME = '" + memberdata.area_name +
                        "', CITY_NAME = '" + memberdata.city_name +
                        "', BLOCK = '" + memberdata.block +
                        "', BUILDING = '" + memberdata.building +
                        "', TEL_NUMBER_1 = '" + memberdata.tel_number_1 +
                        "', TEL_NUMBER_2 = '" + memberdata.tel_number_2 +
                        "', TEL_NUMBER_3 = '" + memberdata.tel_number_3 +
                        "', MOBILE_NUMBER_1 = '" + memberdata.mobile_number_1 +
                        "', MOBILE_NUMBER_2 = '" + memberdata.mobile_number_2 +
                        "', MOBILE_NUMBER_3 = '" + memberdata.mobile_number_3 +
                        "', OTHER_NUMBER_1 = '" + memberdata.other_tel_number_1 +
                        "', OTHER_NUMBER_2 = '" + memberdata.other_tel_number_2 +
                        "', OTHER_NUMBER_3 = '" + memberdata.other_tel_number_3 +
                        "', MAIL_ADDRESS = '" + memberdata.mail_address +
                        "', PASSWORD = '" + memberdata.password +
                        "', CALL_NAME = '" + memberdata.call_name +
                        "', MAILMAGA_DISABLE_FLG = '" + memberdata.mailmaga_disable_flag +
                        "', DM_DISABLE_FLG = '" + memberdata.dm_disable_flag +
                        "', SEX = '" + memberdata.sex +
                        "', BIRTH = '" + memberdata.birth +
                        "', CARD_ISSUE_DATE = '" + memberdata.card_issue_date + 
                        "', CARD_FLG = '" + memberdata.card_flg + 
                        "' WHERE RECEIPT_ID = " + memberdata.receipt_id, con);
                    cmd.ExecuteReader();
                }
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Show(string.Format("エラー:{0}", ex.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public static bool insertMemberData(MemberImportRequest memberdata)
        {
            try
            {
                string receipt_id = "";
                SqlDataReader rd = (null);
                SqlConnection con = new SqlConnection();

                if (Common.SqlCon(con))
                {
                    if(memberdata.receipt_id == "0")
                    {
                        SqlCommand cmdReceipt = new SqlCommand("SELECT NEXT VALUE FOR SEQ_RECEIPT_ID AS RECEIPT_ID", con);
                        rd = cmdReceipt.ExecuteReader();
                        while (rd.Read())
                        {
                            receipt_id = rd["RECEIPT_ID"].ToString();
                        }
                        rd.Close();
                        memberdata.receipt_id = receipt_id;
                    }                

                    SqlCommand cmd = new SqlCommand("INSERT INTO NEW_RECEIPT_LIST VALUES ("
                                + "'" + memberdata.receipt_id + "',"
                                + "'" + memberdata.member_id + "',"
                                + "'" + memberdata.receipt_date + "',"
                                + "'" + memberdata.card_no + "',"
                                + "'" + memberdata.qr_code + "',"
                                + "'" + memberdata.point + "',"
                                + "'" + memberdata.last_name + "',"
                                + "'" + memberdata.first_name + "',"
                                + "'" + memberdata.last_name_y + "',"
                                + "'" + memberdata.first_name_y + "',"
                                + "'" + memberdata.zip_1 + "',"
                                + "'" + memberdata.zip_2 + "',"
                                + "'" + memberdata.pref_name + "',"
                                + "'" + memberdata.area_name + "',"
                                + "'" + memberdata.city_name + "',"
                                + "'" + memberdata.block + "',"
                                + "'" + memberdata.building + "',"
                                + "'" + memberdata.tel_number_1 + "',"
                                + "'" + memberdata.tel_number_2 + "',"
                                + "'" + memberdata.tel_number_3 + "',"
                                + "'" + memberdata.mobile_number_1 + "',"
                                + "'" + memberdata.mobile_number_2 + "',"
                                + "'" + memberdata.mobile_number_3 + "',"
                                + "'" + memberdata.other_tel_number_1 + "',"
                                + "'" + memberdata.other_tel_number_2 + "',"
                                + "'" + memberdata.other_tel_number_3 + "',"
                                + "'" + memberdata.mail_address + "',"
                                + "'" + memberdata.password + "',"
                                + "'" + memberdata.call_name + "',"
                                + "'" + memberdata.mailmaga_disable_flag + "',"
                                + "'" + memberdata.dm_disable_flag + "',"
                                + "'" + memberdata.sex + "',"
                                + "'" + memberdata.birth + "',"
                                + "'" + memberdata.member_type + "',"
                                + "'" + memberdata.card_issue_date + "',"
                                + "'" + memberdata.card_flg +"' )", con);
                    cmd.ExecuteReader();
                }
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Show(string.Format("エラー:{0}", ex.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        public static bool deleteRecipt(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection();

                if (Common.SqlCon(con))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE NEW_RECEIPT_LIST SET CARD_FLG = 'TRUE' WHERE RECEIPT_ID = " + id, con);
                    //SqlCommand cmd = new SqlCommand("DELETE FROM NEW_RECEIPT_LIST WHERE RECEIPT_ID = " + id, con);
                    cmd.ExecuteReader();
                }
                con.Close();
                return true;
            }
            catch(Exception ex)
            {
                MsgBox.Show(string.Format("エラー:{0}", ex.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }
}
