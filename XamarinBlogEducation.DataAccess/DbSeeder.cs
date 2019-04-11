using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinBlogEducation.DataAccess
{
    public static class DbSeeder
    {
        public static void SeedDb(ApplicationDbContext context)
        {
            SeedInitialData(context);
        }
        private static void SeedInitialData(ApplicationDbContext context)
        {
           // context.Database.EnsureCreated();
            if (!context.Categories.Any())
            {
                context.Categories.Add(
                  new Entities.Category()
                  { Name = "common category"
                  });                  
                context.SaveChanges();
            }
                if (!context.Tags.Any())
                {
                    context.Tags.Add(
                      new Entities.Tag()
                      {
                          Name = "common tag"
                      });
                    context.SaveChanges();
                }
            
        }

    }
}

