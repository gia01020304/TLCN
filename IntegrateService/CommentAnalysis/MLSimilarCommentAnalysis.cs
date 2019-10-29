using General;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommentAnalysis
{
    public class MLSimilarCommentAnalysis
    {
        private static MLSimilarCommentAnalysis instance;
        private static object mlock = new object();
        public static MLSimilarCommentAnalysis Instance
        {
            get
            {
                lock (mlock)
                {
                    if (instance == null)
                        instance = new MLSimilarCommentAnalysis();

                    return instance;
                }
            }
        }
        public string GetReplyComment(string message)
        {
            string reply = string.Empty;
            try
            {

            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return reply;
        }
        public void AddNewData(string comment, string replyComment)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
        }
        public void TrainDataSet()
        {
            try
            {

            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
        }
    }
}
