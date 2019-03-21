using System;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using XamarinBlogEducation.Core.ViewModels.Activities;

namespace XamarinBlogEducation.Android.Views.Activities
{
    [Activity(MainLauncher = true)]
    public class LoginView : MvxActivity<LoginViewModel>
    {
       // private readonly IMvxNavigationService _navigationService;
        private EditText inputEmail;
        private EditText inputPassword;
        private Button loginButton;
        private Button buttonRegister;
        private TextView linkSkip;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginView);
          //  drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (savedInstanceState == null)
               // ViewModel.ShowMenu();
    
            inputEmail = FindViewById<EditText>(Resource.Id.inputEmail);
            inputPassword = FindViewById<EditText>(Resource.Id.inputPassword);
            loginButton = FindViewById<Button>(Resource.Id.buttonLogin);
            buttonRegister = FindViewById<Button>(Resource.Id.buttonRegister);
            linkSkip = FindViewById<TextView>(Resource.Id.linkSkip);

            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(inputEmail).To(vm => vm.Email);
            set.Bind(inputPassword).To(vm => vm.Password);
            set.Bind(loginButton).To(vm => vm.LoginCommand);
            set.Apply();
            loginButton.Click += loginButton_OnClickAsync;
            buttonRegister.Click += buttonRegister_OnClickAsync;
            linkSkip.Click += linkSkip_OnClick;
        }

        private  void loginButton_OnClickAsync(object sender, EventArgs e)
        {
         ViewModel.LoginCommand.Execute();
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