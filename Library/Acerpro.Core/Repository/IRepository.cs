using System.Linq.Expressions;

namespace Acerpro.Core.Repository
{
    public interface IRepository<T>
    {
        Task<T> Add(T item);
        Task<T> Update(T item);
        Task<bool> Remove(T item);
        Task<T> GetById(Guid id, params Expression<Func<T, object>>[] includeParameters);
        Task<T> GetByDefault(Expression<Func<T, bool>> exp, params Expression<Func<T, object>>[] includeParameters);
        IQueryable<T> GetDefault(Expression<Func<T, bool>> exp, params Expression<Func<T, object>>[] includeParameters);
        IQueryable<T> GetActive();
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        Task<bool> Any(Expression<Func<T, bool>> exp);
        Task<int> Save();
    }
}
