using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using XamarinBlogEducation.Android.Views.Fragments;
using XamarinBlogEducation.Core.ViewModels.Activities;

namespace XamarinBlogEducation.Android.Views.Activities
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    public class AllPostsView : BaseFragment<AllPostsViewModel>
    {
        private Button addPostButton;
        protected override int FragmentId => Resource.Layout.AllPostsView;
        


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.posts_recycler_view);
            addPostButton = view.FindViewById<Button>(Resource.Id.addPostButton);
            var set = this.CreateBindingSet<AllPostsView, AllPostsViewModel>();
      
            set.Apply();
            addPostButton.Click += addPostButton_OnClickAsync;          
            return view;
        }
        private void addPostButton_OnClickAsync(object sender, EventArgs e)
        {
            ViewModel.AddPostCommand.Execute();
        }
    }
}
