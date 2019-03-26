
using MvvmCross.Base;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;

namespace XamarinBlogEducation.Core.Services
{
    public class HttpService : IHttpService
    {
       
        private const string baseUrl = @"http://195.26.92.83:6776/api";
        private readonly IMvxJsonConverter _jsonConverter;
        public HttpService(IMvxJsonConverter jsonConverter)
        {
            _jsonConverter = jsonConverter;
        }
        public async Task<HttpResponseMessage> ExecuteQuery(string url, HttpOperationMode mode)
        {
            var stringContent = new StringContent(string.Empty);
            return await this.ExecuteQuery(url, mode, stringContent);
        }

        public async Task<HttpResponseMessage> ExecuteQuery(string url, HttpOperationMode mode, HttpContent content)
        {
            var newUrl = $"{baseUrl}{url}";
            var taskResult = new HttpResponseMessage();
            switch (mode)
            {
                case HttpOperationMode.GET:
                    taskResult = await this.GetAsync(newUrl);
                    break;

                case HttpOperationMode.POST:
                    taskResult = await this.PostAsync(newUrl, content);
                    break;
            }

            return taskResult;
        }

        private async Task<HttpResponseMessage> GetAsync(string url)
        {

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url).ConfigureAwait(false);
                return result;
            }

           
        }

        private async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {

            using (var client = new HttpClient())
            {
                try
                {
                   
                    var result =  await client.PostAsync(url, content).ConfigureAwait(false);
                    return result;
                }
                catch(Exception ex)
                {
                    return new HttpResponseMessage();

                }
            }
        }

        public async Task<T> ProcessJson<T>(HttpResponseMessage response)
        {
            
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var deserializedData = JsonConvert.DeserializeObject<T>(json);         
            return deserializedData;
        }
        public async Task<string> ProcessToken(HttpResponseMessage responseMessage)
        {
            var jwtInput = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            
            return jwtInput;
        }
       
    }
}
