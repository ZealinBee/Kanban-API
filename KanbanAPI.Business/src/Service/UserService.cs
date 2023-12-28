using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public class UserService : BaseService<User, CreateUserDto, GetUserDto, UpdateUserDto>, IUserService
{
    public UserService(IUserRepo repo, IMapper mapper) : base(repo, mapper)
    {
    }
}