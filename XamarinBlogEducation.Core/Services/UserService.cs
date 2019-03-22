
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

        public async Task UpdateUserAsync(EditAccountViewModel model)
        {
            var url = "/User/update";
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
        }
        
        public async Task UploadImageAsync(byte[] image,RegisterAccountViewModel model)
        {
            var url = "/Common/addImage";
            ByteArrayContent baContent = new ByteArrayContent(image);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(baContent, "file", "userimg.png");
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
