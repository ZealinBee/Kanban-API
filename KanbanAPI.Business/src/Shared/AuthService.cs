using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public class AuthService : IAuthService
{
    private readonly IUserRepo _userRepo;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepo userRepo, IConfiguration configuration)
    {
        _userRepo = userRepo;
        _configuration = configuration;
    }

    public async Task<string> VerifyCredentials(LoginUserDto dto)
    {
        var foundUser = await _userRepo.GetOneByEmailAsync(dto.Email);
        if (foundUser == null)
            throw new Exception("Email not found");
        var isAuthenticated = PasswordService.VerifyPasswordHash(dto.Password, foundUser.Password, foundUser.Salt);
        if (!isAuthenticated)
            throw new Exception("Password is incorrect");
        return GenerateToken(foundUser);
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
        };
        var secret = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds,
            Issuer = issuer,
            Audience = audience,
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<bool> ChangePassword(UpdatePasswordDto dto, Guid userId)
    {
        var foundUser = await _userRepo.GetOneAsync(userId);
        if (foundUser == null)
            throw new Exception("User not found");
        if (dto.NewPassword == dto.OldPassword)
            throw new Exception("New password cannot be the same as the old password");
        var isAuthenticated = PasswordService.VerifyPasswordHash(dto.OldPassword, foundUser.Password, foundUser.Salt);
        if (!isAuthenticated)
            throw new Exception("Old password is incorrect");
        PasswordService.HashPassword(dto.NewPassword, out var hashedPassword, out var passwordSalt);
        foundUser.Password = hashedPassword;
        foundUser.Salt = passwordSalt;
        await _userRepo.UpdateOneAsync(foundUser);
        return true;
    }
}
