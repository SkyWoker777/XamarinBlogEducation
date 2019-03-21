using System;
using System.Collections.Generic;
using System.Text;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.DataAccess.Repositories.Interfaces;

namespace XamarinBlogEducation.DataAccess.Repositories
{
   public class CategoriesRepository: BaseRepository<Category>,ICategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
