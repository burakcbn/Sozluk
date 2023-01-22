using MediatR;

namespace Sozluk.Api.Application.Features.Command.User.ConfirmEmail
{
    public class ConfirmEmailCommandRequest:IRequest<bool>
    {
        public Guid ConfirmationId { get; set; }
    }
}