using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Helpers;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class AllPostsViewModel : BaseViewModel
    {
        private readonly IBlogService _blogService;
        private string _title;
        private string _description;
        private long _selectedCategoryId;
        private long _selectedFilterId;
        private GetAllCategoryResponseModel _selectedCategory;
        private GetAllPostResponseModel _selectedPost;
        public IEnumerable<GetAllPostResponseModel> filteredPosts;
        public AllPostsViewModel(
            IBlogService blogService,
            IMvxNavigationService navigationService) : base(navigationService)
        {

            _blogService = blogService;
            CategoryItems = new MvxObservableCollection<GetAllCategoryResponseModel>();
            FilterItems = new MvxObservableCollection<Filter>();
            AllPosts = new MvxObservableCollection<GetAllPostResponseModel>();
            OriginalPostList = new MvxObservableCollection<GetAllPostResponseModel>();
            PostSelectedCommand = new MvxAsyncCommand<GetAllPostResponseModel>(PostSelected);
            ItemSelectedCommand = new MvxCommand<GetAllCategoryResponseModel>(ItemSelectedAsync);
            FilterSelectedCommand = new MvxCommand<Filter>(FilterSelectedAsync);
            ShowMenu = new MvxAsyncCommand((async () => await NavigationService.Navigate<MenuViewModel>()));
            FetchPostCommand = new MvxCommand(
                () =>
                {

                    FetchPostsTask = MvxNotifyTask.Create(LoadPosts);
                    RaisePropertyChanged(() => FetchPostsTask);

                });
            RefreshPostsCommand = new MvxCommand(RefreshPosts);
            LoginCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<LoginViewModel>());
            AboutUsComand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AboutViewModel>());
            AddPostCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<CreatePostViewModel>());
        }

        public override async Task Initialize()
        {
            LoadFilters();
            LoadCategories();
            LoadPosts();
        }
        private async Task LoadPosts()
        {
            List<GetAllPostResponseModel> result = await _blogService.GetAllPosts();
            AllPosts.AddRange(result);
            OriginalPostList.AddRange(result);
        }
        private async Task LoadCategories()
        {
            CategoryItems.Add(new GetAllCategoryResponseModel
            {
                Category = "All"
            });
            List<GetAllCategoryResponseModel> result = await _blogService.GetAllCategories();
            CategoryItems.AddRange(result.OrderBy(r => r.Category));
        }
        private async Task LoadFilters()
        {
            AllFilters filters = new AllFilters();
            FilterItems.Add(new Filter
            {
                Name = "Default",
                Key = 0
            });
            FilterItems.AddRange(filters.list);
        }
        public IMvxCommand AddPostCommand { get; private set; }
        public IMvxCommand AboutUsComand { get; private set; }
        public IMvxCommand LoginCommand { get; private set; }
        public IMvxCommand ItemSelectedCommand { get; private set; }
        public IMvxCommand FilterSelectedCommand { get; private set; }
        public IMvxCommand<GetAllPostResponseModel> PostSelectedCommand { get; private set; }
        public IMvxCommand FetchPostCommand { get; private set; }
        public IMvxCommand RefreshPostsCommand { get; private set; }
        public IMvxCommand ShowMenu { get; private set; }
        public MvxNotifyTask LoadPostsTask { get; private set; }
        public MvxNotifyTask FetchPostsTask { get; private set; }
        public MvxNotifyTask LoadCategoriesTask { get; private set; }
        public MvxNotifyTask LoadFiltersTask { get; private set; }

        private MvxObservableCollection<GetAllPostResponseModel> _originalPostList;
        public MvxObservableCollection<GetAllPostResponseModel> OriginalPostList
        {
            get => _originalPostList;
            set
            {
                _originalPostList = value;
                RaisePropertyChanged(() => OriginalPostList);
            }
        }

        private MvxObservableCollection<GetAllPostResponseModel> _allPosts;
        public MvxObservableCollection<GetAllPostResponseModel> AllPosts
        {
            get => _allPosts;
            set
            {
                _allPosts = value;
                RaisePropertyChanged(() => AllPosts);
            }
        }
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
            if (_selectedCategoryId != 0)
            { filteredPosts = OriginalPostList.Where(p => p.CategoryId == _selectedCategoryId).OrderBy(t => t.Title); }
            else
            {
                filteredPosts = OriginalPostList.OrderBy(t => t.Title);
            }

            AllPosts = new MvxObservableCollection<GetAllPostResponseModel>(filteredPosts);
        }
        private async Task SortByDate()
        {
            AllPosts = OriginalPostList;
            if (_selectedCategoryId != 0)
            { filteredPosts = OriginalPostList.Where(p => p.CategoryId == _selectedCategoryId).OrderBy(d => d.CreationDate); }
            else
            {
                filteredPosts = OriginalPostList.OrderBy(d => d.CreationDate);
            }

            AllPosts = new MvxObservableCollection<GetAllPostResponseModel>(filteredPosts);
        }
        private async Task SortByDateDesc()
        {
            AllPosts = OriginalPostList;
            if (_selectedCategoryId != 0)
            { filteredPosts = OriginalPostList.Where(p => p.CategoryId == _selectedCategoryId).OrderByDescending(d => d.CreationDate); }
            else
            {
                filteredPosts = OriginalPostList.OrderByDescending(d => d.CreationDate);
            }

            AllPosts = new MvxObservableCollection<GetAllPostResponseModel>(filteredPosts);
        }
        private async Task PostSelected(GetAllPostResponseModel selectedPost)
        {
            await NavigationService.Navigate<DetailedPostViewModel, GetAllPostResponseModel>(selectedPost);
        }
        public GetAllPostResponseModel SelectedPost
        {
            get => _selectedPost;
            set
            {
                _selectedPost = value;
                NavigationService.Navigate<DetailedPostViewModel, GetAllPostResponseModel>(_selectedPost);
            }
        }
        public long SelectedCategoryId
        {
            get => _selectedCategoryId;
            set
            {
                _selectedCategoryId = value;
                if (SelectedCategoryId != 0)
                {
                    IEnumerable<GetAllPostResponseModel> filteredPosts = OriginalPostList.Where(x => x.CategoryId == value);
                    AllPosts = new MvxObservableCollection<GetAllPostResponseModel>(filteredPosts);

                }
                if (SelectedCategoryId == 0)
                {
                    AllPosts = OriginalPostList;
                }
                RaisePropertyChanged();
            }
        }
        public GetAllCategoryResponseModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                SelectedCategoryId = _selectedCategory.Id;
                RaisePropertyChanged();
            }
        }
        public long SelectedFilterId
        {
            get => _selectedFilterId;
            set
            {
                _selectedFilterId = value;

                IEnumerable<GetAllPostResponseModel> sortedPosts;

                switch (SelectedFilterId)
                {
                    case 0:
                        sortedPosts = GetPostsByCategoryId(SelectedCategoryId);
                        AllPosts = new MvxObservableCollection<GetAllPostResponseModel>(sortedPosts);
                        break;
                    case 1:
                        sortedPosts = GetPostsByCategoryId(SelectedCategoryId).OrderBy(y => y.CreationDate);
                        AllPosts = new MvxObservableCollection<GetAllPostResponseModel>(sortedPosts);
                        break;
                    case 2:
                        sortedPosts = GetPostsByCategoryId(SelectedCategoryId).OrderBy(y => y.Title);
                        AllPosts = new MvxObservableCollection<GetAllPostResponseModel>(sortedPosts);
                        break;
                    case 3:
                        sortedPosts = GetPostsByCategoryId(SelectedCategoryId).OrderByDescending(y => y.CreationDate);
                        AllPosts = new MvxObservableCollection<GetAllPostResponseModel>(sortedPosts);
                        break;
                }
                RaisePropertyChanged();
            }
        }

        private IEnumerable<GetAllPostResponseModel> GetPostsByCategoryId(long categoryId)
        {
            if (categoryId == 0)
            {
                return OriginalPostList;
            }

            return OriginalPostList.Where(x => x.CategoryId == SelectedCategoryId);
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
        private Filter _selectedFilter = new Filter();
        public Filter SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                SelectedFilterId = _selectedFilter.Key;
                RaisePropertyChanged(() => SelectedFilter);
            }
        }
        private void ItemSelectedAsync(GetAllCategoryResponseModel category)
        {
            SelectedCategoryId = category.Id;
        }

        private void FilterSelectedAsync(Filter filter)
        {
            SelectedFilterId = filter.Key;
        }
    }
}
