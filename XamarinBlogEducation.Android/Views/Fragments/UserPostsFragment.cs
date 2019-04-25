using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(AllPostsBaseViewModel), Resource.Id.allposts_frame, false)]
    public class UserPostsFragment:BaseFragment<UserPostsViewModel>
    {  
        protected override int FragmentId => Resource.Layout.UserPostsView;      
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayShowTitleEnabled(true);
            ((AppCompatActivity)Activity).SupportActionBar.SetTitle(Resource.String.UserPostsTitle);         
            return view;
        }

       
    }
    
}