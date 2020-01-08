using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model
{
    public class ReplyMessageModel
    {
       
        public string ContactMessageUID { get; set; }

       
        public string Content { get; set; }

       
        public string ContactID { get; set; }

       
        public string AgentID { get; set; }

       
        public string AgentName { get; set; }

       
        public string ReplyServiceUrl { get; set; }

       
        public string PageAccessToken { get; set; }
       
        public string AdditionalInfo { get; set; }
    }
}
