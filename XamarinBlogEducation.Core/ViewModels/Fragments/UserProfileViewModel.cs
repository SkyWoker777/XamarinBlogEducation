using MvvmCross.Navigation;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class UserProfileViewModel : BaseViewModel
    {
        private IUserService _userService;
        public UserProfileViewModel(IUserService userService, IMvxNavigationService _navigationService) : base(_navigationService)
        {
            _userService = userService;
        }
    }
}
