using General;
using General.Extension;
using Main;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonitoringSocialNetworkWeb.Realtime
{
    public class BackgroundSendMail : IHostedService
    {
        private Timer timer = null;
        private ICommentBusiness commentBusiness;
        private UserManager<ApplicationUser> userManager;
        private readonly IServiceScopeFactory scopeFactory;

        public BackgroundSendMail(ICommentBusiness commentBusiness, IServiceScopeFactory scopeFactory)
        {
            this.commentBusiness = commentBusiness;
            this.scopeFactory = scopeFactory;

            StartTimer();
        }
        private void StartTimer()
        {
            timer = new Timer(SendMailNegativeAsync, null, 1000, 60000);
        }

        private void SendMailNegativeAsync(object state)
        {

            try
            {
                var models = commentBusiness.GetCommentNegative();
                if (models != null && models.Count > 0)
                {
                    using (var scope = scopeFactory.CreateScope())
                    {
                        userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                        foreach (var model in models)
                        {
                            var agent = userManager.FindByIdAsync(model.AgentId).Result;
                            if (agent != null)
                            {
                                var rsSend = EmailExtension.Instance.SendEmailAsync(agent.Email, "Comment Invalid", model.Message).Result;
                                if (rsSend)
                                {
                                    model.DateSend = DateTime.Now;
                                }
                                model.Lock = false;
                                commentBusiness.UpdateComment(model);
                            }
                        }
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
