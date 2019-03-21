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
    public class CommentsRepository : BaseRepository<Comment>, ICommentsRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetList(long postId)
        {
            return await _context.Comments
                .Where(x => x.Post.Id == postId)
                .ToListAsync();
        }
    }
}
