
using MvvmCross;
using MvvmCross.ViewModels;
using MvvmCross.IoC;
using XamarinBlogEducation.Core.ViewModels;


namespace XamarinBlogEducation.Core
{
    public class App: MvxApplication
    {
       
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterCustomAppStart<AppStart>();

        }
       
    }
}
