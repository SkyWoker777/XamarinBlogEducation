
using System;
using System.Drawing;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace XamarinBlogEducation.iOS.Views.Cells
{
    public partial class AllPostViewCell : BaseTableViewCell
    {
        private UILabel _lblTitle;
        private UILabel _lblDate;
        private UILabel _lblDescripion;
        private const float PADDING = 12f;
        public AllPostViewCell(IntPtr handle) : base(handle)
        {
        }
        protected override void CreateView()
        {
            base.CreateView();

            SelectionStyle = UITableViewCellSelectionStyle.None;

            _lblTitle = new UILabel
            {
                TextColor = UIColor.Brown,
                TextAlignment = UITextAlignment.Left,
                Font = UIFont.SystemFontOfSize(30f, UIFontWeight.Bold)
            };
            _lblDate = new UILabel
            {
                TextColor = UIColor.Gray,
                TextAlignment = UITextAlignment.Left,
                Font = UIFont.SystemFontOfSize(10f, UIFontWeight.Thin)
            };
            _lblDescripion = new UILabel
            {
                TextColor = UIColor.Brown,
                TextAlignment = UITextAlignment.Justified,
                Font = UIFont.SystemFontOfSize(20f, UIFontWeight.Bold)
            };
            ContentView.AddSubviews(_lblTitle, _lblDate, _lblDescripion);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.DelayBind(
                () =>
                {
                    this.AddBindings(_lblTitle, "Text Title");
                    this.AddBindings(_lblDate, "Text CreationDate");
                    this.AddBindings(_lblDescripion, "Text Description");
                });
        }
        protected override void CreateConstraints()
        {
            base.CreateConstraints();

            ContentView.AddConstraints(
                _lblTitle.AtLeftOf(ContentView, PADDING),
                _lblTitle.AtTopOf(ContentView, PADDING),
                _lblTitle.AtBottomOf(ContentView, PADDING),
                _lblTitle.AtRightOf(ContentView, PADDING),
                _lblDate.AtLeftOf(ContentView, PADDING),
                _lblDate.AtTopOf(_lblTitle, 10f),
                _lblDate.AtRightOf(ContentView, PADDING),
                 _lblDescripion.AtLeftOf(ContentView, PADDING),
                _lblDescripion.AtTopOf(_lblDate, 10f),
                _lblDescripion.AtRightOf(ContentView, PADDING)
            );
        }
    }
}