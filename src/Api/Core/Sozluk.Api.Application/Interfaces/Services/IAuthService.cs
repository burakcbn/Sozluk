using Sozluk.Api.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserDto> LoginUserAsync(string email, string password);
    }
}
