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
    public class BlogController : Controller
    {
        private readonly IPostsService _postService;
        private readonly ICommentsService _commentService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public BlogController(IPostsService postsService, ICommentsService commentService,IAccountService accountService, IMapper mapper)
        {
            _postService = postsService;
            _commentService = commentService;
            _accountService = accountService;
            _mapper = mapper; 
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("post/{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await _postService.GetDetailsPost(postId);
            var mappedPost = _mapper.Map<GetDetailsPostBlogView>(post);
            return Ok(post);

        }
        [AllowAnonymous]
        [HttpGet]
        [Route("posts")]
        public async Task<IActionResult> GetPosts()
        {
            var list = await _postService.GetAll();
            var mappedList = _mapper.Map<List<GetAllPostsBlogViewItem>>(list);
            return Ok(mappedList);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var list = await _postService.GetAllCategories();
            var mappedList = _mapper.Map<List<GetAllCategoriesblogViewItem>>(list);
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
        [HttpPost("add-new-category")]
        public async Task<IActionResult> AddCategory([FromBody]GetAllCategoriesblogViewItem newCategory)
        {
            await _postService.AddCategory(newCategory);
            return Ok();
        }
        
        [HttpPost("comment/{postId}")]
        public async Task<IActionResult> AddComment([FromBody]AddCommentBlogViewModel newComment)
        {
            newComment.UserId= User.Identity.GetUserId();
            await _commentService.AddComment(newComment, newComment.PostId);
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("comment/{postId}")]
        public async Task<List<GetAllCommentsBlogViewItem>> GetComments(long postId)
        {
            return await _commentService.GetAllComments(postId);
        }
    }
}
