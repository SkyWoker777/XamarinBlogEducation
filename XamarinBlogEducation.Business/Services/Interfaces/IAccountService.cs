using System.Threading.Tasks;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CreateUser(RegisterAccountRequestModel model);
        Task<string> SignIn(LoginAccountRequestModel model);
        Task UpdateUserProfile(EditAccountRequestModel model, string id);
        Task ChangeUserPassword(ChangePasswordAccountRequestModel model);
        Task<GetInfoAccountResponseModel> FindUser(string email);
        //Task<bool> CheckUserLogin(CheckLoginAccountViewModel model);
    }
}
