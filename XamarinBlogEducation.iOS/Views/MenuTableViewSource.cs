using System;
using Foundation;
using UIKit;
using XamarinBlogEducation.iOS.Views.Cells;
using MvvmCross.Platforms.Ios.Binding.Views;

namespace XamarinBlogEducation.iOS.Views
{
    public class MenuTableViewSource : MvxSimpleTableViewSource
    {
        public MenuTableViewSource(UITableView tableView) : base(tableView, typeof(MenuTableViewCell))
        {
            try
            {
                DeselectAutomatically = true;

            }
            catch (Exception ex)
            {
                var p = ex;
              
            }
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 40f;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return base.RowsInSection(tableview, section);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            try
            {
                var cell = base.GetOrCreateCellFor(tableView, indexPath, item);

              

                cell.BackgroundColor = UIColor.Clear;

                cell.SeparatorInset = UIEdgeInsets.Zero;
                cell.LayoutMargins = UIEdgeInsets.Zero;

                cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

                return cell;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}