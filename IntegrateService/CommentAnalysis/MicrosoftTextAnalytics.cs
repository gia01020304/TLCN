using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Main.Model;
using General;

namespace CommentAnalysis
{
    public class MicrosoftTextAnalytics
    {
        private static MicrosoftTextAnalytics instance;
        private TextAnalyticsClient client;
        private static object mlock = new object();
        public static MicrosoftTextAnalytics Instance
        {
            get
            {
                lock (mlock)
                {
                    if (instance == null)
                        instance = new MicrosoftTextAnalytics();

                    return instance;
                }
            }
        }
        public MicrosoftTextAnalytics()
        {
            try
            {
                var credentials = new ApiKeyServiceClientCredentials(SqlDAL.Instance.GetSetting("MSKey").Value);
                client = new TextAnalyticsClient(credentials)
                {
                    Endpoint = SqlDAL.Instance.GetSetting("MSEndPoint").Value
                };
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
        }
        public async Task<ResultAnalysis> TextAnalysisAsync(string text)
        {
            ResultAnalysis model = null;
            try
            {
                model = new ResultAnalysis();
                var level = await client.SentimentAsync(text);
                var key = await client.KeyPhrasesAsync(text);
                model.Score = (int)(level.Score * 10);
                model.Key = key.KeyPhrases;
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }
    }
}
