using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.ViewModels;
using XamarinBlogEducation.Android.Views;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.Android.Services
{
    public class NavigationService
    {
        private MainView mainView;
        public NavigationService(BaseViewModel model)
        {
            mainView.BackButtonPressed += async (s, e) =>
            {
            
                var fragmentsCount = mainView.FragmentManager.BackStackEntryCount;
                if (fragmentsCount > 1)
                {
                    await model.DisposeView(model);
                }
                else
                {
                    mainView.ViewModel.GoBackCommand?.Execute();
                }
            };
        }
        
    }
}