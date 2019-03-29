using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.Core.ViewModels.Dialogs;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class UserProfileViewModel : BaseViewModel<LoginAccountViewModel>
    {
        private string _email;
        private string _firstName;
        private string _lastName;
        private byte[] _userImage;
        private readonly IUserService _userService;
        private EditAccountViewModel _user;
        private LoginAccountViewModel _model;
        public UserProfileViewModel(IUserService userService, IMvxNavigationService _navigationService) : base(_navigationService)
        {
            _userService = userService;
            UpdateCommand = new MvxAsyncCommand(UpdateAsync);
            GetUserInfoCommand = new MvxAsyncCommand(GetUserInfo);
            OpenDialogCommand = new MvxAsyncCommand<LoginAccountViewModel>(OpenDialogAsync);
            ChangePasswordCommand = new MvxAsyncCommand(ChangePasswordAsync);
            GoToPostsCommand = new MvxAsyncCommand(GoToPostsAsync);
        }
        public IMvxCommand GoToPostsCommand { get; private set; }
        public IMvxCommand UpdateCommand { get; private set; }
        public IMvxCommand ChangePasswordCommand { get; private set; }
        public IMvxCommand GetUserInfoCommand { get; private set; }
        public IMvxCommand<LoginAccountViewModel> OpenDialogCommand { get; private set; }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged();
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged();
            }
        }
        public byte[] UserImage
        {
            get => _userImage;
            set
            {
                _userImage = value;
                RaisePropertyChanged();
            }
        }

        private async Task OpenDialogAsync(LoginAccountViewModel user)
        {
            await _navigationService.Navigate<ChangePasswordDialogViewModel, LoginAccountViewModel> (user);
        }
        private async Task ChangePasswordAsync()
        {
            OpenDialogCommand.Execute(_model);
        }
        private async Task GetUserInfo()
        {
            User = await _userService.GetUserInfo(_model);
        }
        private async Task UpdateAsync()
        {
            _user = new EditAccountViewModel()
            {

                Email = _email,
                FirstName = _firstName,
                LastName = _lastName,
                UserImage = _userImage

            };
            await _userService.UpdateUserAsync(_user);
        }
        public LoginAccountViewModel Model
        {
            get => _model;
            set
            {
                _model = value;
                RaisePropertyChanged(() => Model);
            }
        } 
        public EditAccountViewModel User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }
        public override void Prepare(LoginAccountViewModel model)
        {
            Model = model;
            GetUserInfoCommand.Execute();
            
            
        }
        private async Task GoToPostsAsync()
        {
            await _navigationService.Navigate<AllPostsViewModel>();
        }
    }
}
