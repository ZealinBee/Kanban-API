using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KanbanAPI.Business;
using KanbanAPI.Domain;

namespace KanbanAPI.Controller;

[ApiController]
public class UserController : BaseController<User, CreateUserDto, GetUserDto, UpdateUserDto>
{
    private readonly IUserService _service;
    public UserController(IUserService service) : base(service)
    {
        _service = service;
    }
}