using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Requests;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _firstName;
        private string _lastName;
        private byte[] _userImage;
        private bool isModelValid;

        private RegisterAccountRequestModel user;
        private EditAccountRequestModel loginUser;
        private readonly IUserService _userService;
        private readonly IUserDialogs _userDialogs;
        public RegisterViewModel(
            IUserService userService,
            IUserDialogs userDialogs,
            IMvxNavigationService navigationService) : base(navigationService)
        {
            _userService = userService;
            _userDialogs = userDialogs;
            RegistrateCommand = new MvxAsyncCommand(RegistrateAsync);
            LoginCommand = new MvxAsyncCommand(async()=>await DisposeView(this));
        }

        public IMvxCommand RegistrateCommand { get; private set; }
        public IMvxCommand LoginCommand { get; private set; }

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
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                RaisePropertyChanged();
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged();
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged();
            }
        }
        public byte[] UserImage
        {
            get => _userImage;
            set
            {
                _userImage = value;
                RaisePropertyChanged();
            }
        }

        private async Task RegistrateAsync()
        {
            Validate();
            if (isModelValid)
            {
            user = new RegisterAccountRequestModel()
            {
                Email = _email,
                Password = _password,
                ConfirmPassword = _confirmPassword,
                FirstName = _firstName,
                LastName = _lastName,
                UserImage = _userImage
            };
            loginUser = new EditAccountRequestModel()
            {
                Email = user.Email
            };
           
                await _userService.AddUserAsync(user);
                await _userService.AutologinUserAsync(user);
                await NavigationService.Navigate<AllPostsViewModel>();
            }
        }
        public void Validate()
        {
            isModelValid = true;
            if(!string.IsNullOrEmpty(_email))
            { if (!Regex.Match(_email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success && !string.IsNullOrWhiteSpace(_password))
                {
                    _userDialogs.Toast(Strings.WrongEmailFormat);
                    isModelValid = false;
                } }
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_confirmPassword) || string.IsNullOrEmpty(_firstName) || string.IsNullOrEmpty(_lastName))
            {
                _userDialogs.Toast(Strings.EmptyField);
                isModelValid = false;
            }
            if (_password != _confirmPassword)
            {
                _userDialogs.Toast(Strings.DifferentPasswords);
                isModelValid = false;
            }
        }
    }
}
