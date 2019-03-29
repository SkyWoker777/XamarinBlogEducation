using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.ViewModels.Models.Account;

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

        private RegisterAccountViewModel user;
        private LoginAccountViewModel loginUser;
        private readonly IUserService _userService;

        public RegisterViewModel(
            IUserService userService, 
            IMvxNavigationService _navigationService) : base(_navigationService)
        {
            _userService = userService;
            RegistrateCommand = new MvxAsyncCommand(RegistrateAsync);
        }

        public IMvxCommand RegistrateCommand { get; private set; }


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
            get =>_confirmPassword;
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
            get => _userImage ;
            set
            {
                _userImage = value;
                RaisePropertyChanged();
            }
        }

        private async Task RegistrateAsync()
        {
            user = new RegisterAccountViewModel()
            {

                Email = _email,
                Password = _password,
                ConfirmPassword = _confirmPassword,
                FirstName = _firstName,
                LastName = _lastName,
                UserImage=_userImage

            };
            loginUser = new LoginAccountViewModel()
            {
                Email=user.Email,
                Password=user.Password
            };
            await _userService.AddUserAsync(user);
            await _userService.AutologinUserAsync(user);
            await _navigationService.Navigate<UserProfileViewModel,LoginAccountViewModel>(loginUser);
            
        }
    }
}
