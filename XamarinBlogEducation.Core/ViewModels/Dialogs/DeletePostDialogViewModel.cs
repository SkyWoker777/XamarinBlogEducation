using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Core.ViewModels.Dialogs
{
    public class DeletePostDialogViewModel : BaseViewModel<GetAllPostResponseModel>
    {
        public GetAllPostResponseModel currentPost;
        private readonly IBlogService _blogService;
        public DeletePostDialogViewModel(IMvxNavigationService navigationService, IBlogService blogService) : base(navigationService)
        {
            _blogService = blogService;
            CancelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserPostsViewModel>());
            DeleteCommand = new MvxAsyncCommand(DeleteAsync);
        }
        public IMvxCommand CancelCommand { get; private set; }
        public IMvxCommand DeleteCommand { get; private set; }

        public override void Prepare(GetAllPostResponseModel parameter)
        {
            currentPost = parameter;
        }
        private async Task DeleteAsync()
        {
            await _blogService.DeletePost(currentPost.Id);
        }

    }
}
