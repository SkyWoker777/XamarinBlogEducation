using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
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
    public class CommentController : Controller
    {
        private readonly ICommentsService _commentService;
        public CommentController(ICommentsService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("comment/{postId}")]
        public async Task<IActionResult> AddComment([FromBody]AddCommentBlogViewModel newComment)
        {
            newComment.UserId = User.Identity.GetUserId();
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