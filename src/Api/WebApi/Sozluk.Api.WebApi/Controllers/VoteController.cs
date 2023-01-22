using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Command.Entry.CreateVote;
using Sozluk.Api.Application.Features.Command.Entry.DeleteVote;
using Sozluk.Api.Application.Features.Command.EntryComment.CreateVote;
using Sozluk.Api.Application.Features.Command.EntryComment.DeleteVote;
using Sozluk.Api.Application.Features.Queries.GetUserDetail;
using Sozluk.Common.Models;

namespace Sozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : BaseController
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("entry")]
        public async Task<IActionResult> CreateEntryVote([FromBody]string entryId,VoteType voteType=VoteType.UpVote)
        {

            var response = await _mediator.Send(new CreateEntryVoteCommandRequest(entryId,userId,voteType));
            return Ok(response);
        }

        [HttpPost("entryComment")]
        public async Task<IActionResult> CreateEntryCommentVote([FromBody]string entryCommentId, VoteType voteType = VoteType.UpVote)
        {

            var response = await _mediator.Send(new CreateEntryCommentVoteCommandRequest(entryCommentId, userId, voteType));
            return Ok(response);
        }

        [HttpPost("deleteEntryVote")]
        public async Task<IActionResult> DeleteEntryVote([FromBody]string entryId)
        {

            var response = await _mediator.Send(new DeleteEntryVoteCommandRequest(entryId,userId));
            return Ok(response);
        }

        [HttpPost("deleteEntryCommentVote")]
        public async Task<IActionResult> DeleteEntryCommentVote([FromBody]string entryCommentId)
        {

            var response = await _mediator.Send(new DeleteEntryCommentVoteCommandRequest(entryCommentId, userId));
            return Ok(response);
        }

    }
}
