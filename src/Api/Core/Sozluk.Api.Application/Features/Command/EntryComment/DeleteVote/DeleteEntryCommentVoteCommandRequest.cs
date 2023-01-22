using MediatR;

namespace Sozluk.Api.Application.Features.Command.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommandRequest:IRequest<bool>
    {
        public DeleteEntryCommentVoteCommandRequest(string entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }

        public string EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}