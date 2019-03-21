using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Api.Controllers
{
    [Route("api/[controller]")]
    public class CommonController : Controller
    {
        private readonly IHostingEnvironment _environment;
        public CommonController(IHostingEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        // POST: api/Image
        [HttpPost]
        [Route("addImage")]
        public async Task UploadImage(IFormFile file, RegisterAccountViewModel model)
        {
            
        }
    }
}
