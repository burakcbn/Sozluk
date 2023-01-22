using MediatR;
namespace Sozluk.Api.Application.Features.Command.User.ChangePassword
{
    public class ChangePasswordCommandRequest:IRequest<bool>
    {
        public Guid? UserId{ get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}