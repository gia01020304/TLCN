﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class HttpExtension : IHttpClient
    {
        private static HttpExtension instance;
        private static object mlock = new object();
        private readonly HttpClient client;
        public static HttpExtension Instance
        {
            get
            {
                lock (mlock)
                {
                    if (instance == null)
                        instance = new HttpExtension();

                    return instance;
                }
            }
        }
        private HttpExtension()
        {
            client = new HttpClient();
        }
        public async Task<T> InvokeGet<T>(string url)
        {
            T model = default(T);
            try
            {
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    model = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
                }
                else
                {
                    CoreLogger.Instance.Debug(this.CreateMessageLog("InvokeGet: " + httpResponse.RequestMessage));
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return model;
        }
        public async Task<T> InvokePost<T, Y>(string url, Y model)
        {
            T modelReturn = default(T);
            try
            {
                var content = JsonConvert.SerializeObject(model);
                var httpResponse = await client.PostAsync(url, new StringContent(content, Encoding.Default, "application/json"));
                if (httpResponse.IsSuccessStatusCode)
                {
                    modelReturn = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
                }
                else
                {
                    CoreLogger.Instance.Debug(this.CreateMessageLog("InvokePost: " + httpResponse.RequestMessage));
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return modelReturn;
        }

    }
}
