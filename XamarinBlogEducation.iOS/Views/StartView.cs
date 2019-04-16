using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.iOS.Views
{
    [MvxRootPresentation]
    public class StartView: MvxViewController<StartViewModel>
    {
        public StartView()
        {

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
                ViewModel.LoginCommand.Execute(null);
            }
        }
    }
