using Sozluk.Api.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(CreateUserDto createUserDto);
        Task<Guid> UpdateUserAsync(UpdateUserDto updateUserDto);
    }
}
