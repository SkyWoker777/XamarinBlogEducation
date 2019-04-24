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
        public async Task<IActionResult> Login([FromBody]LoginAccountViewModel loginModel)
        {
            var token = await _accountService.SignIn(loginModel);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetUserInfo(string userEmail)
        {
            var user = await _accountService.FindUser(userEmail);
            return Ok(user);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterAccountViewModel registrationModel)
        {
            var registrationResult = await _accountService.CreateUser(registrationModel);
            return Ok(registrationResult);
        }

    }
}
