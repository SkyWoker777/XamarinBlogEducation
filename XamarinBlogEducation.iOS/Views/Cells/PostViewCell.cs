using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.iOS.Views.Cells
{
    public partial class PostViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("PostViewCell");
        public static readonly UINib Nib;

        static PostViewCell()
        {
            Nib = UINib.FromName("PostViewCell", NSBundle.MainBundle);
        }

        protected PostViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<PostViewCell, GetAllPostsBlogViewItem>();
                set.Bind(lbTitle).To(m => m.Title);
                set.Bind(lbCreationDate).To(m => m.CreationDate);
                set.Bind(tvDescription).To(m => m.Description);
                set.Apply();
            });

        }
    }
}
