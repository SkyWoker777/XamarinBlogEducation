// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XamarinBlogEducation.iOS.Views
{
    [Register ("CreatePostView")]
    partial class CreatePostView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnAddPost { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnNewCategory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView pickerPostCategory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtContent { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtDescriotion { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNickName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnAddPost != null) {
                btnAddPost.Dispose ();
                btnAddPost = null;
            }

            if (btnNewCategory != null) {
                btnNewCategory.Dispose ();
                btnNewCategory = null;
            }

            if (pickerPostCategory != null) {
                pickerPostCategory.Dispose ();
                pickerPostCategory = null;
            }

            if (txtContent != null) {
                txtContent.Dispose ();
                txtContent = null;
            }

            if (txtDescriotion != null) {
                txtDescriotion.Dispose ();
                txtDescriotion = null;
            }

            if (txtNickName != null) {
                txtNickName.Dispose ();
                txtNickName = null;
            }

            if (txtTitle != null) {
                txtTitle.Dispose ();
                txtTitle = null;
            }
        }
    }
}