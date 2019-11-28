using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General;
using General.Model.User;
using Main;
using Main.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MonitoringSocialNetworkWeb.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFanpageConfigBusiness fanpageConfigBusiness;

        public UserController(UserManager<ApplicationUser> userManager, IFanpageConfigBusiness fanpageConfigBusiness)
        {
            this.userManager = userManager;
            this.fanpageConfigBusiness = fanpageConfigBusiness;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetUser()
        {
            var user = userManager.Users.Where(x => x.IsActive).ToList();
            return Json(new { data = user });
        }
        public JsonResult AddUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new ApplicationUser();
                newUser.Email = model.Email;
                newUser.UserName = model.UserName;
                newUser.PhoneNumber = model.PhoneNumber;
                var rsCreate = userManager.CreateAsync(newUser, model.Password).Result;
                if (rsCreate.Succeeded)
                {
                    var rsAddRole = userManager.AddToRoleAsync(newUser, "Agent").Result;
                    if (rsAddRole.Succeeded)
                    {
                        return Json(new { success = true, msg = MessageHelper.SaveUserSuccess });
                    }
                    else
                    {
                        return Json(new { success = false, msg = rsAddRole.Errors.Select(x => x.Description).ToList() });
                    }
                }
                return Json(new { success = false, msg = rsCreate.Errors.Select(x => x.Description).ToList() });
            }
            CoreLogger.Instance.Error(this.CreateMessageLog(ModelState.GetMessage()));
            return Json(new { success = false, msg = MessageHelper.SaveNotSuccess });
        }
        public JsonResult DelUser(RegisterModel model)
        {
            if (User.GetUserId() == model.Id)
            {
                return Json(new { success = false, msg = MessageHelper.CanNotDeleteNotification });
            }
            ApplicationUser user = userManager.FindByIdAsync(model.Id).Result;
            if (user != null)
            {
                user.IsActive = false;
                var rsUpdateUser = userManager.UpdateAsync(user).Result;
                if (rsUpdateUser.Succeeded)
                {
                    fanpageConfigBusiness.DelFanpageOfAgent(new FanpageConfigFilter
                    {
                        AgentId = user.Id
                    });
                    return Json(new { success = true, msg = MessageHelper.UpdateUserSuccess });
                }
                return Json(new { success = false, msg = rsUpdateUser.Errors.Select(x => x.Description).ToList() });
            }
            return Json(new { success = false, msg = MessageHelper.CanNotDeleteNotification });
        }
        public JsonResult EditUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = userManager.FindByIdAsync(model.Id).Result;
                if (user == null)
                {
                    return Json(new { success = false, msg = MessageHelper.SaveNotSuccess });
                }

                user.Email = model.Email;
                user.UserName = model.UserName;
                user.PhoneNumber = model.PhoneNumber;
                var token = userManager.GeneratePasswordResetTokenAsync(user).Result;
                var rsChangePassword = userManager.ResetPasswordAsync(user, token, model.Password).Result;
                var rsUpdateUser = userManager.UpdateAsync(user).Result;
                if (rsUpdateUser.Succeeded)
                {
                    return Json(new { success = true, msg = MessageHelper.UpdateUserSuccess });
                }
                return Json(new { success = false, msg = rsUpdateUser.Errors.Select(x => x.Description).ToList() });
            }
            CoreLogger.Instance.Error(this.CreateMessageLog(ModelState.GetMessage()));
            return Json(new { success = false, msg = MessageHelper.SaveNotSuccess });
        }
    }
}