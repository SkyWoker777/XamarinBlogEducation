using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateUser(RegisterAccountViewModel model);
        Task<string> SignIn(LoginAccountViewModel model);
        Task UpdateUserProfile(EditAccountViewModel model, string id);
        Task RemoveUser(string userId);
    }
}
