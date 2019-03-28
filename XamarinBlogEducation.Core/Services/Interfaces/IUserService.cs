using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task GetUserAsync(LoginAccountViewModel model);
        Task AddUserAsync(RegisterAccountViewModel model);
        Task AutologinUserAsync(RegisterAccountViewModel model);
        Task UploadImageAsync(byte[] image, EditAccountViewModel model);
        Task UpdateUserAsync(EditAccountViewModel model);
        Task<EditAccountViewModel> GetUserInfo(LoginAccountViewModel model);
        Task ChangeUserPassword(ChangePasswordViewModel model);
    }
}
