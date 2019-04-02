using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.SecureStorage;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.navigation_frame)]

    public class MenuView : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;
        private IMenuItem _previousMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.MenuView, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            _navigationView.SetNavigationItemSelectedListener(this);
            _navigationView.Menu.FindItem(Resource.Id.nav_home).SetChecked(true);
            if (CrossSecureStorage.Current.GetValue("securityToken") == null)
            {             
                _navigationView.Menu.FindItem(Resource.Id.nav_profile).SetVisible(false);
                _navigationView.Menu.FindItem(Resource.Id.nav_login).SetVisible(true);
                _navigationView.Menu.FindItem(Resource.Id.nav_exit).SetVisible(false);
                
            }
            if (CrossSecureStorage.Current.GetValue("securityToken") != null)
            {
                _navigationView.Menu.FindItem(Resource.Id.nav_profile).SetVisible(true);
                _navigationView.Menu.FindItem(Resource.Id.nav_login).SetVisible(false);
                _navigationView.Menu.FindItem(Resource.Id.nav_exit).SetVisible(true);
            }
            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if (_previousMenuItem != null)
                _previousMenuItem.SetChecked(false);

            item.SetCheckable(true);
            item.SetChecked(true);

            _previousMenuItem = item;

            Navigate(item.ItemId);

            return true;
        }

        private async Task Navigate(int itemId)
        {
            
            ((MainView)Activity).DrawerLayout.CloseDrawers();
            await Task.Delay(TimeSpan.FromMilliseconds(250));

            switch (itemId)
            {
                case Resource.Id.nav_posts:
                    ViewModel.ShowHomeCommand.Execute(null);
                    break;
                case Resource.Id.nav_home:
                    ViewModel.ShowHomeCommand.Execute(null);
                    break;
                case Resource.Id.nav_profile:
                    ViewModel.ShowProfileCommand.Execute(null);
                    break;
                case Resource.Id.nav_exit:
                    ViewModel.ExitCommand.Execute(null);
                    break;
                case Resource.Id.nav_login:
                    ViewModel.LoginCommand.Execute(null);
                    break;
                case Resource.Id.nav_addpost:
                    ViewModel.AddPostCommand.Execute(null);
                    break;
            }
        }
    }
}
