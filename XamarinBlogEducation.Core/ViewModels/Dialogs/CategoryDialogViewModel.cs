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
    public class CategoryDialogViewModel : MvxViewModel
    {
        private string _newCategory;
        private GetAllCategoriesblogViewItem category;
        private readonly IBlogService _blogService;
        private IMvxNavigationService _navigationService;
        public CategoryDialogViewModel(IBlogService blogService, IMvxNavigationService navigationService)
        {
            _blogService = blogService;
            _navigationService = navigationService;
            AddCategoryCommand = new MvxAsyncCommand(AddCategoryAsync);
            GoBackCommand = new MvxAsyncCommand(GoBackAsync);


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
           
        }
        private async Task GoBackAsync()
        {
            await _navigationService.Navigate<CreatePostViewModel>();
        }
    }
}
