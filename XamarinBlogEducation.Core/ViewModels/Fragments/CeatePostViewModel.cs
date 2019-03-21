using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
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
        private string _postContent;
        private readonly IBlogService _blogService;
        private CreatePostBlogViewModel post;
        public CreatePostViewModel(IBlogService blogService, IMvxNavigationService _navigationService) : base(_navigationService)
        {
            _blogService = blogService;

            CategoryItems = new MvxObservableCollection<GetAllCategoriesblogViewItem>();
            AddNewPostCommand = new MvxAsyncCommand(AddNewPost);
            SelectedCategoryCommand = new MvxAsyncCommand(SelectedCategory);

        }

        private Task SelectedCategory()
        {
            throw new NotImplementedException();
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
        public override Task Initialize()
        {

            LoadCategoriesTask = MvxNotifyTask.Create(LoadCategories);

            return Task.FromResult(0);
        }
        public async Task AddNewPost()
        {
            post = new CreatePostBlogViewModel()
            {
                Title = _title,
                Content = _postContent
                //ad other attributes
            };
            await _blogService.AddNewPost(post);
        }
        public MvxNotifyTask LoadCategoriesTask { get; private set; }
        private MvxObservableCollection<GetAllCategoriesblogViewItem> _allCategories;
        public MvxObservableCollection<GetAllCategoriesblogViewItem> CategoryItems
        {
            get
            {
                return _allCategories;
            }
            set
            {
                _allCategories = value;
                RaisePropertyChanged(() => CategoryItems);
            }
        }
        public IMvxCommand AddNewPostCommand { get; private set; }
        
        public IMvxCommand SelectedCategoryCommand { get; private set; }

        private async Task LoadCategories()
        {

            var result = await _blogService.GetAllCategories();
            CategoryItems.AddRange(result);
        }
        private async Task SelectedCategory(GetAllCategoriesblogViewItem selectedCategory)
        {
            //await _navigationService.Navigate<DetailedPostViewModel>();
            // await _navigationService.Navigate<DetailedPostViewModel, GetDetailsPostBlogView,System.Threading.CancellationToken>(selectedPost);
        }
        

    }
}
