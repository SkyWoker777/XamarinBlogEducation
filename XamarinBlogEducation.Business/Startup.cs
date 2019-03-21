using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using XamarinBlogEducation.Business.Services;
using XamarinBlogEducation.Business.Services.Interfaces;
using XamarinBlogEducation.DataAccess;
using XamarinBlogEducation.DataAccess.Entities;

namespace XamarinBlogEducation.Business
{
    public class Startup
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            DataAccess.DependencyManager.Configure(services);
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<SignInManager<ApplicationUser>>();
            services.AddTransient<RoleManager<IdentityRole>>();
        }
        public static void EnsureUpdate(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var applicationDbContext = serviceProvider.GetService<ApplicationDbContext>();
            applicationDbContext.Database.EnsureCreated();

            CreateRoles(serviceProvider, configuration).GetAwaiter().GetResult();
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Guest", "Member" };

            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var email = configuration.GetSection("AdminDetails").GetValue<string>("Email");
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {

                var password = configuration.GetSection("AdminDetails").GetValue<string>("Password");
                var firstName = configuration.GetSection("AdminDetails").GetValue<string>("FirstName");
                var lastName = configuration.GetSection("AdminDetails").GetValue<string>("LastName");
                var admin = new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };

                var createBlogAdmin = await userManager.CreateAsync(admin, password);
                if (createBlogAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }

    }
}
