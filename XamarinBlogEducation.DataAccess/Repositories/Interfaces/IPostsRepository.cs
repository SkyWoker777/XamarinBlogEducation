using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.DataAccess.Entities;

namespace XamarinBlogEducation.DataAccess.Repositories.Interfaces
{
    public interface IPostsRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetByDate(DateTime CreationDate);
        Task<IEnumerable<Post>> GetByCategory(long categoryId);
        Task<Post> GetPost(long id);
        Task<IEnumerable<Category>> GetAllCategories();
        Task<IEnumerable<Post>> GetByAuthor(string userId);
        Task<string> GetCategoryName(long id);
    }
}
