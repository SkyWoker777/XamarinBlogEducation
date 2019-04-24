using System;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class EditPostView: BaseFragment<EditPostViewModel>
    {
        private EditText inpUpdatedContent;
        private EditText inpUndatedDescription;
        private EditText inpUpdatedTitle;
        private Button btnDeletePost;
        private Button btnCancelEditPost;
        private Button btnSaveEditPost;
        private LinearLayout linearLayout;
        protected override int FragmentId => Resource.Layout.EditPostView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayShowTitleEnabled(true);
            ((AppCompatActivity)Activity).SupportActionBar.SetTitle(Resource.String.EditPostTitle);
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

            inpUpdatedContent = view.FindViewById<EditText>(Resource.Id.editContent);
            inpUpdatedTitle = view.FindViewById<EditText>(Resource.Id.editTitle);
            inpUndatedDescription = view.FindViewById<EditText>(Resource.Id.editDescription);
            linearLayout = view.FindViewById<LinearLayout>(Resource.Id.editPostLayout);
            btnSaveEditPost = view.FindViewById<Button>(Resource.Id.saveEditPostButton);
            btnCancelEditPost = view.FindViewById<Button>(Resource.Id.canselEditPostButton);
            btnDeletePost = view.FindViewById<Button>(Resource.Id.deletePostButton);
            linearLayout.VerticalScrollBarEnabled = true;
            inpUpdatedTitle.SetTextIsSelectable(true);

            btnCancelEditPost.Click += btnCancelEditPost_onClick;
            btnSaveEditPost.Click += btnSaveEditPost_onClick;
            btnDeletePost.Click+=new EventHandler(delegate (Object o, EventArgs a)
            {
                ViewModel.DeleteCommand.Execute();
            });
            return view;
        }
        private void btnSaveEditPost_onClick(object sender, EventArgs e)
        {
            ViewModel.SaveEditCommand.Execute();
            var toast = string.Format("All changes were saved");
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
            ViewModel.GoToPostsCommand.Execute();
        }      
        private void btnCancelEditPost_onClick(object sender, EventArgs e)
        {
            var toast = string.Format("No changes were saved");
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
            ViewModel.GoToPostsCommand.Execute();
        }
        
        
    }
}