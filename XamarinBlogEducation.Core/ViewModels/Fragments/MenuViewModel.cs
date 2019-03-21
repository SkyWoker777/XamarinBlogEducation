using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinBlogEducation.Core.ViewModels.Activities;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class MenuViewModel: MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowHomeCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<AllPostsViewModel>());
            ShowProfileCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<UserProfileViewModel>());
            ExitCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<UserProfileViewModel>());
        }

        // MvvmCross Lifecycle

        // MVVM Properties

        // MVVM Commands
        public IMvxCommand ShowHomeCommand { get; private set; }
        public IMvxCommand ShowProfileCommand { get; private set; }
        public IMvxCommand ExitCommand { get; private set; }

        // Private methods
    }
}
