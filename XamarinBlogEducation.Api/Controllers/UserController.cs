using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinBlogEducation.Api.Extensions;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Api.Controllers
{
   [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("update")]
        public async Task<IActionResult> Edit([FromBody]EditAccountViewModel model)
        {
            var id = User.Identity.GetUserId();
            IActionResult res = BadRequest();
            await _accountService.UpdateUserProfile(model, id);
            if (_accountService.UpdateUserProfile(model, id).IsCompleted)
            {
                return Ok();
            }
            return res;
        }

        public async Task<IActionResult> DeleteAccount(string userid)
        {
            await _accountService.RemoveUser(userid);
            return Ok();
        }
        [HttpPost("updatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody]ChangePasswordViewModel model)
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

    }
}
