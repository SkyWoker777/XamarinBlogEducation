using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System.Threading.Tasks;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class MenuViewModel: BaseViewModel
    {
        private string _userName;
        public MenuViewModel (IMvxNavigationService navigationService) : base(navigationService)
        {
            ShowHomeCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AllPostsFragmentViewModel>());
            AddPostCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<CreatePostViewModel>());
            ShowProfileCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserProfileViewModel>());
            ShowUserPostsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserPostsViewModel>());
            ExitCommand = new MvxAsyncCommand(ExitAsync);
            UserName = (CrossSecureStorage.Current.GetValue("UserName") + " " + CrossSecureStorage.Current.GetValue("UserLastName"));
        }

        public IMvxCommand ShowHomeCommand { get; private set; }
        public IMvxCommand ShowUserPostsCommand { get; private set; }
        public IMvxCommand AddPostCommand { get; private set; }
        public IMvxCommand ShowProfileCommand { get; private set; }
        public IMvxCommand ExitCommand { get; private set; }
        private async Task ExitAsync()
        {
            CrossSecureStorage.Current.DeleteKey("securityToken");
            CrossSecureStorage.Current.DeleteKey("UserName");
            CrossSecureStorage.Current.DeleteKey("UserEmail");
            CrossSecureStorage.Current.DeleteKey("UserLastName");
            await NavigationService.Navigate<LoginViewModel>();
            await DisposeView(this);
        }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }
       
    }
}
