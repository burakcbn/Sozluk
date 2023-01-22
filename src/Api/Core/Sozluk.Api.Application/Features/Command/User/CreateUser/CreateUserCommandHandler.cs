using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Dtos.User;
using Sozluk.Api.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, Guid>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserDto createUserDto = _mapper.Map<CreateUserDto>(request);

            return await _userService.CreateUserAsync(createUserDto);
        }
    }
}
