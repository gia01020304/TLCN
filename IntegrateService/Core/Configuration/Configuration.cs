using Microsoft.Extensions.Configuration;

namespace General
{
    public class Configuration
    {
        private static IConfigurationRoot configurationRoot;
        public static IConfigurationRoot ConfigurationRoot
        {
            get
            {
                if (configurationRoot == null)
                {
                    var builder = new ConfigurationBuilder();
                    builder.AddJsonFile("appsettings.json", optional: false);
                    configurationRoot = builder.Build();

                }
                return configurationRoot;
            }
        }
        public static string ConnectString
        {
            get
            {
                return ConfigurationRoot.GetConnectionString("DefaultConnection");
            }
        }
    }
}
