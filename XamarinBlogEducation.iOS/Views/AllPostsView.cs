using System;
using MvvmCross.Platforms.Ios.Views;
using XamarinBlogEducation.Core.ViewModels.Fragments;

using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;

namespace XamarinBlogEducation.iOS.Views
{
    
    public partial class AllPostsView : MvxViewController<AllPostsFragmentViewModel>
    {
        public AllPostsView() : base(nameof(AllPostsView),null)
        {
     
        }
        public NavViewController NavController { get; private set; }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle
       
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            categoryPicker.ShowSelectionIndicator = true;
            var categoryPikerViewModel = new MvxPickerViewModel(categoryPicker);
            categoryPicker.Model = categoryPikerViewModel;
            filterPicker.ShowSelectionIndicator = true;
            var filterPickerViewModel = new MvxPickerViewModel(filterPicker);
            filterPicker.Model = filterPickerViewModel;
            var set = this.CreateBindingSet<AllPostsView, AllPostsFragmentViewModel>();
            set.Bind(categoryPikerViewModel).For(p => p.ItemsSource).To(vm => vm.CategoryItems);
            set.Bind(categoryPikerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedCategoryId);

            set.Bind(filterPickerViewModel).For(p => p.ItemsSource).To(vm => vm.FilterItems);
            set.Bind(filterPickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedFilterId);
            set.Apply();

            var menuButton = new UIBarButtonItem
            {
                Title = "-",
            };

            menuButton.Clicked += (s, e) =>
            {
                ViewModel.ShowMenu?.Execute();
            };

            NavigationItem.HidesBackButton = true;
            NavigationItem.LeftBarButtonItems = new UIBarButtonItem[]
            {
               menuButton
            };

        }

        private void categoryPikerViewModel_SelectedItemChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}