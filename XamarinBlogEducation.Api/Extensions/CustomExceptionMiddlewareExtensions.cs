using Microsoft.AspNetCore.Builder;
using XamarinBlogEducation.Api.Middlewares;

namespace XamarinBlogEducation.Api.Extensions
{
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
