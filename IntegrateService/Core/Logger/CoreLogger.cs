using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace General
{
    public static class CoreLogger
    {
        private static readonly string LOG_CONFIG_FILE = @"log4net.config";

        private static log4net.ILog mInstance;
        public static ILog Instance
        {
            get
            {

                if (mInstance == null)
                {
                    SetLog4NetConfiguration();
                    mInstance = LogManager.GetLogger(typeof(Logger));
                }
                return mInstance;
            }
        }

        private static void SetLog4NetConfiguration()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(LOG_CONFIG_FILE));

            var repo = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }
    }

}
