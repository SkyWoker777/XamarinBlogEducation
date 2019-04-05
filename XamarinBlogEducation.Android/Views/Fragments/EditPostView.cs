using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame, true)]
    public class EditPostView: BaseFragment<EditPostViewModel>
    {
        private EditText content;
        private EditText description;
        private EditText title;
        private Button canselEditPostButton;
        private Button saveEditPostButton;
        private LinearLayout linearLayout;
        protected override int FragmentId => Resource.Layout.EditPostView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
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

            content = view.FindViewById<EditText>(Resource.Id.editContent);
            title = view.FindViewById<EditText>(Resource.Id.editTitle);
            description = view.FindViewById<EditText>(Resource.Id.editDescription);
            linearLayout = view.FindViewById<LinearLayout>(Resource.Id.editPostLayout);
            saveEditPostButton = view.FindViewById<Button>(Resource.Id.saveEditPostButton);
            canselEditPostButton = view.FindViewById<Button>(Resource.Id.canselEditPostButton);
            linearLayout.VerticalScrollBarEnabled = true;
           
            
            canselEditPostButton.Click += canselEditPostButton_onClick;
            saveEditPostButton.Click += saveEditPostButton_onClick;
            return view;
        }

        private void saveEditPostButton_onClick(object sender, EventArgs e)
        {
            ViewModel.SaveEditCommand.Execute(null);
            string toast = string.Format("All changes were saved");
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
            ViewModel.GoToPostsCommand.Execute();
        }

        private void canselEditPostButton_onClick(object sender, EventArgs e)
        {
            string toast = string.Format("No changes were saved");
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
            ViewModel.GoToPostsCommand.Execute();
        }
    }
}