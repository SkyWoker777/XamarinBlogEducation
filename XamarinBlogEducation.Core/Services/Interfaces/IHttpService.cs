using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace XamarinBlogEducation.Core.Services.Interfaces
{
    public interface IHttpService
    {
        Task<string> ProcessToken(HttpResponseMessage responseMessage);
        Task<HttpResponseMessage> ExecuteQuery(string url, HttpOperationMode mode);
        Task<HttpResponseMessage> ExecuteQuery(string url, HttpOperationMode mode, HttpContent content);
        Task<T> ProcessJson<T>(HttpResponseMessage response);
    }
}
