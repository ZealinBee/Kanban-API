using KanbanAPI.Domain;
using KanbanAPI.Business;

using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Infrastructure;

public class BoardRepo : BaseRepo<Board>, IBoardRepo
{
    public BoardRepo(DatabaseContext dbContext) : base(dbContext)
    {
    }
}