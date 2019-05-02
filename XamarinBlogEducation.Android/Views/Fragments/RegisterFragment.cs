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
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(StartViewModel), Resource.Id.login_content_frame, true)]
    public class RegisterFragment : BaseFragment<RegisterViewModel>
    {
        public EditText inpEmail;
        public EditText inpPassword;
        public EditText inpConfirmPassword;
        public EditText inpUserName;
        public EditText inpLastName;
        public Button btnSignUp;
        public Button btnSignUpLogin;

        protected override int FragmentId => Resource.Layout.RegisterView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            inpEmail = view.FindViewById<EditText>(Resource.Id.inputEmail);
            inpPassword = view.FindViewById<EditText>(Resource.Id.inputPassword);
            inpConfirmPassword = view.FindViewById<EditText>(Resource.Id.confirmPassword);
            inpUserName = view.FindViewById<EditText>(Resource.Id.inputUserName);
            inpLastName = view.FindViewById<EditText>(Resource.Id.inputLastName);
            btnSignUp = view.FindViewById<Button>(Resource.Id.signUpButton);
            btnSignUpLogin = view.FindViewById<Button>(Resource.Id.signUpLoginButton);

            var set = this.CreateBindingSet<RegisterFragment, RegisterViewModel>();

            set.Bind(inpEmail).To(vm => vm.Email);
            set.Bind(inpPassword).To(vm => vm.Password);
            set.Bind(inpLastName).To(vm => vm.LastName);
            set.Bind(inpConfirmPassword).To(vm => vm.ConfirmPassword);
            set.Bind(inpUserName).To(vm => vm.FirstName);
            set.Bind(btnSignUpLogin).To(vm => vm.LoginCommand);
            set.Bind(btnSignUp).To(vm => vm.RegistrateCommand);

            set.Apply();
            
            return view;
        } 
        
    }
}