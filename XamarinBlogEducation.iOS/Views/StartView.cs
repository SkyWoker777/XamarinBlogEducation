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
            base.DidReceiveMemoryWarning();
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