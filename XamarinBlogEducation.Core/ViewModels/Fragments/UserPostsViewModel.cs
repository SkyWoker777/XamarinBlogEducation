﻿using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class UserPostsViewModel: MvxViewModel
    {
        private readonly IBlogService _blogService;
        public IMvxNavigationService _navigationService;
        public UserPostsViewModel(
            IBlogService blogService,
            IMvxNavigationService navigationService)
        {

            _blogService = blogService;
            _navigationService = navigationService;

            UserPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>();
            EditPostCommand = new MvxAsyncCommand(EditPost);
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
            await _navigationService.Navigate<CreatePostViewModel>();
        }

        private async Task PostSelected(GetAllPostsBlogViewItem selectedPost)
        {
            await _navigationService.Navigate<DetailedPostViewModel, GetAllPostsBlogViewItem>(selectedPost);
        }
        private void RefreshPosts()
        {
            LoadPostsTask = MvxNotifyTask.Create(LoadPosts);
            RaisePropertyChanged(() => LoadPostsTask);
        }

    }
}