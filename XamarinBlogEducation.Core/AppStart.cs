﻿using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.ViewModels.Activities;

namespace XamarinBlogEducation.Core
{
   public class AppStart: MvxAppStart
    {
        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService)
           : base(app, mvxNavigationService)
        {
        }      
        protected override Task NavigateToFirstViewModel(object hint = null)
        {
           return NavigationService.Navigate<LoginViewModel>();
           
        }
    }
}
