using General;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model
{
    public class Comment
    {
        public int? Id { get; set; }
        public string PageId { get; set; }
        public string PostId { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
        public string ParentId { get; set; }
        public string Link { get; set; }
        public string FromId { get; set; }
        public string CommentId { get; set; }
        public int? Score { get; set; }
        public bool IsNegative { get; set; }
        public bool IsTrain { get; set; }
        public string AgentId { get; set; }
        public bool Lock { get; set; }
        public DateTime? DateSend { get; set; }
        public DateTime? DateReceived { get; set; }
    }

    public class CommentFilter
    {
        public string CommentId { get; set; }
    }
}
