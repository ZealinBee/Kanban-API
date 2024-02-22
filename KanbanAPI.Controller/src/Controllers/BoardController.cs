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
            var board = await _boardService.CreateOneAsync(dto, Guid.Parse(userId));
            return Ok(board);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpGet("{board-id:Guid}")]
    public override async Task<ActionResult<GetBoardDto>> GetOneAsync([FromRoute(Name = "board-id")] Guid boardId)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _customAuthService.IsUserAuthorizedForBoard(boardId, Guid.Parse(userId));
            var board = await _boardService.GetOneAsync(boardId);
            return Ok(board);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpGet("all-my-boards")]
    public async Task<ActionResult<List<GetBoardDto>>> GetMyBoards()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var boards = await _boardService.GetAllAsync(Guid.Parse(userId));
        return Ok(boards);
    }

    [Authorize]
    [HttpPost("{board-id:Guid}/members")]
    public async Task<ActionResult<GetBoardDto>> AddMember([FromRoute(Name = "board-id")] Guid boardId, [FromBody] Guid userId)
    {
        try
        {
            var actionUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _customAuthService.IsUserAuthorizedForBoard(boardId, Guid.Parse(actionUserId));
            var board = await _boardService.AddMember(boardId, userId);
            return Ok(board);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{board-id:Guid}/members")]
    public async Task<ActionResult<bool>> RemoveMember([FromRoute(Name = "board-id")] Guid boardId, [FromBody] Guid userId)
    {
        try
        {
            var actionUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _customAuthService.IsUserAuthorizedForBoard(boardId, Guid.Parse(actionUserId));
            var board = await _boardService.RemoveMember(boardId, userId);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

}