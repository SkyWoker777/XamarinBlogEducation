using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Refractored.Controls;
using System;
using System.IO;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.ViewModels;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(AllPostsBaseViewModel), Resource.Id.allposts_frame, true)]
    public class UserProfileFragment : BaseFragment<UserProfileViewModel>
    {
        private EditText editEmail;
        private EditText editUserName;
        private EditText editLastName;
        private Button btnApplyChanges;
        private Button btnChangePassword;
        protected override int FragmentId => Resource.Layout.UserProfileViewModel;
        public override void OnAttachFragment(global::Android.Support.V4.App.Fragment childFragment)
        {
            base.OnAttachFragment(childFragment);
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            ((AppCompatActivity)Activity).SupportActionBar.SetDisplayShowTitleEnabled(true);
            ((AppCompatActivity)Activity).SupportActionBar.SetTitle(Resource.String.UserProfileTitle);
           
            editEmail = view.FindViewById<EditText>(Resource.Id.editEmail);
            editUserName = view.FindViewById<EditText>(Resource.Id.editUserName);
            editLastName = view.FindViewById<EditText>(Resource.Id.editLastName);
            btnApplyChanges = view.FindViewById<Button>(Resource.Id.applyButton);
            btnChangePassword = view.FindViewById<Button>(Resource.Id.changePasswordButton);

            var set = this.CreateBindingSet<UserProfileFragment, UserProfileViewModel>();

            set.Bind(editEmail).To(vm => vm.Email);
            set.Bind(editLastName).To(vm => vm.LastName);
            set.Bind(editUserName).To(vm => vm.FirstName);           
            set.Bind(btnChangePassword).To(vm => vm.ChangePasswordCommand);

            set.Apply();

            btnApplyChanges.Click += applyButton_OnClick;
            return view;
        }
       
        private void applyButton_OnClick(object sender, EventArgs e)
        {
            ViewModel.UpdateCommand.Execute();
            var toast =Strings.ProfileChangesMessage;
            Toast.MakeText(Context, toast, ToastLength.Long).Show();
            ViewModel.GoToPostsCommand.Execute();
        }

    }
}