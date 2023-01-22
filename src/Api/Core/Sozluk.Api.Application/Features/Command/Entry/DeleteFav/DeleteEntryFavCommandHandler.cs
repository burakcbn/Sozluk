using MediatR;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;
using Sozluk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.Entry.DeleteFav
{
    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommandRequest, bool>
    {
        public Task<bool> Handle(DeleteEntryFavCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessage(SozlukConstants.FavExchangeName,
                SozlukConstants.DefaultExchangeType,
                SozlukConstants.DeleteEntryFavQueueName,
                new DeleteEntryFavEvent()
                {
                    EntryId = request.EntryId,
                    UserId = request.UserId,
                });
            return Task.FromResult(true);
        }
    }
}
