using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KanbanAPI.Business;
using KanbanAPI.Domain;

namespace KanbanAPI.Controller;

[ApiController]

public class BoardController : BaseController<Board, CreateBoardDto, GetBoardDto, UpdateBoardDto>
{
    private readonly IBoardService _service;
    public BoardController(IBoardService service) : base(service)
    {
        _service = service;
    }
}