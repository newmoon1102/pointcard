using log4net;
using System;
using System.Collections.Generic;
using System.Json;
using System.Net;
using System.Net.Http;
using _wp_point.Rest.Request;
using System.Windows.Forms;

namespace _wp_point.Rest
{
    class ApiClient
    {
        /// <summary>
        /// Initializes a new instance of the HttpClient class.
        /// </summary>
        private HttpClient httpClient;
        private ILog logger;

        public ApiClient(ILog logger)
        {
            HttpClientHandler handler = new HttpClientHandler();

            if (!Properties.Settings.Default.SSLVerify)
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }

            this.httpClient = new HttpClient(handler);

            this.logger = logger;
        }

        /// <summary>
        /// Store management authentication
        /// </summary>
        /// <param name="login_id">店舗ＩＤ</param>
        /// <param name="password">パスワード</param>
        /// <returns></returns>
        public JsonValue PostAuthStatus(string login_id, string password)
        {
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("login_id", login_id),
                new KeyValuePair<string, string>("password", password)
            });

            try
            {
                return SendPostRequest(Properties.Settings.Default.APIAuthShopURL, content);
            }
            catch (HttpRequestException ex)
            {
                this.logger.Error(ex.StackTrace);
                this.logger.ErrorFormat("Login fail with id: {0}", login_id);
                return null;
            }
        }

        /// <summary>
        /// Check email in waitingpass server
        /// </summary>
        /// <param name="email">メール</param>
        /// <param name="auth_code">店舗認証コード</param>
        /// <returns></returns>
        public JsonValue checkEmail(string email, string auth_code)
        {
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("shop_auth_code", auth_code),
                new KeyValuePair<string, string>("mail_address", email)
                });
            try
            {
                return SendPostRequest(Properties.Settings.Default.APICheckEmailURL, content);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// 住所検索
        /// </summary>
        /// <param name="code">郵便番号</param>
        /// <returns></returns>
        public JsonValue AddressSearch(string code)
        {
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("zip_code", code)
                });
            try
            {
                return SendPostRequest(Properties.Settings.Default.APIAddressSearchURL, content);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// Insert data to server
        /// </summary>
        /// <param name="dataPost"></param>
        /// <returns></returns>
        public JsonValue PostMemberIm(MemberImportRequest dataPost)
        {
            string member_id = dataPost.member_id;
            if (String.IsNullOrEmpty(member_id))
            {
                member_id = null;
            }
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("shop_auth_code", dataPost.shop_auth_code),
                new KeyValuePair<string, string>("member_id", member_id),
                new KeyValuePair<string, string>("card_no", dataPost.card_no),
                new KeyValuePair<string, string>("qr_code", dataPost.qr_code),
                new KeyValuePair<string, string>("last_name", dataPost.last_name),
                new KeyValuePair<string, string>("first_name", dataPost.first_name),
                new KeyValuePair<string, string>("last_name_y", dataPost.last_name_y),
                new KeyValuePair<string, string>("first_name_y", dataPost.first_name_y),
                new KeyValuePair<string, string>("zip_1", dataPost.zip_1),
                new KeyValuePair<string, string>("zip_2", dataPost.zip_2),
                new KeyValuePair<string, string>("pref_name", dataPost.pref_name),
                new KeyValuePair<string, string>("area_name", dataPost.area_name),
                new KeyValuePair<string, string>("city_name", dataPost.city_name),
                new KeyValuePair<string, string>("block", dataPost.block),
                new KeyValuePair<string, string>("building", dataPost.building),
                new KeyValuePair<string, string>("tel_number_1", dataPost.tel_number_1),
                new KeyValuePair<string, string>("tel_number_2", dataPost.tel_number_2),
                new KeyValuePair<string, string>("tel_number_3", dataPost.tel_number_3),
                new KeyValuePair<string, string>("mobile_number_1", dataPost.mobile_number_1),
                new KeyValuePair<string, string>("mobile_number_2", dataPost.mobile_number_2),
                new KeyValuePair<string, string>("mobile_number_3", dataPost.mobile_number_3),
                new KeyValuePair<string, string>("other_tel_number_1", dataPost.other_tel_number_1),
                new KeyValuePair<string, string>("other_tel_number_2", dataPost.other_tel_number_2),
                new KeyValuePair<string, string>("other_tel_number_3", dataPost.other_tel_number_3),
                new KeyValuePair<string, string>("sex", dataPost.sex.ToString()),
                new KeyValuePair<string, string>("birth", dataPost.birth),
                new KeyValuePair<string, string>("mail_address", dataPost.mail_address),
                new KeyValuePair<string, string>("password", dataPost.password),
                new KeyValuePair<string, string>("call_name", dataPost.call_name),
                new KeyValuePair<string, string>("mailmaga_disable_flag", dataPost.mailmaga_disable_flag),
                new KeyValuePair<string, string>("dm_disable_flag", dataPost.dm_disable_flag)
            });

            try
            {
                return SendPostRequest(Properties.Settings.Default.APIMemberImportURL, content);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.StackTrace);
                this.logger.ErrorFormat("Update transaction_date Error transaction_date: {0}", DateTime.Now);
                return null;
            }
        }

        /// <summary>
        /// Get data from server
        /// </summary>
        /// <param name="dataPost"></param>
        /// <returns></returns>
        public JsonValue PostMemberEx(MemberExportRepuest dataPost)
        {

            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("shop_auth_code", dataPost.shop_auth_code),
                new KeyValuePair<string, string>("member_qr_code", dataPost.qr_code),
                new KeyValuePair<string, string>("with_data_flag", dataPost.with_data_flag.ToString())
            });

            try
            {
                return SendPostRequest(Properties.Settings.Default.APIMemberExportURL, content);
            }
            catch (Exception e)
            {
                this.logger.Error(e.StackTrace);
                this.logger.ErrorFormat("Update transaction_date Error transaction_date: {0}", DateTime.Now);
                return null;
            }
        }

        /// <summary>
        /// Web member data
        /// </summary>
        /// <param name="dataPost"></param>
        /// <returns></returns>
        public JsonValue PostMemberAuth(MemberExportRepuest dataPost)
        {

            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("shop_auth_code", dataPost.shop_auth_code),
                new KeyValuePair<string, string>("qr_code", dataPost.qr_code),
                new KeyValuePair<string, string>("with_data_flag", dataPost.with_data_flag.ToString())
            });

            try
            {
                return SendPostRequest(Properties.Settings.Default.APIAuthQRCodeURL, content);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex.StackTrace);
                this.logger.ErrorFormat("Update transaction_date Error transaction_date: {0}", DateTime.Now);
                return null;
            }
        }

        public JsonValue SendGetRequest(string url)
        {
            this.logger.InfoFormat("Connect to api: {0}", url);

            var result = this.httpClient.GetAsync(url).Result;
            result.EnsureSuccessStatusCode();
            var jsonResult = result.Content.ReadAsAsync<JsonValue>().Result;
            return jsonResult;
        }

        public JsonValue SendPostRequest(string url, HttpContent content)
        {
            this.logger.InfoFormat("Connect to api: {0}", url);
            var result = this.httpClient.PostAsync(url, content).Result;
            result.EnsureSuccessStatusCode();
            var jsonResult = result.Content.ReadAsAsync<JsonValue>().Result;
            return jsonResult;
        }

        private JsonValue CheckResult(JsonValue result)
        {
            if ((string)result["code"] != "200")
            {
                this.logger.ErrorFormat("API Error code: {0} with note: {1}", result["code"], result["note"]);
            }
            else
            {
                this.logger.InfoFormat("Response: {0}", result.ToString());
            }
            return result;
        }


        /// <summary>
        /// 会員情報参照
        /// </summary>
        /// <param name="ShopAuthCode">店舗認証コード</param>
        /// <param name="MemberID">会員ID</param>
        /// <param name="PointCardNo">ポイントカード番号</param>
        /// <param name="QRCode">QRコード値</param>
        /// <param name="WebQRCode">Web会員証QRコード値</param>
        /// <returns></returns>
        public JsonValue MemberInfo_Reference(string ShopAuthCode, string MemberID, string PointCardNo, string QRCode, string WebQRCode)
        {
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("shop_auth_code", ShopAuthCode),
                new KeyValuePair<string, string>("member_id", MemberID),
                new KeyValuePair<string, string>("card_no", PointCardNo),
                new KeyValuePair<string, string>("qr_code", QRCode),
                new KeyValuePair<string, string>("member_qr_code", WebQRCode)
            });

            try
            {
                return SendPostRequest(Properties.Settings.Default.APIMemberExportURL, content);
            }
            catch (Exception e)
            {
                this.logger.Error(e.StackTrace);
                this.logger.ErrorFormat("Update transaction_date Error transaction_date: {0}", DateTime.Now);
                return null;
            }
        }

        /// <summary>
        /// 会員情報更新
        /// </summary>
        /// <param name="data">Requestdata_MemberInfo_InputOrUpdateクラスを使用</param>
        /// <returns></returns>
        public JsonValue MemberInfo_InsertUpdate(Requestdata_MemberInfo_InputOrUpdate data)
        {
            try
            {
                return SendPostRequest(Properties.Settings.Default.APIMemberImportURL, data.GetHttpContentsData());
            }
            catch (Exception e)
            {
                this.logger.Error(e.StackTrace);
                this.logger.ErrorFormat("Update transaction_date Error transaction_date: {0}", DateTime.Now);
                return null;
            }
        }

        /// <summary>
        /// 会員とポイントカード情報との再紐づけ
        /// </summary>
        /// <param name="data">Request Replape Member クラスを使用</param>
        /// <returns></returns>
        public JsonValue MemberMerge(MemberMergeRequest data)
        {
            try
            {
                return SendPostRequest(Properties.Settings.Default.APIMemberMergeURL, data.GetHttpContentsData());
            }
            catch (Exception e)
            {
                this.logger.Error(e.StackTrace);
                this.logger.ErrorFormat("Update transaction_date Error transaction_date: {0}", DateTime.Now);
                return null;
            }
        }
    }
}
