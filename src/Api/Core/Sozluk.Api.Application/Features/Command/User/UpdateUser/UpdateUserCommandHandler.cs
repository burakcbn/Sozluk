using AutoMapper;
using MediatR;
using Sozluk.Api.Application.Dtos.User;
using Sozluk.Api.Application.Interfaces.Services;

namespace Sozluk.Api.Application.Features.Command.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, Guid>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            UpdateUserDto updateUserDto = _mapper.Map<UpdateUserDto>(request);

            return await _userService.UpdateUserAsync(updateUserDto);
        }
    }
}
