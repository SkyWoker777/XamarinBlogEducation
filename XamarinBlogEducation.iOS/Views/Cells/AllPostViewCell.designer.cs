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
    [Register ("AllPostViewCell")]
    partial class AllPostViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableViewCell AllPostsViewCell { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtCreationDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtPostTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AllPostsViewCell != null) {
                AllPostsViewCell.Dispose ();
                AllPostsViewCell = null;
            }

            if (txtCreationDate != null) {
                txtCreationDate.Dispose ();
                txtCreationDate = null;
            }

            if (txtDescription != null) {
                txtDescription.Dispose ();
                txtDescription = null;
            }

            if (txtPostTitle != null) {
                txtPostTitle.Dispose ();
                txtPostTitle = null;
            }
        }
    }
}