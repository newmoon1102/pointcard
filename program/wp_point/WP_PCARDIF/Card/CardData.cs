using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace WP_PCARDIF.Card
{
    /// <summary>
    /// カード情報を表します。
    /// </summary>
    class CardData
    {
        public List<string> Header { get; set; }
        public List<List<string>> Records { get; set; }
        public string OldFileName { get; set; }


        /// <summary>
        /// 指定されたCSVファイルを読み込み、CardDataクラスの新しいインスタンスを初期化します。
        /// これによって、CSVファイル内の全カード情報がこのインスタンスに取り込まれます。
        /// </summary>
        /// <param name="filePath">CSVファイルのファイルパス</param>
        /// <param name="oldFileName">リネーム前のファイル名</param>
        public CardData(string filePath, string oldFileName)
        {
            this.Header = new List<string>();
            this.Records = new List<List<string>>();
            this.OldFileName = oldFileName;
            
            
            TextFieldParser parser = new TextFieldParser(filePath, System.Text.Encoding.GetEncoding("Shift_JIS"));
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            while (!parser.EndOfData)
            {
                string[] row = parser.ReadFields();

                if (row.Length == 4)
                {
                    foreach (string data in row)
                    {
                        Header.Add(data);
                    }
                }
                else
                {
                    List<string> record = new List<string>();
                    foreach (string data in row)
                    {
                        record.Add(data);
                    }
                    Records.Add(record);
                }
            }

            //2016.6.3 ファイル移動のため、予め明示的にClose()する
            parser.Close();
        }

    }
}
