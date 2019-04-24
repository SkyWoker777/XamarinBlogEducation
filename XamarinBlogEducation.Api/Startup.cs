﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using XamarinBlogEducation.Api.Extensions;
using XamarinBlogEducation.Api.Middlewares;
using XamarinBlogEducation.Business;

namespace XamarinBlogEducation.Api
{
    public class Startup
    {
        private const string DefaultConnection= "DefaultConnection";
        private const string authenticationName = "Bearer";
        private const string policyName = "corsPolicy";
        public Startup(IHostingEnvironment env, IServiceScopeFactory serviceScopeFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Console.WriteLine("*Environment: {0}", env.EnvironmentName);
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString(DefaultConnection);

            Business.Startup.Configure(services, connectionString);

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(authenticationName, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:ISSUER"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:AUDIENCE"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });

            var corsPolicy = new CorsPolicy();
            corsPolicy.Headers.Add("*");
            corsPolicy.Methods.Add("*");
            corsPolicy.Origins.Add("*");
            corsPolicy.SupportsCredentials = true;
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, corsPolicy);
            });
           services.AddAutoMapper();


            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors(policyName);
            app.UseCustomExceptionMiddleware();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
           Business.Startup.EnsureUpdate(serviceProvider, Configuration);
        }
    }
}
