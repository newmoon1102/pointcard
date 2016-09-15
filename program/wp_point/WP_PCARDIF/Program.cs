using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using WP_PCARDIF.Config;
using WP_PCARDIF.DB;
using WP_PCARDIF.Card;
using log4net;
using System.Reflection;


namespace WP_PCARDIF
{
    class Program
    {
        static int Main(string[] args)
        {
            //2016.6.6 標準出力→log4netにてログ出力に変更
            ILog logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            int return_code = 0;

            ConfigData configData = new ConfigData(); // Config読込

            DBOperation dbOperation = new DBOperation(configData.DBHostName,
                                                      configData.DBDatabaseName,
                                                      configData.DBUserName,
                                                      configData.DBPassword
                                                      );

            try
            {
                dbOperation.DBConnect(); // DB接続

                if (configData.ProcessingMode == "0")
                {
                    //Console.WriteLine("処理モード0で実行します");
                    logger.Info("処理モード0で実行します");
                    dbOperation.InsertLog("M00001", "カード端末データ取得処理を開始します。", logger);

                    string[] fileList = Directory.GetFiles(configData.CSVMoveSourceDirectory, configData.KigyoCode + "*.txt"); // ファイルサーチ                    

                    foreach (string filePath in fileList)
                    {
                        //2016.6.3 DB処理(正常)完了後にCSVファイルを移動するように動作仕様を変更
                        //string newFilePath = configData.CSVMoveDestinationDirectory + @"\" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(filePath);
                        //
                        //File.Move(filePath, newFilePath); // ファイルリネーム＆ファイル移動
                        //
                        //dbOperation.InsertLog("M00002", string.Format("CSVファイル変換処理を開始します。ファイル名：{0}", Path.GetFileName(filePath)));
                        //
                        //CardData cardData = new CardData(newFilePath, Path.GetFileName(filePath)); // 移動したCSVファイルからCardDataインスタンス生成
                        //
                        //dbOperation.InsertCardData(cardData); // メモリカード情報トランにINSERT

                        dbOperation.InsertLog("M00002", string.Format("CSVファイル変換処理を開始します。ファイル名：{0}", Path.GetFileName(filePath)), logger);
                        
                        CardData cardData = new CardData(filePath, Path.GetFileName(filePath)); // CSVファイルからCardDataインスタンス生成

                        dbOperation.InsertCardData(cardData, logger); // メモリカード情報トランにINSERT

                        string newFilePath = configData.CSVMoveDestinationDirectory + @"\" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(filePath);
                        File.Move(filePath, newFilePath); // ファイルリネーム＆ファイル移動 
                    }

                }
                else if (configData.ProcessingMode == "1")
                {
                    //Console.WriteLine("処理モード1で実行します");
                    logger.Info("処理モード1で実行します");

                    CardData cardData = new CardData(configData.ProcessingFilePath, Path.GetFileName(configData.ProcessingFilePath)); // 指定したCSVファイルからCardDataインスタンス生成

                    dbOperation.InsertCardData(cardData, logger); // メモリカード情報トランにINSERT
                }
                else
                {
                    throw new Exception("処理モードは0,1のいずれかを設定して下さい。");
                }

            }
            catch (Exception ex)
            {
                return_code = 1;
                //Console.WriteLine("\"{0}\"エラー：{1}", ex.TargetSite, ex.Message);
                logger.ErrorFormat("\"{0}\"エラー：{1}", ex.TargetSite, ex.Message);
            }
            finally
            {
                dbOperation.DBClose(); // DB切断
            }

            //Console.ReadKey();

            return return_code;

        }
    }
}
