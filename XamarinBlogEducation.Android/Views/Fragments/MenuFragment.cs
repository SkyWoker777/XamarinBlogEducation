using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.SecureStorage;
using XamarinBlogEducation.Android.Views.Activities;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(AllPostsBaseViewModel), Resource.Id.navigation_frame)]

    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
       
        private NavigationView _navigationView;
        private IMenuItem _previousMenuItem;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var isUserExists = CrossSecureStorage.Current.HasKey("securityToken");
            var view = this.BindingInflate(Resource.Layout.MenuView, null);
            if (isUserExists)
            {
            _navigationView = view.FindViewById<NavigationView>(Resource.Id.menu_view);
            _navigationView.SetNavigationItemSelectedListener(this);
            _navigationView.Menu.FindItem(Resource.Id.menu_profile).SetVisible(true);
            _navigationView.Menu.FindItem(Resource.Id.menu_exit).SetVisible(true);
            return view;
            }
            return null;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if (_previousMenuItem != null)         
            _previousMenuItem = item;

           Navigate(item.ItemId);

            return true;
        }

        private async Task Navigate(int itemId)
        {

            ((AllPostsActivity)Activity).DrawerLayout.CloseDrawers();
            await Task.Delay(TimeSpan.FromMilliseconds(250));

            switch (itemId)
            {
                case Resource.Id.menu_home:
                    ((AppCompatActivity)Activity).SupportActionBar.SetTitle(Resource.String.AllPostsTitle);
                    ViewModel.ShowHomeCommand.Execute();
                   
                    break;
                case Resource.Id.menu_exit:
                    ViewModel.ExitCommand.Execute();
                    break;
                case Resource.Id.menu_profile:
                    ViewModel.ShowProfileCommand.Execute();
                    break;
                case Resource.Id.menu_userPosts:
                    ViewModel.ShowUserPostsCommand.Execute();
                    break;
            }
        }
    }
}
