
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            var url = $"/Blog/post";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var parsedResult = await _httpService.ProcessJson<GetDetailsPostBlogView>(result);
            return parsedResult;

        }
        public async Task AddNewPost(CreatePostBlogViewModel model)
        {
            using (var client = new HttpClient())
            {
                    var json = JsonConvert.SerializeObject(model);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var message = new HttpRequestMessage(HttpMethod.Post, "http://195.26.92.83:6776/api/Blog/post");
                    message.Content = httpContent;
                    client.DefaultRequestHeaders.Add("Authorization", $"{"Bearer"}{CrossSecureStorage.Current.GetValue("securityToken")}");
                    var response = await client.SendAsync(message);   
            }
        }
        public async Task AddComment(AddCommentBlogViewModel model)
        {
            var url = $"{"http://195.26.92.83:6776/api/Blog/comment/"}{model.PostId}";
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var message = new HttpRequestMessage(HttpMethod.Post, url);
                message.Content = httpContent;
                client.DefaultRequestHeaders.Add("Authorization", $"{"Bearer"}{CrossSecureStorage.Current.GetValue("securityToken")}");
                var response = await client.SendAsync(message);
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
            var url = $"/Blog/posts";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var parsedResult = await _httpService.ProcessJson<List<GetAllPostsBlogViewItem>>(result);
            return parsedResult;
        }
        public async Task<List<GetAllPostsBlogViewItem>> GetUserPosts(string userEmail)
        {
            var parsedResult = new List<GetAllPostsBlogViewItem>();
            using (var client = new HttpClient())
            {
                var url = $"{"http://195.26.92.83:6776/api/Blog/user-posts-list?useremail="}{userEmail}";
                var message = new HttpRequestMessage(HttpMethod.Get, url);
                client.DefaultRequestHeaders.Add("Authorization", $"{"Bearer"}{CrossSecureStorage.Current.GetValue("securityToken")}");
                var response = await client.SendAsync(message);
                if (response.IsSuccessStatusCode)
                {
                    parsedResult = await _httpService.ProcessJson<List<GetAllPostsBlogViewItem>>(response);
                    return parsedResult;
                }
                return parsedResult;
            }
        }
        public async Task<List<GetAllCommentsBlogViewItem>> GetAllComments(long postId)
        {
            var url = $"{"/Blog/comment/"}{postId}";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var parsedResult = await _httpService.ProcessJson<List<GetAllCommentsBlogViewItem>>(result);
            return parsedResult;
        }
        public async Task UpdatePost(CreatePostBlogViewModel editedPost)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(editedPost);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var message = new HttpRequestMessage(HttpMethod.Post, "http://195.26.92.83:6776/api/Blog/edit-post");
                message.Content = httpContent;
                client.DefaultRequestHeaders.Add("Authorization", $"{"Bearer"}{CrossSecureStorage.Current.GetValue("securityToken")}");
                var response = await client.SendAsync(message);
            }
        }
        public async Task<List<GetAllCategoriesblogViewItem>> GetAllCategories()
        {
            var url = $"/Blog/categories";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var parsedResult = await _httpService.ProcessJson<List<GetAllCategoriesblogViewItem>>(result);
            return parsedResult;
        }
        public async Task<bool> AddNewCategory(GetAllCategoriesblogViewItem category)
        {
            var url = $"/Blog/add-new-category";
            var json = JsonConvert.SerializeObject(category);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
            return result.IsSuccessStatusCode;
        }
        public async Task DeletePost(long postId)
        {
            var url = $"{"/Blog/delete/"}{postId}";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.POST);
        }
    }
}
