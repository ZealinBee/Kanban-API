using KanbanAPI.Domain;
using AutoMapper;

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
        CreateMap<Board, CreateBoardDto>();
        CreateMap<CreateBoardDto, Board>();
        CreateMap<Board, GetBoardDto>();
        CreateMap<GetBoardDto, Board>();
        CreateMap<Board, UpdateBoardDto>();
        CreateMap<UpdateBoardDto, Board>();
    }
}