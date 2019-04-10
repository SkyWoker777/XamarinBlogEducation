﻿using System;
using Android.App;

using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.SecureStorage;
using XamarinBlogEducation.Android.Views.Fragments;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.Android.Views.Activities
{
    [MvxActivityPresentation]
    [Activity(Theme = "@style/AppTheme")]
    public class AllPostsView : MvxAppCompatActivity<AllPostsViewModel>
    {
        private MvxActionBarDrawerToggle _drawerToggle;
        public DrawerLayout DrawerLayout { get; set; }
        private bool ifUser;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AllPostsView);
            var toolbar = FindViewById<Toolbar>(Resource.Id.filters_toolbar);
            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.allpost_drawer);
            if (bundle == null)
            {
                ViewModel.ShowMenuViewModelCommand.Execute(null);
            }
            ifUser = CrossSecureStorage.Current.HasKey("securityToken");
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayShowTitleEnabled(true);
                SupportActionBar.SetTitle(Resource.String.AllPostsTitle);
                if (ifUser == true)
                {
                    DrawerLayout.Activated = true;
                    SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                    SupportActionBar.SetHomeAsUpIndicator(Android.Resource.Mipmap.icons8_menu_48);
                    _drawerToggle = new MvxActionBarDrawerToggle(this, DrawerLayout,
                        toolbar,
                        Resource.String.drawer_open,
                        Resource.String.drawer_close
                    );

                    _drawerToggle.DrawerOpened += (object sender, ActionBarDrawerEventArgs e) => (this)?.HideSoftKeyboard();
                    DrawerLayout.AddDrawerListener(_drawerToggle);
                }
            }
                var recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.posts_recycler_view);      
            var set = this.CreateBindingSet<AllPostsView, AllPostsViewModel>();
            set.Apply();
            
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Resource.Id.nav_about:
                    ViewModel.AboutCommand.Execute(null);
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
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
                DrawerLayout.CloseDrawers();
               
        }
        public void HideSoftKeyboard()
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
            if (ifUser == true)
            {
                var login = menu.FindItem(Resource.Id.nav_login);
                login.SetVisible(false);
            }
            if (ifUser != true)
            {
                var add = menu.FindItem(Resource.Id.nav_add);
                add.SetVisible(false);
            }
            return true;
        }
    }
}
