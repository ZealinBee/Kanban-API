using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public interface IItemService : IBaseService<CreateItemDto, GetItemDto, UpdateItemDto>
{
    Task<GetItemDto> AssignUser(Guid itemId, Guid userId);
    Task<bool> RemoveUser(Guid itemId, Guid userId);
    Task<bool> IsItemPartOfBoard(Guid itemId, Guid boardId);
    Task<bool> IsItemStatusValid(ItemStatus status);
}