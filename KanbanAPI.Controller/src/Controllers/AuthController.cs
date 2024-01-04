using Microsoft.AspNetCore.Mvc;

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

}