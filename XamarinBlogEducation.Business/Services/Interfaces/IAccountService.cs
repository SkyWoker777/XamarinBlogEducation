using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateUser(RegisterAccountViewModel model);
        Task<string> SignIn(LoginAccountViewModel model);
        Task UpdateUserProfile(EditAccountViewModel model);
        Task RemoveUser(string userId);
    }
}
