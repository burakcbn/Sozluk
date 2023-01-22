using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Infrastructure;
using Sozluk.Common.Infrastructure.Exceptions;

namespace Sozluk.Api.Application.Features.Command.User.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
                throw new ArgumentNullException(nameof(request.UserId));

            var user = await _userRepository.GetByIdAsync(request.UserId.Value);

            if (user is null)
                throw new DatabaseValidationException("User not found!");

            request.OldPassword = PasswordEncryptor.Encrpt(request.OldPassword);

            if (user.Password != request.OldPassword)
                throw new DatabaseValidationException("OldPassword wrong!");

            user.Password = PasswordEncryptor.Encrpt(request.NewPassword);

            return _userRepository.Update(user) > 0;

        }
    }
}
