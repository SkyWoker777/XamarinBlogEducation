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
    [Register ("PasswordDialog")]
    partial class PasswordDialog
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCancelChangePw { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnConfirmChangePw { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtConfirmPw { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNewPw { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtOldPw { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCancelChangePw != null) {
                btnCancelChangePw.Dispose ();
                btnCancelChangePw = null;
            }

            if (btnConfirmChangePw != null) {
                btnConfirmChangePw.Dispose ();
                btnConfirmChangePw = null;
            }

            if (txtConfirmPw != null) {
                txtConfirmPw.Dispose ();
                txtConfirmPw = null;
            }

            if (txtNewPw != null) {
                txtNewPw.Dispose ();
                txtNewPw = null;
            }

            if (txtOldPw != null) {
                txtOldPw.Dispose ();
                txtOldPw = null;
            }
        }
    }
}