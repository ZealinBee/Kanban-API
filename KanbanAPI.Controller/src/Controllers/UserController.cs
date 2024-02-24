using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using KanbanAPI.Business;
using KanbanAPI.Domain;

namespace KanbanAPI.Controller;

[ApiController]
public class UserController : BaseController<User, CreateUserDto, GetUserDto, UpdateUserDto>
{
    private readonly IUserService _userService;
    public UserController(IUserService userService) : base(userService)
    {
        _userService = userService;
    }


    [Authorize]
    [HttpGet("my-profile")]
    public async Task<ActionResult<GetUserDto>> GetOwnProfileAsync()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var item = await _userService.GetOneAsync(userId);
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [Authorize]
    [HttpPut("my-profile")]
    public async Task<ActionResult<GetUserDto>> UpdateOwnProfileAsync([FromBody] UpdateUserDto dto)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _userService.DoesEmailExistAsync(dto.Email);
            var item = await _userService.UpdateOneAsync(dto, userId);
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("my-profile")]
    public async Task<ActionResult> DeleteOwnProfileAsync()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _userService.DeleteOneAsync(userId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpGet("{id:Guid}")]
    public override async Task<ActionResult<GetUserDto>> GetOneAsync([FromRoute] Guid id)
    {
        // you cannot get other users, there is no admin for this app
        try
        {
            throw (new UnauthorizedAccessException());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPut("{id:Guid}")]
    public override async Task<ActionResult<GetUserDto>> UpdateOneAsync([FromRoute] Guid id, [FromBody] UpdateUserDto dto)
    {
        // you cannot update other users
        try
        {
            throw (new UnauthorizedAccessException());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [Authorize]
    [HttpDelete("{id:Guid}")]
    public override async Task<ActionResult> DeleteOneAsync([FromRoute] Guid id)
    {
        // you cannot delete other users
        try
        {
            throw (new UnauthorizedAccessException());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}