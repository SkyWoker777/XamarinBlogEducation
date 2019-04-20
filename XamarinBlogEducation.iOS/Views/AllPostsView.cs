using System;
using MvvmCross.Platforms.Ios.Views;
using XamarinBlogEducation.Core.ViewModels.Fragments;

using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Plugin.SecureStorage;
using CoreGraphics;

namespace XamarinBlogEducation.iOS.Views
{
    
    public partial class AllPostsView : MvxViewController<AllPostsFragmentViewModel>
    {
        private AllPostsTableViewSource _source;
        public AllPostsView() : base(nameof(AllPostsView),null)
        {
     
        }
        public NavViewController NavController { get; private set; }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        #region View lifecycle
       
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            EdgesForExtendedLayout = UIRectEdge.None;
            //MenuTableView.BackgroundColor = UIColor.Clear;
            _source = new AllPostsTableViewSource(AllPostsTableView);
            AllPostsTableView.Source = _source;
            AllPostsTableView.SeparatorColor = UIColor.FromRGBA(109, 179, 206, 255);
            AllPostsTableView.BackgroundColor = UIColor.FromRGBA(209, 188, 171, 255);
            bool ifUser = CrossSecureStorage.Current.HasKey("securityToken");
            categoryPicker.ShowSelectionIndicator = true;
            var categoryPikerViewModel = new MvxPickerViewModel(categoryPicker);
            categoryPicker.Model = categoryPikerViewModel;
            filterPicker.ShowSelectionIndicator = true;
            var filterPickerViewModel = new MvxPickerViewModel(filterPicker);
            filterPicker.Model = filterPickerViewModel;
            var set = this.CreateBindingSet<AllPostsView, AllPostsFragmentViewModel>();
            set.Bind(_source).For(v => v.ItemsSource).To(vm => vm.AllPosts);
            set.Bind(_source).For(v => v.SelectionChangedCommand).To(vm => vm.PostSelectedCommand);
            set.Bind(categoryPikerViewModel).For(p => p.ItemsSource).To(vm => vm.CategoryItems);
            set.Bind(categoryPikerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedCategoryId);

            set.Bind(filterPickerViewModel).For(p => p.ItemsSource).To(vm => vm.FilterItems);
            set.Bind(filterPickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedFilterId);
            set.Apply();

            var threeDotsButton = new UIBarButtonItem
                {

                   Image= UIImage.FromFile("icons8_more_48.png")
                };

                NavigationItem.HidesBackButton = true;
                NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { threeDotsButton };
          
            threeDotsButton.Clicked += (a, e) =>
            {

                var alert = UIAlertController.Create("Hi", "choose an action", UIAlertControllerStyle.ActionSheet);
                alert.AddAction(UIAlertAction.Create("About Us", UIAlertActionStyle.Default, (UIAlertAction obj) =>
                {
                    ViewModel.AboutUsComand.Execute();
                }));
                if (ifUser)
                {
                    alert.AddAction(UIAlertAction.Create("Add Post", UIAlertActionStyle.Default, (UIAlertAction obj) =>
                    {
                        ViewModel.AddPostCommand.Execute();
                    }));

                }
                if (!ifUser)
                {
                    alert.AddAction(UIAlertAction.Create("Join Us", UIAlertActionStyle.Default, (UIAlertAction obj) =>
                     {
                         ViewModel.LoginCommand.Execute();
                     }));
                }
              
                PresentViewController(alert, true,null);
            };
            if (ifUser)
            {
                var menuButton = new UIBarButtonItem
                {

                    Image = UIImage.FromFile("menu.png")
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
         
            
            
        }

        private void categoryPikerViewModel_SelectedItemChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBar.Hidden = false;
            NavigationController.NavigationBar.BackgroundColor = UIColor.FromRGBA(209, 188, 171, 255);
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