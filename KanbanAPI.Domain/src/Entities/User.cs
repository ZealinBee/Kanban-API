namespace KanbanAPI.Domain;
public record User

{
    public Guid Id { get; init; }
    public string Username { get; set; } = String.Empty;
    public string Email { get; init; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public byte[] Salt { get; set; }
    public string UserImage = "https://media.istockphoto.com/id/1337144146/vector/default-avatar-profile-icon-vector.jpg?s=612x612&w=0&k=20&c=BIbFwuv7FxTWvh5S3vB6bkT0Qv8Vn8N5Ffseq84ClGI=";
    public ICollection<UserBoard> UserBoards { get; set; }
}