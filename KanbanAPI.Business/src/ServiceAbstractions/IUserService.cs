using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public interface IUserService : IBaseService<CreateUserDto, GetUserDto, UpdateUserDto>
{
}