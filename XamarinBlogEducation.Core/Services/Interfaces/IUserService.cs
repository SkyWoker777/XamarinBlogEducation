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
        Task UploadImageAsync(Stream image);
        Task UpdateUserAsync();
    }
}
