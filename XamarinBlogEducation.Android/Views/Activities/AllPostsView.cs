using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using XamarinBlogEducation.Android.Extensions;
using XamarinBlogEducation.Android.Views.Fragments;
using XamarinBlogEducation.Core.ViewModels;
using ActionBar = Android.Support.V7.App.ActionBar;

namespace XamarinBlogEducation.Android.Views.Activities
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, false)]
    public class AllPostsView : BaseFragment<AllPostsViewModel>
    {

        protected override int FragmentId => Resource.Layout.AllPostsView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayShowTitleEnabled(true);
            ((AppCompatActivity)Activity).SupportActionBar.SetTitle(Resource.String.AllPostsTitle);
            //TODO:set gravity
       
            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.posts_recycler_view);
            //if (recyclerView != null)
            //{
            //    recyclerView.HasFixedSize = true;
            //    var layoutManager = new LinearLayoutManager(Activity);
            //    recyclerView.SetLayoutManager(layoutManager);

            //    recyclerView.AddOnScrollFetchItemsListener(layoutManager, () => ViewModel.FetchPostsTask, () => this.ViewModel.FetchPostCommand);
            //}
            var set = this.CreateBindingSet<AllPostsView, AllPostsViewModel>();
            set.Apply();
            return view;
        }

    }
}
