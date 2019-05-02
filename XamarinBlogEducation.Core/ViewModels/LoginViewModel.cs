using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.SecureStorage;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.ViewModels.Requests;

namespace XamarinBlogEducation.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private LoginAccountRequestModel user;
        private EditAccountRequestModel _loggedUser;
        private readonly IUserService _userService;
        private readonly IUserDialogs _userDialogs;
        private bool isModelValid;

        public LoginViewModel(IUserService userService,
             IMvxNavigationService navigationService,
             IUserDialogs userDialogs
            ) : base(navigationService)
        {
            _userService = userService;
            _userDialogs = userDialogs;
            LoginCommand = new MvxAsyncCommand(LoginAsync);
            SingUpCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<RegisterViewModel>());
            SkipCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AllPostsViewModel>());
            GoNextCommand = new MvxAsyncCommand(GoNextAsync);
        }
        public IMvxAsyncCommand LoginCommand { get; private set; }
        public IMvxAsyncCommand ValidateCommand { get; private set; }
        public IMvxAsyncCommand GoNextCommand { get; private set; }
        public IMvxCommand SingUpCommand { get; private set; }
        public IMvxCommand SkipCommand { get; private set; }

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
        public EditAccountRequestModel loggedUser
        {
            get => _loggedUser;
            set
            {
                _loggedUser = value;
                RaisePropertyChanged();
            }
        }

        private async Task LoginAsync()
        {
            Validate();
            if (isModelValid)
            {
                user = new LoginAccountRequestModel()
                {
                    Email = _email,
                    Password = _password
                };
                _loggedUser = await _userService.GetUserAsync(user);
                string mail = CrossSecureStorage.Current.GetValue("UserEmail");
                _userDialogs.Toast(Strings.SuccessLogin);
                await GoNextAsync();
            }
            if (!isModelValid)
            {
                _userDialogs.Toast(Strings.WrongLogin);
            }

        }

        private async Task GoNextAsync()
        {
            await NavigationService.Navigate<AllPostsViewModel>();
            await DisposeView(this);
        }

        public void Validate()
        {
            if (Regex.Match(_email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success && !string.IsNullOrWhiteSpace(_password))
            {
                isModelValid = true;
            }
            else
            {
                isModelValid = false;
            }
        }
    }
}

