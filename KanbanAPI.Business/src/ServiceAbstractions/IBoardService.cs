using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public interface IBoardService : IBaseService<CreateBoardDto, GetBoardDto, UpdateBoardDto>
{
    Task<GetBoardDto> AddMember(Guid boardId, Guid userId);
    Task<bool> RemoveMember(Guid boardId, Guid userId);
    Task<List<GetBoardDto>> GetAllAsync(QueryOptions options, Guid userId);
}