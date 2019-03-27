
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace XamarinBlogEducation.Core.ViewModels.Activities
{
   public abstract class BaseViewModel : MvxViewModel
   {
        public IMvxNavigationService _navigationService;
        protected BaseViewModel(IMvxNavigationService navigationService)
            {
            _navigationService = navigationService;
            }
    }

    public abstract class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>
            where TParameter : class
            where TResult : class
    {
        public IMvxNavigationService _navigationService;

        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

    }
}
