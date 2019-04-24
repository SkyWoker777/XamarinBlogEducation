using MvvmCross.Platforms.Ios.Views;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.iOS.Views
{
    public partial class StartView : MvxViewController<StartViewModel>
    {
        public StartView() : base(nameof(StartView),null)
        {
        }
        
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
           
            ViewModel.LoginCommand.Execute();
        }
        

        #endregion
    }
}