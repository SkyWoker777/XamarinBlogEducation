using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.Core.ViewModels
{
    public class AllPostsBaseViewModel : BaseViewModel
    {
        private readonly IBlogService _blogService; 
        public AllPostsBaseViewModel(
            IBlogService blogService, 
            IMvxNavigationService navigationService): base(navigationService)
        {

            _blogService = blogService;
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MenuViewModel>());
            ShowHomeCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AllPostsViewModel>());
            AddPostCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<CreatePostViewModel>());
            ShowProfileCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserProfileViewModel>());
            LoginCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<LoginViewModel>());
            GoBackCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AllPostsViewModel>());
            AboutCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AboutViewModel>());

        }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
        public IMvxCommand ShowHomeCommand { get; private set; }
        public IMvxCommand AddPostCommand { get; private set; }
        public IMvxCommand ShowProfileCommand { get; private set; }
        public IMvxCommand LoginCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
        public IMvxCommand AboutCommand { get; private set; }
   
    }
}
