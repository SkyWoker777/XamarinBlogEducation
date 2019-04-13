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
        private readonly ICategoriesRepository _repository;
        public DbSeeder(ICategoriesRepository repository)
        {
            _repository = repository;
        }
        public  void SeedDb()
        {
            _repository.Add(new Category()
            {
                Name = "common category"
           });
        }
      

    }
}

