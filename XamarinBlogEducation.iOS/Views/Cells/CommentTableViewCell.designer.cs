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
    [Register ("CommentTableViewCell")]
    partial class CommentTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblAuthor { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblComment { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDate { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblAuthor != null) {
                lblAuthor.Dispose ();
                lblAuthor = null;
            }

            if (lblComment != null) {
                lblComment.Dispose ();
                lblComment = null;
            }

            if (lblDate != null) {
                lblDate.Dispose ();
                lblDate = null;
            }
        }
    }
}