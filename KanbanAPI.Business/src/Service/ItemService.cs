using KanbanAPI.Domain;
using AutoMapper;

namespace KanbanAPI.Business;

public class ItemService : BaseService<Item, CreateItemDto, GetItemDto, UpdateItemDto>, IItemService
{
    private readonly IItemRepo _itemRepo;
    private readonly IMapper _mapper;
    private readonly IBoardRepo _boardRepo;

    public ItemService(IItemRepo repo, IMapper mapper, IBoardRepo boardRepo) : base(repo, mapper)
    {
        _itemRepo = repo;
        _mapper = mapper;
        _boardRepo = boardRepo;
    }

    public override async Task<GetItemDto> CreateOneAsync(CreateItemDto dto, Guid userId)
    {
        var board = await _boardRepo.GetOneWithItemsAsync(dto.BoardId);
        if (board == null)
            throw new KeyNotFoundException("Board not found");
        var item = _mapper.Map<Item>(dto);
        board.Items.Add(item);
        await _boardRepo.UpdateOneAsync(board);
        return _mapper.Map<GetItemDto>(item);
    }

    public async Task<GetItemDto> AddUser(Guid itemId, MemberDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveUser(Guid itemId, MemberDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsItemPartOfBoard(Guid itemId, Guid boardId)
    {
        var board = await _boardRepo.GetOneWithItemsAsync(boardId);
        if (board == null)
            throw new KeyNotFoundException("Board not found");
        if (!board.Items.Any(i => i.Id == itemId))
            return false;
        return true;
    }
}