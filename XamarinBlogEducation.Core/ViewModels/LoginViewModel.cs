using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.SecureStorage;
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
        private EditAccountViewModel _loggedUser;
        private readonly IUserService _userService;

        public LoginViewModel(IUserService userService,
             IMvxNavigationService navigationService) : base(navigationService)
        {
            _userService = userService;
            LoginCommand = new MvxAsyncCommand(LoginAsync);
            SingUpCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<RegisterViewModel>());
            SkipCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AllPostsFragmentViewModel>());
            GoNextCommand= new MvxAsyncCommand(GoNextAsync);
        }
        public IMvxAsyncCommand LoginCommand { get; private set; }
        public IMvxCommand GoNextCommand { get; private set; }
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
        public EditAccountViewModel loggedUser
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
            user = new LoginAccountViewModel()
            {
                Email = _email,
                Password = _password
            };
          _loggedUser=  await _userService.GetUserAsync(user);
         var mail = CrossSecureStorage.Current.GetValue("UserEmail");
            await DisposeView(this);
        }
        private async Task GoNextAsync()
        {
            await NavigationService.Navigate<AllPostsFragmentViewModel>();
            await DisposeView(this);
        }
    }
}
