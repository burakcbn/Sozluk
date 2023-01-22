using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.Entry.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommandRequest, bool>
    {
        public Task<bool> Handle(CreateEntryVoteCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessage(SozlukConstants.VoteExchangeName,
                                     SozlukConstants.DefaultExchangeType,
                                     SozlukConstants.CreateEntryVoteQueueName,
                                     new CreateEntryVoteEvent()
                                     {
                                         CreatedBy = request.CreatedBy.Value,
                                         EntryId = request.EntryId,
                                         VoteType = request.VoteType
                                     });
            return Task.FromResult(true);
        }
    }
}
