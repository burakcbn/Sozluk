using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.EntryComment.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommandRequest, bool>
    {

        public Task<bool> Handle(CreateEntryCommentVoteCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessage(SozlukConstants.VoteExchangeName,
                SozlukConstants.DefaultExchangeType,
                SozlukConstants.CreateEntryCommentVoteQueueName,
                new CreateEntryCommentVoteEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    VoteType = request.VoteType,
                    UserId = request.UserId,
                });
            return Task.FromResult(true);
        }
    }
}
