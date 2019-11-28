using AutoMapper;
using Business;
using General;
using General.Service.Filter;
using Main.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SocialCare_BackEnd.Data;
using System;
using System.Net;
using System.Text;

namespace SocialCare_BackEnd
{
    public class Startup
    {


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Identity
            IdentityBuilder builder = services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(ApplicationRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.AddRoleValidator<RoleValidator<ApplicationRole>>();
            builder.AddRoleManager<RoleManager<ApplicationRole>>();
            builder.AddSignInManager<SignInManager<ApplicationUser>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #endregion
            #region Database

            #endregion

            #region Policy
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("PolicyAdmin", policy => policy.RequireRole("Admin"));
            });
            #endregion
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(op =>
            {
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            #region Service
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddCors();
            services.AddScoped<LogActivityFilter>();
            services.AddScoped<ISystemConfigureBusiness, SystemConfigureBusiness>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseCors(x => x.AllowAnyOrigin()
                            .AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
            AddDefaultUser(app.ApplicationServices);
        }
        #region Custom
        private void AddDefaultUser(IServiceProvider serviceProvider)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var hasAdminRole = roleManager.RoleExistsAsync("admin").Result;
                    if (!hasAdminRole)
                    {
                        roleManager.CreateAsync(new ApplicationRole() { Name = "admin" }).Wait();
                    }
                    string userName = Configuration.GetSection("User").GetSection("UserName").Value;
                    var testUser = userManager.FindByNameAsync(userName).Result;
                    if (testUser == null)
                    {
                        ApplicationUser administrator = new ApplicationUser();
                        administrator.Email = Configuration.GetSection("User").GetSection("Email").Value;
                        administrator.UserName = userName;
                        administrator.PhoneNumber = Configuration.GetSection("User").GetSection("PhoneNumber").Value;

                        var newUser = userManager.CreateAsync(administrator, administrator.PhoneNumber).Result;
                        if (newUser.Succeeded)
                        {
                            userManager.AddToRoleAsync(administrator, "admin").Wait();
                        }
                        else
                        {
                            throw new Exception(newUser.Errors.GetMessage());
                        }
                    }

                    var hasUserRole = roleManager.RoleExistsAsync("user").Result;
                    if (!hasAdminRole)
                    {
                        roleManager.CreateAsync(new ApplicationRole() { Name = "user" }).Wait();
                    }
                    userName = "user@gmail.com";
                    testUser = userManager.FindByNameAsync(userName).Result;
                    if (testUser == null)
                    {
                        ApplicationUser user = new ApplicationUser();
                        user.Email = userName;
                        user.UserName = userName;

                        var newUser = userManager.CreateAsync(user, "123456").Result;
                        if (newUser.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "user").Wait();
                        }
                        else
                        {
                            throw new Exception(newUser.Errors.GetMessage());
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
        }
        #endregion
    }
}
