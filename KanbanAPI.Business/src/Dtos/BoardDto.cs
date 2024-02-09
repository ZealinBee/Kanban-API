namespace KanbanAPI.Business;

public class CreateBoardDto
{
    public string Name { get; set; } = String.Empty;
}

public class GetBoardDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = String.Empty;
    public List<GetUserDto> Users { get; set; } = new List<GetUserDto>();
    public List<GetItemDto> Items { get; set; } = new List<GetItemDto>();
}

public class UpdateBoardDto
{
    public string Name { get; set; } = String.Empty;
}

