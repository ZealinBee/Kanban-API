namespace KanbanAPI.Domain;

public record UserBoard
{
    public Guid UserId { get; init; }
    public Guid BoardId { get; init; }
    public User User { get; set; }
    public Board Board { get; set; }
}