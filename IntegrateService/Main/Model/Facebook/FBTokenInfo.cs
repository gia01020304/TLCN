using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model.Facebook
{
    public class FBTokenInfo
    {
        public Data data { get; set; }
    }
    public class GranularScope
    {
        public string scope { get; set; }
    }
    public class Data
    {
        public string app_id { get; set; }
        public string type { get; set; }
        public string application { get; set; }
        public int data_access_expires_at { get; set; }
        public int expires_at { get; set; }
        public bool is_valid { get; set; }
        public string profile_id { get; set; }
        public List<string> scopes { get; set; }
        public List<GranularScope> granular_scopes { get; set; }
        public string user_id { get; set; }
    }
}
