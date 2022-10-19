using System.Linq.Expressions;

namespace StudyBuddy.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T: class
{
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate=null, params Expression<Func<T, object>>[] includeProperties);
    Task CreateAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
}