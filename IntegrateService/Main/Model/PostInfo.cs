using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Model
{
    public class PostInfo
    {
        public string PostId { get; set; }
        public float AvgScore { get; set; }
        public float NumberNegative { get; set; }
        public float NumberComment { get; set; }
    }
    public class PostInfoFilter
    {
        public string PostId { get; set; }
        public string PageId { get; set; }
    }
}
