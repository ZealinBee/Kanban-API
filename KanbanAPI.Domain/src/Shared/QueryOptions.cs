namespace KanbanAPI.Domain;
public class QueryOptions
{
    public int Offset { get; set; } = 0;
    public int Limit { get; set; } = 10;
}