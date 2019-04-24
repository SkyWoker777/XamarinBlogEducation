﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinBlogEducation.DataAccess.Repositories.Interfaces;

namespace XamarinBlogEducation.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetList()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _dbContext.AddAsync<TEntity>(entity);
            await SaveChangesAsync();
        }

        public async void DeleteAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Edit(TEntity entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChangesAsync();
        }
    }
}
