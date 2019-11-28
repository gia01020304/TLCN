using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using General;
using General.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SocialCare_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        //[ServiceFilter(typeof(LogActivityFilter))]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(InputModel model)
        {
            var userLogin = await userManager.FindByNameAsync(model.Email);
            var result = await signInManager.CheckPasswordSignInAsync(userLogin, model.Password, false);
            if (result.Succeeded)
            {
                return Ok(new { token = CoreExtension.GenerateJwtTokenAsync(userLogin, userManager, configuration.GetSection("AppSettings:Token").Value).Result });
            }
            return Unauthorized();
        }
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync().Wait();
            HttpContext.Session.Clear();
            return Ok();
        }
    }
}