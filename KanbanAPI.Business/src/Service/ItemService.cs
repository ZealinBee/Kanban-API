using KanbanAPI.Domain;
using AutoMapper;

namespace KanbanAPI.Business;

public class ItemService : BaseService<Item, CreateItemDto, GetItemDto, UpdateItemDto>, IItemService
{
    private readonly IItemRepo _itemRepo;
    private readonly IMapper _mapper;

    public ItemService(IItemRepo repo, IMapper mapper) : base(repo, mapper)
    {
        _itemRepo = repo;
        _mapper = mapper;
    }

    public async Task<GetItemDto> AddUser(Guid taskId, MemberDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveUser(Guid taskId, MemberDto dto)
    {
        throw new NotImplementedException();
    }
}