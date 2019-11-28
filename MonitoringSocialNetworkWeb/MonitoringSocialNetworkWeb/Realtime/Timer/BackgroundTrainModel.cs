using CommentAnalysis;
using General;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringSocialNetworkWeb.Realtime
{
    public class BackgroundTrainModel : IHostedService
    {
        private Timer timer = null;
        private bool trainRunning = false;
        public BackgroundTrainModel()
        {
            timer = new Timer(TrainNewModel, null, 1000, 3000);
        }

        private void TrainNewModel(object state)
        {
            try
            {
                string isTrain = SqlDAL.Instance.GetSetting("IsTrain").Value;
                bool bCheck;
                if (bool.TryParse(isTrain, out bCheck))
                {
                    if (bCheck && !trainRunning)
                    {
                        trainRunning = true;
                        MLSimilarCommentAnalysis.Instance.TrainDataSet();
                        SqlDAL.Instance.SaveSettingModel(new SettingModel()
                        {
                            Key = "IsTrain",
                            Value = "false"
                        });
                        trainRunning = false;
                    } 
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(ex.Message);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            return Task.CompletedTask;
        }
    }
}
