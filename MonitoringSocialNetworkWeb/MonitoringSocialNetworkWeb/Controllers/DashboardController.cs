using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MonitoringSocialNetworkWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ISystemConfigureBusiness systemConfigureBusiness;

        public DashboardController(ISystemConfigureBusiness systemConfigureBusiness)
        {
            this.systemConfigureBusiness = systemConfigureBusiness;
        }
        public IActionResult Index()
        {
            var model = systemConfigureBusiness.GetSystemConfigure();
            return View(model);
        }
    }
}