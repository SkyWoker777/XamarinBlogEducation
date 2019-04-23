// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XamarinBlogEducation.iOS.Views.Modals
{
    [Register ("CategoryModal")]
    partial class CategoryModal
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnAddCategory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNewCategory { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnAddCategory != null) {
                btnAddCategory.Dispose ();
                btnAddCategory = null;
            }

            if (btnCancel != null) {
                btnCancel.Dispose ();
                btnCancel = null;
            }

            if (txtNewCategory != null) {
                txtNewCategory.Dispose ();
                txtNewCategory = null;
            }
        }
    }
}