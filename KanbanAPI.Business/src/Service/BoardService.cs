using KanbanAPI.Domain;
using AutoMapper;

namespace KanbanAPI.Business;

public class BoardService : BaseService<Board, CreateBoardDto, GetBoardDto, UpdateBoardDto>, IBoardService
{
    private readonly IBoardRepo _boardRepo;
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;
    public BoardService(IBoardRepo repo, IMapper mapper, IUserRepo userRepo) : base(repo, mapper)
    {
        _boardRepo = repo;
        _mapper = mapper;
        _userRepo = userRepo;
    }

    public override async Task<GetBoardDto> CreateOneAsync(CreateBoardDto dto, Guid id)
    {
        var board = _mapper.Map<Board>(dto);
        var user = await _userRepo.GetOneAsync(id);
        if (user == null)
            throw new KeyNotFoundException("User not found");

        board.Users.Add(user);
        user.Boards.Add(board);

        await _boardRepo.CreateOneAsync(board);
        await _userRepo.UpdateOneAsync(user);
        return _mapper.Map<GetBoardDto>(board);
    }

    public async Task<GetBoardDto> AddMember(Guid boardId, MemberDto dto)
    {
        var board = await _boardRepo.GetOneAsync(boardId);
        if (board == null)
            throw new KeyNotFoundException("Board not found");
        var user = await _userRepo.GetOneAsync(dto.UserId);
        if (user == null)
            throw new KeyNotFoundException("User not found");

        board.Users.Add(user);
        user.Boards.Add(board);

        await _boardRepo.UpdateOneAsync(board);
        await _userRepo.UpdateOneAsync(user);
        return _mapper.Map<GetBoardDto>(board);
    }

    public async Task<bool> RemoveMember(Guid boardId, MemberDto dto)
    {
        var board = await _boardRepo.GetOneWithUsersAsync(boardId);
        if (board == null)
            throw new KeyNotFoundException("Board not found");
        var user = await _userRepo.GetOneWithBoardsAsync(dto.UserId);
        if (user == null)
            throw new KeyNotFoundException("User not found");

        board.Users.Remove(user);
        user.Boards.Remove(board);

        await _boardRepo.UpdateOneAsync(board);
        await _userRepo.UpdateOneAsync(user);
        return true;
    }

    public async Task<List<GetBoardDto>> GetBoardsForUser(Guid userId)
    {
        var user = await _userRepo.GetOneWithBoardsAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("User not found");
        return _mapper.Map<List<GetBoardDto>>(user.Boards);
    }



}