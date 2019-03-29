using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Core.ViewModels.Activities
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private LoginAccountViewModel user;
        private readonly IUserService _userService;  

       public LoginViewModel(
            IUserService userService,
            IMvxNavigationService _navigationService) : base(_navigationService)
        {
            _userService = userService;
            LoginCommand  = new MvxAsyncCommand(LoginAsync);
            SingUpCommand  = new MvxAsyncCommand(SignUpAsync);
            SkipCommand = new MvxAsyncCommand(SkipAsync);
            ToProfileCommand = new MvxAsyncCommand<LoginAccountViewModel>(ToProfileAsync);
        }
        public IMvxCommand LoginCommand { get; private set; } 
        public IMvxCommand SingUpCommand { get; private set; }
        public IMvxCommand SkipCommand { get; private set; }
        public IMvxCommand<LoginAccountViewModel> ToProfileCommand { get; private set; }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        private async Task LoginAsync()
        {
               user = new LoginAccountViewModel() {
                Email = _email,
                Password = _password};
                
           await _userService.GetUserAsync(user);
            ToProfileCommand.Execute(user);
        }

        private async Task SignUpAsync()
        {
          
           await _navigationService.Navigate<RegisterViewModel>();
        }

        private async Task SkipAsync()
        {
            await _navigationService.Navigate<AllPostsViewModel>();
        }
        private async Task ToProfileAsync(LoginAccountViewModel user)
        {
            await _navigationService.Navigate<UserProfileViewModel, LoginAccountViewModel>(user);
        }

    }
}
