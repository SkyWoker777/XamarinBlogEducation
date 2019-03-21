using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByCategory(long categoryId)
        {
            return await _context.Posts.Where(x => x.CategoryId != null && x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByDate(DateTime creationDate)
        {
            return await _context.Posts
               .Where(x => x.CreationDate == creationDate)
               .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetByKey(string key)
        {
            var result = await _context.Posts
                .Where(x => x.Content != null && x.Content.Contains(key))
                .ToListAsync();
            return result;
        }

        public async Task<Post> GetPost(int id)
        {
            var res = await _context.Posts.Where(x => x.Id == id).FirstOrDefaultAsync<Post>();
            if (res == null)
                return null;
            return res;
        }

    }
}