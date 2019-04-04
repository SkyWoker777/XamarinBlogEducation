using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private LoginAccountViewModel user;
        private readonly IUserService _userService;

        public LoginViewModel(IUserService userService,
             IMvxNavigationService navigationService) : base(navigationService)
        {
            _userService = userService;
            LoginCommand = new MvxAsyncCommand(LoginAsync);
            SingUpCommand = new MvxAsyncCommand(SignUpAsync);
            SkipCommand = new MvxAsyncCommand(SkipAsync);
            ToProfileCommand = new MvxAsyncCommand<EditAccountViewModel>(ToProfileAsync);
        }
        public IMvxCommand LoginCommand { get; private set; }
        public IMvxCommand SingUpCommand { get; private set; }
        public IMvxCommand SkipCommand { get; private set; }
        public IMvxCommand<EditAccountViewModel> ToProfileCommand { get; private set; }
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
            user = new LoginAccountViewModel()
            {
                Email = _email,
                Password = _password
            };

            var loggedUser = await _userService.GetUserAsync(user);
            await NavigationService.Navigate<AllPostsViewModel>();
            await DisposeView(this);
        }

        private async Task SignUpAsync()
        {

            await NavigationService.Navigate<RegisterViewModel>();
        }

        private async Task SkipAsync()
        {
            await NavigationService.Navigate<AllPostsViewModel>();

        }
        private async Task ToProfileAsync(EditAccountViewModel user)
        {
            await NavigationService.Navigate<UserProfileViewModel, EditAccountViewModel>(user);
        }

    }
}
