using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using XamarinBlogEducation.Android.Views.Activities;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTop)]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {

        private MvxActionBarDrawerToggle _drawerToggle;
        public DrawerLayout DrawerLayout { get; set; }
        private bool ifUser;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainView);        
            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);          
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            ifUser = CrossSecureStorage.Current.HasKey("securityToken");
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                //SetActionBar(toolbar);
                SupportActionBar.SetDisplayShowTitleEnabled(false);         
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);    
            }
         
           
        }
        public override bool OnSupportNavigateUp()
        {

            //ViewModel.GoBackCommand.Execute();
            BackButtonPressed?.Invoke(this, EventArgs.Empty);
            return base.OnSupportNavigateUp();

        }

        public event EventHandler BackButtonPressed;

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.home:
                    ViewModel.GoBackCommand.Execute();
                    break;
                case Resource.Id.nav_posts:
                    ViewModel.ShowHomeCommand.Execute(null);
                    break;
                case Resource.Id.nav_add:
                    ViewModel.AddPostCommand.Execute(null);
                    break;
                case Resource.Id.nav_login:
                    ViewModel.LoginCommand.Execute(null);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        public override void OnBackPressed()
        {
          
        }
        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
                return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }
        private void ShowMenu(object sender, EventArgs e)
        {
            DrawerLayout.OpenDrawer(GravityCompat.Start);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.navigation_drawer, menu);
            if (ifUser == true)
            {   
                var login = menu.FindItem(Resource.Id.nav_login);
                login.SetVisible(false);
            }
            if (ifUser != true)
            {
                var login = menu.FindItem(Resource.Id.nav_add);
                login.SetVisible(false);
            }
            return true;
        }

        
    }
}