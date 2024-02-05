using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public interface ICustomAuthorizationService
{
    Task<bool> IsUserAuthorizedForBoard(Guid boardId, Guid userId);
}