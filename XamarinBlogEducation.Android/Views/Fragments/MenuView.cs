﻿using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.SecureStorage;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(AllPostsViewModel), Resource.Id.navigation_frame)]

    public class MenuView : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;
        private IMenuItem _previousMenuItem;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var ifUser = CrossSecureStorage.Current.HasKey("securityToken");
            var view = this.BindingInflate(Resource.Layout.MenuView, null);           
            if (ifUser==true)
            {
            _navigationView = view.FindViewById<NavigationView>(Resource.Id.menu_view);
            _navigationView.SetNavigationItemSelectedListener(this);
            _navigationView.Menu.FindItem(Resource.Id.menu_home).SetChecked(true);
            _navigationView.Menu.FindItem(Resource.Id.menu_profile).SetVisible(true);
            _navigationView.Menu.FindItem(Resource.Id.menu_exit).SetVisible(true);
             return view;
            }
            return null;
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
                case Resource.Id.menu_posts:
                    ViewModel.ShowHomeCommand.Execute(null);
                    break;
                case Resource.Id.menu_exit:
                    ViewModel.ExitCommand.Execute(null);
                    break;
                case Resource.Id.menu_addpost:
                    ViewModel.AddPostCommand.Execute(null);
                    break;
                case Resource.Id.menu_profile:
                    ViewModel.ShowProfileCommand.Execute(null);
                    break;
                case Resource.Id.menu_userPosts:
                    ViewModel.ShowUserPostsCommand.Execute(null);
                    break;
            }
        }
    }
}
