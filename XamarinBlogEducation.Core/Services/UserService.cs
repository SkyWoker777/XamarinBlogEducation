
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var response = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
            var testToken = await _httpService.ProcessToken(response);
            try { 
            CrossSecureStorage.Current.SetValue("securityToken",testToken);
           }
            catch(Exception ex)
            {
                var exc = ex.Message;
            }
            
        }

        public async Task UpdateUserAsync(EditAccountViewModel model)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    
                    var json = JsonConvert.SerializeObject(model);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var message = new HttpRequestMessage(HttpMethod.Post, "http://195.26.92.83:6776/api/User/update");
                    message.Content = httpContent;
                    message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CrossSecureStorage.Current.GetValue("securityToken"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CrossSecureStorage.Current.GetValue("securityToken"));
                    var response = await client.SendAsync(message);

                    if (response.IsSuccessStatusCode)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                }
            }
                
           // await _httpService.ExecuteQuery(url, HttpOperationMode.POST, message);
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
