namespace KanbanAPI.Business;

public class CreateBoardDto
{
    public string Name { get; set; } = String.Empty;
}

public class GetBoardDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = String.Empty;
}

public class UpdateBoardDto
{
    public string Name { get; set; } = String.Empty;
}


public class MemberDto
{
    public Guid UserId { get; set; } = Guid.Empty;
}