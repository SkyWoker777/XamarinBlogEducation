
using System;
using System.Drawing;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace XamarinBlogEducation.iOS.Views.Cells
{
    public partial class MenuTableViewCell : BaseTableViewCell
    {
        private const float PADDING = 12f;

        private UILabel _lblName;
      

        public MenuTableViewCell(IntPtr handle) : base(handle)
        {
        }

        protected override void CreateView()
        {
            base.CreateView();

            SelectionStyle = UITableViewCellSelectionStyle.None;

            _lblName = new UILabel
            {
                TextColor = UIColor.Black,
                TextAlignment=UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Bold)
            };
            ContentView.AddSubview(_lblName);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.DelayBind(
                () =>
                {
                    this.AddBindings(_lblName, "Text Title");
                });
        }
        protected override void CreateConstraints()
        {
            base.CreateConstraints();

            ContentView.AddConstraints(
                _lblName.AtLeftOf(ContentView, PADDING),
                _lblName.AtTopOf(ContentView, PADDING),
                _lblName.AtBottomOf(ContentView, PADDING),
                _lblName.AtRightOf(ContentView, PADDING)
            );
        }
    }
}