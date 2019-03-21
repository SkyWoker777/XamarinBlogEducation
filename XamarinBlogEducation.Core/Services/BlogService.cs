﻿using MvvmCross.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Blog;

namespace XamarinBlogEducation.Core.Services
{
    public class BlogService: IBlogService
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
            //var result=   await _httpService.MakeApiCallAsync<GetDetailsPostBlogView>(url, HttpMethod.Get);
            var parsedResult = await _httpService.ProcessJson<GetDetailsPostBlogView>(result);
            return  parsedResult;

        }
        public async Task AddNewPost(CreatePostBlogViewModel model)
        {
            var url = "/Blog/add";
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.POST, httpContent);
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
            var parsedResult = await _httpService.ProcessJson<List<GetAllPostsBlogViewItem>>(result);
            return parsedResult;           
        }

        public Task UpdatePost()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAllCategoriesblogViewItem>> GetAllCategories()
        {
            var url = $"/Blog/getCategoryList";
            var result = await _httpService.ExecuteQuery(url, HttpOperationMode.GET);
            var parsedResult = await _httpService.ProcessJson<List<GetAllCategoriesblogViewItem>>(result);
            return parsedResult;
        }
    }
}
