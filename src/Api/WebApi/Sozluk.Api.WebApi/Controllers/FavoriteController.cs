using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Command.Entry.CreateEntry;
using Sozluk.Api.Application.Features.Command.Entry.CreateFav;
using Sozluk.Api.Application.Features.Command.Entry.DeleteFav;
using Sozluk.Api.Application.Features.Command.EntryComment.CreateFav;
using Sozluk.Api.Application.Features.Command.EntryComment.DeleteFav;

namespace Sozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : BaseController
    {
        private readonly IMediator _mediator;

        public FavoriteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createEntryFav")]
        public async Task<IActionResult> CreateEntryFav(CreateEntryFavCommandRequest createEntryCommandRequest)
        {
            createEntryCommandRequest.CreatedById = userId;
            var response= await _mediator.Send(createEntryCommandRequest);
            return Ok(response);
        }

        [HttpPost("createEntryCommentFav")]
        public async Task<IActionResult> CreateEntryCommentFav(CreateEntryCommentFavCommandRequest createEntryCommentFavCommandRequest)
        {
            createEntryCommentFavCommandRequest.UserId = userId;
            var response = await _mediator.Send(createEntryCommentFavCommandRequest);
            return Ok(response);
        }

        [HttpPost("deleteEntryFav")]
        public async Task<IActionResult> DeleteEntryFav(DeleteEntryFavCommandRequest deleteEntryFavCommandRequest)
        {
            deleteEntryFavCommandRequest.UserId= userId;
            var response = await _mediator.Send(deleteEntryFavCommandRequest);
            return Ok(response);
        }


        [HttpPost("deleteEntryCommentFav")]
        public async Task<IActionResult> DeleteEntryCommentFav(DeleteEntryCommentFavCommandRequest deleteEntryCommentFavCommandRequest)
        {
            deleteEntryCommentFavCommandRequest.UserId = userId;
            var response = await _mediator.Send(deleteEntryCommentFavCommandRequest);
            return Ok(response);
        }



    }
}
