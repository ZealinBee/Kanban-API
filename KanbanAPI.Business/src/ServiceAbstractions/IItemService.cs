using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public interface IItemService : IBaseService<CreateItemDto, GetItemDto, UpdateItemDto>
{
    Task<GetItemDto> AddUser(Guid taskId, MemberDto dto);
    Task<bool> RemoveUser(Guid taskId, MemberDto dto);
}