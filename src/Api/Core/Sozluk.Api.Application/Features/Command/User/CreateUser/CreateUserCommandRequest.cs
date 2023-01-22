using MediatR;

namespace Sozluk.Api.Application.Features.Command.User.CreateUser
{
    public class CreateUserCommandRequest:IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName  { get; set; }
        public string Password { get; set; }
    }
}