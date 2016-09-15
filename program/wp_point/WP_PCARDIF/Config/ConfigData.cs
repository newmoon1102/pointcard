using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WP_PCARDIF.Config
{
    /// <summary>
    /// 設定データを表します。
    /// </summary>
    class ConfigData
    {
        public string DBHostName { get; set; }
        public string DBDatabaseName { get; set; }
        public string DBUserName { get; set; }
        public string DBPassword { get; set; }
        public string CSVMoveSourceDirectory { get; set; }
        public string CSVMoveDestinationDirectory { get; set; }
        public string ProcessingMode { get; set; }
        public string ProcessingFilePath { get; set; }
        public string KigyoCode { get; set; }//ポイントカード機に設定された企業コード

        /// <summary>
        /// アプリケーション構成ファイルを読み込み、ConfigDataクラスの新しいインスタンスを初期化します。
        /// </summary>
        public ConfigData()
        {
            this.DBHostName                  = ConfigurationManager.AppSettings["DB_HostName"];
            this.DBDatabaseName              = ConfigurationManager.AppSettings["DB_DatabaseName"];
            this.DBUserName                  = ConfigurationManager.AppSettings["DB_UserName"];
            this.DBPassword                  = ConfigurationManager.AppSettings["DB_Password"];
            this.CSVMoveSourceDirectory      = ConfigurationManager.AppSettings["CSV_MoveSourceDirectory"];
            this.CSVMoveDestinationDirectory = ConfigurationManager.AppSettings["CSV_MoveDestinationDirectory"];
            this.ProcessingMode              = ConfigurationManager.AppSettings["ProcessingMode"];
            this.ProcessingFilePath          = ConfigurationManager.AppSettings["ProcessingFilePath"];
            this.KigyoCode                   = ConfigurationManager.AppSettings["KigyoCode"];
        }
        
    }
}
