using MediatR;

namespace Sozluk.Api.Application.Features.Command.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandRequest:IRequest<bool>
    {
        public DeleteEntryVoteCommandRequest(string entryId, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public string EntryId { get; set; }
        public Guid UserId{ get; set; }
        
    }
}