namespace KanbanAPI.Domain;

public record Board
{
    public Guid Id { get; init; }
    public string Name { get; set; } = String.Empty;
    public ICollection<UserBoard> UserBoards { get; set; }
}