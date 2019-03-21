using Android.Content;
using Android.OS;
using Android.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class UserProfileView : BaseFragment<UserProfileViewModel>
    {
        protected override int FragmentId => Resource.Layout.UserProfileViewModel;
       
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var set = this.CreateBindingSet<UserProfileView, UserProfileViewModel>();
            //
            set.Apply();
            return view;
        }
        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {

            base.OnActivityResult(requestCode, resultCode, data);
           

        }

    }
}