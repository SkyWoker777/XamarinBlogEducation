
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.iOS.Views
{
    public partial class RegisterView : MvxViewController<RegisterViewModel>
    {
        public RegisterView(): base(nameof(RegisterView), null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var set = this.CreateBindingSet<RegisterView, RegisterViewModel>();

            set.Bind(textEmail).To(vm => vm.Email);
            set.Bind(textPassword).To(vm => vm.Password);
            set.Bind(textConfirmPassword).To(vm => vm.ConfirmPassword);
            set.Bind(textName).To(vm => vm.FirstName);
            set.Bind(textSurname).To(vm => vm.LastName);
            set.Bind(registerButton).To(vm => vm.RegistrateCommand);
            set.Bind(haveAccountButton).To(vm => vm.LoginCommand);

            set.Apply();
            var viewTap = new UITapGestureRecognizer(() =>
            {
                View.EndEditing(true);
            });
            View.AddGestureRecognizer(viewTap);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBar.Hidden = true;
        }
        #endregion
    }
}