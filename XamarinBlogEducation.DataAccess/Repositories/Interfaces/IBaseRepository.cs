using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinBlogEducation.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        Task<IEnumerable<TEntity>> GetList();

        Task Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        Task SaveChanges();
    }
}
