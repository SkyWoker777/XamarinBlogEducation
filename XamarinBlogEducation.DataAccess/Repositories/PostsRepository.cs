using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinBlogEducation.DataAccess.Entities;
using XamarinBlogEducation.DataAccess.Repositories.Interfaces;

namespace XamarinBlogEducation.DataAccess.Repositories
{
    public class PostsRepository : BaseRepository<Post>, IPostsRepository
    {
        private readonly ApplicationDbContext _context;

        public PostsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            List<Category> allCategories = await _context.Categories.ToListAsync();
            return allCategories;
        }

        public async Task<IEnumerable<Post>> GetByCategory(long categoryId)
        {
            List<Post> postsByCategory = await _context.Posts.Where(x => x.CategoryId == categoryId).ToListAsync();
            return postsByCategory;
        }
        public async Task<IEnumerable<Post>> GetByAuthor(string userId)
        {
            List<Post> postsByAuthor = await _context.Posts.Where(x => x.AuthorId != null && x.AuthorId == userId).ToListAsync();
            return postsByAuthor;
        }
        public async Task<IEnumerable<Post>> GetByDate(DateTime creationDate)
        {
            List<Post> postsByDate = await _context.Posts
               .Where(x => x.CreationDate == creationDate)
               .ToListAsync();
            return postsByDate;
        }
        public async Task<string> GetCategoryName(long id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            string categoryName = category?.Name;
            return categoryName;
        }

        public async Task<Post> GetPost(long id)
        {
            Post res = await _context.Posts.FirstOrDefaultAsync<Post>(x => x.Id == id);
            return res;
        }

    }
}