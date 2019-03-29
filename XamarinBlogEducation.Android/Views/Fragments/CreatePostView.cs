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
using XamarinBlogEducation.Android.Elements;
using Android.Support.V4.App;
using System.Windows.Input;
using Android.Text.Method;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class CreatePostView : BaseFragment<CreatePostViewModel>
    {
        private EditText inputTitle;
        private EditText inputPostContent;
        private EditText inputPostDescription;
        private EditText inputNickName;
        //private long categoryId;
        private Button addNewPostButton;
        private Button addCategoryButton;
        private MvxAppCompatSpinner mvxSpinner;
        protected override int FragmentId => Resource.Layout.NewPost;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view= base.OnCreateView(inflater, container, savedInstanceState);

            addNewPostButton = view.FindViewById<Button>(Resource.Id.addNewPostButton);
            addCategoryButton = view.FindViewById<Button>(Resource.Id.addCategoryButton);
            mvxSpinner = view.FindViewById<MvxAppCompatSpinner>(Resource.Id.allCategoriesSpinner);
            inputTitle = view.FindViewById<EditText>(Resource.Id.inputTitle);
            inputPostContent = view.FindViewById<EditText>(Resource.Id.inputPostContent);
            inputPostContent.VerticalScrollBarEnabled = true;
            inputPostContent.MovementMethod = new ScrollingMovementMethod();
            inputNickName= view.FindViewById<EditText>(Resource.Id.inputNickName);
            inputPostDescription = view.FindViewById<EditText>(Resource.Id.inputPostDescription);
            inputPostDescription.VerticalScrollBarEnabled = true;
            inputPostDescription.MovementMethod = new ScrollingMovementMethod();
            //categoryId= mvxSpinner.GetItemIdAtPosition(mvxSpinner.SelectedItemPosition);
            //categoryId = mvxSpinner.s;
            var set = this.CreateBindingSet<CreatePostView, CreatePostViewModel>();
            set.Bind(inputTitle).To(vm => vm.Title);
            set.Bind(inputPostContent).To(vm => vm.PostContent);
            set.Bind(inputNickName).To(vm => vm.NickName);
            set.Bind(inputPostDescription).To(vm => vm.Description);
            set.Bind(addCategoryButton).To(vm => vm.OpenDialogCommand);
           // set.Bind(addNewPostButton).To(vm => vm.AddNewPostCommand);
            //set.Bind(categoryId).To(vm => vm.SelectedCategoryId);
            set.Apply();
            addNewPostButton.Click += addNewPostButton_OnClick;
            return view;
        }

        private void addNewPostButton_OnClick(object sender, EventArgs e)
        {
            ViewModel.AddNewPostCommand.Execute();
            
            string toast = "Your post was successfuly added";
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
            ViewModel.GoBackCommand.Execute();
        }
    }
}