using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommentAnalysis
{
    public class Data
    {
        [LoadColumn(0)]
        public string Comment { get; set; }

        [LoadColumn(1)]
        public string ReplyComment { get; set; }
    }
}
