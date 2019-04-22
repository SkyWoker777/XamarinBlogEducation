
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Binding.BindingContext;
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
            set.Bind(textEditUserEmail).To(vm => vm.Email);
            set.Bind(textEditUserLastName).To(vm => vm.LastName);
            set.Bind(textEditUserName).To(vm => vm.FirstName);
            set.Bind(btnSaveChanges).To(vm => vm.UpdateCommand);
            set.Apply();
            this.DelayBind(
                () =>
                  {
                      this.AddBindings(textEditUserName.Text, "ItemsSource User; Text User.FirstName");
                      this.AddBindings(textEditUserEmail.Text, "ItemsSource User;Text User.Email");
                      this.AddBindings(textEditUserLastName.Text, "ItemsSource User;Text User.LastName");
                  });
        }

       

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewModel.OnResume();
        }
        #endregion
    }
}