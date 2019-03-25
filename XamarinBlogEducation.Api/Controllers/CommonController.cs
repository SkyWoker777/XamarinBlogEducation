using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using XamarinBlogEducation.Api.Extensions;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Api.Controllers
{
    [Route("api/[controller]")]
    public class CommonController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHostingEnvironment _environment;
        public CommonController(IHostingEnvironment environment, IAccountService accountService)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _accountService = accountService;
        }
        // POST: api/Image
        //[HttpPost]
        //[Route("addImage")]
        //public async Task<IActionResult> UploadImage(IFormFile file)
        //{
        //var id = User.Identity.GetUserId();
        //    IActionResult res = BadRequest();
        //    byte[] img;
        //    foreach (var item in HttpContext.Request.Form)
        //    {   
        //    }
        //       
        //    return res;
        //}
    }
}
