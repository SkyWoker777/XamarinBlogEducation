using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinBlogEducation.Api.Extensions;
using XamarinBlogEducation.Business.Services;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Blog;

namespace XamarinBlogEducation.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : Controller
    {
        private readonly IPostsService _postService;
        private readonly IMapper _mapper;
        public PostController(IPostsService postsService,  IMapper mapper)
        {
            _postService = postsService;
            _mapper = mapper; 
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("post/{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await _postService.GetDetailsPost(postId);
            var category = await _postService.GetCategoryName(post.CategoryId);
            var mappedPost = _mapper.Map<GetDetailsPostBlogView>(post);
            mappedPost.Category = category;
            return Ok(mappedPost);

        }
        [AllowAnonymous]
        [HttpGet]
        [Route("posts")]
        public async Task<IActionResult> GetPosts()
        {
            var list = await _postService.GetAll();
            var mappedList = _mapper.Map<List<GetAllPostsBlogViewItem>>(list);
            foreach (var item in mappedList)
            {
                item.Category = await _postService.GetCategoryName(item.CategoryId);
            }
            return Ok(mappedList);
        }
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("post")]
        public async Task<IActionResult> DeleteAsync(int postId)
        {
            await _postService.DeletePost(postId);
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Add([FromBody]CreatePostBlogViewModel newpost)
        {
            var id = User.Identity.GetUserId();
            newpost.AuthorId = id;
            await _postService.CreatePost(newpost);
            return Ok();
        }
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost()]
        [Route("edit-post")]
        public async Task<IActionResult> EditPost([FromBody]CreatePostBlogViewModel post)
        {
            await _postService.EditPostAsync(post);
            return Ok();
        }
        
        [HttpGet]
        [Route("user-posts-list")]
        public async Task<IActionResult> GetUserPosts(string userEmail)
        {
            var list = await _postService.GetUserPosts(userEmail);
            var mappedList = _mapper.Map<List<GetAllPostsBlogViewItem>>(list);
            return Ok(mappedList);
        }
        [AllowAnonymous]
        [HttpPost("delete/{postId}")]
        public async Task<IActionResult> DeletePost(long postId)
        {
            await _postService.DeletePost(postId);
            return Ok();
        }
    }
}
