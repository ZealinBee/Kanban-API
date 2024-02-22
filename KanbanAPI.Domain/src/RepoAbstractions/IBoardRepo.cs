namespace KanbanAPI.Domain;

public interface IBoardRepo : IBaseRepo<Board>
{
    Task<Board> GetOneWithUsersAsync(Guid id);
    Task<Board> GetOneWithItemsAsync(Guid id);
    Task<Board> GetOneWithUsersAndItemsAsync(Guid id);
}