using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model
{
    public class ReplyComment
    {
        public string PageId { get; set; }
        public SocialConfig SocialConfig { get; set; }
        public string Comment { get; set; }
        public string CommentId { get; set; }
    }
}
