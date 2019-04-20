using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using XamarinBlogEducation.Core.ViewModels.Fragments;

namespace XamarinBlogEducation.iOS.Views
{
    
    public partial class MenuView : MvxViewController<MenuViewModel>
    {
        private MenuTableViewSource _source;

        public MenuView() : base(nameof(MenuView), null)
        {
           
        }
        
        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            EdgesForExtendedLayout = UIRectEdge.None;
            //MenuTableView.BackgroundColor = UIColor.Clear;
            _source = new MenuTableViewSource(MenuTableView);
            MenuTableView.Source = _source;
            MenuTableView.SeparatorColor = UIColor.FromRGBA(209, 188, 171, 255);
            MenuTableView.BackgroundColor = UIColor.FromRGBA(209, 188, 171,255);
            var set = this.CreateBindingSet<MenuView, MenuViewModel>();

            set.Bind(_source).For(v => v.ItemsSource).To(vm => vm.MenuItems);
            lblUserName.Text= ViewModel.UserName;
            set.Bind(_source).For(v => v.SelectionChangedCommand).To(vm => vm.MenuItemSelectedCommand);
            set.Apply();
            
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBar.BackgroundColor = UIColor.FromRGBA(209, 188, 171, 255);
            NavigationController.NavigationBar.Hidden = false;
            var menuButton = new UIBarButtonItem
            {

                Image = UIImage.FromFile("menu.png")
            };

            menuButton.Clicked += async (s, e) =>
            {
                await ViewModel.DisposeView(ViewModel);
            };

            NavigationItem.HidesBackButton = true;
            NavigationItem.LeftBarButtonItems = new UIBarButtonItem[]
            {
               menuButton
            };
        }


        #endregion
    }
}