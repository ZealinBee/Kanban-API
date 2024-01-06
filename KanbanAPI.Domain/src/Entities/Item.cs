using System.Text.Json.Serialization;

namespace KanbanAPI.Domain;

public record Item
{
    public Guid Id { get; init; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime CreatedAt { get; init; }
    public ItemStatus Status { get; set; } = ItemStatus.Todo;
    public Guid BoardId { get; set; }
    public Board Board { get; set; } = null!;
    public List<User> Users { get; } = new();
}



[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ItemStatus
{
    Todo,
    InProgress,
    Review,
    Done
}