using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Text.Method;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.SecureStorage;
using XamarinBlogEducation.Android.Extensions;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class DetailedPostView : BaseFragment<DetailedPostViewModel>
    {
        private Toolbar _toolbar;
        private TextView content;
        private EditText inputComment;
        private TextView cantLeaveCommentMessage;
        private TextView leaveCommentMessage;
        private Button addCommentButton;
        private MvxRecyclerView recyclerView;
        //private CommentsAdapter commentsAdapter;
        protected override int FragmentId => Resource.Layout.DetailedPostView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var view = base.OnCreateView(inflater, container, savedInstanceState);
            ((AppCompatActivity)Activity).SupportActionBar.SetTitle(Resource.String.DetailedPostTitle);
            if (Activity is MainView mainView)
            {
                mainView.BackButtonPressed += (s, e) =>
                {
                    var fragmentsCount = Activity.FragmentManager.BackStackEntryCount;
                    if (fragmentsCount > 1)
                    {
                        ViewModel.GoBackCommand?.Execute();
                    }
                    else
                    {
                        mainView.ViewModel.GoBackCommand?.Execute();
                    }
                };
            }
            
            recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.comments_recycler_view);
           // commentsAdapter = new CommentsAdapter((IMvxAndroidBindingContext)this.BindingContext);
            //recyclerView.Adapter = commentsAdapter;
            content = view.FindViewById<TextView>(Resource.Id.textViewContent);
            content.VerticalScrollBarEnabled = true;
            content.MovementMethod = new ScrollingMovementMethod();
            inputComment = view.FindViewById<EditText>(Resource.Id.inputComment);
            
            cantLeaveCommentMessage = view.FindViewById<TextView>(Resource.Id.cantLeaveCommentMessage);
            leaveCommentMessage = view.FindViewById<TextView>(Resource.Id.leaveCommentMessage);
            addCommentButton = view.FindViewById<Button>(Resource.Id.addCommentButton);
            if (CrossSecureStorage.Current.HasKey("securityToken") == true)
            {
                cantLeaveCommentMessage.Visibility = ViewStates.Invisible;
                leaveCommentMessage.Visibility = ViewStates.Visible;
            }
            if (CrossSecureStorage.Current.HasKey("securityToken") == false)
            {
                cantLeaveCommentMessage.Visibility = ViewStates.Visible;
                leaveCommentMessage.Visibility = ViewStates.Invisible;
                inputComment.Visibility = ViewStates.Invisible;
                addCommentButton.Visibility = ViewStates.Invisible;
            }
            addCommentButton.Click += addCommentButton_OnClick;
            var set = this.CreateBindingSet<DetailedPostView, DetailedPostViewModel>();
            set.Bind(inputComment).To(vm => vm.Content);
            set.Apply();
            return view;
        }

        private void addCommentButton_OnClick(object sender, EventArgs e)
        {
            if (inputComment.Text == null)
            {
                string toast = "Please, write something";
                Toast.MakeText(Context, toast, ToastLength.Long).Show();
            }
            if (inputComment.Text != null)
            {
                ViewModel.AddCommentCommand.Execute();
                string toast = "Your comment was successfuly added";
                Toast.MakeText(Context, toast, ToastLength.Long).Show();
                inputComment.Text = "";
            }
            
        }

    }
}