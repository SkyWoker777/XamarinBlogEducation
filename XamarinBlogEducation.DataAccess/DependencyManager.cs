using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinBlogEducation.DataAccess.Repositories;
using XamarinBlogEducation.DataAccess.Repositories.Interfaces;

namespace XamarinBlogEducation.DataAccess
{
    public class DependencyManager
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IPostsRepository, PostsRepository>();
            services.AddTransient<ICommentsRepository, CommentsRepository>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();

        }
    }
}
