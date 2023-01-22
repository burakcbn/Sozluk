using MediatR;
using Sozluk.Common.Models;

namespace Sozluk.Api.Application.Features.Command.Entry.CreateVote
{
    public class CreateEntryVoteCommandRequest:IRequest<bool>
    {
        public CreateEntryVoteCommandRequest(string? entryId, Guid? createdBy, VoteType voteType)
        {
            EntryId = entryId;
            CreatedBy = createdBy;
            VoteType = voteType;
        }

        public string? EntryId { get; set; }
        public Guid? CreatedBy { get; set; }
        public VoteType VoteType { get; set; }

    }
}