using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public interface IBoardService : IBaseService<CreateBoardDto, GetBoardDto, UpdateBoardDto>
{
}