using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sozluk.Api.Application.Features.Command.Entry.CreateEntry;
using Sozluk.Api.Application.Features.Command.EntryComment.CreateEntryComment;
using Sozluk.Api.Application.Features.Queries.Entry.GetEntries;
using Sozluk.Api.Application.Features.Queries.Entry.GetEntryComment;
using Sozluk.Api.Application.Features.Queries.Entry.GetEntryDetail;
using Sozluk.Api.Application.Features.Queries.Entry.GetMainPageEntries;
using Sozluk.Api.Application.Features.Queries.GetUserEntriesDetail;
using Sozluk.Api.Application.Features.Queries.SearchBySubject;

namespace Sozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : BaseController
    {
        private readonly IMediator _mediator;

        public EntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQueryRequest getEntriesQueryRequest)
        {

            var response = await _mediator.Send(getEntriesQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] string Id)
        {

            var response = await _mediator.Send(new GetEntryDetailQueryRequest(Id, userId));
            return Ok(response);
        }

        [HttpGet("comments")]
        public async Task<IActionResult> GetEntryComments([FromQuery] GetEntryCommentsQuery getEntryCommentsQuery)
        {
            getEntryCommentsQuery.UserId = userId;
            var response = await _mediator.Send(getEntryCommentsQuery);
            return Ok(response);
        }

        [HttpGet("userEntries")]
        public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
        {
            if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
                userId = this.userId;


            var response = await _mediator.Send(new GetUserEntriesDetailQueryRequest(userId, userName, page, pageSize));
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQueryRequest searchEntryQueryRequest)
        {

            var response = await _mediator.Send(searchEntryQueryRequest);
            return Ok(response);
        }


        [HttpGet("mainPageEntries")]
        public async Task<IActionResult> MainPageEntries([FromQuery] int page, int size)
        {
            var getMainPageEntriesQuery = new GetMainPageEntriesQuery(userId, page, size);

            var response = await _mediator.Send(getMainPageEntriesQuery);
            return Ok(response);
        }

        [HttpPost("createEntry")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommandRequest createEntryCommandRequest)
        {
            if (createEntryCommandRequest.CreatedById == null)
                createEntryCommandRequest.CreatedById = userId.ToString();

            var response = await _mediator.Send(createEntryCommandRequest);
            return Ok(response);
        }

        [HttpPost("createEntryComment")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommandRequest createEntryCommentCommandRequest)
        {
            if (createEntryCommentCommandRequest.CreatedById != null)
                createEntryCommentCommandRequest.CreatedById = userId.ToString();

            var response = await _mediator.Send(createEntryCommentCommandRequest);
            return Ok(response);
        }
    }
}
