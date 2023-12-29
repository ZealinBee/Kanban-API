using KanbanAPI.Domain;
using KanbanAPI.Business;

using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Infrastructure;

public class UserRepo : BaseRepo<User>, IUserRepo
{
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

}