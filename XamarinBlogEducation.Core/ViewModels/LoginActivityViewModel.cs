using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.Core.ViewModels
{
    public class LoginActivityViewModel : BaseViewModel
    {
        public LoginActivityViewModel(IMvxNavigationService _navigationService) : base(_navigationService)
        {
            LoginCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<LoginViewModel>());
        }
        public IMvxCommand LoginCommand { get; private set; }
    }
}
