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
    [Register ("LoginView")]
    partial class LoginView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgWelcome { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField inputEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField inputPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton signInButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton signUpButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton skipButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgWelcome != null) {
                imgWelcome.Dispose ();
                imgWelcome = null;
            }

            if (inputEmail != null) {
                inputEmail.Dispose ();
                inputEmail = null;
            }

            if (inputPassword != null) {
                inputPassword.Dispose ();
                inputPassword = null;
            }

            if (signInButton != null) {
                signInButton.Dispose ();
                signInButton = null;
            }

            if (signUpButton != null) {
                signUpButton.Dispose ();
                signUpButton = null;
            }

            if (skipButton != null) {
                skipButton.Dispose ();
                skipButton = null;
            }
        }
    }
}