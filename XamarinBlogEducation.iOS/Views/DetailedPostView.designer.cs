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
    [Register ("DetailedPostView")]
    partial class DetailedPostView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnComment { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView CommentsTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField inpComment { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblAuthor { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCategory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDate { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblLoginToWrite { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtPostContent { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnComment != null) {
                btnComment.Dispose ();
                btnComment = null;
            }

            if (CommentsTableView != null) {
                CommentsTableView.Dispose ();
                CommentsTableView = null;
            }

            if (inpComment != null) {
                inpComment.Dispose ();
                inpComment = null;
            }

            if (lblAuthor != null) {
                lblAuthor.Dispose ();
                lblAuthor = null;
            }

            if (lblCategory != null) {
                lblCategory.Dispose ();
                lblCategory = null;
            }

            if (lblDate != null) {
                lblDate.Dispose ();
                lblDate = null;
            }

            if (lblLoginToWrite != null) {
                lblLoginToWrite.Dispose ();
                lblLoginToWrite = null;
            }

            if (lblTitle != null) {
                lblTitle.Dispose ();
                lblTitle = null;
            }

            if (txtPostContent != null) {
                txtPostContent.Dispose ();
                txtPostContent = null;
            }
        }
    }
}