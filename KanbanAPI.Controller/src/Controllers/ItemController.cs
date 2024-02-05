using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using KanbanAPI.Business;
using KanbanAPI.Domain;

namespace KanbanAPI.Controller;

[ApiController]

public class ItemController : BaseController<Item, CreateItemDto, GetItemDto, UpdateItemDto>
{
    private readonly IItemService _service;
    private readonly ICustomAuthorizationService _authService;
    public ItemController(IItemService service, ICustomAuthorizationService authService) : base(service)
    {
        _service = service;
        _authService = authService;
    }

    [Authorize]
    [HttpPost]
    public override async Task<ActionResult<GetItemDto>> CreateOneAsync([FromBody] CreateItemDto dto)
    {
        try
        {
            await _authService.IsUserAuthorizedForBoard(dto.BoardId, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _service.CreateOneAsync(dto, Guid.Parse(userId));

        }
        catch (UnauthorizedAccessException e)
        {
            return BadRequest(e.Message);
        }
    }
}