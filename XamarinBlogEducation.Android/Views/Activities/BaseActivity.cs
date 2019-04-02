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
using MvvmCross.Platforms.Android.Views;

namespace XamarinBlogEducation.Android.Views.Activities
{
   public  class BaseActivity: MvxActivity
    {
        public override bool OnNavigateUp()
        {
            base.Dispose();
            return base.OnNavigateUp();
        }
        public override bool OnNavigateUpFromChild(Activity child)
        {
            child.Dispose();
            return base.OnNavigateUpFromChild(child);
        }
    }
}