
using MvvmCross.Platforms.Ios.Views;
using XamarinBlogEducation.Core.ViewModels;

namespace XamarinBlogEducation.iOS.Views
{
    public partial class MainController : MvxViewController<MainViewModel>
    {
        public MainController() : base(nameof(MainController),null)
        {
        }
     
    }
}