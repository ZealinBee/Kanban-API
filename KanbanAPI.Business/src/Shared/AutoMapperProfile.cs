using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<GetUserDto, User>();
        CreateMap<CreateUserDto, User>();
        CreateMap<User, CreateUserDto>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, UpdateUserDto>();


    }
}