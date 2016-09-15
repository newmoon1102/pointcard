using System.Collections.Generic;
using System.Json;

namespace _wp_point.Rest.Request
{
    public class MemberImportRequest
    {
        public string shop_auth_code { get; set; }
        public string receipt_id { get; set; }
        public string member_id { get; set; }
        public string receipt_date { get; set; }
        public string card_no { get; set; }
        public string qr_code { get; set; }
        public string point { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string last_name_y { get; set; }
        public string first_name_y { get; set; }
        public string zip_1 { get; set; }
        public string zip_2 { get; set; }
        public string pref_name { get; set; }
        public string area_name { get; set; }
        public string city_name { get; set; }
        public string block { get; set; }
        public string building { get; set; }
        public string tel_number_1 { get; set; }
        public string tel_number_2 { get; set; }
        public string tel_number_3 { get; set; }
        public string mobile_number_1 { get; set; }
        public string mobile_number_2 { get; set; }
        public string mobile_number_3 { get; set; }
        public string other_tel_number_1 { get; set; }
        public string other_tel_number_2 { get; set; }
        public string other_tel_number_3 { get; set; }
        public string mail_address { get; set; }
        public string password { get; set; }
        public string call_name { get; set; }
        public string mailmaga_disable_flag { get; set; }
        public string dm_disable_flag { get; set; }
        public string sex { get; set; }
        public string birth { get; set; }
        public string member_type { get; set; }
        public string card_issue_date { get; set; }
        public string card_flg { get; set; }

        /// <summary>
        /// 戻り値からデータ取得
        /// </summary>
        /// <param name="jsondata">会員情報照会の戻り値をそのまま格納</param>
        public void GetJsonData(JsonValue jsondata)
        {
            member_id = (string)jsondata["member_id"];
            card_no = (string)jsondata["card_no"];
            last_name = (string)jsondata["last_name"];
            first_name = (string)jsondata["first_name"];
            last_name_y = (string)jsondata["last_name_y"];
            first_name_y = (string)jsondata["first_name_y"];
            zip_1 = (string)jsondata["zip_1"];
            zip_2 = (string)jsondata["zip_2"];
            pref_name = (string)jsondata["pref_name"];
            area_name = (string)jsondata["area_name"];
            city_name = (string)jsondata["city_name"];
            block = (string)jsondata["block"];
            building = (string)jsondata["bulding"];
            tel_number_1 = (string)jsondata["tel_number_1"];
            tel_number_2 = (string)jsondata["tel_number_2"];
            tel_number_3 = (string)jsondata["tel_number_3"];
            mobile_number_1 = (string)jsondata["mobile_number_1"];
            mobile_number_2 = (string)jsondata["mobile_number_2"];
            mobile_number_3 = (string)jsondata["mobile_number_3"];
            other_tel_number_1 = (string)jsondata["other_tel_number_1"];
            other_tel_number_2 = (string)jsondata["other_tel_number_2"];
            other_tel_number_3 = (string)jsondata["other_tel_number_3"];
            mail_address = (string)jsondata["mail_address"];
            password = (string)jsondata["password"];
            call_name = (string)jsondata["call_name"];
            mailmaga_disable_flag = (string)jsondata["mailmaga_disable_flag"];
            dm_disable_flag = (string)jsondata["dm_disable_flag"];
            sex = (string)jsondata["sex"];
            birth = (string)jsondata["birth"];
            member_type = (string)jsondata["member_type"];
        }
    }
}
