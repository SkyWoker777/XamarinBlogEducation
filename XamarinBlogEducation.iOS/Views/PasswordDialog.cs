
using System;
using System.Drawing;

using Foundation;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using XamarinBlogEducation.Core.ViewModels.Dialogs;

namespace XamarinBlogEducation.iOS.Views
{
    [MvxModalPresentation(ModalTransitionStyle = UIModalTransitionStyle.CoverVertical, ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen)]
    public partial class PasswordDialog : MvxViewController<ChangePasswordDialogViewModel>
    {
        public PasswordDialog(IntPtr handle) : base(handle)
        {
        }
        public PasswordDialog() : base(nameof(PasswordDialog), null)
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
            var set = this.CreateBindingSet<PasswordDialog, ChangePasswordDialogViewModel>();

            set.Bind(btnCancelChangePw).To(vm => vm.GoBackCommand);
            set.Bind(txtConfirmPw).To(vm => vm.ComfirmPassword);
            set.Bind(txtNewPw).To(vm => vm.NewPassword);
            set.Bind(txtOldPw).To(vm => vm.OldPassword);
            set.Bind(btnConfirmChangePw).To(vm => vm.ChangePasswordCommand);
            set.Apply();

        }



        #endregion
    }
}