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
    [Register ("UserProfileView")]
    partial class UserProfileView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton changePasswordBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textEditUserEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textEditUserLastName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textEditUserName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (changePasswordBtn != null) {
                changePasswordBtn.Dispose ();
                changePasswordBtn = null;
            }

            if (textEditUserEmail != null) {
                textEditUserEmail.Dispose ();
                textEditUserEmail = null;
            }

            if (textEditUserLastName != null) {
                textEditUserLastName.Dispose ();
                textEditUserLastName = null;
            }

            if (textEditUserName != null) {
                textEditUserName.Dispose ();
                textEditUserName = null;
            }
        }
    }
}