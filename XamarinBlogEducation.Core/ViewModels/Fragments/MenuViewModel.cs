﻿using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System.Threading.Tasks;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class MenuViewModel: MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowHomeCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<AllPostsFragmentViewModel>());
            AddPostCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<CreatePostViewModel>());
            ShowProfileCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<UserProfileViewModel>());
            ShowUserPostsCommand= new MvxAsyncCommand(async () => await _navigationService.Navigate<UserPostsViewModel>());
            ExitCommand = new MvxAsyncCommand(ExitAsync);
        }

        public IMvxCommand ShowHomeCommand { get; private set; }
        public IMvxCommand ShowUserPostsCommand { get; private set; }
        public IMvxCommand AddPostCommand { get; private set; }
        public IMvxCommand ShowProfileCommand { get; private set; }
        public IMvxCommand ExitCommand { get; private set; }
        private async Task ExitAsync()
        {
            CrossSecureStorage.Current.DeleteKey("securityToken");
            await _navigationService.Navigate<LoginViewModel>();

        }
    }
}
