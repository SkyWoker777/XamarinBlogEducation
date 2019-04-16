using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Helpers;
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
        private long _selectedFilterId;
        public IEnumerable<GetAllPostsBlogViewItem> filteredPosts;
        public AllPostsFragmentViewModel(
            IBlogService blogService,
            IMvxNavigationService navigationService) : base(navigationService)
        {

            _blogService = blogService;
            CategoryItems = new MvxObservableCollection<GetAllCategoriesblogViewItem>();
            FilterItems = new MvxObservableCollection<Filter>();
            AllPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>();
            OriginalPostList = new MvxObservableCollection<GetAllPostsBlogViewItem>();      
            PostSelectedCommand = new MvxAsyncCommand<GetAllPostsBlogViewItem>(PostSelected);
            ItemSelectedCommand = new MvxCommand<GetAllCategoriesblogViewItem>(ItemSelectedAsync);
            FilterSelectedCommand = new MvxCommand<Filter>(FilterSelectedAsync);

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
            LoadFiltersTask = MvxNotifyTask.Create(LoadFilters);
            LoadCategoriesTask = MvxNotifyTask.Create(LoadCategories);
            LoadPostsTask = MvxNotifyTask.Create(LoadPosts);
            return Task.FromResult(0);
        }
        private async Task LoadPosts()
        {
            var result = await _blogService.GetAllPosts();
            AllPosts.AddRange(result);
            OriginalPostList.AddRange(result);
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
        private async Task LoadFilters()
        {
            var filters = new AllFilters();
            FilterItems.Add(new Filter
            {
               Name="Default",
               Key=0
            });
            FilterItems.AddRange(filters.list);
        }

        public IMvxCommand ItemSelectedCommand { get; private set; }
        public IMvxCommand FilterSelectedCommand { get; private set; }      
        public IMvxCommand<GetAllPostsBlogViewItem> PostSelectedCommand { get; private set; }
        public IMvxCommand FetchPostCommand { get; private set; }
        public IMvxCommand RefreshPostsCommand { get; private set; }

        public MvxNotifyTask LoadPostsTask { get; private set; }
        public MvxNotifyTask FetchPostsTask { get; private set; }
        public MvxNotifyTask LoadCategoriesTask { get; private set; }
        public MvxNotifyTask LoadFiltersTask { get; private set; }

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
        private MvxObservableCollection<Filter> _allFilters;
        public MvxObservableCollection<Filter> FilterItems
        {
            get => _allFilters;
            set
            {
                _allFilters = value;
                RaisePropertyChanged(() => FilterItems);
            }
        }

        private async Task SortByAlphabet()
        {
            AllPosts = OriginalPostList;
            if(SelectedCategoryId!=0)
            { filteredPosts = OriginalPostList.Where(p=>p.CategoryId== SelectedCategoryId).OrderBy(t => t.Title); }
            else
            filteredPosts = OriginalPostList.OrderBy(t => t.Title);
            AllPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>(filteredPosts);
        }
        private async Task SortByDate()
        {
            AllPosts = OriginalPostList;
            if (SelectedCategoryId != 0)
            { filteredPosts = OriginalPostList.Where(p => p.CategoryId == SelectedCategoryId).OrderBy(d => d.CreationDate); }
            else
            filteredPosts = OriginalPostList.OrderBy(d => d.CreationDate);
            AllPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>(filteredPosts);
        }
        private async Task SortByDateDesc()
        {
            AllPosts = OriginalPostList;
            if (SelectedCategoryId != 0)
            { filteredPosts = OriginalPostList.Where(p => p.CategoryId == SelectedCategoryId).OrderByDescending(d => d.CreationDate); }
            else
            filteredPosts = OriginalPostList.OrderByDescending(d => d.CreationDate);
            AllPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>(filteredPosts);
        }
        private async Task PostSelected(GetAllPostsBlogViewItem selectedPost)
        {
            await NavigationService.Navigate<DetailedPostViewModel, GetAllPostsBlogViewItem>(selectedPost);
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
        public long SelectedFilterId
        {
            get => _selectedFilterId;
            set
            {
                _selectedFilterId = value;
                RaisePropertyChanged();
                switch (SelectedFilterId)
                {
                    case 0:
                        if (SelectedCategoryId != 0)
                        {
                            IEnumerable<GetAllPostsBlogViewItem> filteredPosts = OriginalPostList.Where(x => x.CategoryId == value);
                            AllPosts = new MvxObservableCollection<GetAllPostsBlogViewItem>(filteredPosts);
                        }
                        if (SelectedCategoryId == 0)
                        {
                            AllPosts = OriginalPostList;
                        }
                        break;
                    case 1:
                        SortByDate();
                        break;
                    case 2:
                        SortByAlphabet();
                        break;
                    case 3:
                        SortByDateDesc();
                        break;
                       
                }              
            }
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
        private Filter _selectedFilter = new Filter();
        public Filter SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                RaisePropertyChanged(() => SelectedFilter);
            }
        }
        private void ItemSelectedAsync(GetAllCategoriesblogViewItem category)
        {
            SelectedCategoryId = category.Id;
        }
       
        private void FilterSelectedAsync(Filter filter)
        {
            SelectedFilterId = filter.Key;
        }
       
    }
}
