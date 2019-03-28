using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateUser(RegisterAccountViewModel model);
        Task<string> SignIn(LoginAccountViewModel model);
        Task UpdateUserProfile(EditAccountViewModel model, string id);
        Task ChangeUserPassword(ChangePasswordViewModel model);
        Task<EditAccountViewModel> FindUser(LoginAccountViewModel model);
        Task RemoveUser(string userId);
    }
}
