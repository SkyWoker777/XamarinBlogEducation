using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Dialogs;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class EditPostViewModel : BaseViewModel<GetAllPostResponseModel>
    {
        private string _content;
        private string _title;
        private string _description;
        private EditPostBlogRequestModel _editedPost;
        private GetAllPostResponseModel _postToEdit;
        private readonly IBlogService _blogService;
        public EditPostViewModel(IBlogService blogService, IMvxNavigationService navigationService) : base(navigationService)
        {
            _blogService = blogService;
            SaveEditCommand = new MvxAsyncCommand(SaveAsync);
            CancelEditCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserPostsViewModel>());
            GoToPostsCommand = new MvxAsyncCommand(GoToPostsAsync);
            DeleteCommand = new MvxAsyncCommand(Delete);
            DeletePostCommand = new MvxAsyncCommand(DeletePost);
            OpenDialogCommand = new MvxAsyncCommand<GetAllPostResponseModel>(OpenDialogAsync);
            GoBackCommand = new MvxAsyncCommand(async () => await DisposeView(this));
        }
        public IMvxCommand SaveEditCommand { get; private set; }
        public IMvxCommand CancelEditCommand { get; private set; }
        public IMvxCommand GoToPostsCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
        public IMvxCommand<GetAllPostResponseModel> OpenDialogCommand { get; private set; }
        public IMvxCommand DeleteCommand { get; private set; }
        public IMvxCommand DeletePostCommand { get; private set; }
        public override Task Initialize()
        {
            return Task.FromResult(0);
        }
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        public GetAllPostResponseModel PostToEdit
        {
            get => _postToEdit;
            set
            {
                _postToEdit = value;
                RaisePropertyChanged(() => PostToEdit);
            }
        }

        private async Task SaveAsync()
        {
            _editedPost = new EditPostBlogRequestModel()
            {
                Id = PostToEdit.Id,
                Title = PostToEdit.Title,
                Description = PostToEdit.Description,
                Content = PostToEdit.Content
            };
            await _blogService.UpdatePost(_editedPost);
            await DisposeView(this);
        }

        private async Task GoToPostsAsync()
        {
            await DisposeView(this);
            await NavigationService.Navigate<UserPostsViewModel>();
        }
        public override void Prepare(GetAllPostResponseModel parameter)
        {
            _postToEdit = parameter;
        }
        private async Task Delete()
        {
            OpenDialogCommand.Execute(_postToEdit);
        }
        private async Task DeletePost()
        {
            await _blogService.DeletePost(_postToEdit.Id);
            await DisposeView(this);
        }
        private async Task OpenDialogAsync(GetAllPostResponseModel post)
        {
            await NavigationService.Navigate<DeletePostDialogViewModel, GetAllPostResponseModel>(post);
        }
    }
}
