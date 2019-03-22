using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class UserProfileViewModel : BaseViewModel
    {
        private string _email;
        private string _firstName;
        private string _lastName;
        private byte[] _userImage;
        private EditAccountViewModel user;
        private readonly IUserService _userService;

        public UserProfileViewModel(IUserService userService, IMvxNavigationService _navigationService) : base(_navigationService)
        {
            _userService = userService;
            UpdateCommand = new MvxAsyncCommand(UpdateAsync);
        }

        public IMvxCommand UpdateCommand { get; private set; }


        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }
        //public string Password
        //{
        //    get => _password;
        //    set
        //    {
        //        _password = value;
        //        RaisePropertyChanged();
        //    }
        //}
        //public string ConfirmPassword
        //{
        //    get => _confirmPassword;
        //    set
        //    {
        //        _confirmPassword = value;
        //        RaisePropertyChanged();
        //    }
        //}
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

        private async Task UpdateAsync()
        {
            user = new EditAccountViewModel()
            {

                Email = _email,
                //Password = _password,
                //ConfirmPassword = _confirmPassword,
                FirstName = _firstName,
                LastName = _lastName,
                UserImage = _userImage

            };
            await _userService.UpdateUserAsync(user);
            await _navigationService.Navigate<UserProfileViewModel>();
            //  await _navigationService.Navigate<LoginViewModel>();

        }
    }
}
