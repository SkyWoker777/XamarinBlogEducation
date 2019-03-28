using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Account;
using XamarinBlogEducation.Business.Services.Interfaces;

namespace XamarinBlogEducation.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginAccountViewModel model)
        {
            IActionResult res = Unauthorized();
            var token = await _accountService.SignIn(model);
            if (token != null)
            {
                return Ok(token);
            }
            return res;
        }
        [HttpPost]
        [Route("getInfo")]
        public async Task<IActionResult> Find([FromBody]LoginAccountViewModel model)
        {
            IActionResult res = Unauthorized();
            var user = await _accountService.FindUser(model);
            return Ok(user);
            
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterAccountViewModel model)
        {

          var res =  await _accountService.CreateUser(model);

            return Ok(res);
        }

    }
}
