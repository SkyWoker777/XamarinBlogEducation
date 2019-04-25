using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using UIKit;
using XamarinBlogEducation.ViewModels.Responses;

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
                MvxFluentBindingDescriptionSet<CommentTableViewCell, GetAllCommentResponseModel> set = this.CreateBindingSet<CommentTableViewCell, GetAllCommentResponseModel>();
                set.Bind(lblAuthor).To(m => m.UserName);
                set.Bind(lblDate).To(m => m.CreationDate);
                set.Bind(lblComment).To(m => m.Content);
                set.Apply();
            });
        }
    }
}
