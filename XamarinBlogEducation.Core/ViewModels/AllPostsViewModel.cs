using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.Core.ViewModels
{
    public class AllPostsViewModel : BaseViewModel
    {
        private readonly IBlogService _blogService;

        public AllPostsViewModel(
            IBlogService blogService, 
            IMvxNavigationService navigationService): base(navigationService)
        {

            _blogService = blogService;

            AllPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>();
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MenuViewModel>());
            ShowHomeCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AllPostsViewModel>());
            AddPostCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<CreatePostViewModel>());
            ShowProfileCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserProfileViewModel>());
            ExitCommand = new MvxAsyncCommand(async () => System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow());
            LoginCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<LoginViewModel>());
            GoBackCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AllPostsViewModel>());
            PostSelectedCommand = new MvxAsyncCommand<GetAllPostsBlogViewItem>(PostSelected);
            FetchPostCommand = new MvxCommand(
                () =>
                {
                   
                        FetchPostsTask = MvxNotifyTask.Create(LoadPosts);
                        RaisePropertyChanged(() => FetchPostsTask);
                   
                });
            RefreshPostsCommand = new MvxCommand(RefreshPosts);
        }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
        public IMvxCommand ShowHomeCommand { get; private set; }
        public IMvxCommand AddPostCommand { get; private set; }
        public IMvxCommand ShowProfileCommand { get; private set; }
        public IMvxCommand ExitCommand { get; private set; }
        public IMvxCommand LoginCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
        public override Task Initialize()
       {

            LoadPostsTask = MvxNotifyTask .Create( LoadPosts);

            return Task.FromResult(0);
        }
        public MvxNotifyTask LoadPostsTask { get; private set; }

        public MvxNotifyTask FetchPostsTask { get; private set; }

        private MvxObservableCollection<GetAllPostsBlogViewItem> _allPosts;
        public MvxObservableCollection<GetAllPostsBlogViewItem> AllPosts
        {
            get=> _allPosts;
            set
            {
                _allPosts = value;
                RaisePropertyChanged(() => AllPosts);
            }
        }

        
        public IMvxCommand<GetAllPostsBlogViewItem> PostSelectedCommand { get; private set; }
        public IMvxCommand FetchPostCommand { get; private set; }
        public IMvxCommand RefreshPostsCommand { get; private set; }

        private async Task LoadPosts()
        {
            var result = await _blogService.GetAllPosts();
            List<GetAllPostsBlogViewItem> postsToAdd = new List<GetAllPostsBlogViewItem>();
            postsToAdd.AddRange(result);   
            for(int i=0; i < postsToAdd.Count; i++)
            {
                AllPosts.Add(postsToAdd[i]);
            }
        }

        private async Task PostSelected(GetAllPostsBlogViewItem selectedPost)
        {
            await NavigationService.Navigate<DetailedPostViewModel, GetAllPostsBlogViewItem> (selectedPost);  
        }
        private void RefreshPosts()
        {
            LoadPostsTask = MvxNotifyTask.Create(LoadPosts);
            RaisePropertyChanged(() => LoadPostsTask);
        }

    }
}
