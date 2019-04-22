// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XamarinBlogEducation.iOS.Views.Cells
{
    [Register ("PostViewCell")]
    partial class PostViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbCreationDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView tvDescription { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lbCreationDate != null) {
                lbCreationDate.Dispose ();
                lbCreationDate = null;
            }

            if (lbTitle != null) {
                lbTitle.Dispose ();
                lbTitle = null;
            }

            if (tvDescription != null) {
                tvDescription.Dispose ();
                tvDescription = null;
            }
        }
    }
}