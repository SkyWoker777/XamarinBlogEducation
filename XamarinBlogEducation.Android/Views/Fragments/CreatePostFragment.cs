using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;
using Android.Text.Method;
using Plugin.SecureStorage;
using Android.Support.V7.App;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using XamarinBlogEducation.Android.Extensions;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class CreatePostFragment : BaseFragment<CreatePostViewModel>
    {
        private EditText inpTitle;
        private EditText inpPostContent;
        private EditText inpPostDescription;
        private EditText inpNickName;
        private TextView txtAnonimPostWarning;
        private Button btnAddNewPost;
        private Button btnAddCategory;
        private MvxAppCompatSpinner mvxSpinner;
        protected override int FragmentId => Resource.Layout.NewPost;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view= base.OnCreateView(inflater, container, savedInstanceState);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayShowTitleEnabled(true);
            ((AppCompatActivity)Activity).SupportActionBar.SetTitle(Resource.String.CreatePostTitle);
            btnAddNewPost = view.FindViewById<Button>(Resource.Id.addNewPostButton);
            btnAddCategory = view.FindViewById<Button>(Resource.Id.addCategoryButton);
            mvxSpinner = view.FindViewById<MvxAppCompatSpinner>(Resource.Id.allCategoriesSpinner);
            mvxSpinner.LimitSpinner(500);
            inpTitle = view.FindViewById<EditText>(Resource.Id.inputTitle);
            inpPostContent = view.FindViewById<EditText>(Resource.Id.inputPostContent);
            inpPostContent.VerticalScrollBarEnabled = true;
            inpPostContent.MovementMethod = new ScrollingMovementMethod();
            inpNickName= view.FindViewById<EditText>(Resource.Id.inputNickName);
            inpPostDescription = view.FindViewById<EditText>(Resource.Id.inputPostDescription);
            txtAnonimPostWarning= view.FindViewById<TextView>(Resource.Id.anonimPostWarning);
            inpPostDescription.VerticalScrollBarEnabled = true;
            inpPostDescription.MovementMethod = new ScrollingMovementMethod();

            if (CrossSecureStorage.Current.HasKey("securityToken")==false)
            {
                txtAnonimPostWarning.Visibility = ViewStates.Visible;
            }

            var set = this.CreateBindingSet<CreatePostFragment, CreatePostViewModel>();

            set.Bind(inpTitle).To(vm => vm.Title);
            set.Bind(inpPostContent).To(vm => vm.PostContent);
            set.Bind(inpNickName).To(vm => vm.NickName);
            set.Bind(inpPostDescription).To(vm => vm.Description);
            set.Bind(btnAddCategory).To(vm => vm.OpenDialogCommand);

            set.Apply();

            btnAddNewPost.Click += btnNewPost_OnClick;
            return view;
        }
      
      

        private void btnNewPost_OnClick(object sender, EventArgs e)
        {
            ViewModel.AddNewPostCommand.Execute();
            
            var toast = "Your post was successfuly added";
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
        }
       
    }
}