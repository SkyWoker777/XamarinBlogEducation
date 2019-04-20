using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;
using XamarinBlogEducation.iOS.Views.Cells;

namespace XamarinBlogEducation.iOS.Views
{
    public class AllPostsTableViewSource : MvxSimpleTableViewSource
    {
        public AllPostsTableViewSource(UITableView tableView) : base(tableView, typeof(AllPostViewCell))
        {
           DeselectAutomatically = true;

        }
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60f;
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