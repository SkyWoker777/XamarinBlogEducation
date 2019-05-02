﻿using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Dialogs;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class EditPostViewModel : BaseViewModel<GetAllUserPostResponseModel>
    {
        private string _content;
        private string _title;
        private string _description;
        private EditPostBlogRequestModel _editedPost;
        private GetAllUserPostResponseModel _postToEdit;
        private readonly IBlogService _blogService;
        private readonly IUserDialogs _userDialogs;
        public EditPostViewModel(IBlogService blogService, IUserDialogs userDialogs, IMvxNavigationService navigationService) : base(navigationService)
        {
            _blogService = blogService;
            _userDialogs = userDialogs;

            SaveEditCommand = new MvxAsyncCommand(SaveAsync);
            CancelEditCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserPostsViewModel>());
            GoToPostsCommand = new MvxAsyncCommand(GoToPostsAsync);
            DeleteCommand = new MvxAsyncCommand(Delete);
            OpenDialogCommand = new MvxAsyncCommand<GetAllUserPostResponseModel>(OpenDialogAsync);
            GoBackCommand = new MvxAsyncCommand(async () => await DisposeView(this));
        }
        public IMvxCommand SaveEditCommand { get; private set; }
        public IMvxCommand CancelEditCommand { get; private set; }
        public IMvxCommand GoToPostsCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
        public IMvxCommand<GetAllUserPostResponseModel> OpenDialogCommand { get; private set; }
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

        public GetAllUserPostResponseModel PostToEdit
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
           var isResultSuccessful = await _blogService.UpdatePost(_editedPost);
            await GoToPostsAsync();
        }

        private async Task GoToPostsAsync()
        {
            await DisposeView(this);
            await NavigationService.Navigate<UserPostsViewModel>();
        }
        public override void Prepare(GetAllUserPostResponseModel parameter)
        {
            _postToEdit = parameter;
        }
        private async Task Delete()
        {
            OpenDialogCommand.Execute(_postToEdit);
        }
 
        private async Task OpenDialogAsync(GetAllUserPostResponseModel post)
        {
            await NavigationService.Navigate<DeletePostDialogViewModel, GetAllUserPostResponseModel>(post);
        }
    }
}