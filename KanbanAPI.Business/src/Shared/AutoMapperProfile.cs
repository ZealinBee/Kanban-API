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
        CreateMap<Item, CreateItemDto>();
        CreateMap<CreateItemDto, Item>();
        CreateMap<Item, GetItemDto>();
        CreateMap<GetItemDto, Item>();
        CreateMap<Item, UpdateItemDto>();
        CreateMap<UpdateItemDto, Item>();

    }
}