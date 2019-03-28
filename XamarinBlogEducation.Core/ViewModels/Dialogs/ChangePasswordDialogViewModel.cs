using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Core.ViewModels.Dialogs
{
   public class ChangePasswordDialogViewModel: BaseViewModel<EditAccountViewModel>
    {
        private string _newPassword;
        private string _oldPassword;
        private string _comfirmPassword;
        private string _token;
        private string _email;
        private ChangePasswordViewModel passwordModel;
        private readonly IUserService _userService;
        public ChangePasswordDialogViewModel(IUserService userService, IMvxNavigationService navigationService) : base(navigationService)
        {
            _userService = userService;
            ChangePasswordCommand = new MvxAsyncCommand(ChangePassword);

        }
        public IMvxCommand ChangePasswordCommand { get; private set; }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                RaisePropertyChanged();
            }
        }
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                RaisePropertyChanged();
            }
        }
        public string ComfirmPassword
        {
            get => _comfirmPassword;
            set
            {
                _comfirmPassword = value;
                RaisePropertyChanged();
            }
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
        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                RaisePropertyChanged();
            }
        }

        private async Task ChangePassword()
        {
            passwordModel = new ChangePasswordViewModel()
            {
                Password = _newPassword,
                OldPassword = _oldPassword,
                ConfirmPassword = _comfirmPassword,
                Token = _token,
                Email = _email
            };
            await _userService.ChangeUserPassword(passwordModel);

        }

        public override void Prepare(EditAccountViewModel parameter)
        {
            Email = parameter.Email;
        }
    }
}
