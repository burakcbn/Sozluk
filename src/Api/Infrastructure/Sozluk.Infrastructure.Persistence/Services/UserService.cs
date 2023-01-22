using AutoMapper;
using Sozluk.Api.Application.Dtos.User;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Application.Interfaces.Services;
using Sozluk.Api.Domain.Models;
using Sozluk.Common;
using Sozluk.Common.Events.User;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Infrastructure.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = await _userRepository.GetSingleAsync(u => u.EmailAddress == createUserDto.EmailAddress);
            if (user != null)
                throw new DatabaseValidationException("User already exist!");

            user = _mapper.Map<User>(createUserDto);
            var rows = await _userRepository.AddAsync(user);

            if (rows > 0)
            {
                var @event = new UserEmailChangeEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = user.EmailAddress,
                };

                QueueFactory.SendMessage(SozlukConstants.UserExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.UserEmailChangedQueueName, @event);
            }
            return user.Id;
        }

        public async Task<Guid> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(updateUserDto.Id);

            if (user is null)
                throw new DatabaseValidationException("User not found!");

            var emailAddress = user.EmailAddress;
            var change = string.CompareOrdinal(emailAddress, updateUserDto.EmailAddress) is not 0;

            _mapper.Map(user, updateUserDto);
            var rows = _userRepository.Update(user);

            if (rows > 0 && change)
            {
                var @event = new UserEmailChangeEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = user.EmailAddress,
                };

                QueueFactory.SendMessage(SozlukConstants.UserExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.UserEmailChangedQueueName, @event);

                user.EmailConfirmed = false;
                _userRepository.Update(user);
            }
            return user.Id;
        }
    }
}
