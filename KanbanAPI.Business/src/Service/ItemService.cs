using KanbanAPI.Domain;
using AutoMapper;

namespace KanbanAPI.Business;

public class ItemService : BaseService<Item, CreateItemDto, GetItemDto, UpdateItemDto>, IItemService
{
    private readonly IItemRepo _itemRepo;
    private readonly IMapper _mapper;
    private readonly IBoardRepo _boardRepo;
    private readonly IUserRepo _userRepo;

    public ItemService(IItemRepo repo, IMapper mapper, IBoardRepo boardRepo, IUserRepo userRepo) : base(repo, mapper)
    {
        _itemRepo = repo;
        _mapper = mapper;
        _boardRepo = boardRepo;
        _userRepo = userRepo;
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

    public async Task<GetItemDto> AssignUser(Guid itemId, Guid userId)
    {
        var item = await _itemRepo.GetOneAsync(itemId);
        if (item == null)
            throw new KeyNotFoundException("Item not found");
        var user = await _userRepo.GetOneWithItemsAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("User not found");
        if (item.Users.Any(u => u.Id == userId))
            throw new InvalidOperationException("User already assigned to item");

        item.Users.Add(user);
        user.Items.Add(item);

        await _itemRepo.UpdateOneAsync(item);
        await _userRepo.UpdateOneAsync(user);
        return _mapper.Map<GetItemDto>(item);
    }
    public async Task<bool> RemoveUser(Guid itemId, Guid userId)
    {
        var item = await _itemRepo.GetOneAsync(itemId);
        if (item == null)
            throw new KeyNotFoundException("Item not found");
        var user = await _userRepo.GetOneWithItemsAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("User not found");
        if (!item.Users.Any(u => u.Id == userId))
            throw new InvalidOperationException("User not assigned to item");

        item.Users.Remove(user);
        user.Items.Remove(item);

        await _itemRepo.UpdateOneAsync(item);
        await _userRepo.UpdateOneAsync(user);
        return true;
    }

    public async Task<bool> IsItemPartOfBoard(Guid itemId, Guid boardId)
    {
        var board = await _boardRepo.GetOneWithItemsAsync(boardId);
        if (board == null)
            throw new KeyNotFoundException("Board not found");
        if (!board.Items.Any(i => i.Id == itemId))
            throw new KeyNotFoundException("Item not part of the board");
        return true;
    }

    public async Task<bool> IsItemStatusValid(ItemStatus status)
    {
        if (status != ItemStatus.Todo && status != ItemStatus.InProgress && status != ItemStatus.Done)
        {
            throw new InvalidOperationException("Invalid item status");
        }
        return true;
    }
}