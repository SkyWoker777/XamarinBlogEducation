using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class AllPostsFragmentViewModel : BaseViewModel
    {
        private readonly IBlogService _blogService;
        private string _title;
        private string _description;
        private long _selectedCategoryId;
        public AllPostsFragmentViewModel(
            IBlogService blogService,
            IMvxNavigationService navigationService) : base(navigationService)
        {

            _blogService = blogService;
            CategoryItems = new MvxObservableCollection<GetAllCategoriesblogViewItem>();
            AllPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>();
            OriginalPostList = new MvxObservableCollection<GetAllPostsBlogViewItem>();
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<MenuViewModel>());
            GoBackCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AllPostsFragmentViewModel>());
            PostSelectedCommand = new MvxAsyncCommand<GetAllPostsBlogViewItem>(PostSelected);
            ItemSelectedCommand = new MvxCommand<GetAllCategoriesblogViewItem>(ItemSelectedAsync);
            FetchPostCommand = new MvxCommand(
                () =>
                {

                    FetchPostsTask = MvxNotifyTask.Create(LoadPosts);
                    RaisePropertyChanged(() => FetchPostsTask);

                });
            RefreshPostsCommand = new MvxCommand(RefreshPosts);
        }
        public MvxNotifyTask LoadCategoriesTask { get; private set; }
        public IMvxCommand ItemSelectedCommand { get; private set; }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }
        public override Task Initialize()
        {
            LoadCategoriesTask = MvxNotifyTask.Create(LoadCategories);
            LoadPostsTask = MvxNotifyTask.Create(LoadPosts);
            return Task.FromResult(0);
        }
        public MvxNotifyTask LoadPostsTask { get; private set; }

        public MvxNotifyTask FetchPostsTask { get; private set; }

        private MvxObservableCollection<GetAllPostsBlogViewItem> _originalPostList;
        public MvxObservableCollection<GetAllPostsBlogViewItem> OriginalPostList
        {
            get => _originalPostList;
            set
            {
                _originalPostList = value;
                RaisePropertyChanged(() => OriginalPostList);
            }
        }

        private MvxObservableCollection<GetAllPostsBlogViewItem> _allPosts;
        public MvxObservableCollection<GetAllPostsBlogViewItem> AllPosts
        {
            get => _allPosts;
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
            AllPosts.AddRange(result);
            OriginalPostList.AddRange(result);
        }
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
        private async Task LoadCategories()
        {
            CategoryItems.Add(new GetAllCategoriesblogViewItem
            {
                Category = "All"
            });
            var result = await _blogService.GetAllCategories();
            //result.OrderBy(r => r.Category);
            CategoryItems.AddRange(result.OrderBy(r => r.Category));

        }

        public long SelectedCategoryId
        {
            get => _selectedCategoryId;
            set
            {
                _selectedCategoryId = value;
                RaisePropertyChanged();
                if (SelectedCategoryId != 0)
                {
                    IEnumerable<GetAllPostsBlogViewItem> filteredPosts = OriginalPostList.Where(x => x.CategoryId == value);
                    AllPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>(filteredPosts);
                }
                if (SelectedCategoryId == 0)
                {
                    AllPosts = OriginalPostList;
                }
            }
        }

        private async Task PostSelected(GetAllPostsBlogViewItem selectedPost)
        {

            await NavigationService.Navigate<DetailedPostViewModel, GetAllPostsBlogViewItem>(selectedPost);
        }
        private void RefreshPosts()
        {
            LoadPostsTask = MvxNotifyTask.Create(LoadPosts);
            RaisePropertyChanged(() => LoadPostsTask);
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
        private void ItemSelectedAsync(GetAllCategoriesblogViewItem category)
        {
            SelectedCategoryId = category.Id;
        }
    }
}
