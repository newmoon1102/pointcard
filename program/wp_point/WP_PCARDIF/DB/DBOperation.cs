using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Transactions;
using System.Data;
using WP_PCARDIF.Card;
using log4net;


namespace WP_PCARDIF.DB
{
    /// <summary>
    /// データベースの操作を行います。
    /// </summary>
    class DBOperation
    {
        private string strSqlConnection;
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;


        /// <summary>
        /// データベースへの接続に必要な情報を指定して、DBOperationクラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="dbHostName">ホスト名</param>
        /// <param name="dbDatabaseName">データベース名</param>
        /// <param name="dbUserName">ユーザー名</param>
        /// <param name="dbPassword">パスワード</param>
        public DBOperation(string dbHostName, string dbDatabaseName, string dbUserName, string dbPassword)
        {
            this.strSqlConnection = string.Empty;
            this.strSqlConnection += "Data Source = " + dbHostName + ";";
            this.strSqlConnection += "Initial Catalog =" + dbDatabaseName + ";";
            this.strSqlConnection += "Integrated Security = False;";
            this.strSqlConnection += "User ID = " + dbUserName + ";";
            this.strSqlConnection += "Password = " + dbPassword + ";";
        }


        /// <summary>
        /// データベースへ接続します。
        /// </summary>
        public void DBConnect()
        {
            this.sqlConnection = new SqlConnection(this.strSqlConnection);
            this.sqlConnection.Open();
            this.sqlCommand = sqlConnection.CreateCommand();
        }


        /// <summary>
        /// データベースの接続を閉じます。
        /// </summary>
        public void DBClose()
        {
            this.sqlConnection.Close();
            this.sqlConnection.Dispose();
        }


        /// <summary>
        /// 実行ログを標準出力し、T_PCARD_LOGに実行ログを追加します。
        /// </summary>
        /// <param name="msgID">メッセージID</param>
        /// <param name="msg">メッセージ</param>
        public void InsertLog(string msgID, string msg, ILog logger)
        {
            string systemDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            //Console.WriteLine(msg);
            logger.Info(msg);
            this.sqlCommand.CommandText = string.Format("INSERT INTO dbo.T_PCARD_LOG VALUES(GETDATE(), '{0}', '{1}', '1', 'SystemManager', '{2}', 'SystemManager', '{3}')",
                                                        msgID,
                                                        msg,
                                                        "DT" + systemDate,
                                                        "DT" + systemDate);
            this.sqlCommand.ExecuteNonQuery();
        }


        /// <summary>
        /// CardDataインスタンスにあるカード情報をすべてT_PCARDに追加します。
        /// 同じカード情報が既にT_PCARDに存在している場合は、そのカード情報はスキップされます。
        /// </summary>
        /// <param name="cardData">CardDataクラスのインスタンス</param>
        public void InsertCardData(CardData cardData, ILog logger)
        {
            // トランザクション開始
            System.Data.SqlClient.SqlTransaction tr = null;
            tr = this.sqlConnection.BeginTransaction();
            this.sqlCommand.Transaction = tr;

            try
            {
                int insertCount = 0;

                foreach (List<string> record in cardData.Records)
                {
                    if (!this.cardDataExists(cardData.Header, record))
                    {
                        this.insertCardData(cardData.Header, record);
                        insertCount++;
                    }
                }

                this.InsertLog("M00003", string.Format("CSVファイル変換処理に成功しました。ファイル名：{0}　件数：{1}件", cardData.OldFileName, insertCount), logger);

                System.Threading.Thread.Sleep(10); // 少し時間が立たないと、T_PCARD_LOGのシステム時刻の情報が重複してログが追加できない
            }
            catch (Exception ex)
            {
                if (tr != null)
                {
                    tr.Rollback();
                    tr.Dispose();
                    tr = null;
                }

                //Console.WriteLine("\"{0}\"エラー：{1}", ex.TargetSite, ex.Message);
                logger.ErrorFormat("\"{0}\"エラー：{1}", ex.TargetSite, ex.Message);
                throw ex;
            }
            finally
            {
                if (tr != null)
                {
                    tr.Commit();
                    tr.Dispose();
                    tr = null;
                }
            }
        }


        /// <summary>
        /// T_PCARDにカード情報が存在しているかチェックします。
        /// </summary>
        /// <param name="header">ヘッダレコード</param>
        /// <param name="record">データレコード</param>
        /// <returns>存在していたらtrue、存在していなかったらfalseを返します。</returns>
        private bool cardDataExists(List<string> header, List<string> record)
        {
            bool exist = true;

            try
            {
                this.sqlCommand.CommandText = string.Format("SELECT * FROM dbo.T_PCARD WHERE dbo.T_PCARD.TAMMATSU_CD + dbo.T_PCARD.RIYO_DATE = '{0}'", header[1] + record[0]);

                using (SqlDataReader reader = this.sqlCommand.ExecuteReader())
                {
                    exist = reader.HasRows;
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return exist;
        }


        /// <summary>
        /// カード情報をT_PCARDに追加します。
        /// </summary>
        /// <param name="header">ヘッダレコード</param>
        /// <param name="record">データレコード</param>
        private void insertCardData(List<string> header, List<string> record)
        {
            try
            {
                string systemDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                this.sqlCommand.CommandText = string.Format("INSERT INTO dbo.T_PCARD VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', '{30}', '{31}', '{32}', '{33}', '{34}', '{35}', '{36}', '{37}', '{38}', '{39}', '{40}', '{41}', '{42}', '1', NULL, '1', 'SystemManager', '{43}', 'SystemManager', '{44}')",
                                                            header[0], header[1], header[2], header[3],
                                                            record[0], record[1], record[2], record[3], record[4], record[5], record[6], record[7], record[8], record[9], record[10], record[11], record[12], record[13], record[14], record[15], record[16], record[17], record[18], record[19], record[20], record[21], record[22], record[23], record[24], record[25], record[26], record[27], record[28], record[29], record[30], record[31], record[32], record[33], record[34], record[35], record[36], record[37], record[38],
                                                            "DT" + systemDate,
                                                            "DT" + systemDate);

                this.sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
