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
    [Register ("EditPostView")]
    partial class EditPostView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCancelEdit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnDeletePost { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSaveEdit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView editContent { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView editDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView editTite { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCreationDate { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCancelEdit != null) {
                btnCancelEdit.Dispose ();
                btnCancelEdit = null;
            }

            if (btnDeletePost != null) {
                btnDeletePost.Dispose ();
                btnDeletePost = null;
            }

            if (btnSaveEdit != null) {
                btnSaveEdit.Dispose ();
                btnSaveEdit = null;
            }

            if (editContent != null) {
                editContent.Dispose ();
                editContent = null;
            }

            if (editDescription != null) {
                editDescription.Dispose ();
                editDescription = null;
            }

            if (editTite != null) {
                editTite.Dispose ();
                editTite = null;
            }

            if (lblCreationDate != null) {
                lblCreationDate.Dispose ();
                lblCreationDate = null;
            }
        }
    }
}