﻿// WARNING
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
    [Register ("UserPostsView")]
    partial class UserPostsView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView UserPostsTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (UserPostsTable != null) {
                UserPostsTable.Dispose ();
                UserPostsTable = null;
            }
        }
    }
}