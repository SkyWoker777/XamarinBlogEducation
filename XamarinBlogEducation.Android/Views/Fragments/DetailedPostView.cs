using Android.OS;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System.Threading.Tasks;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class DetailedPostView : BaseFragment<DetailedPostViewModel>
    {
        private TextView content;
        protected override int FragmentId => Resource.Layout.DetailedPostView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view= base.OnCreateView(inflater, container, savedInstanceState);
            content = view.FindViewById<TextView>(Resource.Id.textViewContent);
            content.VerticalScrollBarEnabled = true;
            content.MovementMethod = new ScrollingMovementMethod();
            return view;
        }
       
        
    }
}