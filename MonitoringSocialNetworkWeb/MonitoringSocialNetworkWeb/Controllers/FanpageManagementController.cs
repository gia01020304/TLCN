using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main;
using Main.Model;
using Microsoft.AspNetCore.Mvc;

namespace MonitoringSocialNetworkWeb.Controllers
{
    public class FanpageManagementController : Controller
    {

        private readonly ICommentBusiness commentBusiness;

        public FanpageManagementController(ICommentBusiness commentBusiness)
        {
            this.commentBusiness = commentBusiness;

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetPostInfo(PostInfoFilter filter)
        {
            var list = commentBusiness.GetPostInfo(filter);
            return Json(new { data = list });
        }
    }
}