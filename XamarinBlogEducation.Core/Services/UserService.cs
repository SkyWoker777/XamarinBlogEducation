
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Requests;

namespace XamarinBlogEducation.Core.Services.Interfaces
{
    public class UserService : IUserService
    {

        private readonly IHttpService _httpService;
        public UserService(IHttpService httpService)
        {
            _httpService = httpService;

        }
        public async Task AddUserAsync(RegisterAccountRequestModel model)
        {
            string url = "/Account/register";
            string json = JsonConvert.SerializeObject(model);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);

        }

        public async Task<EditAccountRequestModel> GetUserAsync(LoginAccountRequestModel model)
        {
            string testToken;
            EditAccountRequestModel loggedUser = new EditAccountRequestModel();
            string url = "/Account/login";
            string json = JsonConvert.SerializeObject(model);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
            if (response.IsSuccessStatusCode)
            {
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
        public async Task<EditAccountRequestModel> GetUserInfo(string email)
        {
            string url = $"{ "/Account/info?userEmail=" }{ email }";
            HttpResponseMessage response = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            EditAccountRequestModel parsedResult = await _httpService.ProcessJson<EditAccountRequestModel>(response);
            return parsedResult;

        }
        public async Task<bool> UpdateUserAsync(EditAccountRequestModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(model);
                StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "http://195.26.92.83:6776/api/Account/profile")
                {
                    Content = httpContent
                };
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer { CrossSecureStorage.Current.GetValue("securityToken")}");
               
                HttpResponseMessage response = await client.SendAsync(message);
                if (response.IsSuccessStatusCode) { 
                CrossSecureStorage.Current.SetValue("UserName", model.FirstName);
                CrossSecureStorage.Current.SetValue("UserLastName", model.LastName);
                CrossSecureStorage.Current.SetValue("UserEmail", model.Email);}
                return response.IsSuccessStatusCode;

            }
        }
        public async Task<bool> ChangeUserPassword(ChangePasswordAccountRequestModel model)
        {
            model.Token = CrossSecureStorage.Current.GetValue("securityToken");
            string url = "/Account/change-password";
            string json = JsonConvert.SerializeObject(model);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
            return result.IsSuccessStatusCode;
        }

        public async Task AutologinUserAsync(RegisterAccountRequestModel model)
        {
            LoginAccountRequestModel loginModel = new LoginAccountRequestModel
            {
                Email = model.Email,
                Password = model.Password
            };
            await GetUserAsync(loginModel);
        }


    }
}
