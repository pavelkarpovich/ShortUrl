using Microsoft.EntityFrameworkCore;
using ShortUrl.ApplicationCore.Interfaces;
using System.Linq.Expressions;

namespace ShortUrl.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        public IQueryable<T> Items => _dbContext.Set<T>();

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
            return Items.AsNoTracking().Where(predicate).ToList();
        }


        public void Create(T item)
        {
            _dbContext.Add(item);
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Items.Where(predicate).ToListAsync();
        }

        public void Delete(T item)
        {
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }
    }
}
