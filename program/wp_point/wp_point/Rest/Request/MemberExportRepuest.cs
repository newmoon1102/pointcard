using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace _wp_point.Rest.Request
{
    class MemberExportRepuest
    {
        public string shop_auth_code { get; set; }
        public string qr_code { get; set; }
        public string login_id { get; set; }
        public string password { get; set; }
        public int with_data_flag { get; set; }
    }

    #region 送信データ
    /// <summary>
    /// 会員情報登録・更新の送信用データクラス
    /// </summary>
    public class Requestdata_MemberInfo_InputOrUpdate
    {
        public string shop_auth_code { get; set; }
        public string member_id { get; set; }
        public string card_no { get; set; }
        public string qr_code { get; set; }
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
        public string sex { get; set; }
        public string birth { get; set; }
        public string call_name { get; set; }
        public string mailmaga_disable_flag { get; set; }
        public string dm_disable_flag { get; set; }

        /// <summary>
        /// HTTP送信用にJson形式出力
        /// </summary>
        /// <returns>Json形式のメンバ変数リスト</returns>
        public HttpContent GetHttpContentsData()
        {
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("shop_auth_code",shop_auth_code),
                new KeyValuePair<string,string>("member_id",member_id),
                new KeyValuePair<string,string>("card_no",card_no),
                new KeyValuePair<string,string>("qr_code",qr_code),
                new KeyValuePair<string,string>("last_name",last_name),
                new KeyValuePair<string,string>("first_name",first_name),
                new KeyValuePair<string,string>("last_name_y",last_name_y),
                new KeyValuePair<string,string>("first_name_y",first_name_y),
                new KeyValuePair<string,string>("zip_1",zip_1),
                new KeyValuePair<string,string>("zip_2",zip_2),
                new KeyValuePair<string,string>("pref_name",pref_name),
                new KeyValuePair<string,string>("area_name",area_name),
                new KeyValuePair<string,string>("city_name",city_name),
                new KeyValuePair<string,string>("block",block),
                new KeyValuePair<string,string>("building",building),
                new KeyValuePair<string,string>("tel_number_1",tel_number_1),
                new KeyValuePair<string,string>("tel_number_2",tel_number_2),
                new KeyValuePair<string,string>("tel_number_3",tel_number_3),
                new KeyValuePair<string,string>("mobile_number_1",mobile_number_1),
                new KeyValuePair<string,string>("mobile_number_2",mobile_number_2),
                new KeyValuePair<string,string>("mobile_number_3",mobile_number_3),
                new KeyValuePair<string,string>("other_tel_number_1",other_tel_number_1),
                new KeyValuePair<string,string>("other_tel_number_2",other_tel_number_2),
                new KeyValuePair<string,string>("other_tel_number_3",other_tel_number_3),
                new KeyValuePair<string,string>("mail_address",mail_address),
                new KeyValuePair<string,string>("password",password),
                new KeyValuePair<string,string>("sex",sex),
                new KeyValuePair<string,string>("birth",birth),
                new KeyValuePair<string,string>("call_name",call_name),
                new KeyValuePair<string,string>("mailmaga_disable_flag",mailmaga_disable_flag),
                new KeyValuePair<string,string>("dm_disable_flag",dm_disable_flag)
            });

            return content;
        }

        /// <summary>
        /// 会員情報照会にて得たデータをそのままコピーする
        /// </summary>
        /// <param name="data">会員情報照会応答データ</param>
        public void SetReferenceData(ref ResponseData_MemberInfo_Reference data)
        {
            member_id = data.member_id;
            card_no = data.card_no;
            qr_code = data.card_qr;
            last_name = data.last_name;
            first_name = data.first_name;
            last_name_y = data.last_name_y;
            first_name_y = data.first_name_y;
            zip_1 = data.zip_1;
            zip_2 = data.zip_2;
            pref_name = data.pref_name;
            area_name = data.area_name;
            city_name = data.city_name;
            block = data.block;
            building = data.building;
            tel_number_1 = data.tel_number_1;
            tel_number_2 = data.tel_number_2;
            tel_number_3 = data.tel_number_3;
            mobile_number_1 = data.mobile_number_1;
            mobile_number_2 = data.mobile_number_2;
            mobile_number_3 = data.mobile_number_3;
            other_tel_number_1 = data.other_tel_number_1;
            other_tel_number_2 = data.other_tel_number_2;
            other_tel_number_3 = data.other_tel_number_3;
            mail_address = data.mail_address;
            password = data.password;
            sex = data.sex;
            birth = data.birth;
            call_name = data.call_name;
            mailmaga_disable_flag = data.mailmaga_disable_flag;
            dm_disable_flag = data.dm_disable_flag;
        }
    }
    #endregion

    #region 戻り値
    /// <summary>
    /// 会員情報照会の戻り値格納クラス
    /// </summary>
    public class ResponseData_MemberInfo_Reference
    {
        public string code { get; set; }
        public string member_id { get; set; }
        public string card_no { get; set; }
        public string card_qr { get; set; }
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

        /// <summary>
        /// 戻り値からデータ取得
        /// </summary>
        /// <param name="jsondata">会員情報照会の戻り値をそのまま格納</param>
        public void GetJsonData(JsonValue jsondata)
        {
            code = (string)jsondata["code"];
            member_id = (string)jsondata["member_id"];
            card_no = (string)jsondata["card_no"];
            card_qr = (string)jsondata["card_qr"];
            point = (string)jsondata["point"];
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

        /// <summary>
        /// 戻り値からデータ取得
        /// </summary>
        /// <param name="jsondata">会員情報照会の戻り値をそのまま格納</param>
        public void GetJsonDataSearch(JsonValue jsondata)
        {
            member_id = (string)jsondata["member_id"];
            card_no = (string)jsondata["card_no"];
            card_qr = (string)jsondata["card_qr"];
            point = (string)jsondata["point"];
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
            //call_name = (string)jsondata["call_name"];
            //mailmaga_disable_flag = (string)jsondata["mailmaga_disable_flag"];
            //dm_disable_flag = (string)jsondata["dm_disable_flag"];
            sex = (string)jsondata["sex"];
            birth = (string)jsondata["birth"];
            member_type = (string)jsondata["member_type"];
        }

    }

    public class ResponseData_MemberInfo_InputOrUpdate
    {
        /// <summary>ステータスコード</summary>
        public int code { get; set; }
        /// <summary>会員ID</summary>
        public int member_id { get; set; }
        /// <summary>メールアドレス</summary>
        public string mail_address { get; set; }
        /// <summary>備考</summary>
        public string note { get; set; }

        /// <summary>
        /// 戻り値からデータ取得
        /// </summary>
        /// <param name="jsondata">会員情報照会の戻り値をそのまま格納</param>
        public void GetJsonData(JsonValue jsondata)
        {
            try
            {
                code = (int)jsondata["code"];
                if (code == 200)
                {
                    member_id = (int)jsondata["member_id"];
                    mail_address = (string)jsondata["mail_address"];
                }
                else
                {
                    if (code == 405)
                    {
                        note = "QRコード値重複エラー";
                    }
                    else
                    {
                        note = (string)jsondata["note"];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    #endregion
}
