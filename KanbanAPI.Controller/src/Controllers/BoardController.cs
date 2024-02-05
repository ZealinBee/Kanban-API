using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using KanbanAPI.Business;
using KanbanAPI.Domain;

namespace KanbanAPI.Controller;

[ApiController]

public class BoardController : BaseController<Board, CreateBoardDto, GetBoardDto, UpdateBoardDto>
{
    private readonly IBoardService _boardService;
    private readonly ICustomAuthorizationService _customAuthService;
    public BoardController(IBoardService boardService, ICustomAuthorizationService customAuthService) : base(boardService)
    {
        _boardService = boardService;
        _customAuthService = customAuthService;
    }

    [Authorize]
    [HttpPost]
    public override async Task<ActionResult<GetBoardDto>> CreateOneAsync([FromBody] CreateBoardDto dto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var board = await _service.CreateOneAsync(dto, Guid.Parse(userId));
            return Ok(board);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpGet("my-boards")]
    public async Task<ActionResult<List<GetBoardDto>>> GetMyBoards()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var boards = await _boardService.GetBoardsForUser(Guid.Parse(userId));
        return Ok(boards);
    }

    [HttpPut("{board-id:Guid}/members")]
    public async Task<ActionResult<GetBoardDto>> AddMember([FromRoute(Name = "board-id")] Guid boardId, [FromBody] MemberDto dto)
    {
        try
        {
            await _customAuthService.IsUserAuthorizedForBoard(boardId, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var board = await _boardService.AddMember(boardId, dto);
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
            await _customAuthService.IsUserAuthorizedForBoard(boardId, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var board = await _boardService.RemoveMember(boardId, dto);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

}