
using System;
using System.Drawing;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.iOS.Views.Cells
{
    public partial class AllPostViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(AllPostViewCell));
        public static readonly UINib Nib;
        
        static AllPostViewCell()
        {
            try { 
            Nib = UINib.FromName(nameof(AllPostViewCell), NSBundle.MainBundle);
            }
            catch(Exception e)
            {
                var p = e;
            }
        }
        protected AllPostViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(
                () =>
                {
                    this.AddBindings(txtPostTitle, "Text Title");
                    this.AddBindings(txtCreationDate, "Text CreationDate");
                    this.AddBindings(txtDescription, "Text Description");

                });
        }
      
    }
}