using KanbanAPI.Domain;
using KanbanAPI.Business;

using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Infrastructure;

public class ItemRepo : BaseRepo<Item>, IItemRepo
{
    public ItemRepo(DatabaseContext dbContext) : base(dbContext)
    {
    }

}