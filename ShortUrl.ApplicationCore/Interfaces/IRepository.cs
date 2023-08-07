using System.Linq.Expressions;

namespace ShortUrl.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Items { get; }
        T? GetById(int id);
        void Update(T entity);
        List<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        void Create(T item);
        void Delete(T item);
    }
}
