
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.iOS.Views
{
    public partial class EditPostView : MvxViewController<EditPostViewModel>
    {
        public EditPostView() : base(nameof(EditPostView), null)
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
            var set = this.CreateBindingSet<EditPostView, EditPostViewModel>();
            
            set.Bind(editTite).For(t=>t.Text).To(vm => vm.PostToEdit.Title);
            set.Bind(editDescription).For(t => t.Text).To(vm => vm.PostToEdit.Description);
            set.Bind(editContent).For(t => t.Text).To(vm => vm.PostToEdit.Content);
            set.Bind(lblCreationDate).For(t => t.Text).To(vm => vm.PostToEdit.CreationDate);
            set.Bind(btnSaveEdit).To(vm => vm.SaveEditCommand);
            set.Bind(btnCancelEdit).To(vm => vm.GoBackCommand);

            set.Apply();
            btnDeletePost.TouchDown+= btnDeletePost_onTouch;
        }

        private void btnDeletePost_onTouch(object sender, EventArgs e)
        {
            var alert = UIAlertController.Create("Post deleting", "Are you really want to delete this post? ", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("DELETE", UIAlertActionStyle.Default, (UIAlertAction obj) =>
            {
                ViewModel.DeleteCommand.Execute();
            }));
            alert.AddAction(UIAlertAction.Create("CANCEL", UIAlertActionStyle.Cancel, (UIAlertAction obj) =>
            {
                alert.Dispose();
            }));
            PresentViewController(alert, true, null);
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.Title = Strings.EditPostTitle;
        }
        #endregion
    }
}