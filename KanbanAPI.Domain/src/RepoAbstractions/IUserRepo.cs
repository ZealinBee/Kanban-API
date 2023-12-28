namespace KanbanAPI.Domain;

public interface IUserRepo : IBaseRepo<User>
{
    Task<User> GetOneByUsernameAsync(string username);
    Task<User> GetOneByEmailAsync(string email);
}
