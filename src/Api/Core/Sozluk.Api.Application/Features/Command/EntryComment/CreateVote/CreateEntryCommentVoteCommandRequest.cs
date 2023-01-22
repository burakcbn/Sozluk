using MediatR;
using Sozluk.Common.Models;

namespace Sozluk.Api.Application.Features.Command.EntryComment.CreateVote
{
    public class CreateEntryCommentVoteCommandRequest:IRequest<bool>
    {
        public CreateEntryCommentVoteCommandRequest(string entryCommentId, Guid userId, VoteType voteType)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
            VoteType = voteType;
        }

        public string    EntryCommentId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType{ get; set; }
    }
}