using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using XamarinBlogEducation.Core;

namespace XamarinBlogEducation.iOS
{
    public class Setup : MvxIosSetup<App>
    {
       
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
            
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
            };
        }

    }
}