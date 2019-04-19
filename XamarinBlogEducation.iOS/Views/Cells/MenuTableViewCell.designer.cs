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
    [Register ("MenuTableViewCell")]
    partial class MenuTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lbTitle != null) {
                lbTitle.Dispose ();
                lbTitle = null;
            }
        }
    }
}