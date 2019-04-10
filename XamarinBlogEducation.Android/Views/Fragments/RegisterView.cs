using System;
using Android.OS;
using Android.Widget;
using Android.App;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Binding.BindingContext;
using XamarinBlogEducation.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Android.Views;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(LoginActivityViewModel), Resource.Id.login_content_frame, true)]
    public class RegisterView : BaseFragment<RegisterViewModel>
    {
        private EditText inputEmail;
        private EditText inputPassword;
        private EditText confirmPassword;
        private EditText inputUserName;
        private EditText inputLastName;
        private Button signUpButton;
        protected override int FragmentId => Resource.Layout.RegisterView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            inputEmail = view.FindViewById<EditText>(Resource.Id.inputEmail);
            inputPassword = view.FindViewById<EditText>(Resource.Id.inputPassword);
            confirmPassword = view.FindViewById<EditText>(Resource.Id.confirmPassword);
            inputUserName = view.FindViewById<EditText>(Resource.Id.inputUserName);
            inputLastName = view.FindViewById<EditText>(Resource.Id.inputLastName);
            signUpButton = view.FindViewById<Button>(Resource.Id.signUpButton);

            var set = this.CreateBindingSet<RegisterView, RegisterViewModel>();
            set.Bind(inputEmail).To(vm => vm.Email);
            set.Bind(inputPassword).To(vm => vm.Password);
            set.Bind(inputLastName).To(vm => vm.LastName);
            set.Bind(confirmPassword).To(vm => vm.ConfirmPassword);
            set.Bind(inputUserName).To(vm => vm.FirstName);
            set.Bind(signUpButton).To(vm => vm.RegistrateCommand);
            set.Apply();
            signUpButton.Click += signUpButton_OnClickAsync;
            return view;
        }     
        private void signUpButton_OnClickAsync(object sender, EventArgs e)
        {
            ViewModel.RegistrateCommand.Execute();
        }
    }
}