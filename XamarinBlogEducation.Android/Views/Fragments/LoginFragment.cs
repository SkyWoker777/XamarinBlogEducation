using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.SecureStorage;
using XamarinBlogEducation.Android.Views.Activities;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.Android.Views.Fragments
{
    [MvxFragmentPresentation(typeof(StartViewModel), Resource.Id.login_content_frame, true)]
    public class LoginFragment : BaseFragment<LoginViewModel>
    {
        private EditText inpEmail;
        private EditText inpPassword;
        private Button btnLogin;
        private Button btnRegister;
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
            inpEmail = view.FindViewById<EditText>(Resource.Id.inputEmail);
            inpPassword = view.FindViewById<EditText>(Resource.Id.inputPassword);
            btnLogin = view.FindViewById<Button>(Resource.Id.buttonLogin);
            btnRegister = view.FindViewById<Button>(Resource.Id.buttonRegister);
            linkSkip = view.FindViewById<TextView>(Resource.Id.linkSkip);

            var set = this.CreateBindingSet<LoginFragment, LoginViewModel>();
            set.Bind(inpEmail).To(vm => vm.Email);
            set.Bind(inpPassword).To(vm => vm.Password);

            set.Apply();

            btnLogin.Click += loginButton_OnClickAsync;
            btnRegister.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                ViewModel.SingUpCommand.Execute();
            });
            linkSkip.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                ViewModel.SkipCommand.Execute();
            });

            return view;
        }

        private async void loginButton_OnClickAsync(object sender, EventArgs e)
        {
            await ViewModel.LoginCommand.ExecuteAsync();

            var mail = CrossSecureStorage.Current.GetValue("UserEmail");
            if (mail != inpEmail.Text)
            {
                inpEmail.Text = "";
                inpPassword.Text = "";
                var toast = string.Format("Wrong mail or password");
                Toast.MakeText(Context, toast, ToastLength.Long).Show();
            }
            else
            {
                ViewModel.GoNextCommand.Execute();
            }
        }    
    }
}