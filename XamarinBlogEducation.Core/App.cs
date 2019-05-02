
using MvvmCross;
using MvvmCross.ViewModels;
using MvvmCross.IoC;
using XamarinBlogEducation.Core.ViewModels;
using Acr.UserDialogs;

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
            Mvx.IoCProvider.RegisterSingleton(() => UserDialogs.Instance);
            RegisterCustomAppStart<AppStart>();

        }
       
    }
}
