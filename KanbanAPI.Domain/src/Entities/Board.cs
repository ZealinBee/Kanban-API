namespace KanbanAPI.Domain;

public record Board
{
    public Guid Id { get; init; }
    public string Name { get; set; } = String.Empty;
    public List<User> Users { get; } = new();
}