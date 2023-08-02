using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T? GetById(int id);
        void Update(T entity);
        List<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        void Create(T item);
    }
}
