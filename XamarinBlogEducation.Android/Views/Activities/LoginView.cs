using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Plugin.SecureStorage;
using XamarinBlogEducation.Android.Views.Fragments;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.Android.Views.Activities
{
    [MvxFragmentPresentation(typeof(LoginActivityViewModel), Resource.Id.login_content_frame, true)]
    public class LoginView : BaseFragment<LoginViewModel>
    {
        private EditText inputEmail;
        private EditText inputPassword;
        private Button loginButton;
        private Button buttonRegister;
        private TextView linkSkip;

        protected override int FragmentId => Resource.Layout.LoginFragment;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
           
            CrossSecureStorage.Current.DeleteKey("securityToken");
            CrossSecureStorage.Current.DeleteKey("UserName");
            CrossSecureStorage.Current.DeleteKey("UserEmail");
            CrossSecureStorage.Current.DeleteKey("UserLastName");
            if (savedInstanceState == null)
            inputEmail = view.FindViewById<EditText>(Resource.Id.inputEmail);
            inputPassword = view.FindViewById<EditText>(Resource.Id.inputPassword);
            loginButton = view.FindViewById<Button>(Resource.Id.buttonLogin);
            buttonRegister = view.FindViewById<Button>(Resource.Id.buttonRegister);
            linkSkip = view.FindViewById<TextView>(Resource.Id.linkSkip);

            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(inputEmail).To(vm => vm.Email);
            set.Bind(inputPassword).To(vm => vm.Password);
            set.Bind(loginButton).To(vm => vm.LoginCommand);
            set.Apply();
            loginButton.Click += loginButton_OnClickAsync;
            buttonRegister.Click += buttonRegister_OnClickAsync;
            linkSkip.Click += linkSkip_OnClick;
            return view;
        }

        private void loginButton_OnClickAsync(object sender, EventArgs e)
        {
         
            ViewModel.LoginCommand.Execute();
           inputEmail.Text = "";
           inputPassword.Text = "";

        }
        private void linkSkip_OnClick(object sender, EventArgs e)
        {
            ViewModel.SkipCommand.Execute();
        }
        private void buttonRegister_OnClickAsync(object sender, EventArgs e)
        {
            ViewModel.SingUpCommand.Execute();
        }
    }
}