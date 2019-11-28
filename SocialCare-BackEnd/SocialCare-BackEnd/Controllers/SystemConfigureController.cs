using General;
using Main.Interface;
using Main.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace SocialCare_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemConfigureController : ControllerBase
    {
        private readonly ISystemConfigureBusiness systemConfigureBusiness;
        private readonly IConfiguration configuration;

        public SystemConfigureController(ISystemConfigureBusiness systemConfigureBusiness, IConfiguration configuration)
        {
            this.systemConfigureBusiness = systemConfigureBusiness;
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetConfigure()
        {
            var model = systemConfigureBusiness.GetSystemConfigure();
            return Ok(model);
        }
        [HttpPost("save")]
        public IActionResult SaveConfigure(SystemConfigure model)
        {
            try
            {
                if (systemConfigureBusiness.SaveConfigure(model))
                {
                    return Ok(MessageHelper.SaveSuccess);
                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(ex.Message);
            }
            return BadRequest(MessageHelper.SaveNotSuccess);
        }
    }
}
