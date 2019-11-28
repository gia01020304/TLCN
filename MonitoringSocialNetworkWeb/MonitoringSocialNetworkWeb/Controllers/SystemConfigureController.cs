using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General;
using Main.Interface;
using Main.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MonitoringSocialNetworkWeb.Controllers
{
    [Authorize]
    public class SystemConfigureController : Controller
    {
        private readonly ISystemConfigureBusiness systemConfigureBusiness;
        public SystemConfigureController(ISystemConfigureBusiness systemConfigureBusiness)
        {
            this.systemConfigureBusiness = systemConfigureBusiness;
        }
        public IActionResult Index()
        {
            var model = systemConfigureBusiness.GetSystemConfigure();
            return View(model);
        }
        public JsonResult SaveConfigure(SystemConfigure model)
        {
            if (ModelState.IsValid)
            {
                if (systemConfigureBusiness.SaveConfigure(model))
                {
                    return Json(new { success = true, msg = MessageHelper.SaveSuccess });
                }
            }
            CoreLogger.Instance.Error(this.CreateMessageLog(ModelState.GetMessage()));
            return Json(new { success = false, msg = MessageHelper.SaveNotSuccess });
        }
    }
}