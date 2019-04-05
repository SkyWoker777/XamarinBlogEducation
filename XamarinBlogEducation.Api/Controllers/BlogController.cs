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
    public class BlogController : Controller
    {
        private readonly IPostsService _postService;
        private readonly ICommentsService _commentService;
        private readonly IAccountService _accountService;
        public BlogController(IPostsService postsService, ICommentsService commentService,IAccountService accountService)
        {
            _postService = postsService;
            _commentService = commentService;
            _accountService = accountService;
        }

        [HttpGet]
        [Route("getPost")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await _postService.GetDetailsPost(postId);
            return Ok(post);

        }
        [AllowAnonymous]
        [HttpGet]
        [Route("getPostList")]
        public async Task<IActionResult> GetPosts()
        {
            var list = await _postService.GetAll();
            return Ok(list);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("getCategoryList")]
        public async Task<IActionResult> GetCategories()
        {
            var list = await _postService.GetAllCategories();
            return Ok(list);
        }
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync(int postId)
        {
            await _postService.DeletePost(postId);
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost()]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody]CreatePostBlogViewModel newpost)
        {
            var id = User.Identity.GetUserId();
            newpost.AuthorId = id;
            await _postService.CreatePost(newpost);
            return Ok();
        }
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost()]
        [Route("edit")]
        public async Task<IActionResult> EditPost([FromBody]CreatePostBlogViewModel post)
        {
            await _postService.EditPostAsync(post);
            return Ok();
        }
        
        [HttpGet]
        [Route("getUserPosts")]
        public async Task<IActionResult> GetUserPosts(string userEmail)
        {
            var list = await _postService.GetUserPosts(userEmail);
            return Ok(list);
        }
        [AllowAnonymous]
        [HttpPost("addCategory")]
        public async Task<IActionResult> AddCategory([FromBody]GetAllCategoriesblogViewItem newCategory)
        {
            await _postService.AddCategory(newCategory);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("{postId}")]
        public async Task<List<GetAllCommentsBlogViewItem>> AddComment([FromBody]AddCommentBlogViewModel newComment, int postId)
        {
            await _commentService.AddComment(newComment, postId);
            return await _postService.ShowComments(postId);
        }
        
    }
}
