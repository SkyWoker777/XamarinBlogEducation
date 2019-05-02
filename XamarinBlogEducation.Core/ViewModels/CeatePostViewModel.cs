﻿using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Requests;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class CreatePostViewModel : BaseViewModel
    {
        private string _title;
        private string _description;
        private string _postContent;
        private string _nickName;
        private long _selectedCategoryId;
        private readonly IBlogService _blogService;
        private readonly IUserDialogs _userDialogs;
        private bool isModelValid;
        private GetAllCategoryResponseModel _selectedCategory;
        private CreatePostRequestModel post;
        public CreatePostViewModel(IBlogService blogService, IUserDialogs userDialogs, IMvxNavigationService navigationService) : base(navigationService)
        {
            _blogService = blogService;
            _userDialogs = userDialogs;
            CategoryItems = new MvxObservableCollection<GetAllCategoryResponseModel>();

            AddNewPostCommand = new MvxAsyncCommand(AddNewPost);
            GoBackCommand = new MvxAsyncCommand(GoBack);
            OpenDialogCommand = new MvxAsyncCommand(OpenDialogAsync);
            ItemSelectedCommand = new MvxAsyncCommand<GetAllCategoryResponseModel>(ItemSelectedAsync);
        }
        public override Task Initialize()
        {
            LoadCategoriesTask = MvxNotifyTask.Create(LoadCategories);
            return Task.FromResult(0);

        }
        public MvxNotifyTask LoadCategoriesTask { get; private set; }
        public IMvxCommand OpenDialogCommand { get; private set; }
        public IMvxCommand AddNewPostCommand { get; private set; }
        public IMvxCommand ItemSelectedCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
        private MvxObservableCollection<GetAllCategoryResponseModel> _allCategories;
        public MvxObservableCollection<GetAllCategoryResponseModel> CategoryItems
        {
            get => _allCategories;
            set
            {
                _allCategories = value;
                RaisePropertyChanged(() => CategoryItems);
            }
        }
        private async Task LoadCategories()
        {
            List<GetAllCategoryResponseModel> result = await _blogService.GetAllCategories();
            CategoryItems.AddRange(result);
        }

        public long SelectedCategoryId
        {
            get => _selectedCategoryId;
            set
            {
                _selectedCategoryId = value;
                RaisePropertyChanged();
            }
        }
        public GetAllCategoryResponseModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                _selectedCategoryId = _selectedCategory.Id;
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
        public string NickName
        {
            get => _nickName;
            set
            {
                _nickName = value;
                RaisePropertyChanged();
            }
        }
        public string PostContent
        {
            get => _postContent;
            set
            {
                _postContent = value;
                RaisePropertyChanged();
            }
        }

        public async Task AddNewPost()
        {
            Validate();
            if (isModelValid)
            {
                post = new CreatePostRequestModel()
                {
                    Title = _title,
                    Content = _postContent,
                    CategoryId = _selectedCategoryId,
                    Author = _nickName,
                    Description = _description
                };
                var isResultSuccessful = await _blogService.AddNewPost(post);
                if (isResultSuccessful)
                {
                    _userDialogs.Toast(Strings.SuccessPost);
                }
                if (!isResultSuccessful)
                {
                    _userDialogs.Toast(Strings.ErrorPost);
                }
                await DisposeView(this);
                await NavigationService.Navigate<UserPostsViewModel>();
            }
        }
        public async Task GoBack()
        {
            await DisposeView(this);

        }
        private GetAllCategoryResponseModel _selectedItem = new GetAllCategoryResponseModel();

        public GetAllCategoryResponseModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }
        private async Task OpenDialogAsync()
        {
            await NavigationService.Navigate<CategoryDialogViewModel>();
        }
        private async Task ItemSelectedAsync(GetAllCategoryResponseModel category)
        {
            _selectedCategoryId = category.Id;
        }
        public void Validate()
        {
            isModelValid = true; 
            if (string.IsNullOrEmpty(_title) || string.IsNullOrEmpty(_description) || string.IsNullOrEmpty(_postContent) ||_selectedItem==null)
            {
                _userDialogs.Toast(Strings.EmptyField);
                isModelValid = false;
            }
        }
    }


}