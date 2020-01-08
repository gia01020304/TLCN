using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using General;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MonitoringSocialNetworkWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CoreLogger.Instance.Debug("Server Start");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                CoreLogger.Instance.Error(ex.Message);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}
