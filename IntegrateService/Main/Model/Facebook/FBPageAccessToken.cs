using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Main.Model.Facebook
{
    public class FBPageAccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string state { get; set; }
        public FanpageConfigFilter FanpageConfigFilter { get; set; }
        public void ConvertFanpageConfigFilter()
        {
            if (!string.IsNullOrEmpty(state))
            {
                state = WebUtility.UrlDecode(state);
                FanpageConfigFilter = JsonConvert.DeserializeObject<FanpageConfigFilter>(state);
            }
        }
    }
}
