using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public class CustomAuthorizationService : ICustomAuthorizationService
{
    private readonly IBoardRepo _boardRepo;

    private readonly IUserRepo _userRepo;

    public CustomAuthorizationService(IBoardRepo boardRepo, IUserRepo userRepo)
    {
        _boardRepo = boardRepo;
        _userRepo = userRepo;
    }

    public async Task<bool> IsUserAuthorizedForBoard(Guid boardId, Guid userId)
    {
        var board = await _boardRepo.GetOneWithUsersAsync(boardId);
        if (board == null)
            throw new KeyNotFoundException("Board not found");
        if (!board.Users.Any(u => u.Id == userId))
            throw new UnauthorizedAccessException("User not authorized for the board");
        return true;
    }
}