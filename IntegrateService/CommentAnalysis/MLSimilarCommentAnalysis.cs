
using General;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CommentAnalysis
{
    public class MLSimilarCommentAnalysis
    {
        private static MLSimilarCommentAnalysis instance;
        private static object mlock = new object();
        private MLContext mlContext;
        private IDataView trainingDataView;
        private ITransformer trainedModel;
        private PredictionEngine<Data, Prediction> predEngine;



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
                if (predEngine != null)
                {
                    Data replyComment = new Data
                    {
                        Comment = message
                    };
                    var prediction = predEngine.Predict(replyComment);
                    if (predEngine != null)
                    {
                        reply = prediction.ReplyComment;
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return reply;
        }
        public void TrainDataSet()
        {
            try
            {
                mlContext = new MLContext(seed: 0);
                trainingDataView = LoadData();
                var pipeline = ProcessData();
                var trainingPipeline = BuildAndTrainModel(trainingDataView, pipeline);
                Evaluate(trainingDataView.Schema);
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
        }
        private IEstimator<ITransformer> ProcessData()
        {
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "ReplyComment", outputColumnName: "Label")
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Comment", outputColumnName: "CommentFeaturized"))
                .Append(mlContext.Transforms.Concatenate("Features", "CommentFeaturized"))
                .AppendCacheCheckpoint(mlContext);
            return pipeline;

        }
        private IDataView LoadData()
        {
            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<Dataset>();
            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance, Configuration.ConnectString, @"SELECT Comment,ReplyComment FROM  dbo.Dataset");
            //DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance, "Data Source=(local);Initial Catalog=MonitoringSocialNetwork;Integrated Security=True", @"SELECT Comment,ReplyComment FROM  dbo.Dataset");
            return loader.Load(dbSource);
        }
        private void Evaluate(DataViewSchema trainingDataViewSchema)
        {
            var testDataView = LoadData();
            var testMetrics = mlContext.MulticlassClassification.Evaluate(trainedModel.Transform(testDataView));
            CoreLogger.Instance.Debug($"*       MicroAccuracy:    {testMetrics.MicroAccuracy:0.###}");
            CoreLogger.Instance.Debug($"*       MacroAccuracy:    {testMetrics.MacroAccuracy:0.###}");
            CoreLogger.Instance.Debug($"*       LogLoss:          {testMetrics.LogLoss:#.###}");
            CoreLogger.Instance.Debug($"*       LogLossReduction: {testMetrics.LogLossReduction:#.###}");
        }
        private IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
        {
            IEstimator<ITransformer> trainingPipeline = null;
            try
            {
                trainingPipeline = pipeline.Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
              .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
                trainedModel = trainingPipeline.Fit(trainingDataView);
                predEngine = mlContext.Model.CreatePredictionEngine<Data, Prediction>(trainedModel);
                //Data dt = new Data()
                //{
                //    Question = "Hello",
                //};
                //var prediction = _predEngine.Predict(dt);
                //Console.WriteLine($"=============== Single Prediction just-trained-model - Result: {prediction.Answer} ===============");
            }
            catch (Exception ex)
            {

                throw;
            }
            return trainingPipeline;
        }
    }
}
