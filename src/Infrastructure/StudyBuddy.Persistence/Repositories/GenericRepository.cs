using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Interfaces.Repositories;
using StudyBuddy.Persistence.Context;

namespace StudyBuddy.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T: class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet;
        query = query.Where(predicate);

        if (includeProperties.Any())
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }
        return await query.SingleOrDefaultAsync();
    }

    public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet;
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (includeProperties.Any())
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }
        return await query.ToListAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        var result=await _dbSet.AddAsync(entity);
        return result.Entity;
    }

    public void UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
    }
}