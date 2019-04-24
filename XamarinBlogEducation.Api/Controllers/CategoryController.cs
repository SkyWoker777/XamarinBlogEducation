using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Api.Extensions;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Blog;

namespace XamarinBlogEducation.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : Controller
    {
        private readonly IPostsService _postService;
        private readonly IMapper _mapper;
        public CategoryController(IPostsService postsService, ICommentsService commentService, IAccountService accountService, IMapper mapper)
        {
            _postService = postsService;
            _mapper = mapper;
        }
      
        [AllowAnonymous]
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetCategories()
        {
            List<DataAccess.Entities.Category> list = await _postService.GetAllCategories();
            List<GetAllCategoriesblogViewItem> mappedList = _mapper.Map<List<GetAllCategoriesblogViewItem>>(list);
            return Ok(mappedList);
        }
     
        [AllowAnonymous]
        [HttpPost("add-new-category")]
        public async Task<IActionResult> AddCategory([FromBody]GetAllCategoriesblogViewItem newCategory)
        {
            await _postService.AddCategory(newCategory);
            return Ok();
        }

    }
}
