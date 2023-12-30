using KanbanAPI.Domain;
using AutoMapper;

namespace KanbanAPI.Business;

public class BoardService : BaseService<Board, CreateBoardDto, GetBoardDto, UpdateBoardDto>, IBoardService
{
    public BoardService(IBoardRepo repo, IMapper mapper) : base(repo, mapper)
    {
    }
}