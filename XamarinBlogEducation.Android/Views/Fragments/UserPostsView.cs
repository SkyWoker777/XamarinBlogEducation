using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(AllPostsViewModel), Resource.Id.allposts_frame, false)]
    public class UserPostsView:BaseFragment<UserPostsViewModel>
    {
        private Button deleteButton;
        protected override int FragmentId => Resource.Layout.UserPostsView;      
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayShowTitleEnabled(true);
            ((AppCompatActivity)Activity).SupportActionBar.SetTitle(Resource.String.UserPostsTitle);         
            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.user_posts_recycler_view);
        
            var set = this.CreateBindingSet<UserPostsView, UserPostsViewModel>();
            set.Apply();
            return view;
        }

       
    }
    
}