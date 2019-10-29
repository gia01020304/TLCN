using General;
using Main;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetworkMonitor
{
    public class FacebookService : BackgroundService
    {
        private readonly IFacebookBusiness business;
        public FacebookService(IFacebookBusiness business)
        {
            this.business = business;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var models = business.GetFacebookApplication();
            foreach (var model in models)
            {
                business.GetAccessToken(model.AppDataConfig as FacebookApp);
                
            }
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    CoreLogger.Instance.Info("FB:" + DateTime.Now);
            //    await Task.Delay(5000, stoppingToken);
            //}
        }
    }
}
