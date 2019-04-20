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
    [Register ("AllPostsView")]
    partial class AllPostsView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView AllPostsTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView categoryPicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView filterPicker { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AllPostsTableView != null) {
                AllPostsTableView.Dispose ();
                AllPostsTableView = null;
            }

            if (categoryPicker != null) {
                categoryPicker.Dispose ();
                categoryPicker = null;
            }

            if (filterPicker != null) {
                filterPicker.Dispose ();
                filterPicker = null;
            }
        }
    }
}