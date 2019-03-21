using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamarinBlogEducation.Core.Services.Interfaces
{
    public interface IHttpService
    {
        
        Task<HttpResponseMessage> ExecuteQuery(string url, HttpOperationMode mode);
        Task<HttpResponseMessage> ExecuteQuery(string url, HttpOperationMode mode, HttpContent content);
        Task<T> ProcessJson<T>(HttpResponseMessage response);
    }
}
