using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Command.User.ChangePassword;
using Sozluk.Api.Application.Features.Command.User.ConfirmEmail;
using Sozluk.Api.Application.Features.Command.User.CreateUser;
using Sozluk.Api.Application.Features.Command.User.LoginUser;
using Sozluk.Api.Application.Features.Command.User.UpdateUser;
using Sozluk.Api.Application.Features.Queries.GetUserDetail;

namespace Sozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get( Guid id)
        {

            var response = await _mediator.Send(new GetUserDetailQueryRequest(id));
            return Ok(response);
        }

        [HttpGet("userName/{userName}")]
        public async Task<IActionResult> GetByUserName(string userName)
        {

            var response = await _mediator.Send(new GetUserDetailQueryRequest(Guid.Empty,userName));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommandRequest loginUserCommandRequest)
        {
            var response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest createUserCommandRequest)
        {
            var response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("userUpdate")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest updateUserCommandRequest)
        {
            var response = await _mediator.Send(updateUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommandRequest confirmEmailCommandRequest)
        {
            var response = await _mediator.Send(confirmEmailCommandRequest);
            return Ok(response);
        }
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommandRequest changePasswordCommandRequest)
        {
            if (!changePasswordCommandRequest.UserId.HasValue)
                changePasswordCommandRequest.UserId= userId;

            var response = await _mediator.Send(changePasswordCommandRequest);
            return Ok(response);
        }
    }
}
