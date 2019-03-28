using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.ViewModels.Blog;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.Core.ViewModels.Activities
{
    public class AllPostsViewModel : MvxViewModel
    {
        private readonly IBlogService _blogService;
        public IMvxNavigationService _navigationService;

        public AllPostsViewModel(
            IBlogService blogService, 
            IMvxNavigationService navigationService)
        {

            _blogService = blogService;
            _navigationService = navigationService;

            AllPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>();

            AddPostCommand = new MvxAsyncCommand(AddPost);
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

        public IMvxCommand AddPostCommand { get; private set; }
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

        public async Task AddPost()
        {         
            await _navigationService.Navigate<CreatePostViewModel>();
        }

        private async Task PostSelected(GetAllPostsBlogViewItem selectedPost)
        {
            await _navigationService.Navigate<DetailedPostViewModel, GetAllPostsBlogViewItem> (selectedPost);  
        }
        private void RefreshPosts()
        {
            LoadPostsTask = MvxNotifyTask.Create(LoadPosts);
            RaisePropertyChanged(() => LoadPostsTask);
        }

    }
}
