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
            set.Apply();
        }



        #endregion
    }
}