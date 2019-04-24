﻿using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Core.ViewModels.Dialogs
{
   public class ChangePasswordDialogViewModel: BaseViewModel<LoginAccountViewModel>
    {
        private string _newPassword;
        private string _oldPassword;
        private string _comfirmPassword;
        private string _email;
        private ChangePasswordViewModel passwordModel;
        private readonly IUserService _userService;
        public ChangePasswordDialogViewModel(IUserService userService, IMvxNavigationService navigationService) : base(navigationService)
        {
            _userService = userService;
            ChangePasswordCommand = new MvxAsyncCommand(ChangePassword);
            GoBackCommand = new MvxAsyncCommand(async() => await DisposeView(this));

        }
        public IMvxCommand ChangePasswordCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
            }
        }
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
            }
        }
        public string ComfirmPassword
        {
            get => _comfirmPassword;
            set
            {
                _comfirmPassword = value;
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
        private async Task ChangePassword()
        {
            passwordModel = new ChangePasswordViewModel()
            {
                Password = _newPassword,
                OldPassword = _oldPassword,
                ConfirmPassword = _comfirmPassword,
                Token = "",
                Email = _email
            };
            await _userService.ChangeUserPassword(passwordModel);

        }
        public override void Prepare(LoginAccountViewModel parameter)
        {
            Email = parameter.Email;
        }
    }
}
