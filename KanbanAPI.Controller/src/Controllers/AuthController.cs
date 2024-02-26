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
    public async Task<ActionResult<RefreshTokenDto>> Login([FromBody] LoginUserDto dto)
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

    [Authorize]
    [HttpPost("refresh")]
    public async Task<ActionResult<string>> Refresh([FromBody] RefreshTokenDto dto)
    {
        try
        {
            var principal = _authService.GetPrincipalFromExpiredToken(dto.AccessToken);
            Console.WriteLine("principal" + principal);
            Console.WriteLine("principal identity" + principal.Identity);
            Console.WriteLine("principal identity name" + principal.Identity.Name);
            if (principal?.Identity.Name is null)
                return Unauthorized();
            var userId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
            return Ok(await _authService.GenerateToken(userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}