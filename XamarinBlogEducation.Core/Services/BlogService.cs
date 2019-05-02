
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly IHttpService _httpService;
        public BlogService(IHttpService httpService)
        {
            _httpService = httpService;

        }
        public async Task<GetDetailsPostResponseModel> ShowDetailedPost()
        {
            var url = $"/Post/post";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var parsedResult = await _httpService.ProcessJson<GetDetailsPostResponseModel>(result);
            return parsedResult;

        }
        public async Task<bool> AddNewPost(CreatePostRequestModel model)
        {
            using (var client = new HttpClient())
            {
                    var json = JsonConvert.SerializeObject(model);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var message = new HttpRequestMessage(HttpMethod.Post, "http://195.26.92.83:6776/api/Post/post");
                    message.Content = httpContent;
                var token = CrossSecureStorage.Current.GetValue("securityToken");
                    client.DefaultRequestHeaders.Add("Authorization", $"{"Bearer "}{token}");
                    var response = await client.SendAsync(message);
                return response.IsSuccessStatusCode;
            }
        }
        public async Task<bool> AddComment(AddCommentRequestBlogView model)
        {
            var url = $"{"http://195.26.92.83:6776/api/Comment/comment/"}{model.PostId}";
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var message = new HttpRequestMessage(HttpMethod.Post, url);
                message.Content = httpContent;
                client.DefaultRequestHeaders.Add("Authorization", $"{"Bearer "}{CrossSecureStorage.Current.GetValue("securityToken")}");
                var response = await client.SendAsync(message);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<List<GetAllPostResponseModel>> GetAllPosts()
        {
            var url = $"/Post/posts";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var parsedResult = await _httpService.ProcessJson<List<GetAllPostResponseModel>>(result);
            return parsedResult;
        }
        public async Task<List<GetAllUserPostResponseModel>> GetUserPosts(string userEmail)
        {
            var parsedResult = new List<GetAllUserPostResponseModel>();
            using (var client = new HttpClient())
            {
                var url = $"{"http://195.26.92.83:6776/api/Post/user-posts-list?useremail="}{userEmail}";
                var message = new HttpRequestMessage(HttpMethod.Get, url);
                client.DefaultRequestHeaders.Add("Authorization", $"{"Bearer "}{CrossSecureStorage.Current.GetValue("securityToken")}");
                var response = await client.SendAsync(message);
                if (response.IsSuccessStatusCode)
                {
                    parsedResult = await _httpService.ProcessJson<List<GetAllUserPostResponseModel>>(response);
                    return parsedResult;
                }
                return parsedResult;
            }
        }
        public async Task<List<GetAllCommentResponseModel>> GetAllComments(long postId)
        {
            var url = $"{"/Comment/comment/"}{postId}";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var parsedResult = await _httpService.ProcessJson<List<GetAllCommentResponseModel>>(result);
            return parsedResult;
        }
        public async Task<bool> UpdatePost(EditPostBlogRequestModel editedPost)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(editedPost);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var message = new HttpRequestMessage(HttpMethod.Post, "http://195.26.92.83:6776/api/Post/edit-post");
                message.Content = httpContent;
                client.DefaultRequestHeaders.Add("Authorization", $"{"Bearer "}{CrossSecureStorage.Current.GetValue("securityToken")}");
                var response = await client.SendAsync(message);
                return response.IsSuccessStatusCode;
            }
        }
        public async Task<List<GetAllCategoryResponseModel>> GetAllCategories()
        {
            var url = $"/Category/categories";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var parsedResult = await _httpService.ProcessJson<List<GetAllCategoryResponseModel>>(result);
            return parsedResult;
        }
        public async Task<bool> AddNewCategory(AddNewCategoryRequestModel category)
        {
            var url = $"/Category/add-new-category";
            var json = JsonConvert.SerializeObject(category);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeletePost(long postId)
        {
            var url = $"{"/Post/delete/"}{postId}";
            var response = await _httpService.ExecuteQuery(url, HttpOperationMode.POST);
            return response.IsSuccessStatusCode;
        }
    }
}
