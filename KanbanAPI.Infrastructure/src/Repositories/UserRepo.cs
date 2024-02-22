using KanbanAPI.Domain;
using KanbanAPI.Business;

using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Infrastructure;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    private readonly DatabaseContext _dbContext;
    public UserRepo(DatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<User> GetOneByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetOneByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> GetOneWithBoardsAsync(Guid id)
    {
        return await _dbSet
                               .Include(u => u.Boards)
                               .ThenInclude(b => b.Users)
                               .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> GetOneWithItemsAsync(Guid id)
    {
        return await _dbSet
                               .Include(u => u.Items)
                               .FirstOrDefaultAsync(u => u.Id == id);
    }

}