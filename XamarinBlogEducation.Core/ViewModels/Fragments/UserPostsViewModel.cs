using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class UserPostsViewModel: BaseViewModel
    {
        private GetAllPostsBlogViewItem _selectedPost;
        private readonly IBlogService _blogService;
        public UserPostsViewModel(
            IBlogService blogService,
            IMvxNavigationService navigationService):base(navigationService)
        {

            _blogService = blogService;

            UserPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>();
            EditPostCommand = new MvxAsyncCommand(EditPost);
            GoBackCommand = new MvxAsyncCommand(GoBackAsync);
            PostSelectedCommand = new MvxAsyncCommand<GetAllPostsBlogViewItem>(PostSelected);
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
        private async Task GoBackAsync()
        {
            await this.NavigationService.Close(this);
        }
        public MvxNotifyTask LoadPostsTask { get; private set; }

        public MvxNotifyTask FetchPostsTask { get; private set; }

        private MvxObservableCollection<GetAllPostsBlogViewItem> _userPosts;
        public MvxObservableCollection<GetAllPostsBlogViewItem> UserPosts
        {
            get => _userPosts;
            set
            {
                _userPosts = value;
                RaisePropertyChanged(() => UserPosts);
            }
        }
        
        public IMvxCommand EditPostCommand { get; private set; }
        public IMvxCommand<GetAllPostsBlogViewItem> PostSelectedCommand { get; private set; }
        public IMvxCommand FetchPostCommand { get; private set; }
        public IMvxCommand RefreshPostsCommand { get; private set; }

        private async Task LoadPosts()
        {
            var result = await _blogService.GetUserPosts(CrossSecureStorage.Current.GetValue("UserEmail"));
            List<GetAllPostsBlogViewItem> postsToAdd = new List<GetAllPostsBlogViewItem>();
            postsToAdd.AddRange(result);
            for (int i = 0; i < postsToAdd.Count; i++)
            {
                UserPosts.Add(postsToAdd[i]);
            }
        }

        public async Task EditPost()
        {
            await NavigationService.Navigate<CreatePostViewModel>();
        }

        private async Task PostSelected(GetAllPostsBlogViewItem selectedPost)
        {
            await NavigationService.Navigate<EditPostViewModel, GetAllPostsBlogViewItem>(selectedPost);
        }
        private void RefreshPosts()
        {
            LoadPostsTask = MvxNotifyTask.Create(LoadPosts);
            RaisePropertyChanged(() => LoadPostsTask);
        }
        public GetAllPostsBlogViewItem SelectedPost
        {
            get => _selectedPost;
            set
            {
                _selectedPost = value;
                NavigationService.Navigate<EditPostViewModel, GetAllPostsBlogViewItem>(_selectedPost);
                RaisePropertyChanged();
            }
        }
    }

}
