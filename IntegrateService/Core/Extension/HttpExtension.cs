using Newtonsoft.Json;
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
        /// <summary>
        /// InvokeGet
        /// </summary>
        /// <typeparam name="T">T is result responsed</typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">T is result responsed</typeparam>
        /// <typeparam name="Y">Y is model post into api</typeparam>
        /// <param name="url"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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
        public async Task<T> InvokePut<T, Y>(string url, Y model)
        {
            T modelReturn = default(T);
            try
            {
                var content = JsonConvert.SerializeObject(model);
                var httpResponse = await client.PutAsync(url, new StringContent(content, Encoding.Default, "application/json"));
                if (httpResponse.IsSuccessStatusCode)
                {
                    modelReturn = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
                }
                else
                {
                    CoreLogger.Instance.Debug(this.CreateMessageLog("InvokePut: " + httpResponse.RequestMessage));
                }
            }
            catch (System.Exception ex)
            {
                CoreLogger.Instance.Error(this.CreateMessageLog(ex.Message));
            }
            return modelReturn;
        }
        public async Task<T> InvokeDelete<T>(string url)
        {
            T modelReturn = default(T);
            try
            {
                var httpResponse = await client.DeleteAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    modelReturn = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
                }
                else
                {
                    CoreLogger.Instance.Debug(this.CreateMessageLog("InvokeDelete: " + httpResponse.RequestMessage));
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
