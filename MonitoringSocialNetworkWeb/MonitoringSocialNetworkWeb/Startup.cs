using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoringSocialNetworkWeb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using General;
using Microsoft.AspNetCore.Authorization;
using Main;
using Business;
using MonitoringSocialNetworkWeb.Realtime;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Main.Interface;

namespace MonitoringSocialNetworkWeb
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultUI(UIFramework.Bootstrap4)
               .AddDefaultTokenProviders();
            services.Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });

            #region Service
            services.AddTransient<ISocialConfigBusiness, SocialConfigBusiness>();
            services.AddTransient<ICommentBusiness, CommentBusiness>();
            services.AddTransient<IFanpageConfigBusiness, FanpageConfigBusiness>();
            services.AddTransient<IFacebookBusiness, FacebookBusiness>();
            services.AddTransient<ISystemConfigureBusiness, SystemConfigureBusiness>();
            services.AddTransient<IDatasetBusiness, DatasetBusiness>();

            services.AddScoped<UserManager<ApplicationUser>>();



            services.AddHostedService<BackgroundSendMail>();
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
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
                    var hasAdminRole = roleManager.RoleExistsAsync("Admin").Result;
                    if (!hasAdminRole)
                    {
                        roleManager.CreateAsync(new ApplicationRole() { Name = "Admin" }).Wait();
                    }
                    string email = Configuration.GetSection("User").GetSection("Email").Value;
                    var testUser = userManager.FindByEmailAsync(email).Result;
                    if (testUser == null)
                    {
                        ApplicationUser administrator = new ApplicationUser();
                        administrator.Email = email;
                        administrator.UserName = email;
                        administrator.PhoneNumber = Configuration.GetSection("User").GetSection("PhoneNumber").Value;

                        var newUser = userManager.CreateAsync(administrator, administrator.PhoneNumber).Result;
                        if (newUser.Succeeded)
                        {
                            userManager.AddToRoleAsync(administrator, "Admin").Wait();
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
