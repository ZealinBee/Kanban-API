using KanbanAPI.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace KanbanAPI.Business;
public interface IAuthService
{
    Task<RefreshTokenDto> VerifyCredentials(LoginUserDto dto);
    Task<bool> ChangePassword(UpdatePasswordDto dto, Guid userId);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Task<string> GenerateToken(Guid userId);
}