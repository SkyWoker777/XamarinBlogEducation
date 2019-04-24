using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.DataAccess.Repositories.Interfaces;

namespace XamarinBlogEducation.DataAccess.Repositories
{
    public class DbSeeder:IDbSeeder
    {
        private readonly ICategoriesRepository _categoryRepository;

        public DbSeeder(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void SeedDb()
        {
            _categoryRepository.Add(new Category()
            {
                Name = "common category"
           });
        }
      

    }
}

