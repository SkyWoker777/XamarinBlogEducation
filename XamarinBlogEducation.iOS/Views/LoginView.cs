
using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Plugin.SecureStorage;
using UIKit;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.iOS.Views
{
    public partial class LoginView : MvxViewController<LoginViewModel>
    {

        public LoginView() : base(nameof(LoginView), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            imgWelcome.Image = new UIImage("welcome_png_1496568.png");
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(inputEmail).To(vm => vm.Email);
            set.Bind(inputPassword).To(vm => vm.Password);
            set.Bind(signInButton).To(vm => vm.LoginCommand);
            set.Bind(signUpButton).To(vm => vm.SingUpCommand);
            set.Bind(skipButton).To(vm => vm.SkipCommand);
            set.Apply();
            var viewTap = new UITapGestureRecognizer(() =>
            {
                View.EndEditing(true);
            });
            View.AddGestureRecognizer(viewTap);
            signInButton.TouchDown += signInButton_onTouchDown;
            signUpButton.TouchDown += (sender, args) => { ViewModel.SingUpCommand.Execute(); };
            skipButton.TouchDown += (sender, args) => { ViewModel.SkipCommand.Execute(); };
            inputPassword.EditingDidEndOnExit += (sender, args) => { inputPassword.ResignFirstResponder(); };
           
            
        }

        private void signInButton_onTouchDown(object sender, EventArgs e)
        {
            ViewModel.LoginCommand.Execute();
            var mail = CrossSecureStorage.Current.GetValue("UserEmail");
            ViewModel.GoNextCommand.Execute();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBar.Hidden = true;
        }

        #endregion
    }
}