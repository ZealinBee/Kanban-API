using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using KanbanAPI.Business;
using KanbanAPI.Domain;

namespace KanbanAPI.Controller;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] LoginUserDto dto)
    {
        try
        {
            return Ok(await _authService.VerifyCredentials(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPut("change-password")]
    public async Task<ActionResult<bool>> ChangePassword([FromBody] UpdatePasswordDto dto)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _authService.ChangePassword(dto, userId);
            return Ok(true);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}