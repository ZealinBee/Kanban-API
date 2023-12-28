namespace KanbanAPI.Domain;
public interface IBaseRepo<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetOneAsync(Guid id);
    Task<T> CreateOneAsync(T entity);
    Task<T> UpdateOneAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
}