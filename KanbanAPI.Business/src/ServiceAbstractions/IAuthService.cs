using KanbanAPI.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanAPI.Business;
public interface IAuthService
{
    Task<string> VerifyCredentials(LoginUserDto dto);
}