using MediatR;
using Sozluk.Common;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommandRequest, bool>
    {
        
        public Task<bool> Handle(CreateEntryFavCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessage(SozlukConstants.FavExchangeName,
                                     SozlukConstants.DefaultExchangeType,
                                     SozlukConstants.CreateEntryFavQueueName,
                                     new CreateEntryFavEvent()
                                     {
                                         CreatedById= request.CreatedById.Value,
                                         EntryId=request.EntryId.Value,
                                     });

            return Task.FromResult(true);
        }
    }
}
