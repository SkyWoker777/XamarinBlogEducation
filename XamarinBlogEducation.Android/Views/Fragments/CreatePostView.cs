using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using XamarinBlogEducation.Core.ViewModels.Activities;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class CreatePostView : BaseFragment<CreatePostViewModel>
    {
        private EditText inputTitle;
        private EditText inputPostContent;
        private Button addNewPostButton;
        private MvxAppCompatSpinner mvxSpinner;
        protected override int FragmentId => Resource.Layout.NewPost;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view= base.OnCreateView(inflater, container, savedInstanceState);
            addNewPostButton = view.FindViewById<Button>(Resource.Id.addNewPostButton);
            mvxSpinner = view.FindViewById<MvxAppCompatSpinner>(Resource.Id.allCategoriesSpinner);
            inputTitle = view.FindViewById<EditText>(Resource.Id.inputTitle);
            inputPostContent = view.FindViewById<EditText>(Resource.Id.inputPostContent);

            var set = this.CreateBindingSet<CreatePostView, CreatePostViewModel>();
            set.Bind(inputTitle).To(vm => vm.Title);
            set.Bind(inputPostContent).To(vm => vm.PostContent);
            set.Apply();
            addNewPostButton.Click+= addNewPostButton_OnClickAsync;
            return view;
        }
        private void addNewPostButton_OnClickAsync(object sender, EventArgs e)
        {
            ViewModel.AddNewPostCommand.Execute();
        }
    }
}