using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model.Facebook
{
    public class FBPageInfo
    {

        public string name { get; set; }
        public string access_token { get; set; }
        public string id { get; set; }
    }
    public class ListFBPageInfo
    {
        public List<FBPageInfo> data { get; set; }
    }
}
