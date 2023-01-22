using MediatR;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Models.Page;
using Sozluk.Common.Models.Queries;

namespace Sozluk.Api.Application.Features.Queries.Entry.GetEntryComment
{
    public class GetEntryCommentsQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryCommentViewModel>>
    {
        public GetEntryCommentsQuery(Guid userId, string entryId, int page, int pageSize) : base(page, pageSize)
        {
            UserId = userId;
            EntryId = entryId;
        }

        public Guid? UserId { get; set; }
        public string EntryId { get; set; }
    }
}
