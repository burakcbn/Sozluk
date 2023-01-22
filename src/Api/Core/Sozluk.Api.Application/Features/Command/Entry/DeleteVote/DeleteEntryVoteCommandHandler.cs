using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommandRequest, bool>
    {
        public Task<bool> Handle(DeleteEntryVoteCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessage(SozlukConstants.VoteExchangeName,
                SozlukConstants.DefaultExchangeType,
                SozlukConstants.DeleteEntryVoteQueueName,
                new DeleteEntryVoteEvent()
                {
                    EntryId = request.EntryId,
                    UserId = request.UserId
                });

            return Task.FromResult(true);
        }
    }
}
