using MvvmCross.Commands;
using MvvmCross.Navigation;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Core.ViewModels.Activities
{
   public class MainViewModel : BaseViewModel
    {

        public MainViewModel(IMvxNavigationService _navigationService) : base(_navigationService)
        {
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
            ShowHomeCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<AllPostsViewModel>());
            AddPostCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<CreatePostViewModel>());
            ShowProfileCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<UserProfileViewModel>());
            ExitCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<UserProfileViewModel>());
        }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
        public IMvxCommand ShowHomeCommand { get; private set; }
        public IMvxCommand AddPostCommand { get; private set; }
        public IMvxCommand ShowProfileCommand { get; private set; }
        public IMvxCommand ExitCommand { get; private set; }
    }
}
