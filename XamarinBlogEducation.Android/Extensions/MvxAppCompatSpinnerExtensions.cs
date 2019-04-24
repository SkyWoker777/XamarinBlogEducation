using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;

namespace XamarinBlogEducation.Android.Extensions
{
   public static class MvxAppCompatSpinnerExtensions
    {
        public static void LimitSpinner(this MvxAppCompatSpinner mvxAppCompatSpinner, int height)
        {
            var jClass = Java.Lang.Class.FromType(typeof(Spinner));
            var mPopupField = jClass.GetDeclaredField("mPopup");
            mPopupField.Accessible = true;
            ListPopupWindow popupWindow = (ListPopupWindow)mPopupField.Get(mvxAppCompatSpinner);
            popupWindow.Height = height;
        }
    }
}