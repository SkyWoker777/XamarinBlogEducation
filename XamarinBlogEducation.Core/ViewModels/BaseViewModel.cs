
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace XamarinBlogEducation.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected IMvxNavigationService NavigationService;
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public async Task DisposeView(IMvxViewModel model)
        {
           await NavigationService.Close(model);
        }
    }

    public abstract class BaseViewModel<TParameter> : MvxViewModel<TParameter>
            where TParameter : class
    {
        protected IMvxNavigationService NavigationService;

        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        public async Task DisposeView(IMvxViewModel model)
        {
            await NavigationService.Close(model);
        }
    }
}
