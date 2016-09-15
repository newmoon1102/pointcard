using _wp_upload_point.Rest.Request;
using log4net;
using System;
using System.Collections.Generic;
using System.Json;
using System.Net;
using System.Net.Http;
using System.Linq;
using System.Web.Script.Serialization;

namespace _wp_upload_point.Rest
{
    class ApiClient
    {
        private HttpClient httpClient;
        private ILog logger;

        public ApiClient(ILog logger)
        {
            HttpClientHandler handler = new HttpClientHandler();
            if (Properties.Settings.Default.UseProxy)
            {
                WebProxy proxy = new WebProxy(Properties.Settings.Default.Proxy, true);
                proxy.Credentials = new NetworkCredential(Properties.Settings.Default.ProxyUser, Properties.Settings.Default.ProxyPassword);
                handler.Proxy = proxy;
                handler.UseProxy = true;
                ServicePointManager.Expect100Continue = false;
            }

            if (!Properties.Settings.Default.SSLVerify)
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }

            this.httpClient = new HttpClient(handler);

            this.logger = logger;
        }

        public JsonValue ShopAuth()
        {
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("login_id", Properties.Settings.Default.ShopAuthLoginID),
                new KeyValuePair<string, string>("password", Properties.Settings.Default.ShopAuthPassword)
            });

            try
            {
                return SendPostRequest(Properties.Settings.Default.APIShopAuthUrl, content);
            }
            catch (Exception e)
            {
                this.logger.Error(e.StackTrace);
                this.logger.ErrorFormat("Shop Auth Error. login_id : {0}", Properties.Settings.Default.ShopAuthLoginID);
                return null;
            }
        }

        public JsonValue HistoyUpload(T_PCARD t_pcard, string shop_auth_code)
        {
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("shop_auth_code", shop_auth_code),
                new KeyValuePair<string, string>("card_no", t_pcard.card_no),//ポイントカード番号
                new KeyValuePair<string, string>("use_datetime", t_pcard.use_datetime),//利用日時
                new KeyValuePair<string, string>("use_shop_no", t_pcard.use_shop_no),//利用店舗番号
                new KeyValuePair<string, string>("user_name", t_pcard.user_name),//カード氏名
                new KeyValuePair<string, string>("sales", t_pcard.sales.ToString()),//売上額
                new KeyValuePair<string, string>("bonus_point", t_pcard.bonus_point.ToString()),//売上ポイント
                new KeyValuePair<string, string>("get_point", t_pcard.get_point.ToString()),//新規ポイント
                new KeyValuePair<string, string>("used_point", t_pcard.used_point.ToString()),//交換ポイント
                new KeyValuePair<string, string>("card_point", t_pcard.card_point.ToString()),//累計ポイント
                new KeyValuePair<string, string>("charge_prepaid", t_pcard.charge_prepaid.ToString()),//入金額
                new KeyValuePair<string, string>("card_prepaid", t_pcard.card_prepaid.ToString()),//プリカ残高
                new KeyValuePair<string, string>("preca_process_type", t_pcard.preca_process_type)//プリカ処理区分
            });

            try
            {
                return SendPostRequest(Properties.Settings.Default.APIHistoryUploadUrl, content);
            }
            catch (Exception e)
            {
                this.logger.Error(e.StackTrace);
                this.logger.ErrorFormat("History Upload Error. TAMMATSU_CD:{0}, RIYO_DATE:{1}, card_no:{2}", t_pcard.tammatsu_cd, t_pcard.riyo_date, t_pcard.card_no); 
                return null;
            }
        }

        public JsonValue SendGetRequest(string url)
        {
            this.logger.InfoFormat("Connect to api: {0}", url);

            var result = this.httpClient.GetAsync(url).Result;
            result.EnsureSuccessStatusCode();
            var jsonResult = result.Content.ReadAsAsync<JsonValue>().Result;

            return CheckResult(jsonResult);
        }

        public JsonValue SendPostRequest(string url, HttpContent content)
        {
            this.logger.InfoFormat("Connect to api: {0}", url);
            var result = this.httpClient.PostAsync(url, content).Result;
            result.EnsureSuccessStatusCode();
            var jsonResult = result.Content.ReadAsAsync<JsonValue>().Result;
            return CheckResult(jsonResult);
        }

        public DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime).ToLocalTime();
        }

        private JsonValue CheckResult(JsonValue result)
        {
            this.logger.InfoFormat("Response: {0}", result.ToString());
            if ((string)result["code"] != "200")
            {
                this.logger.ErrorFormat("API status code: {0} with note: {1}", result["code"], result["note"]);
                throw new Exception("API Error");
            }
            return result;
        }
    }
}
