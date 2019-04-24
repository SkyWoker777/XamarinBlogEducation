
using Android.App;
using MvvmCross.Platforms.Android.Views;

namespace XamarinBlogEducation.Android.Views.Activities
{
   public  class BaseActivity: MvxActivity
    {
        public override bool OnNavigateUp()
        {
            Dispose();
            return base.OnNavigateUp();
        }
        public override bool OnNavigateUpFromChild(Activity child)
        {
            child.Dispose();
            return base.OnNavigateUpFromChild(child);
        }
    }
}