using MvvmCross.Commands;
using MvvmCross.Navigation;
using Plugin.SecureStorage;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Dialogs;
using XamarinBlogEducation.ViewModels.Requests;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class UserProfileViewModel : BaseViewModel<EditAccountRequestModel>
    {
        private string _email;
        private string _firstName;
        private string _lastName;
        private byte[] _userImage;
        private readonly string _userEmail;
        private readonly IUserService _userService;
        private EditAccountRequestModel _user;
        private LoginAccountRequestModel _model;
        public UserProfileViewModel(IUserService userService, IMvxNavigationService navigationService) : base(navigationService)
        {
            _userService = userService;
            _userEmail = CrossSecureStorage.Current.GetValue("UserEmail");
            GetUserInfoCommand = new MvxAsyncCommand(GetUserInfoAsync);
            GetUserInfoCommand.Execute();
            LoginAccountRequestModel loginModel = new LoginAccountRequestModel() { Email = _userEmail };
            Model = loginModel;
            UpdateCommand = new MvxAsyncCommand(UpdateAsync);
            OpenDialogCommand = new MvxAsyncCommand<LoginAccountRequestModel>(OpenDialogAsync);
            ChangePasswordCommand = new MvxAsyncCommand(ChangePasswordAsync);
            GoToPostsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AllPostsViewModel>());
            GoBackCommand = new MvxAsyncCommand(async () => await DisposeView(this));


        }

        public IMvxCommand GoToPostsCommand { get; private set; }
        public IMvxCommand UpdateCommand { get; private set; }
        public IMvxCommand ChangePasswordCommand { get; private set; }
        public IMvxCommand GetUserInfoCommand { get; private set; }
        public IMvxCommand<LoginAccountRequestModel> OpenDialogCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
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

        private async Task OpenDialogAsync(LoginAccountRequestModel user)
        {
            await NavigationService.Navigate<ChangePasswordDialogViewModel, LoginAccountRequestModel>(user);
        }

        private async Task ChangePasswordAsync()
        {
            OpenDialogCommand.Execute(_model);
        }
        private async Task GetUserInfoAsync()
        {
            User = await _userService.GetUserInfo(_userEmail);
        }
        private async Task UpdateAsync()
        {
            _user = new EditAccountRequestModel()
            {

                Email = _email,
                FirstName = _firstName,
                LastName = _lastName,
                UserImage = _userImage

            };
            await _userService.UpdateUserAsync(_user);
            await DisposeView(this);
        }
        public LoginAccountRequestModel Model
        {
            get => _model;
            set
            {
                _model = value;
                RaisePropertyChanged(() => Model);
            }
        }
        public EditAccountRequestModel User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }
        public override void Prepare(EditAccountRequestModel model)
        {
            LoginAccountRequestModel loginModel = new LoginAccountRequestModel() { Email = model.Email };
            Model = loginModel;
        }

    }
}
