using UIKit;

namespace XamarinBlogEducation.iOS
{
    public class Application
    {
        static void Main(string[] args)
        {
            try
            {
                UIApplication.Main(args, null, nameof(AppDelegate));

            }
            catch (System.Exception ex)
            {

                var p = ex;
            }
        }
    }
}