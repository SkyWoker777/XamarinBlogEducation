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
        public CommentsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetList(long postId)
        {

            var posts= await _dbContext.Comments
                .Where(x => x.Post.Id == postId)
                .ToListAsync();

            return posts;
        }
    }
}
