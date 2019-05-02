using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Requests;

namespace XamarinBlogEducation.Core.ViewModels.Dialogs
{
    public class ChangePasswordDialogViewModel : BaseViewModel<LoginAccountRequestModel>
    {
        private string _newPassword;
        private string _oldPassword;
        private string _comfirmPassword;
        private string _email;
        private ChangePasswordAccountRequestModel passwordModel;
        private readonly IUserService _userService;
        private readonly IUserDialogs _userDialogs;
        private bool isModelValid;
        public ChangePasswordDialogViewModel(IUserService userService, IUserDialogs userDialogs, IMvxNavigationService navigationService) : base(navigationService)
        {
            _userService = userService;
            _userDialogs = userDialogs;
            ChangePasswordCommand = new MvxAsyncCommand(ChangePassword);
            GoBackCommand = new MvxAsyncCommand(async () => await DisposeView(this));

        }
        public IMvxAsyncCommand ChangePasswordCommand { get; private set; }
        public IMvxAsyncCommand GoBackCommand { get; private set; }
        public string NewPassword
        {
            get => _newPassword;
            set => _newPassword = value;
        }
        public string OldPassword
        {
            get => _oldPassword;
            set => _oldPassword = value;
        }
        public string ComfirmPassword
        {
            get => _comfirmPassword;
            set => _comfirmPassword = value;
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }
        private async Task ChangePassword()
        {
            Validate();
            if (isModelValid)
            {
                passwordModel = new ChangePasswordAccountRequestModel()
                {
                    Password = _newPassword,
                    OldPassword = _oldPassword,
                    ConfirmPassword = _comfirmPassword,
                    Token = "",
                    Email = _email
                };
                var isResultSuccesful= await _userService.ChangeUserPassword(passwordModel);
                if (isResultSuccesful)
                {
                    _userDialogs.Toast(Strings.SuccessChangePassword);
                }
                if (!isResultSuccesful)
                {
                    _userDialogs.Toast(Strings.ErrorChangePassword);
                }
                await DisposeView(this);
            }

        }
        public override void Prepare(LoginAccountRequestModel parameter)
        {
            Email = parameter.Email;
        }
        public void Validate()
        {
            isModelValid = true;
            if (!string.IsNullOrEmpty(_newPassword) && !string.IsNullOrEmpty(_comfirmPassword) && !string.IsNullOrEmpty(_oldPassword))
            {
                if (_newPassword != _comfirmPassword)
                {
                    _userDialogs.Toast(Strings.DifferentPasswords);
                    isModelValid = false;
                }
            }
            if (string.IsNullOrEmpty(_newPassword) || string.IsNullOrEmpty(_comfirmPassword) || string.IsNullOrEmpty(_oldPassword))
            {
                _userDialogs.Toast(Strings.EmptyField);
                isModelValid = false;
            }
        }
    }
}
