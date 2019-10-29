using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    interface IHttpClient
    {
        Task<T> InvokeGet<T>(string url);
        Task<T> InvokePost<T, Y>(string url, Y model);
    }
}
