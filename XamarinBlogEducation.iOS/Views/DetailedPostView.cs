
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using Plugin.SecureStorage;
using UIKit;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.iOS.Views.Cells;

namespace XamarinBlogEducation.iOS.Views
{
    public partial class DetailedPostView : MvxViewController<DetailedPostViewModel>
    {
        private MvxSimpleTableViewSource _source;
        public DetailedPostView() : base(nameof(DetailedPostView), null)
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
            _source = new MvxSimpleTableViewSource(CommentsTableView, nameof(CommentTableViewCell), CommentTableViewCell.Key);
            CommentsTableView.RowHeight = 70;

            bool ifUser = CrossSecureStorage.Current.HasKey("securityToken");

            EdgesForExtendedLayout = UIRectEdge.None;
            
            var set = this.CreateBindingSet<DetailedPostView, DetailedPostViewModel>();
           
            set.Bind(_source).For(v => v.ItemsSource).To(vm => vm.AllComments);
            set.Bind(inpComment).To(vm => vm.Content);
            set.Bind(btnComment).To(vm => vm.AddCommentCommand);
            set.Bind(lblAuthor).For(lb => lb.Text).To(vm => vm.DetailedPost.AuthorName);
            set.Bind(lblTitle).For(lb => lb.Text).To(vm => vm.DetailedPost.Title);
            set.Bind(lblDate).For(lb => lb.Text).To(vm => vm.DetailedPost.CreationDate);
            set.Bind(txtPostContent).For(lb => lb.Text).To(vm => vm.DetailedPost.Content);
            set.Bind(lblCategory).For(lb => lb.Text).To(vm => vm.DetailedPost.Category);

            set.Apply();

            CommentsTableView.Source = _source;
            CommentsTableView.ReloadData();
           
            CommentsTableView.SeparatorColor = UIColor.FromRGBA(109, 179, 206, 255);
            CommentsTableView.BackgroundColor = UIColor.FromRGBA(209, 188, 171, 255);
            if (!ifUser)
            {
                lblLoginToWrite.Hidden = false;
                lblLoginToWrite.BackgroundColor = UIColor.Clear;

                inpComment.Hidden = true;
                btnComment.Hidden = true;
            }
        }

        #endregion
    }
}