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
        private GetAllPostResponseModel _selectedPost;
        private readonly IBlogService _blogService;
        public UserPostsViewModel(
            IBlogService blogService,
            IMvxNavigationService navigationService) : base(navigationService)
        {

            _blogService = blogService;

            UserPosts = new MvxObservableCollection<GetAllPostResponseModel>();
            EditPostCommand = new MvxAsyncCommand(EditPost);
            GoBackCommand = new MvxAsyncCommand(async () => await DisposeView(this));
            PostSelectedCommand = new MvxAsyncCommand<GetAllPostResponseModel>(PostSelected);
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
        public IMvxCommand<GetAllPostResponseModel> PostSelectedCommand { get; private set; }
        public IMvxCommand FetchPostCommand { get; private set; }
        public IMvxCommand RefreshPostsCommand { get; private set; }
        public MvxNotifyTask LoadPostsTask { get; private set; }
        public MvxNotifyTask FetchPostsTask { get; private set; }

        private MvxObservableCollection<GetAllPostResponseModel> _userPosts;
        public MvxObservableCollection<GetAllPostResponseModel> UserPosts
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
            List<GetAllPostResponseModel> result = await _blogService.GetUserPosts(CrossSecureStorage.Current.GetValue("UserEmail"));
            List<GetAllPostResponseModel> postsToAdd = new List<GetAllPostResponseModel>();
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

        private async Task PostSelected(GetAllPostResponseModel selectedPost)
        {
            await NavigationService.Navigate<EditPostViewModel, GetAllPostResponseModel>(selectedPost);
        }
        private void RefreshPosts()
        {
            LoadPostsTask = MvxNotifyTask.Create(LoadPosts);
            RaisePropertyChanged(() => LoadPostsTask);
        }
        public GetAllPostResponseModel SelectedPost
        {
            get => _selectedPost;
            set
            {
                _selectedPost = value;
                NavigationService.Navigate<EditPostViewModel, GetAllPostResponseModel>(_selectedPost);
                RaisePropertyChanged();
            }
        }
    }

}
