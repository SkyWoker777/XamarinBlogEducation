using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class DetailedPostView : BaseFragment<DetailedPostViewModel>
    {
        private Toolbar _toolbar;
        private TextView content;
        protected override int FragmentId => Resource.Layout.DetailedPostView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
           
            var view= base.OnCreateView(inflater, container, savedInstanceState);

            if(Activity is MainView mainView)
            {
                mainView.BackButtonPressed += (s, e) =>
                {
                    var fragmentsCount = Activity.FragmentManager.BackStackEntryCount;
                    if(fragmentsCount > 1)
                    {
                        ViewModel.GoBackCommand?.Execute();
                    }
                    else
                    {
                        mainView.ViewModel.GoBackCommand?.Execute();
                    }
                };
            }
            // ((AppCompatActivity)Activity).SupportActionBar.SetHomeAsUpIndicator(Android.Resource.Mipmap.icons8_back_arrow_64);
            //((AppCompatActivity)Activity).SupportActionBar.SetHomeButtonEnabled(false);
            //((AppCompatActivity)Activity).SupportActionBar.SetDisplayShowHomeEnabled(false);

            //  _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            // _toolbar.NavigationOnClick += NavigationOnClick;
            content = view.FindViewById<TextView>(Resource.Id.textViewContent);
            content.VerticalScrollBarEnabled = true;
            content.MovementMethod = new ScrollingMovementMethod();
            return view;
        }

    }
}