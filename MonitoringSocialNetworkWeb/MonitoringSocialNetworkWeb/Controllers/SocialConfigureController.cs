using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using General;
using log4net.Core;
using Main;
using Main.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Education
{
    [Authorize]
    public class SocialConfigureController : Controller
    {
        private readonly ISocialConfigBusiness socialConfigBusiness;
        public SocialConfigureController(ISocialConfigBusiness socialConfigBusiness)
        {
            this.socialConfigBusiness = socialConfigBusiness;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetListSocialConfigure()
        {
            var list = socialConfigBusiness.GetListSocialConfig(new FilterSocialConfig() { Deleted = false });
            return Json(new { data = list });
        }
        public JsonResult AddSocialConfigure(SocialConfig model)
        {
            if (ModelState.IsValid)
            {
                if (socialConfigBusiness.AddSocialConfigure(model))
                {
                    return Json(new { success = true, msg = MessageHelper.SaveSuccess });
                }
            }
            CoreLogger.Instance.Error(this.CreateMessageLog(ModelState.GetMessage()));
            return Json(new { success = false, msg = MessageHelper.SaveNotSuccess });
        }
        public JsonResult EditSocialConfigure(SocialConfig model)
        {
            if (ModelState.IsValid)
            {
                if (socialConfigBusiness.EditSocialConfigure(model))
                {
                    return Json(new { success = true, msg = MessageHelper.SaveSuccess });
                }
            }
            CoreLogger.Instance.Error(this.CreateMessageLog(ModelState.GetMessage()));
            return Json(new { success = false, msg = MessageHelper.SaveNotSuccess });
        }
        public JsonResult DelSocialConfigure(SocialConfig model)
        {
            if (model != null)
            {
                if (socialConfigBusiness.DelSocialConfigure(model))
                {
                    return Json(new { success = true, msg = MessageHelper.DelSuccess });
                }
            }
            return Json(new { success = false, msg = MessageHelper.DelNotSuccess });
        }

    }
}