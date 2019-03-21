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

        }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
    }
}
