using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.ViewModels.Blog.Items;
using XamarinBlogEducation.ViewModels.Models.Blog;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class CreatePostViewModel : BaseViewModel
    {
        private string _title;
        private long _selectedCategoryId;
        private string _postContent;
        private readonly IBlogService _blogService;
        private CreatePostBlogViewModel post;
        public CreatePostViewModel(IBlogService blogService, IMvxNavigationService _navigationService) : base(_navigationService)
        {
            _blogService = blogService;

            CategoryItems = new MvxObservableCollection<GetAllCategoriesblogViewItem>();

            AddNewPostCommand = new MvxAsyncCommand(AddNewPost);
            OpenDialogCommand = new MvxAsyncCommand(OpenDialogAsync);
        }
        public override  Task Initialize()
        {
            LoadCategoriesTask = MvxNotifyTask.Create(LoadCategories);
            return Task.FromResult(0);

        }
        public MvxNotifyTask LoadCategoriesTask { get; private set; }
        public IMvxCommand OpenDialogCommand { get; private set; }

        private MvxObservableCollection<GetAllCategoriesblogViewItem> _allCategories;
        public MvxObservableCollection<GetAllCategoriesblogViewItem> CategoryItems
        {
            get => _allCategories;
            set
            {
                _allCategories = value;
                RaisePropertyChanged(() => CategoryItems);
            }
        }

        public IMvxCommand AddNewPostCommand { get; private set; }
        public IMvxCommand AddNewCategoryCommand { get; private set; }


        private async Task LoadCategories()
        {
          
            var result = await _blogService.GetAllCategories();
            List<GetAllCategoriesblogViewItem> categoriesToAdd = new List<GetAllCategoriesblogViewItem>();
            categoriesToAdd.AddRange(result);
            for (int i = 0; i < categoriesToAdd.Count; i++)
            {
                CategoryItems.Add(categoriesToAdd[i]);
            }
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

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
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
            post = new CreatePostBlogViewModel()
            {
                Title = _title,
                Content = _postContent,
                CategoriesId = SelectedCategoryId
                //ad other attributes
            };
            await _blogService.AddNewPost(post);
        }

        private GetAllCategoriesblogViewItem _selectedItem = new GetAllCategoriesblogViewItem();

        public GetAllCategoriesblogViewItem SelectedItem
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
            await _navigationService.Navigate<CategoryDialogViewModel>();
        }
    }


}
