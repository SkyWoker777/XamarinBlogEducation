using System;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;
using ListPopupWindow = Android.Widget.ListPopupWindow;
using Spinner = Android.Widget.Spinner;

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
            //_toolbar = ((AppCompatActivity)Activity).FindViewById<Toolbar>(Resource.Id.toolbar);
            //_toolbar.NavigationClick += NavigationClick;
            return view;
        }

        private void NavigationClick(object sender, Toolbar.NavigationClickEventArgs e)
        {
            
         
        }
        public void LimitSpinner(MvxAppCompatSpinner mvxAppCompatSpinner,int height)
        {
            var jClass = Java.Lang.Class.FromType(typeof(Spinner));
            var mPopupField = jClass.GetDeclaredField("mPopup");
            mPopupField.Accessible = true;
            ListPopupWindow popupWindow = (ListPopupWindow)mPopupField.Get(mvxAppCompatSpinner);
            popupWindow.Height = height;
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
        public void LimitSpinner(MvxAppCompatSpinner mvxAppCompatSpinner, int height)
        {
            var jClass = Java.Lang.Class.FromType(typeof(Spinner));
            var mPopupField = jClass.GetDeclaredField("mPopup");
            mPopupField.Accessible = true;
            ListPopupWindow popupWindow = (ListPopupWindow)mPopupField.Get(mvxAppCompatSpinner);
            popupWindow.Height = height;
        }
    }
}