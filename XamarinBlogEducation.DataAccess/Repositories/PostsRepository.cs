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
        public PostsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Post>> GetByCategory(long categoryId)
        {
            List<Post> postsByCategory = await _dbContext.Posts.Where(x => x.CategoryId == categoryId).ToListAsync();
            return postsByCategory;
        }
        public async Task<IEnumerable<Post>> GetByAuthor(string userId)
        {
            List<Post> postsByAuthor = await _dbContext.Posts.Where(x => x.AuthorId != null && x.AuthorId == userId).ToListAsync();
            return postsByAuthor;
        }
        public async Task<IEnumerable<Post>> GetByDate(DateTime creationDate)
        {
            List<Post> postsByDate = await _dbContext.Posts
               .Where(x => x.CreationDate == creationDate)
               .ToListAsync();
            return postsByDate;
        }
        public async Task<string> GetCategoryName(long id)
        {
            Category category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            string categoryName = category?.Name;
            return categoryName;
        }

        public async Task<Post> GetPost(long id)
        {
            Post postById = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return postById;
        }

    }
}