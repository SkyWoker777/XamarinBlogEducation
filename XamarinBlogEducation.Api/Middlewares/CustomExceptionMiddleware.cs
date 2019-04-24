using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using XamarinBlogEducation.Business.Exceptions;

namespace XamarinBlogEducation.Api.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BaseException ex)
            {
                
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex);
            }


        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception ex)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new
            {
                ErrorCode = statusCode,
                ErrorMessage = ex.Message ?? "Internal Server Error."
            }.ToString());
        }
    }

}
