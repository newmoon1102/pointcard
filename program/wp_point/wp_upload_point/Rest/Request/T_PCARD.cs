using System.Collections.Generic;

namespace _wp_upload_point.Rest.Request
{
    class T_PCARD
    {
        public string tammatsu_cd;
        public string riyo_date;

        public string card_no;//ポイントカード番号
        public string use_datetime;//利用日時
        public string use_shop_no;//利用店舗番号
        public string user_name;//カード氏名
        public int sales;//売上額
        public int bonus_point;//売上ポイント
        public int get_point;//新規ポイント
        public int used_point;//交換ポイント
        public int card_point;//累計ポイント
        public int charge_prepaid;//入金額
        public int card_prepaid;//プリカ残高
        public string preca_process_type;//プリカ処理区分

        public T_PCARD(
            string tammatsu_cd,
            string riyo_date,

            string card_no,
            string use_datetime,
            string use_shop_no,
            string user_name,
            int sales,
            int bonus_point,
            int get_point,
            int used_point,
            int card_point,
            int charge_prepaid,
            int card_prepaid,
            string preca_process_type)
        {
            this.tammatsu_cd = tammatsu_cd;
            this.riyo_date = riyo_date;

            this.card_no = card_no;
            this.use_datetime = use_datetime;
            this.use_shop_no = use_shop_no;
            this.user_name = user_name;
            this.sales = sales;
            this.bonus_point = bonus_point;
            this.get_point = get_point;
            this.used_point = used_point;
            this.card_point = card_point;
            this.charge_prepaid = charge_prepaid;
            this.card_prepaid = card_prepaid;
            this.preca_process_type = preca_process_type;
        }
    }
}
