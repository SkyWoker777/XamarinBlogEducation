
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.iOS.Views.Cells;

namespace XamarinBlogEducation.iOS.Views
{
    public partial class UserPostsView : MvxViewController<UserPostsViewModel>
    {
        private MvxSimpleTableViewSource _source;
        public UserPostsView(IntPtr handle) : base(handle)
        {
        }
        public UserPostsView() : base(nameof(UserPostsView), null)
        {

        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _source = new MvxSimpleTableViewSource(UserPostsTable, nameof(UserPostViewCell), UserPostViewCell.Key);
            UserPostsTable.RowHeight = 130;

            var set = this.CreateBindingSet<UserPostsView, UserPostsViewModel>();

            set.Bind(_source).For(v => v.ItemsSource).To(vm => vm.UserPosts);
            set.Bind(_source).For(v => v.SelectionChangedCommand).To(vm => vm.PostSelectedCommand);

            set.Apply();

            UserPostsTable.Source = _source;
            UserPostsTable.ReloadData();

            UserPostsTable.SeparatorColor = UIColor.FromRGBA(109, 179, 206, 255);
            UserPostsTable.BackgroundColor = UIColor.FromRGBA(209, 188, 171, 255);
        }

        #endregion
    }
}