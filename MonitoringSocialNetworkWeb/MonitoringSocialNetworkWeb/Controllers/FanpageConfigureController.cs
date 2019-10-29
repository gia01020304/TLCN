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
    public class FanPageConfigureController : Controller
    {
        private readonly IFanpageConfigBusiness fanpageConfigBusiness;
        public FanPageConfigureController(IFanpageConfigBusiness fanpageConfigBusiness)
        {
            this.fanpageConfigBusiness = fanpageConfigBusiness;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetListFanPageConfigure()
        {
            var list = fanpageConfigBusiness.GetListFanPageConfigure(new FanpageConfigFilter() { Deleted = false });
            return Json(new { data = list });
        }
        //public JsonResult AddFanPageConfigure(SocialConfig model)
        //{

        //}
        //public JsonResult EditFanPageConfigure(SocialConfig model)
        //{

        //}
        //public JsonResult DelFanPageConfigure(SocialConfig model)
        //{

        //}

    }
}