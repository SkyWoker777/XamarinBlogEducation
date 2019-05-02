using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Requests;

namespace XamarinBlogEducation.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<EditAccountRequestModel> GetUserAsync(LoginAccountRequestModel model);
        Task AddUserAsync(RegisterAccountRequestModel model);
        Task AutologinUserAsync(RegisterAccountRequestModel model);
        Task<bool> UpdateUserAsync(EditAccountRequestModel model);
        Task<EditAccountRequestModel> GetUserInfo(string email);
        Task<bool> ChangeUserPassword(ChangePasswordAccountRequestModel model);
    }
}
