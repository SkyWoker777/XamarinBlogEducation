using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using Plugin.SecureStorage;
using System;
using UIKit;
using XamarinBlogEducation.Core.ViewModels.Fragments;
using XamarinBlogEducation.iOS.Views.Cells;
using XamarinBlogEducation.ViewModels.Responses;

namespace XamarinBlogEducation.iOS.Views
{

    public partial class AllPostsView : MvxViewController<AllPostsViewModel>
    {
        private MvxSimpleTableViewSource _source;
        public AllPostsView() : base(nameof(AllPostsView), null)
        {

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _source = new MvxSimpleTableViewSource(AllPostsTableView, nameof(PostViewCell), PostViewCell.Key);
            AllPostsTableView.RowHeight = 130;



            bool isUserExists = CrossSecureStorage.Current.HasKey("securityToken");

            EdgesForExtendedLayout = UIRectEdge.None;

            categoryPicker.ShowSelectionIndicator = true;
            MvxPickerViewModel categoryPikerViewModel = new MvxPickerViewModel(categoryPicker);
            categoryPicker.Model = categoryPikerViewModel;
            categoryPikerViewModel.SelectedItemChanged += CategoryPikerViewModel_SelectedItemChanged;

            filterPicker.ShowSelectionIndicator = true;
            MvxPickerViewModel filterPickerViewModel = new MvxPickerViewModel(filterPicker);
            filterPicker.Model = filterPickerViewModel;
            MvxFluentBindingDescriptionSet<AllPostsView, AllPostsViewModel> set = this.CreateBindingSet<AllPostsView, AllPostsViewModel>();

            set.Bind(_source).For(v => v.ItemsSource).To(vm => vm.AllPosts);
            set.Bind(_source).For(v => v.SelectedItem).To(vm => vm.SelectedPost);
            set.Bind(categoryPikerViewModel).For(p => p.ItemsSource).To(vm => vm.CategoryItems);
            set.Bind(filterPickerViewModel).For(p => p.ItemsSource).To(vm => vm.FilterItems);
            set.Bind(filterPickerViewModel).For(p => p.SelectedItem).To(vm => vm.SelectedFilter);

            set.Apply();

            AllPostsTableView.Source = _source;
            AllPostsTableView.ReloadData();

            AllPostsTableView.SeparatorColor = UIColor.FromRGBA(109, 179, 206, 255);
            AllPostsTableView.BackgroundColor = UIColor.FromRGBA(209, 188, 171, 255);
            UIBarButtonItem threeDotsButton = new UIBarButtonItem
            {

                Image = UIImage.FromFile("icons8_more_48.png")
            };

            NavigationItem.HidesBackButton = true;
            NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { threeDotsButton };
            filterPickerViewModel.SelectedItemChanged += (a, e) => { AllPostsTableView.ReloadData(); };
            categoryPikerViewModel.SelectedItemChanged += (a, e) => { AllPostsTableView.ReloadData(); };
            threeDotsButton.Clicked += (a, e) =>
            {

                UIAlertController alert = UIAlertController.Create("Hi", "choose an action", UIAlertControllerStyle.ActionSheet);
                alert.AddAction(UIAlertAction.Create("About Us", UIAlertActionStyle.Default, (UIAlertAction obj) =>
                {
                    ViewModel.AboutUsComand.Execute();
                }));
                if (isUserExists)
                {
                    alert.AddAction(UIAlertAction.Create("Add Post", UIAlertActionStyle.Default, (UIAlertAction obj) =>
                    {
                        ViewModel.AddPostCommand.Execute();
                    }));

                }
                if (!isUserExists)
                {
                    alert.AddAction(UIAlertAction.Create("Join Us", UIAlertActionStyle.Default, (UIAlertAction obj) =>
                     {
                         ViewModel.LoginCommand.Execute();
                     }));
                }

                PresentViewController(alert, true, null);
            };
            if (isUserExists)
            {
                UIBarButtonItem menuButton = new UIBarButtonItem
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

        private void CategoryPikerViewModel_SelectedItemChanged(object sender, EventArgs e)
        {
            if (categoryPicker.Model is MvxPickerViewModel mvxPickerViewModel)
            {
                if (mvxPickerViewModel.SelectedItem is GetAllCategoryResponseModel item)
                {
                    ViewModel.SelectedCategory = item;
                }
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewModel.OnResume();
            NavigationController.NavigationBar.Hidden = false;
            NavigationController.NavigationBar.BackgroundColor = UIColor.FromRGBA(209, 188, 171, 255);
        }
    }

}