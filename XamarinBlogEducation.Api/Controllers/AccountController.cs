using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using XamarinBlogEducation.Api.Extensions;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Requests;

namespace XamarinBlogEducation.Api.Controllers
{
   [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("profile")]
        public async Task<IActionResult> Edit([FromBody]EditAccountRequestModel model)
        {
            var id = User.Identity.GetUserId();
            IActionResult res = BadRequest();
            await _accountService.UpdateUserProfile(model,id);
            if (_accountService.UpdateUserProfile(model,id).IsCompleted)
            {
                return Ok();
            }
            return res;
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> UpdatePassword([FromBody]ChangePasswordAccountRequestModel model)
        {
            var id = User.Identity.GetUserId();
            IActionResult res = BadRequest();
            await _accountService.ChangeUserPassword(model);
            if (_accountService.ChangeUserPassword(model).IsCompleted)
            {
                return Ok();
            }
            return res;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginAccountRequestModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).FirstOrDefault();

                return BadRequest(errorMessage);
            }

            var token = await _accountService.SignIn(loginModel);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetUserInfo(string userEmail)
        {
            var user = await _accountService.FindUser(userEmail);
            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterAccountRequestModel registrationModel)
        {
            var registrationResult = await _accountService.CreateUser(registrationModel);
            return Ok();
        }
    }
}
