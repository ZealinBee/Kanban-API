using KanbanAPI.Domain;
using KanbanAPI.Business;

using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Infrastructure;

public class BaseRepo<T> : IBaseRepo<T> where T : class
{
    protected readonly DatabaseContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepo(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public virtual async Task<T> CreateOneAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T> GetOneAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(QueryOptions options)
    {
        var entities = await _dbSet.Skip(options.Offset).Take(options.Limit).ToListAsync();
        return entities;
    }

    public virtual async Task<T> UpdateOneAsync(T entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteOneAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }

}