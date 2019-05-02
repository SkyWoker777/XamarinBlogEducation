
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Plugin.SecureStorage;
using UIKit;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.iOS.Views
{
    public partial class UserProfileView : MvxViewController<UserProfileViewModel>
    {
        public UserProfileView() : base(nameof(UserProfileView), null)
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

           
            var set = this.CreateBindingSet<UserProfileView, UserProfileViewModel>();

            set.Bind(changePasswordBtn).To(vm => vm.ChangePasswordCommand);
            set.Bind(textEditUserEmail).To(vm => vm.Email);
            set.Bind(textEditUserLastName).To(vm => vm.LastName);
            set.Bind(textEditUserName).To(vm => vm.FirstName);
            set.Bind(btnSaveChanges).To(vm => vm.UpdateCommand);

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
            NavigationController.Title = Strings.UserProfileTitle;
            ViewModel.OnResume();
            textEditUserEmail.Text = CrossSecureStorage.Current.GetValue("UserEmail");
            textEditUserLastName.Text = CrossSecureStorage.Current.GetValue("UserLastName");
            textEditUserName.Text = CrossSecureStorage.Current.GetValue("UserName");


        }
        #endregion
    }
}