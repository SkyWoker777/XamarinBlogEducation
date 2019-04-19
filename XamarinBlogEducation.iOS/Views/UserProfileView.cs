
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace XamarinBlogEducation.iOS.Views
{
	public partial class UserProfileView : MvxViewController
	{
		public UserProfileView (IntPtr handle) : base (handle)
		{
		}
        

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		#endregion
	}
}