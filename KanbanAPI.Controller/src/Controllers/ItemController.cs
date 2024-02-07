using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using KanbanAPI.Business;
using KanbanAPI.Domain;

namespace KanbanAPI.Controller;

[ApiController]

public class ItemController : BaseController<Item, CreateItemDto, GetItemDto, UpdateItemDto>
{
    private readonly IItemService _itemService;
    private readonly ICustomAuthorizationService _customAuthService;
    public ItemController(IItemService itemService, ICustomAuthorizationService customAuthService) : base(itemService)
    {
        _itemService = itemService;
        _customAuthService = customAuthService;
    }

    [Authorize]
    [HttpPost]
    public override async Task<ActionResult<GetItemDto>> CreateOneAsync([FromBody] CreateItemDto dto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _customAuthService.IsUserAuthorizedForBoard(dto.BoardId, Guid.Parse(userId));
            return await _itemService.CreateOneAsync(dto, Guid.Parse(userId));
        }
        catch (UnauthorizedAccessException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public override async Task<ActionResult<GetItemDto>> UpdateOneAsync([FromRoute] Guid id, [FromBody] UpdateItemDto dto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _customAuthService.IsUserAuthorizedForBoard(dto.BoardId, Guid.Parse(userId));
            var updatedItem = await _itemService.UpdateOneAsync(dto, id);
            return Ok(updatedItem);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

}