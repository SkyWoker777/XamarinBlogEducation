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
        public EditText inputEmail;
        public EditText inputPassword;
        public EditText confirmPassword;
        public EditText inputUserName;
        public EditText inputLastName;
        public Button signUpButton;
        public Button signUpLoginButton;
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
            signUpLoginButton = view.FindViewById<Button>(Resource.Id.signUpLoginButton);
            var set = this.CreateBindingSet<RegisterView, RegisterViewModel>();
            set.Bind(inputEmail).To(vm => vm.Email);
            set.Bind(inputPassword).To(vm => vm.Password);
            set.Bind(inputLastName).To(vm => vm.LastName);
            set.Bind(confirmPassword).To(vm => vm.ConfirmPassword);
            set.Bind(inputUserName).To(vm => vm.FirstName);
            set.Bind(signUpLoginButton).To(vm => vm.LoginCommand);
            set.Apply();
            signUpButton.Click += signUpButton_OnClickAsync;
            return view;
        }     
        private void signUpButton_OnClickAsync(object sender, EventArgs e)
        {
            if (ValidateForm(inputEmail.Text,inputPassword.Text,confirmPassword.Text,inputUserName.Text,inputLastName.Text))
            { ViewModel.RegistrateCommand.Execute(); }
        }
        public bool ValidateForm(string Email,string Password, string ConfirmPassword, string Name, string LastName)
        {
            var valid = true;
            if(Password!= ConfirmPassword)
            {
                string toast = string.Format("Passwords doesn`t the same!");
                Toast _tost = Toast.MakeText(Context, toast, ToastLength.Long);
                _tost.SetGravity(GravityFlags.Center, 0, 600);
                _tost.Show();
                inputPassword.Text = "";
                confirmPassword.Text = "";
                valid = false;
            }
            if(string.IsNullOrEmpty(Email)|| string.IsNullOrEmpty(Password)|| string.IsNullOrEmpty(ConfirmPassword)|| string.IsNullOrEmpty(Name)|| string.IsNullOrEmpty(LastName))
            {
                string toast = string.Format("Fill all the gaps");
                Toast _tost = Toast.MakeText(Context, toast, ToastLength.Long);
                _tost.SetGravity(GravityFlags.Center, 0, 400);
                _tost.Show();
                valid = false;
            }
            var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            var emailValid= regex.IsMatch(Email);
            if (!emailValid)
            {
                string toast = string.Format("Wrong format of email");
                Toast _tost = Toast.MakeText(Context, toast, ToastLength.Long);
                _tost.SetGravity(GravityFlags.Center, 0, 200);
                _tost.Show();
                valid = false;
            }
            return valid;
        }
    }
}