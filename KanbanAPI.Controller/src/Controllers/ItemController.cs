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
    [HttpGet("{id:Guid}")]
    public override async Task<ActionResult<GetItemDto>> GetOneAsync([FromRoute] Guid id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var item = await _itemService.GetOneAsync(id);
            await _customAuthService.IsUserAuthorizedForBoard(item.BoardId, Guid.Parse(userId));
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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
    [HttpPut("{id:Guid}")]
    public override async Task<ActionResult<GetItemDto>> UpdateOneAsync([FromRoute] Guid id, [FromBody] UpdateItemDto dto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _itemService.IsItemStatusValid(dto.Status);
            await _customAuthService.IsUserAuthorizedForBoard(dto.BoardId, Guid.Parse(userId));
            var updatedItem = await _itemService.UpdateOneAsync(dto, id);
            return Ok(updatedItem);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{id:Guid}")]
    public override async Task<ActionResult> DeleteOneAsync([FromRoute] Guid id)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var item = await _itemService.GetOneAsync(id);
            await _customAuthService.IsUserAuthorizedForBoard(item.BoardId, Guid.Parse(userId));
            await _itemService.DeleteOneAsync(id);
            return NoContent();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPost("{item-id:Guid}/assign-user")]
    public async Task<ActionResult<GetItemDto>> AssignUser([FromRoute(Name = "item-id")] Guid itemId, [FromBody] AssignUserDto dto)
    {
        try
        {
            var actionUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _itemService.IsItemPartOfBoard(itemId, dto.BoardId);
            await _customAuthService.IsUserAuthorizedForBoard(dto.BoardId, actionUserId);
            var updatedItem = await _itemService.AssignUser(itemId, dto.UserId);
            return Ok(updatedItem);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{item-id:Guid}/remove-user")]
    public async Task<ActionResult<GetItemDto>> RemoveUser([FromRoute(Name = "item-id")] Guid itemId, [FromBody] AssignUserDto dto)
    {
        try
        {
            var actionUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _itemService.IsItemPartOfBoard(itemId, dto.BoardId);
            await _customAuthService.IsUserAuthorizedForBoard(dto.BoardId, actionUserId);
            var updatedItem = await _itemService.RemoveUser(itemId, dto.UserId);
            return NoContent();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }



}