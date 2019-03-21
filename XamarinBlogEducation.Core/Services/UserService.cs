
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Core.Services.Interfaces
{
    public class UserService : IUserService
    {
        private readonly IHttpService _httpService;
        public UserService(IHttpService httpService)
        {
            _httpService = httpService;

        }
        public async Task AddUserAsync(RegisterAccountViewModel model)
        {
            var url = "/Account/register";
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result= await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
            
        }

        public async Task GetUserAsync(LoginAccountViewModel model)
        {
            var url = "/Account/login";
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);           
        }

        public Task UpdateUserAsync()
        {
            throw new NotImplementedException();
        }
        
        public async Task UploadImageAsync(Stream image,RegisterAccountViewModel model)
        {
            var url = "/Common/addImage";
            HttpContent fileStreamContent = new StreamContent(image);
            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = "NULL"};
            //fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);
                var response = await client.PostAsync(url, formData);
               
            }
        }
        public async Task AutologinUserAsync(RegisterAccountViewModel model)
        {
            var loginModel = new LoginAccountViewModel();
            loginModel.Email = model.Email;
            loginModel.Password = model.Password;
            await GetUserAsync(loginModel);

        }

    }
}
