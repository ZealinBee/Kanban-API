namespace KanbanAPI.Domain;
public interface IBaseRepo<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetOneAsync(Guid id);
    Task<T> CreateOneAsync(T entity);
    Task<T> UpdateOneAsync(T entity);
    Task<bool> DeleteOneAsync(Guid id);
}