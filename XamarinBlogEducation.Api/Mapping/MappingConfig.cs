using AutoMapper;
using System;
using StructureMap;
using XamarinBlogEducation.Business;

namespace XamarinBlogEducation.Api.Mapping
{
    public class MappingConfig
    {
        private static readonly Lazy<MappingConfig> AutomappingConfig = new Lazy<MappingConfig>(InternalConfigure);
        private static IContainer _container;
        /// <summary>
        /// Configures the automapping profiles only once at startup.
        /// Should be triggered early.
        /// </summary>
        public static void Configure(IContainer container)
        {
            _container = container;
            var value = AutomappingConfig.Value;
        }

        private MappingConfig()
        {

        }

        private static MappingConfig InternalConfigure()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            _container.Configure(c => c.For<MapperConfiguration>().Use(config));
            _container.Configure(c => c.For<IMapper>().Use(ctx => ctx.GetInstance<MapperConfiguration>().CreateMapper(ctx.GetInstance)));

            return new MappingConfig();
        }
    }
}
