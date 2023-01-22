using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.EntryComment;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommandRequest, bool>
    {
        public Task<bool> Handle(DeleteEntryCommentFavCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessage(SozlukConstants.FavExchangeName,
                SozlukConstants.DefaultExchangeType,
                SozlukConstants.DeleteEntryCommentFavQueueName,
                new DeleteEntryCommentFavEvent()
                {
                    EntryCommentId= request.EntryCommentId,
                    UserId= request.UserId,
                });
            return Task.FromResult(true);
        }
    }
}
