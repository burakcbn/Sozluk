using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Dtos.User;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }


        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var userDto = await _authService.LoginUserAsync(request.Email, request.Password);

            return new() { UserDto = userDto };
        }
    }
}
