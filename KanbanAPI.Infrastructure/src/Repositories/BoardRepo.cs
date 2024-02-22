using KanbanAPI.Domain;
using KanbanAPI.Business;

using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Infrastructure;

public class BoardRepo : BaseRepo<Board>, IBoardRepo
{
    public BoardRepo(DatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<Board> GetOneWithUsersAsync(Guid id)
    {
        return await _dbSet
                               .Include(b => b.Users)
                               .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Board> GetOneWithItemsAsync(Guid id)
    {
        return await _dbSet
                               .Include(b => b.Items)
                               .FirstOrDefaultAsync(b => b.Id == id);
    }
    public async Task<Board> GetOneWithUsersAndItemsAsync(Guid id)
    {
        return await _dbSet
                               .Include(b => b.Users)
                               .Include(b => b.Items)
                               .FirstOrDefaultAsync(b => b.Id == id);
    }
}