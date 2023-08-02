﻿using Microsoft.EntityFrameworkCore;
using ShortUrl.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        DbSet<T> _dbSet;

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public T? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }


        public void Create(T item)
        {
            _dbSet.Add(item);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
