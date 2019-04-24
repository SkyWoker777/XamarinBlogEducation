using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class CategoryDialogViewModel : BaseViewModel
    {
        private string _newCategory;
        private GetAllCategoriesblogViewItem category;
        private readonly IBlogService _blogService;
        public CategoryDialogViewModel(IBlogService blogService, IMvxNavigationService navigationService) : base(navigationService)
        {
            _blogService = blogService;
            AddCategoryCommand = new MvxAsyncCommand(AddCategoryAsync);
            GoBackCommand = new MvxAsyncCommand(async() => await DisposeView(this));


        }
        public IMvxCommand AddCategoryCommand { get; private set; }
        public IMvxCommand GoBackCommand { get; private set; }

        public string NewCategory
        {
            get => _newCategory;
            set
            {
                _newCategory = value;
                RaisePropertyChanged();
            }
        }
        private async Task AddCategoryAsync()
        {
            category = new GetAllCategoriesblogViewItem()
            {

                Category = _newCategory

            };
            var result = await _blogService.AddNewCategory(category);
            await DisposeView(this);

        }
    }
}
