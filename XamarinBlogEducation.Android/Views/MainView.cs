using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.SecureStorage;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTop)]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
   
        public DrawerLayout DrawerLayout { get; set; }
        private bool isUserExists;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MainView);        
            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);          
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            isUserExists = CrossSecureStorage.Current.HasKey("securityToken");
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayShowTitleEnabled(false);         
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);    
            }
         
           
        }
        public override bool OnSupportNavigateUp()
        {
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
                case Resource.Id.nav_add:
                    ViewModel.AddPostCommand.Execute();
                    break;
                case Resource.Id.nav_login:
                    ViewModel.LoginCommand.Execute();
                    break;
                case Resource.Id.nav_about:
                    ViewModel.AboutCommand.Execute();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        public override void OnBackPressed()
        {
          
        }
        private void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
                return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.navigation_drawer, menu);
            if (isUserExists )
            {   
                var lblLogin = menu.FindItem(Resource.Id.nav_login);
                lblLogin.SetVisible(false);
            }
            if (!isUserExists)
            {
                var lblAddPost = menu.FindItem(Resource.Id.nav_add);
                lblAddPost.SetVisible(false);
            }
            return true;
        }

        
    }
}