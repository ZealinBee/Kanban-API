namespace KanbanAPI.Domain;

public interface IBoardRepo : IBaseRepo<Board>
{
    Task<Board> GetOneWithUsersAsync(Guid id);
}