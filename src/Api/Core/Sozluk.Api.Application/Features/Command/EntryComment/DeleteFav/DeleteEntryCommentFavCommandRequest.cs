using MediatR;

namespace Sozluk.Api.Application.Features.Command.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommandRequest:IRequest<bool>
    {
        public string EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}