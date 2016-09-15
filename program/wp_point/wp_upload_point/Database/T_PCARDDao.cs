using _wp_upload_point.Rest.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace _wp_upload_point.Database
{
    class T_PCARDDao
    {
        public List<T_PCARD> GetT_PCARDs()
        {
            DataGeneral.ParamBuilder paramBuilder = new DataGeneral.ParamBuilder();

            string sql = @"
SELECT
 TAMMATSU_CD,
 RIYO_DATE,
 SUBSTRING(KAIIN_NO,1,10) AS card_no,
 '20' + SUBSTRING(RIYO_DATE,1,2) + '-' + SUBSTRING(RIYO_DATE,3,2) + '-' + SUBSTRING(RIYO_DATE,5,2)
+ ' ' + SUBSTRING(RIYO_DATE,7,2) + ':'+ SUBSTRING(RIYO_DATE,9,2) + ':'+ SUBSTRING(RIYO_DATE,11,2) AS use_datetime,
 SUBSTRING(TAMMATSU_CD,5,4) AS use_shop_no,
 NAMAE_DATA AS user_name,
 URIAGE_GAKU AS sales,
 URIAGE_POINT AS bonus_point,
 SHIN_POINT AS get_point,
 KOKAN_POINT AS used_point,
 RUIK_POINT AS card_point,
 NYUKIN_GAKU AS charge_prepaid,
 ZANGAKU AS card_prepaid,
 M_MEISHO_B.DATA1 AS preca_process_type
FROM T_PCARD
LEFT JOIN M_MEISHO_B ON 
 M_MEISHO_B.MEISHO_KBN = 'M009'
 AND PRECA_FLG = M_MEISHO_B.MEISHO_CD
WHERE UPLOAD_FLG='1'
ORDER BY RIYO_DATE
";
            List<T_PCARD> memberList = new List<T_PCARD>();
            using (SqlDataReader sqlDataReader = DataGeneral.ExecuteReader(sql, paramBuilder.Parameters, false))
            {
                while (sqlDataReader.Read())
                {
                    T_PCARD member = new T_PCARD(
                        sqlDataReader["TAMMATSU_CD"] as string,
                        sqlDataReader["RIYO_DATE"] as string,

                        sqlDataReader["card_no"] as string,//ポイントカード番号
                        sqlDataReader["use_datetime"] as string,//利用日時
                        sqlDataReader["use_shop_no"] as string,//利用店舗番号
                        sqlDataReader["user_name"] as string,//カード氏名
                        Convert.ToInt32(sqlDataReader["sales"]),//売上額
                        Convert.ToInt32(sqlDataReader["bonus_point"]),//売上ポイント
                        Convert.ToInt32(sqlDataReader["get_point"]),//新規ポイント
                        Convert.ToInt32(sqlDataReader["used_point"]),//交換ポイント
                        Convert.ToInt32(sqlDataReader["card_point"]),//累計ポイント
                        Convert.ToInt32(sqlDataReader["charge_prepaid"]),//入金額
                        Convert.ToInt32(sqlDataReader["card_prepaid"]),//プリカ残高
                        sqlDataReader["preca_process_type"] as string//プリカ処理区分
                    );
                    memberList.Add(member);
                }
            }
            return memberList;
        }

        public void SetT_PCARDUploadFlg(T_PCARD t_pcard, int upload_flg, string upload_date)
        {
            DataGeneral.ParamBuilder paramBuilder = new DataGeneral.ParamBuilder();
            paramBuilder.AddParam(SqlDbType.NVarChar, "@tammatsu_cd", t_pcard.tammatsu_cd);
            paramBuilder.AddParam(SqlDbType.NVarChar, "@riyo_date", t_pcard.riyo_date);
            paramBuilder.AddParam(SqlDbType.Int, "@upload_flg", upload_flg);
            paramBuilder.AddParam(SqlDbType.NVarChar, "@upload_date", upload_date);

            string sql = @"
UPDATE T_PCARD
SET
 UPLOAD_FLG=@upload_flg,
 UPLOAD_DATE=@upload_date
WHERE
 TAMMATSU_CD=@tammatsu_cd
AND RIYO_DATE=@riyo_date
";

            int rowsAffected = DataGeneral.ExecuteNonQuery(sql, paramBuilder.Parameters, false);
            if (rowsAffected != 1)
            {//UPLOAD_FLGが更新されない場合は危険が大きいため、例外をthrowし、Fatal Errorで終了させる
                throw new Exception("SetT_PCARDUploadFlg Error. rowsAffected:" + rowsAffected.ToString()
                    + ", TAMMATSU_CD:" + t_pcard.tammatsu_cd
                    + ", RIYO_DATE:" + t_pcard.riyo_date
                    + ", UPLOAD_FLG:" + upload_flg
                    + ", UPLOAD_DATE:" + upload_date
                );
            }
        }

    }
}
