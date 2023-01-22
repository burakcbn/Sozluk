using AutoMapper;
using Sozluk.Api.Application.Dtos.User;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Application.Interfaces.Services;
using Sozluk.Api.Application.Interfaces.Token;
using Sozluk.Api.Domain.Models;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Infrastructure.Exceptions;
using Sozluk.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Infrastructure.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;
        public AuthService(IUserRepository userRepository, IMapper mapper, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
        }

        public async Task<UserDto> LoginUserAsync(string email, string password)
        {
            User user = await _userRepository.GetSingleAsync(u => u.EmailAddress == email);
            if (user == null)
                throw new DatabaseValidationException("User not found!");
            var passwordEncrpt = PasswordEncryptor.Encrpt(password);

            if (user.Password != passwordEncrpt)
                throw new DatabaseValidationException("Password is wrong!");

            if (!user.EmailConfirmed)
                throw new DatabaseValidationException("Email address is not confirmed yet!");

            UserDto userDto = _mapper.Map<UserDto>(user);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName),
            };

            var token= _tokenHandler.CreateToken(claims);
            userDto.Token = token;
            return userDto;
        }
    }
    

}
