using Android.OS;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Views;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    public class NavigationBlogView : MvxActivity<NavigationBlogViewModel>
    {
        private readonly IMvxNavigationService _navigationService;
        public NavigationBlogView(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }
    }
}