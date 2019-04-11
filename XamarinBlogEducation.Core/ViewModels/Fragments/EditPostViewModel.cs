using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Dialogs;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Blog;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class EditPostViewModel : BaseViewModel<GetAllPostsBlogViewItem>
    {
        private string _content;
        private string _title;
        private string _description;
        private CreatePostBlogViewModel _editedPost;
        private GetAllPostsBlogViewItem _postToEdit;
        private IBlogService _blogService;
        public EditPostViewModel(IBlogService blogService, IMvxNavigationService navigationService) : base(navigationService)
        {
            _blogService = blogService;
            SaveEditCommand = new MvxAsyncCommand(SaveAsync);
            CancelEditCommand = new MvxAsyncCommand(async()=> await NavigationService.Navigate<UserPostsViewModel>());
            GoToPostsCommand = new MvxAsyncCommand(GoToPostsAsync);
            DeleteCommand = new MvxAsyncCommand(Delete);
            OpenDialogCommand = new MvxAsyncCommand<GetAllPostsBlogViewItem>(OpenDialogAsync);
            GoBackCommand = new MvxAsyncCommand(async()=> await this.NavigationService.Close(this));
        }
        public IMvxCommand SaveEditCommand { get; private set; }
        public IMvxCommand CancelEditCommand { get; private set; }
        public IMvxCommand GoToPostsCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
        public IMvxCommand<GetAllPostsBlogViewItem> OpenDialogCommand { get; private set; }
        public IMvxCommand DeleteCommand { get; private set; }
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
        
        public GetAllPostsBlogViewItem PostToEdit
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
            _editedPost = new CreatePostBlogViewModel()
            {
                Id=PostToEdit.Id,
                Title = PostToEdit.Title,
                Description = PostToEdit.Description,
                Content = PostToEdit.Content
            };
            await _blogService.UpdatePost(_editedPost);
        }

        private async Task GoToPostsAsync()
        {
            await DisposeView(this);
            await NavigationService.Navigate<UserPostsViewModel>();
        }
        public override void Prepare(GetAllPostsBlogViewItem parameter)
        {
            _postToEdit = parameter;
        }
        private async Task Delete()
        {
            OpenDialogCommand.Execute(_postToEdit);
        }
        private async Task OpenDialogAsync(GetAllPostsBlogViewItem post)
        {
            await NavigationService.Navigate<DeletePostDialogViewModel, GetAllPostsBlogViewItem>(post);
        }
    }
}
