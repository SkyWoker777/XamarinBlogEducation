﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.Android.Views.Activities
{
    [MvxActivityPresentation]
    [Activity(MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class Start : MvxAppCompatActivity<StartViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginView);
            ViewModel.LoginCommand.Execute();
        }
        public override void OnBackPressed()
        {

        }
    }
}