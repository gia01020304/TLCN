using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using General;
using log4net.Core;
using Main;
using Main.Model;
using Main.Model.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.SqlServer.Server;
using Microsoft.VisualStudio.Web.CodeGeneration.Templating;
using MonitoringSocialNetworkWeb.Realtime;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Education
{
    [Authorize]
    public class FanPageConfigureController : Controller
    {
        private readonly IFacebookBusiness facebookBusiness;
        private readonly IHubContext<CallBackPageAccessHub> hubContext;
        private readonly ISocialConfigBusiness socialConfigBusiness;
        private readonly IFanpageConfigBusiness fanpageConfigBusiness;

        public UserManager<ApplicationUser> UserManager { get; }

        public FanPageConfigureController(IFacebookBusiness facebookBusiness, IHubContext<CallBackPageAccessHub> hubContext, ISocialConfigBusiness socialConfigBusiness, IFanpageConfigBusiness fanpageConfigBusiness, UserManager<ApplicationUser> userManager)
        {
            this.facebookBusiness = facebookBusiness;
            this.hubContext = hubContext;
            this.socialConfigBusiness = socialConfigBusiness;
            this.fanpageConfigBusiness = fanpageConfigBusiness;
            UserManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetListFanPageConfigure(FanpageConfigFilter filter)
        {
            filter.Deleted = false;
            var list = fanpageConfigBusiness.GetListFanPageConfigure(filter);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    var subInfo = facebookBusiness.GetSubscribedInfo(item);
                    if (subInfo != null && subInfo.subscribed_fields.Count > 0)
                    {
                        item.IsSubscribe = true;
                    }
                    else
                    {
                        item.IsSubscribe = false;
                    }
                }
            }
            return Json(new { data = list });
        }
        public JsonResult AddFanPageConfigure(FanpageConfig model)
        {
            if (ModelState.IsValid)
            {
                ProcessDataAsync(model).Wait();
                if (fanpageConfigBusiness.AddFanPageConfigure(model))
                {
                    return Json(new { success = true, msg = MessageHelper.SaveSuccess });
                }
            }
            CoreLogger.Instance.Error(this.CreateMessageLog(ModelState.GetMessage()));
            return Json(new { success = false, msg = MessageHelper.SaveNotSuccess });
        }
        public JsonResult EditFanPageConfigure(FanpageConfig model)
        {
            ProcessDataAsync(model).Wait();
            if (ModelState.IsValid)
            {
                if (fanpageConfigBusiness.EditFanPageConfigure(model))
                {
                    return Json(new { success = true, msg = MessageHelper.SaveSuccess });
                }
            }
            CoreLogger.Instance.Error(this.CreateMessageLog(ModelState.GetMessage()));
            return Json(new { success = false, msg = MessageHelper.SaveNotSuccess });
        }
        public JsonResult DelFanPageConfigure(FanpageConfig model)
        {
            if (fanpageConfigBusiness.DelFanPageConfigure(model))
            {
                return Json(new { success = true, msg = MessageHelper.DelSuccess });
            }
            CoreLogger.Instance.Error(this.CreateMessageLog(ModelState.GetMessage()));
            return Json(new { success = false, msg = MessageHelper.DelNotSuccess });
        }
        private async Task ProcessDataAsync(FanpageConfig model)
        {
            try
            {
                var agent = await UserManager.FindByIdAsync(model.AgentId);
                model.AgentName = agent.UserName;
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
        }
        public JsonResult GetUrlGetCode(FanpageConfigFilter model)
        {
            var socialConfig = socialConfigBusiness.GetSocialConfig(new FilterSocialConfig
            {
                Id = model.SocialConfigId
            });
            var url = string.Format("https://www.facebook.com/v5.0/dialog/oauth?response_type=token&display=popup&client_id={0}&redirect_uri={1}/FanPageConfigure/CallBackGetCode&auth_type=reauthenticate&scope=manage_pages,pages_messaging,pages_messaging_subscriptions&state={2}"
                                , socialConfig.AppId
                                , $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}"
                                //, "https://4b67bf72.ngrok.io"
                                , "{connectionId:'" + model.ConnectionId + "',socialConfigId:'" + model.SocialConfigId + "'}");
            return Json(new { success = true, data = url });
            //return Json(new { success = true, data = $"https://www.facebook.com/v5.0/dialog/oauth?response_type=token&display=popup&client_id={socialConfig.AppId}&redirect_uri=https://f14b4b83.ngrok.io/FanPageConfigure/CallBackGetCode&scope=manage_pages&state=" });
            //return Json(new { success = true, data = $"https://www.facebook.com/v5.0/dialog/oauth?response_type=token&display=popup&client_id={socialConfig.AppId}&redirect_uri=https://ff7c660e.ngrok.io/api/facebook/getaccess&scope=email,manage_pages,pages_messaging_phone_number,pages_show_list,pages_messaging,pages_messaging_subscriptions,publish_pages" });
        }
        [AllowAnonymous]
        public IActionResult CallBackGetCode()
        {
            return View();
        }
        [AllowAnonymous]
        public JsonResult ProccessCallBack(FBPageAccessToken model)
        {
            try
            {
                model.ConvertFanpageConfigFilter();
                if (model.FanpageConfigFilter != null)
                {
                    var socialConfig = socialConfigBusiness.GetSocialConfig(new FilterSocialConfig
                    {
                        Id = model.FanpageConfigFilter.SocialConfigId
                    });
                    //increment expired token
                    var pageToken = facebookBusiness.IncrementExpiredToken(socialConfig, model.access_token);
                    if (pageToken != null)
                    {
                        //Get PageID
                        var pageInfo = facebookBusiness.GetPageId(pageToken);
                        if (pageInfo != null)
                        {
                            FanpageConfig modelReturn = new FanpageConfig
                            {
                                PageTitle = pageInfo.name,
                                PageId = pageInfo.id,
                                PageAccessToken = pageInfo.access_token
                            };
                            hubContext.Clients.Client(model.FanpageConfigFilter.ConnectionId).SendAsync("SendMessage", modelReturn);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return Json(null);
        }

        public JsonResult ChangeSubscribed(FanpageConfig model)
        {
            bool rsChange = false;
            string msg = "Change Not Success.";
            string field = "";
            if (model.IsSubscribe)
            {
                field = "feed";
            }
            rsChange = facebookBusiness.ChangeChangeSubscribed(model, field);
            if (rsChange)
            {
                msg = "Change Successfully";
            }
            return Json(new { success = rsChange, msg = msg });
        }
    }
}