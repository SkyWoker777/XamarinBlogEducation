using System;
using MvvmCross.Platforms.Ios.Views;
using XamarinBlogEducation.Core.ViewModels.Fragments;

using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Plugin.SecureStorage;
using CoreGraphics;
using Cirrious.FluentLayouts.Touch;
using XamarinBlogEducation.iOS.Views.Cells;
using Foundation;

namespace XamarinBlogEducation.iOS.Views
{

    public partial class AllPostsView : MvxViewController<AllPostsFragmentViewModel>
    {
        private MvxSimpleTableViewSource _source;
        public AllPostsView() : base(nameof(AllPostsView), null)
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

            _source = new MvxSimpleTableViewSource(AllPostsTableView, nameof(PostViewCell), PostViewCell.Key);
            AllPostsTableView.RowHeight = 130;



            bool ifUser = CrossSecureStorage.Current.HasKey("securityToken");

            EdgesForExtendedLayout = UIRectEdge.None;
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
           
            AllPostsTableView.Source = _source;
            AllPostsTableView.ReloadData();

            AllPostsTableView.SeparatorColor = UIColor.FromRGBA(109, 179, 206, 255);
            AllPostsTableView.BackgroundColor = UIColor.FromRGBA(209, 188, 171, 255);
           // AllPostsTableView.ContentInset = UIEdgeInsets.FromString("20.0, 20.0, 20.0, 20.0");
           // AllPostsTableView.RowHeight = 215;

            var threeDotsButton = new UIBarButtonItem
            {

                Image = UIImage.FromFile("icons8_more_48.png")
            };

            NavigationItem.HidesBackButton = true;
            NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { threeDotsButton };
            filterPickerViewModel.SelectedItemChanged += (a, e) => { AllPostsTableView.ReloadData(); };
            categoryPikerViewModel.SelectedItemChanged += (a, e) => { AllPostsTableView.ReloadData(); };
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

                PresentViewController(alert, true, null);
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

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewModel.OnResume();
            NavigationController.NavigationBar.Hidden = false;
            NavigationController.NavigationBar.BackgroundColor = UIColor.FromRGBA(209, 188, 171, 255);
        }
        #endregion
    }
    
}