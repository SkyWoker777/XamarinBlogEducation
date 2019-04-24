using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using XamarinBlogEducation.Android.Extensions;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(AllPostsViewModel), Resource.Id.allposts_frame, true)]
    public class AllPostsFragment : BaseFragment<AllPostsFragmentViewModel>
    {
        private MvxAppCompatSpinner filterByCategorySpinner;
        private MvxAppCompatSpinner sortingSpinner;
        protected override int FragmentId => Resource.Layout.AllPostFragment;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            filterByCategorySpinner = view.FindViewById<MvxAppCompatSpinner>(Resource.Id.filterByCategorySpinner);
            filterByCategorySpinner.DropDownWidth = 500;
         //   LimitSpinner(filterByCategorySpinner, 500);

            sortingSpinner = view.FindViewById<MvxAppCompatSpinner>(Resource.Id.filterSpinner);
            sortingSpinner.DropDownWidth = 500;

            return view;
        }
        
    }
}