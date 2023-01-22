using MediatR;

namespace Sozluk.Api.Application.Features.Command.Entry.CreateFav
{
    public class CreateEntryFavCommandRequest : IRequest<bool>
    {
        public Guid? EntryId { get; set; }
        public Guid? CreatedById { get; set; }

    }
}