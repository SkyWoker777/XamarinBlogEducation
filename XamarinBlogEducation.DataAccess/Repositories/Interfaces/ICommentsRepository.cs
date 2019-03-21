using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.DataAccess.Entities;

namespace XamarinBlogEducation.DataAccess.Repositories.Interfaces
{
    public interface ICommentsRepository : IBaseRepository<Comment>
    {
      Task<List<Comment>> GetList(long postId);
    }
}
