using MvvmCross.Base;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Blog;

namespace XamarinBlogEducation.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly IHttpService _httpService;
        public BlogService(IHttpService httpService)
        {
            _httpService = httpService;

        }
        public async Task<GetDetailsPostBlogView> ShowDetailedPost()
        {
            var url = $"/Blog/getPost";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var parsedResult = await _httpService.ProcessJson<GetDetailsPostBlogView>(result);
            return parsedResult;

        }
        public async Task AddNewPost(CreatePostBlogViewModel model)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(model);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var message = new HttpRequestMessage(HttpMethod.Post, "http://195.26.92.83:6776/api/Blog/add");
                    message.Content = httpContent;
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CrossSecureStorage.Current.GetValue("securityToken"));
                    var response = await client.SendAsync(message);
                    //await UploadImageAsync(model.UserImage, model);                 
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

        public Task NavigatePosts()
        {
            throw new NotImplementedException();
        }

        public Task RemovePost()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAllPostsBlogViewItem>> GetAllPosts()
        {
            var url = $"/Blog/getPostList";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var json = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var parsedResult = JsonConvert.DeserializeObject<List<GetAllPostsBlogViewItem>>(json);
            return parsedResult;
        }
        public async Task<List<GetAllPostsBlogViewItem>> GetUserPosts(string userEmail)
        {
            var parsedResult = new List<GetAllPostsBlogViewItem>();
            using (var client = new HttpClient())
            {
                var url = $"{"http://195.26.92.83:6776/api/Blog/getUserPosts?useremail="}{userEmail}";
                var message = new HttpRequestMessage(HttpMethod.Get, url);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CrossSecureStorage.Current.GetValue("securityToken"));
                var response = await client.SendAsync(message);              
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    parsedResult = JsonConvert.DeserializeObject<List<GetAllPostsBlogViewItem>>(json);
                    return parsedResult;
                }
                return parsedResult;
            }
        }
        public Task UpdatePost()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAllCategoriesblogViewItem>> GetAllCategories()
        {
            var url = $"/Blog/getCategoryList";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var json = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var parsedResult = JsonConvert.DeserializeObject<List<GetAllCategoriesblogViewItem>>(json);
            return parsedResult;
        }
        public async Task<bool> AddNewCategory(GetAllCategoriesblogViewItem category)
        {
            var url = $"/Blog/addCategory";
            var json = JsonConvert.SerializeObject(category);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
            return result.IsSuccessStatusCode;
        }
    }
}
