using MediatR;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;
using Sozluk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Infrastructure.Exceptions;

namespace Sozluk.Api.Application.Features.Command.User.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommandRequest, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailConfirmationRepository _emailConfirmationRepository;

        public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
        {
            _userRepository = userRepository;
            _emailConfirmationRepository = emailConfirmationRepository;
        }

        public async Task<bool> Handle(ConfirmEmailCommandRequest request, CancellationToken cancellationToken)
        {
            var confirmation = await _emailConfirmationRepository.GetByIdAsync(request.ConfirmationId);

            if (confirmation is null)
                throw new DatabaseValidationException("Confirmation not found!");

            var user = await _userRepository.GetSingleAsync(i => i.EmailAddress == confirmation.NewEmailAddress);
            if (user is null)
                throw new DatabaseValidationException("User not found with this email!");
            if (user.EmailConfirmed)
                throw new DatabaseValidationException("Email address already confirmed!");

            user.EmailConfirmed = true;
            _userRepository.Update(user);
            return true;
        }
    }
}
