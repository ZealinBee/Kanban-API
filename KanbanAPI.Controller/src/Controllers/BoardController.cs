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

    [HttpPut("{board-id:Guid}/members")]
    public async Task<ActionResult<GetBoardDto>> AddMember([FromRoute(Name = "board-id")] Guid boardId, [FromBody] MemberDto dto)
    {
        try
        {
            var board = await _service.AddMember(boardId, dto);
            return Ok(board);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{board-id:Guid}/members")]
    public async Task<ActionResult<bool>> RemoveMember([FromRoute(Name = "board-id")] Guid boardId, [FromBody] MemberDto dto)
    {
        try
        {
            var board = await _service.RemoveMember(boardId, dto);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

}