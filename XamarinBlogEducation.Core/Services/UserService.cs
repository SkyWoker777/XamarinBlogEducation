
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
            var url = "/User/register";
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);

        }

        public async Task<EditAccountViewModel> GetUserAsync(LoginAccountViewModel model)
        {
            string testToken;
            EditAccountViewModel loggedUser=new EditAccountViewModel();
            var url = "/User/login";
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
            if (response.IsSuccessStatusCode) { 
            loggedUser = await GetUserInfo(model.Email);
            if (loggedUser != null)
            {
                testToken = await _httpService.ProcessToken(response);
                CrossSecureStorage.Current.SetValue("securityToken", testToken);
                CrossSecureStorage.Current.SetValue("UserName", loggedUser.FirstName);
                CrossSecureStorage.Current.SetValue("UserLastName", loggedUser.LastName);
                CrossSecureStorage.Current.SetValue("UserEmail", loggedUser.Email);
            }
            }
            return loggedUser;

        }
        public async Task<EditAccountViewModel> GetUserInfo(string email)
        {
            var url = "/User/info";
            var httpContent = new StringContent(email, Encoding.UTF8, "application/json");
            var response = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
            var parsedResult = await _httpService.ProcessJson<EditAccountViewModel>(response);
            return parsedResult;

        }
        public async Task UpdateUserAsync(EditAccountViewModel model)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(model);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var message = new HttpRequestMessage(HttpMethod.Post, "http://195.26.92.83:6776/api/User/profile");
                    message.Content = httpContent;
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer { CrossSecureStorage.Current.GetValue("securityToken")}");
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
        }
        public async Task ChangeUserPassword(ChangePasswordViewModel model)
        {
            model.Token = CrossSecureStorage.Current.GetValue("securityToken");
            var url = "/User/change-password";
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
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
