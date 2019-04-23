
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.iOS.Views.Modals
{
    [MvxModalPresentation(ModalTransitionStyle = UIModalTransitionStyle.CoverVertical, ModalPresentationStyle = UIModalPresentationStyle.BlurOverFullScreen)]
    public partial class CategoryModal : MvxViewController<CategoryDialogViewModel>
    {
        public CategoryModal(IntPtr handle) : base(handle)
        {
        }
        public CategoryModal() : base(nameof(CategoryModal), null)
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
            var set = this.CreateBindingSet<CategoryModal, CategoryDialogViewModel>();

            set.Bind(txtNewCategory).To(vm => vm.NewCategory);
            set.Bind(btnCancel).To(vm => vm.GoBackCommand);
            set.Bind(btnAddCategory).To(vm => vm.AddCategoryCommand);
            set.Apply();
        }

        #endregion
    }
}