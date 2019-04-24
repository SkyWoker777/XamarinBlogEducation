using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Text;
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
        private TextView txtPostContent;
        private EditText inpComment;
        private TextView msgCantLeaveComment;
        private TextView msgLeaveComment;
        private Button btnAddComment;
        private MvxRecyclerView recyclerView;
        protected override int FragmentId => Resource.Layout.DetailedPostView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var view = base.OnCreateView(inflater, container, savedInstanceState);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayShowTitleEnabled(true);
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
            txtPostContent = view.FindViewById<TextView>(Resource.Id.textViewContent);
            txtPostContent.VerticalScrollBarEnabled = true;          
            txtPostContent.MovementMethod = new ScrollingMovementMethod();
            inpComment = view.FindViewById<EditText>(Resource.Id.inputComment);
            
            msgCantLeaveComment = view.FindViewById<TextView>(Resource.Id.cantLeaveCommentMessage);
            var coomentLayout = view.FindViewById<LinearLayout>(Resource.Id.coomentLayout);
            btnAddComment = view.FindViewById<Button>(Resource.Id.addCommentButton);
            if (CrossSecureStorage.Current.HasKey("securityToken") == true)
            {
                msgCantLeaveComment.Visibility = ViewStates.Gone;
                
            }
            if (CrossSecureStorage.Current.HasKey("securityToken") == false)
            {
                msgCantLeaveComment.Visibility = ViewStates.Visible;
                coomentLayout.Visibility = ViewStates.Gone;
            }
            btnAddComment.Click += btnAddComment_OnClick;
            var set = this.CreateBindingSet<DetailedPostView, DetailedPostViewModel>();
            set.Bind(inpComment).To(vm => vm.Content);
            set.Apply();
            return view;
        }

        private void btnAddComment_OnClick(object sender, EventArgs e)
        {
            if (inpComment.Text == null)
            {
                var toast = "Please, write something";
                Toast.MakeText(Context, toast, ToastLength.Long).Show();
            }
            if (inpComment.Text != null)
            {
                ViewModel.AddCommentCommand.Execute();
                var toast = "Your comment was successfuly added";
                Toast.MakeText(Context, toast, ToastLength.Long).Show();
                inpComment.Text = "";
            }
            
        }

    }
}