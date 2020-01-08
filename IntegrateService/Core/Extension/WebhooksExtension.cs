using System;
using System.Collections.Generic;
using System.Text;

namespace General.Extension
{
    class WebhooksExtension
    {
        private static WebhooksExtension instance;
        private static object mlock = new object();

        public static WebhooksExtension Instance
        {
            get
            {
                lock (mlock)
                {
                    if (instance == null)
                        instance = new WebhooksExtension();

                    return instance;
                }
            }
        }
        public string GetAuthenticationAndAuthorizationConfig()
        {
            return "";
        }
    }
}
