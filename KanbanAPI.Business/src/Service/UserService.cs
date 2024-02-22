using KanbanAPI.Domain;
using AutoMapper;

namespace KanbanAPI.Business;

public class UserService : BaseService<User, CreateUserDto, GetUserDto, UpdateUserDto>, IUserService
{
    private readonly IUserRepo _userRepo;
    private readonly IMapper _mapper;
    public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public override async Task<GetUserDto> CreateOneAsync(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        if (await _userRepo.GetOneByEmailAsync(user.Email) != null)
        {
            throw new Exception("Email already exists");
        }
        PasswordService.HashPassword(user.Password, out string hashedPassword, out byte[] passwordSalt);
        user.Password = hashedPassword;
        user.Salt = passwordSalt;
        var createdUser = await _userRepo.CreateOneAsync(user);
        return _mapper.Map<GetUserDto>(createdUser);
    }

    public async Task DoesEmailExistAsync(string email)
    {
        if (await _userRepo.GetOneByEmailAsync(email) != null)
        {
            throw new Exception("Email already exists");
        }
    }
}