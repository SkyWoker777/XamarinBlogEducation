using Foundation;
using MvvmCross.Platforms.Ios.Core;
using UIKit;
using XamarinBlogEducation.Core;
using XamarinBlogEducation.iOS;

namespace Blank
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            CustomizeAppearance();

            return result;
        }

        private void CustomizeAppearance()
        {

        }
    }
}


