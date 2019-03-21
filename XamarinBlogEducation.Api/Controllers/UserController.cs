﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            IActionResult res = BadRequest();
            await _accountService.UpdateUserProfile(model);
            if (_accountService.UpdateUserProfile(model).IsCompleted)
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


    }
}
