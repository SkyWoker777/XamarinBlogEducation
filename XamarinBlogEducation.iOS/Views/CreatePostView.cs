
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using XamarinBlogEducation.Core.Resources;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.iOS.Views
{
	public partial class CreatePostView :MvxViewController<CreatePostViewModel>
	{
		public CreatePostView (IntPtr handle) : base (handle)
		{
		}
        public CreatePostView():base(nameof(CreatePostView),null)
        {

        }
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            pickerPostCategory.ShowSelectionIndicator = true;
            var pickerPostCategoryViewModel = new MvxPickerViewModel(pickerPostCategory);
            pickerPostCategory.Model = pickerPostCategoryViewModel;

            var set = this.CreateBindingSet<CreatePostView, CreatePostViewModel>();

            set.Bind(txtDescriotion).To(vm => vm.Description);
            set.Bind(txtNickName).To(vm => vm.NickName);
            set.Bind(txtTitle).To(vm => vm.Title);
            set.Bind(btnAddPost).To(vm => vm.AddNewPostCommand);
            set.Bind(txtContent).To(vm => vm.PostContent);
            set.Bind(btnNewCategory).To(vm => vm.OpenDialogCommand);
            set.Bind(pickerPostCategoryViewModel).For(p => p.ItemsSource).To(vm => vm.CategoryItems);
            set.Bind(pickerPostCategoryViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedCategory);

            set.Apply();
		}
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.Title = Strings.CreatePostTitle;
        }
        #endregion
    }
}