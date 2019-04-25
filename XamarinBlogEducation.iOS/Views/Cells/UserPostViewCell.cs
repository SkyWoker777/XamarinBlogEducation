using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using System;
using UIKit;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.iOS.Views.Cells
{
    public partial class UserPostViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("UserPostViewCell");
        public static readonly UINib Nib;

        static UserPostViewCell()
        {
            Nib = UINib.FromName("UserPostViewCell", NSBundle.MainBundle);
        }

        protected UserPostViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                MvxFluentBindingDescriptionSet<UserPostViewCell, GetAllPostResponseModel> set = this.CreateBindingSet<UserPostViewCell, GetAllPostResponseModel>();
                set.Bind(lbTitle).To(m => m.Title);
                set.Bind(lbCreationDate).To(m => m.CreationDate);
                set.Bind(tvDescription).To(m => m.Description);
                set.Apply();
            });

        }
    }
}
