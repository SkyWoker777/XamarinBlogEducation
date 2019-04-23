using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.iOS.Views.Cells
{
    public partial class CommentTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("CommentTableViewCell");
        public static readonly UINib Nib;

        static CommentTableViewCell()
        {
            Nib = UINib.FromName("CommentTableViewCell", NSBundle.MainBundle);
        }

        protected CommentTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<CommentTableViewCell, GetAllCommentsBlogViewItem>();
                set.Bind(lblAuthor).To(m => m.UserName);
                set.Bind(lblDate).To(m => m.CreationDate);
                set.Bind(lblComment).To(m => m.Content);
                set.Apply();
            });
        }
    }
}
