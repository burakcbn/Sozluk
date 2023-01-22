using MediatR;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommandRequest, bool>
    {
        public Task<bool> Handle(DeleteEntryCommentVoteCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessage(SozlukConstants.FavExchangeName,
                SozlukConstants.DefaultExchangeType,
                SozlukConstants.DeleteEntryCommentVoteQueueName,
                new DeleteEntryCommentVoteEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    UserId = request.UserId,
                });
            return Task.FromResult(true);
        }
    }
}
