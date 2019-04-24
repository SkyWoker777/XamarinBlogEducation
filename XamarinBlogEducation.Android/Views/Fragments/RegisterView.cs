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
    public class RegisterView : BaseFragment<RegisterViewModel>
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

            var set = this.CreateBindingSet<RegisterView, RegisterViewModel>();

            set.Bind(inpEmail).To(vm => vm.Email);
            set.Bind(inpPassword).To(vm => vm.Password);
            set.Bind(inpLastName).To(vm => vm.LastName);
            set.Bind(inpConfirmPassword).To(vm => vm.ConfirmPassword);
            set.Bind(inpUserName).To(vm => vm.FirstName);
            set.Bind(btnSignUpLogin).To(vm => vm.LoginCommand);

            set.Apply();

            btnSignUp.Click += btnSignUp_OnClick;

            return view;
        }     
        private void btnSignUp_OnClick(object sender, EventArgs e)
        {
            if (ValidateForm(inpEmail.Text,inpPassword.Text,inpConfirmPassword.Text,inpUserName.Text,inpLastName.Text))
            { ViewModel.RegistrateCommand.Execute(); }
        }
        public bool ValidateForm(string Email,string Password, string ConfirmPassword, string Name, string LastName)
        {
            var valid = true;
            if(Password!= ConfirmPassword)
            {
                var toastText = string.Format("Passwords doesn`t the same!");
                var toast = Toast.MakeText(Context, toastText, ToastLength.Long);
                toast.SetGravity(GravityFlags.Center, 0, 600);
                toast.Show();
                inpPassword.Text = "";
                inpConfirmPassword.Text = "";
                valid = false;
            }
            if(string.IsNullOrEmpty(Email)|| string.IsNullOrEmpty(Password)|| string.IsNullOrEmpty(ConfirmPassword)|| string.IsNullOrEmpty(Name)|| string.IsNullOrEmpty(LastName))
            {
                var toastText = string.Format("Fill all the gaps");
                var toast = Toast.MakeText(Context, toastText, ToastLength.Long);
                toast.SetGravity(GravityFlags.Center, 0, 400);
                toast.Show();
                valid = false;
            }
            var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            var emailValid= regex.IsMatch(Email);
            if (!emailValid)
            {
                var toastText = string.Format("Wrong format of email");
                var toast = Toast.MakeText(Context, toastText, ToastLength.Long);
                toast.SetGravity(GravityFlags.Center, 0, 200);
                toast.Show();
                valid = false;
            }
            return valid;
        }
    }
}