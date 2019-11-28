using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model.Facebook
{
    public class FBSubscribedInfo
    {
        public string category { get; set; }
        public string link { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public List<string> subscribed_fields { get; set; }
    }
    public class FBSubscribedInfos
    {
        public List<FBSubscribedInfo> data { get; set; }
    }

}
