using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : Controller
    {
        private readonly IPostsService _postService;
        private readonly IMapper _mapper;
        public CategoryController(IPostsService postsService, IMapper mapper)
        {
            _postService = postsService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<List<GetAllCategoryResponseModel>>> GetCategories()
        {
            var list = await _postService.GetAllCategories();
            List<GetAllCategoryResponseModel> mappedList = _mapper.Map<List<GetAllCategoryResponseModel>>(list);
            return Ok(mappedList);
        }

        [AllowAnonymous]
        [HttpPost("add-new-category")]
        public async Task<IActionResult> AddCategory([FromBody]AddNewCategoryRequestModel newCategory)
        {
            await _postService.AddCategory(newCategory);
            return Ok();
        }

    }
}
