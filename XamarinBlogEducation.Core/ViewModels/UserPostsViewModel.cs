using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class UserPostsViewModel : BaseViewModel
    {
        private GetAllUserPostResponseModel _selectedPost;
        private readonly IBlogService _blogService;
        public UserPostsViewModel(
            IBlogService blogService,
            IMvxNavigationService navigationService) : base(navigationService)
        {

            _blogService = blogService;

            UserPosts = new MvxObservableCollection<GetAllUserPostResponseModel>();
            EditPostCommand = new MvxAsyncCommand(EditPost);
            GoBackCommand = new MvxAsyncCommand(async () => await DisposeView(this));
            PostSelectedCommand = new MvxAsyncCommand<GetAllUserPostResponseModel>(PostSelected);
            FetchPostCommand = new MvxCommand(
                () =>
                {
                    FetchPostsTask = MvxNotifyTask.Create(LoadPosts);
                    RaisePropertyChanged(() => FetchPostsTask);
                });
            RefreshPostsCommand = new MvxCommand(RefreshPosts);
        }
        public override Task Initialize()
        {
            LoadPostsTask = MvxNotifyTask.Create(LoadPosts);
            return Task.FromResult(0);
        }
        public IMvxCommand GoBackCommand { get; private set; }
        public IMvxCommand EditPostCommand { get; private set; }
        public IMvxCommand<GetAllUserPostResponseModel> PostSelectedCommand { get; private set; }
        public IMvxCommand FetchPostCommand { get; private set; }
        public IMvxCommand RefreshPostsCommand { get; private set; }
        public MvxNotifyTask LoadPostsTask { get; private set; }
        public MvxNotifyTask FetchPostsTask { get; private set; }

        private MvxObservableCollection<GetAllUserPostResponseModel> _userPosts;
        public MvxObservableCollection<GetAllUserPostResponseModel> UserPosts
        {
            get => _userPosts;
            set
            {
                _userPosts = value;
                RaisePropertyChanged(() => UserPosts);
            }
        }



        private async Task LoadPosts()
        {
            List<GetAllUserPostResponseModel> result = await _blogService.GetUserPosts(CrossSecureStorage.Current.GetValue("UserEmail"));
            List<GetAllUserPostResponseModel> postsToAdd = new List<GetAllUserPostResponseModel>();
            postsToAdd.AddRange(result);
            for (int i = 0; i < postsToAdd.Count; i++)
            {
                UserPosts.Add(postsToAdd[i]);
            }
        }

        private async Task EditPost()
        {
            await NavigationService.Navigate<CreatePostViewModel>();
        }

        private async Task PostSelected(GetAllUserPostResponseModel selectedPost)
        {
            await NavigationService.Navigate<EditPostViewModel, GetAllUserPostResponseModel>(selectedPost);
        }
        private void RefreshPosts()
        {
            LoadPostsTask = MvxNotifyTask.Create(LoadPosts);
            RaisePropertyChanged(() => LoadPostsTask);
        }
        public GetAllUserPostResponseModel SelectedPost
        {
            get => _selectedPost;
            set
            {
                _selectedPost = value;
                NavigationService.Navigate<EditPostViewModel, GetAllUserPostResponseModel>(_selectedPost);
                RaisePropertyChanged();
            }
        }
    }

}
