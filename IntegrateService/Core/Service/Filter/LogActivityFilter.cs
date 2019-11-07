using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace General.Service.Filter
{
    public class LogActivityFilter : IAsyncActionFilter
    {
        private readonly UserManager<ApplicationUser> userManager;

        public LogActivityFilter(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContent = await next();
            var user =await userManager.FindByNameAsync(resultContent.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            user.LastActive = DateTime.Now;
            await userManager.UpdateAsync(user);
        }
    }
}
