using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public class CreateUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserImage { get; set; } = "https://media.istockphoto.com/id/1337144146/vector/default-avatar-profile-icon-vector.jpg?s=612x612&w=0&k=20&c=BIbFwuv7FxTWvh5S3vB6bkT0Qv8Vn8N5Ffseq84ClGI=";
}

public class GetUserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string UserImage { get; set; }
}

public class UpdateUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string UserImage { get; set; }
}

public class UpdatePasswordDto
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}

public class LoginUserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RefreshTokenDto
{
    public string RefreshToken { get; set; }
    public string AccessToken { get; set; }
}
