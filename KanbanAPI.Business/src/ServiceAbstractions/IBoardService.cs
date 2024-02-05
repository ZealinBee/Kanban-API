using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public interface IBoardService : IBaseService<CreateBoardDto, GetBoardDto, UpdateBoardDto>
{
    Task<GetBoardDto> AddMember(Guid id, MemberDto dto);
    Task<bool> RemoveMember(Guid id, MemberDto dto);
    Task<List<GetBoardDto>> GetBoardsForUser(Guid userId);
}