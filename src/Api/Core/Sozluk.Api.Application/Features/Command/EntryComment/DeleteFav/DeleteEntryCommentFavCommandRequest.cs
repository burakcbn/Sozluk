using MediatR;

namespace Sozluk.Api.Application.Features.Command.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommandRequest:IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}