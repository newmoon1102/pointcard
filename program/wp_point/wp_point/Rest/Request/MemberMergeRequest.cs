using System.Collections.Generic;
using System.Net.Http;

namespace _wp_point.Rest.Request
{
    class MemberMergeRequest
    {
        public string shop_auth_code { get; set; }
        public string old_member_id { get; set; }
        public string new_member_id { get; set; }
        public string force_flag { get; set; }
        public string om_delete_flag { get; set; }

        public HttpContent GetHttpContentsData()
        {
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string,string>("shop_auth_code",shop_auth_code),
                new KeyValuePair<string,string>("old_member_id",old_member_id),
                new KeyValuePair<string,string>("new_member_id",new_member_id),
                new KeyValuePair<string,string>("force_flag",force_flag),
                new KeyValuePair<string,string>("om_delete_flag",om_delete_flag)
            });

            return content;
        }
    }
}
