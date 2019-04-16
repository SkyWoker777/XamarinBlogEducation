// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace XamarinBlogEducation.iOS
{
    [Register ("RegisterView")]
    partial class RegisterView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton haveAccountButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton registerButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel registrationLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textConfirmPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textSurname { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (haveAccountButton != null) {
                haveAccountButton.Dispose ();
                haveAccountButton = null;
            }

            if (registerButton != null) {
                registerButton.Dispose ();
                registerButton = null;
            }

            if (registrationLabel != null) {
                registrationLabel.Dispose ();
                registrationLabel = null;
            }

            if (textConfirmPassword != null) {
                textConfirmPassword.Dispose ();
                textConfirmPassword = null;
            }

            if (textEmail != null) {
                textEmail.Dispose ();
                textEmail = null;
            }

            if (textName != null) {
                textName.Dispose ();
                textName = null;
            }

            if (textPassword != null) {
                textPassword.Dispose ();
                textPassword = null;
            }

            if (textSurname != null) {
                textSurname.Dispose ();
                textSurname = null;
            }
        }
    }
}