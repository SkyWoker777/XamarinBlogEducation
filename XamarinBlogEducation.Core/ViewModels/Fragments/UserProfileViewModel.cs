using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.SecureStorage;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Dialogs;
using XamarinBlogEducation.ViewModels.Models.Account;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class UserProfileViewModel : BaseViewModel<EditAccountViewModel>
    {
        private string _email;
        private string _firstName;
        private string _lastName;
        private byte[] _userImage;
        private string _userEmail;
        private readonly IUserService _userService;
        private EditAccountViewModel _user;
        private LoginAccountViewModel _model;
        public UserProfileViewModel(IUserService userService, IMvxNavigationService navigationService) : base(navigationService)
        {
            _userService = userService;
            _userEmail = CrossSecureStorage.Current.GetValue("UserEmail");
            GetUserInfoCommand = new MvxAsyncCommand(GetUserInfoAsync);
            GetUserInfoCommand.Execute();
            var loginModel = new LoginAccountViewModel() { Email = _userEmail};
            Model = loginModel;
            UpdateCommand = new MvxAsyncCommand(UpdateAsync);
            OpenDialogCommand = new MvxAsyncCommand<LoginAccountViewModel>(OpenDialogAsync);
            ChangePasswordCommand = new MvxAsyncCommand(ChangePasswordAsync);
            GoToPostsCommand = new MvxAsyncCommand(GoToPostsAsync);
            GoBackCommand = new MvxAsyncCommand(GoBackAsync);
        }
        
        public IMvxCommand GoToPostsCommand { get; private set; }
        public IMvxCommand UpdateCommand { get; private set; }
        public IMvxCommand ChangePasswordCommand { get; private set; }
        public IMvxCommand GetUserInfoCommand { get; private set; }
        public IMvxCommand<LoginAccountViewModel> OpenDialogCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
        private async Task GoBackAsync()
        {
            await this.NavigationService.Close(this);
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
            await NavigationService.Navigate<ChangePasswordDialogViewModel, LoginAccountViewModel> (user);
        }
        private async Task ChangePasswordAsync()
        {
            OpenDialogCommand.Execute(_model);
        }
        private async Task GetUserInfoAsync()
        {
            User= await  _userService.GetUserInfo(_userEmail);
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
        public override void Prepare(EditAccountViewModel model)
        {
            var loginModel = new LoginAccountViewModel() { Email = model.Email };
            Model = loginModel;
            //User = model;
            // GetUserInfoCommand.Execute(); 
        }
        private async Task GoToPostsAsync()
        {
            await NavigationService.Navigate<AllPostsViewModel>();
        }
    }
}
