using log4net;
using System;
using System.Reflection;
using _wp_upload_point.Database;
using _wp_upload_point.Rest;
using System.Collections.Generic;
using _wp_upload_point.Rest.Request;
using System.Threading;
using CommandLine;
using System.Text;
using System.Json;

namespace _wp_upload_point
{
    class Options {
        [Option('h', null, HelpText = "Show the help.")]
        public bool Help { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("wp_upload_point Application Usage");
            usage.AppendLine("------------------------------------------------------------------------------");
            return usage.ToString();
        }
    }

    class Program
    {
        static int RunClient(ILog logger)
        {

            //ポイント履歴データを抽出(抽出条件: UPLOAD_FLG=1)
            T_PCARDDao t_pcardDao = new T_PCARDDao();
            List<T_PCARD> t_pcards = t_pcardDao.GetT_PCARDs();

            if (t_pcards.Count > 0)
            {
                ApiClient apiClient = new ApiClient(logger);

                //店舗認証コードの取得
                JsonValue shopAuth = apiClient.ShopAuth();
                if (shopAuth == null)
                {
                    return 1;
                }
                string shop_auth_code = (string)shopAuth["shop_auth_code"];

                //ポイント履歴をアップロード
                int result = 0;
                for (int i = 0; i < t_pcards.Count; i++)
                {
                    JsonValue value = apiClient.HistoyUpload(t_pcards[i], shop_auth_code);
                    if (value != null)//アップロード成功時
                    {
                        t_pcardDao.SetT_PCARDUploadFlg(t_pcards[i], 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));//アップロードフラグを下ろし、アップロード時刻をセット
                    }
                    else
                    {
                        result = 1;
                    }
                    //※なお、アップロードエラー時にも、自動的にはアップロードフラグを下ろさない
                    //また、エラー時にも処理を中断せず、次のデータに処理を進める

                    logger.InfoFormat("Wait {0} seconds", Properties.Settings.Default.SleepPerRequest);
                    Thread.Sleep((int)(Properties.Settings.Default.SleepPerRequest * 1000));
                }
                return result;
            }
            else
            {
                logger.Info("No data to upload.");
                return 0;
            }
        }

        static int Main(string[] args)
        {
            ILog logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            var options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                logger.Error("Arguments parse error.");
                return 1;
            }
            else {
                if (options.Help) {
                    Console.WriteLine(options.GetUsage());
                    return 0;
                }
            }
            
            try
            {
                logger.Info("Application Start.");
                int result = RunClient(logger);
                if (result == 0)
                {
                    logger.Info("Application Finish Success.");
                }
                else
                {
                    logger.Fatal("Application Finish with Error");
                }
#if DEBUG
                Console.ReadLine();
#endif
                return result;
            }
            catch (Exception e)
            {
                logger.Fatal("Application Finish with Error", e);
#if DEBUG
                Console.ReadLine();
#endif
                return 1;
            }
        }
    }
}
