using MvvmCross.Commands;
using MvvmCross.Navigation;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Core.ViewModels
{
   public class MainViewModel : BaseViewModel
    {

        public MainViewModel(IMvxNavigationService _navigationService) : base(_navigationService)
        {
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MenuViewModel>());
            ShowHomeCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AllPostsFragmentViewModel>());
            AddPostCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<CreatePostViewModel>());
            ShowProfileCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserProfileViewModel>());
            ExitCommand = new MvxAsyncCommand(async() =>  System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow());
            LoginCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<LoginViewModel>());
            AboutCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AboutFragmentModel>());
            GoBackCommand = new MvxAsyncCommand(async () => await DisposeView(this));
        }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
        public IMvxCommand ShowHomeCommand { get; private set; }
        public IMvxCommand AddPostCommand { get; private set; }
        public IMvxCommand ShowProfileCommand { get; private set; }
        public IMvxCommand ExitCommand { get; private set; }
        public IMvxCommand LoginCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
        public IMvxCommand AboutCommand { get; private set; }
    }
}
