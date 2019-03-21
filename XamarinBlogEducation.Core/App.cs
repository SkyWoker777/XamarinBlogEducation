using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.ViewModels;
using MvvmCross.IoC;
using XamarinBlogEducation.Core.Services;
using XamarinBlogEducation.Core.Services.Interfaces;
using XamarinBlogEducation.Core.ViewModels.Activities;
using MvvmCross.Base;
using MvvmCross.Plugin.Json;
using MvvmCross.Plugin;

namespace XamarinBlogEducation.Core
{
    public class App: MvxApplication
    {
       
        public override void Initialize()
        {
            //CreatableTypes()
             //   .EndingWith("Service")
            //    .AsInterfaces()
            //    .RegisterAsLazySingleton();
            Mvx.IoCProvider.RegisterType<IUserService, UserService>();
            Mvx.IoCProvider.RegisterType<IBlogService, BlogService>();
            Mvx.IoCProvider.RegisterType<IHttpService, HttpService>();
            Mvx.IoCProvider.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.IoCProvider.RegisterType<IUserDialogs>(() => UserDialogs.Instance);
            RegisterAppStart<LoginViewModel>();

        }
       
    }
}
