using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommentAnalysis
{
    public class Prediction
    {
        [ColumnName("PredictedLabel")]
        public string ReplyComment;
        [ColumnName("Score")]
        public float[] Score;
    }
}
