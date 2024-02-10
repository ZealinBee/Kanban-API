using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public class CreateItemDto
{
    public string Name { get; set; } = String.Empty;
    public Guid BoardId { get; set; } = Guid.Empty;
}

public class GetItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime CreatedAt { get; init; }
    public ItemStatus Status { get; set; } = ItemStatus.Todo;
    public Guid BoardId { get; set; }
    public List<GetUserDto> Users { get; set; } = new();
}

public class UpdateItemDto
{
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public ItemStatus Status { get; set; } = ItemStatus.Todo;
    public Guid BoardId { get; set; } = Guid.Empty;
}


public class AssignUserDto
{
    public Guid UserId { get; set; } = Guid.Empty;
    public Guid BoardId { get; set; } = Guid.Empty;
}