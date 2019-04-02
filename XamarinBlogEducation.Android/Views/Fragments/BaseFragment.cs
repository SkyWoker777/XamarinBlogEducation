﻿using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    public abstract class BaseFragment : MvxFragment
    {
        private Toolbar _toolbar;
        private MvxActionBarDrawerToggle _drawerToggle;

        public MvxAppCompatActivity ParentActivity
        {
            get
            {
                return (MvxAppCompatActivity)Activity;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(FragmentId, null);
            _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            //if (_toolbar != null)
            //{
            //    ParentActivity.SetSupportActionBar(_toolbar);
            //    ParentActivity.SupportActionBar.SetDisplayShowTitleEnabled(false);
            //    ParentActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //    _drawerToggle = new MvxActionBarDrawerToggle(
            //        Activity,                               
            //        ((MainView)ParentActivity).DrawerLayout, 
            //        _toolbar,                             
            //        Resource.String.drawer_open,         
            //        Resource.String.drawer_close    
            //    );
            //    _drawerToggle.DrawerOpened += (object sender, ActionBarDrawerEventArgs e) => ((MainView)Activity)?.HideSoftKeyboard();
            //    ((MainView)ParentActivity).DrawerLayout.AddDrawerListener(_drawerToggle);

            //}
            return view;
        }

        protected abstract int FragmentId { get; }
    }
    public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}