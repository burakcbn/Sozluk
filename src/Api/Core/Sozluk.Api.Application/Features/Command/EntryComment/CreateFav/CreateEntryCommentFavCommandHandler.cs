using MediatR;
using Sozluk.Common;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozluk.Common.Events.EntryComment;
namespace Sozluk.Api.Application.Features.Command.EntryComment.CreateFav
{
    public  class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommandRequest, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentFavCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessage(SozlukConstants.FavExchangeName,
                                     SozlukConstants.DefaultExchangeType,
                                     SozlukConstants.CreateEntryCommentFavQueueName,
                                     new EntryCommentFavEvent()
                                     {
                                         EntryCommentId = request.EntryCommentId,
                                         UserId = request.UserId,
                                     });
            return await Task.FromResult(true);
        }

    }
}
