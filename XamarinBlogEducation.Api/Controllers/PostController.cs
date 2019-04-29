using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Api.Extensions;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : Controller
    {
        private readonly IPostsService _postService;
        private readonly IMapper _mapper;
        public PostController(IPostsService postsService, IMapper mapper)
        {
            _postService = postsService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("post/{postId}")]
        public async Task<ActionResult<List<GetDetailsPostResponseModel>>> GetPost(int postId)
        {
            DataAccess.Entities.Post post = await _postService.GetDetailsPost(postId);
            string category = await _postService.GetCategoryName(post.CategoryId);
            GetDetailsPostResponseModel mappedPost = _mapper.Map<GetDetailsPostResponseModel>(post);
            mappedPost.Category = category;
            return Ok(mappedPost);

        }
        [AllowAnonymous]
        [HttpGet]
        [Route("posts")]
        public async Task<ActionResult<List<GetAllPostResponseModel>>> GetPosts()
        {
            IEnumerable<DataAccess.Entities.Post> list = await _postService.GetAll();
            List<GetAllPostResponseModel> mappedList = _mapper.Map<List<GetAllPostResponseModel>>(list);
            foreach (GetAllPostResponseModel item in mappedList)
            {
                item.Category = await _postService.GetCategoryName(item.CategoryId);
            }
            return Ok(mappedList);
        }
   
        [HttpDelete]
        [Route("post")]
        public async Task<IActionResult> DeleteAsync(int postId)
        {
            await _postService.DeletePost(postId);
            return Ok();
        }
        
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Add([FromBody]CreatePostRequestModel newpost)
        {
            string id = User.Identity.GetUserId();
            newpost.AuthorId = id;
            await _postService.CreatePost(newpost);
            return Ok();
        }
       
        [HttpPost]
        [Route("edit-post")]
        public async Task<IActionResult> EditPost([FromBody]EditPostBlogRequestModel post)
        {
            await _postService.EditPostAsync(post);
            return Ok();
        }

        [HttpGet]
        [Route("user-posts-list")]
        public async Task<ActionResult<List<GetAllUserPostResponseModel>>> GetUserPosts(string userEmail)
        {
            IEnumerable<DataAccess.Entities.Post> list = await _postService.GetUserPosts(userEmail);
            List<GetAllUserPostResponseModel> mappedList = _mapper.Map<List<GetAllUserPostResponseModel>>(list);
            return Ok(mappedList);
        }
       
        [HttpPost]
        [Route("delete/{postId}")]
        public async Task<IActionResult> DeletePost(long postId)
        {
            await _postService.DeletePost(postId);
            return Ok();
        }
    }
}
