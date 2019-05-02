using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.ViewModels.Requests;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class CategoryDialogViewModel : BaseViewModel
    {
        private string _newCategory;
        private AddNewCategoryRequestModel category;
        private readonly IBlogService _blogService;
        private readonly IUserDialogs _userDialogs;
        public CategoryDialogViewModel(IBlogService blogService, IUserDialogs userDialogs, IMvxNavigationService navigationService) : base(navigationService)
        {
            _blogService = blogService;
            _userDialogs = userDialogs;
            AddCategoryCommand = new MvxAsyncCommand(AddCategoryAsync);
            GoBackCommand = new MvxAsyncCommand(async () => await DisposeView(this));


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
            category = new AddNewCategoryRequestModel()
            {

                Category = _newCategory

            };
            bool isResultSuccessful = await _blogService.AddNewCategory(category);
            if (isResultSuccessful)
            {
                _userDialogs.Toast(Strings.SuccessAddCategory);
                await DisposeView(this);
            }
            if (!isResultSuccessful)
            {
                _userDialogs.Toast(Strings.ErrorPost);
                await DisposeView(this);
            }


        }
    }
}
