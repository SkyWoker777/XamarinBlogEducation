﻿using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinBlogEducation.Core.ViewModels.Fragments
{
    public class AboutViewModel: BaseViewModel
    {
        public AboutViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {
            GoBackCommand = new MvxAsyncCommand(async () => await DisposeView(this));
        }
        public IMvxCommand GoBackCommand { get; private set; }
    }
}