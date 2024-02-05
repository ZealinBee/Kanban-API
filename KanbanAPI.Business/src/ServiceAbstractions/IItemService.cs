using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public interface IItemService : IBaseService<CreateItemDto, GetItemDto, UpdateItemDto>
{
    Task<GetItemDto> AddUser(Guid itemId, MemberDto dto);
    Task<bool> RemoveUser(Guid itemId, MemberDto dto);
    Task<bool> IsItemPartOfBoard(Guid itemId, Guid boardId);
}