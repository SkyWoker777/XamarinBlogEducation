
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.iOS.Views
{
    public partial class UserProfileView : MvxViewController<UserProfileViewModel>
    {
        public UserProfileView() : base(nameof(UserProfileView), null)
        {
        }


        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var set = this.CreateBindingSet<UserProfileView, UserProfileViewModel>();

            set.Bind(changePasswordBtn).To(vm => vm.ChangePasswordCommand);
            set.Bind(textEditUserEmail).For(t=>t.Text).To(vm => vm.User.Email);
            set.Bind(textEditUserLastName).For(t => t.Text).To(vm => vm.User.LastName);
            set.Bind(textEditUserName).For(t => t.Text).To(vm => vm.User.FirstName);
            set.Bind(btnSaveChanges).To(vm => vm.UpdateCommand);
            set.Apply();         
        }

       

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewModel.OnResume();
        }
        #endregion
    }
}