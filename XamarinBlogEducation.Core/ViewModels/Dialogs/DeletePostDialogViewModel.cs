using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Core.ViewModels.Dialogs
{
    public class DeletePostDialogViewModel : BaseViewModel<GetAllUserPostResponseModel>
    {
        public GetAllUserPostResponseModel currentPost;
        private readonly IBlogService _blogService;
        private readonly IUserDialogs _userDialogs;
        public DeletePostDialogViewModel(IMvxNavigationService navigationService, IBlogService blogService, IUserDialogs userDialogs) : base(navigationService)
        {
            _blogService = blogService;
            _userDialogs = userDialogs;
            CancelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserPostsViewModel>());
            DeleteCommand = new MvxAsyncCommand(DeleteAsync);
        }
        public IMvxCommand CancelCommand { get; private set; }
        public IMvxCommand DeleteCommand { get; private set; }

        public override void Prepare(GetAllUserPostResponseModel parameter)
        {
            currentPost = parameter;
        }
        private async Task DeleteAsync()
        {
            var isResultSuccessful=await _blogService.DeletePost(currentPost.Id);
            if (isResultSuccessful)
            {
                _userDialogs.Toast(Strings.SuccessDeletePost);
                await DisposeView(this);
            }
            if (!isResultSuccessful)
            {
                _userDialogs.Toast(Strings.ErrorDeletePost);
            }
        }

    }
}
