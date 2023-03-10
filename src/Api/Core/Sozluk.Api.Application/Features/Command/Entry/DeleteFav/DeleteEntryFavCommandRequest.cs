using MediatR;

namespace Sozluk.Api.Application.Features.Command.Entry.DeleteFav
{
    public class DeleteEntryFavCommandRequest : IRequest<bool>
    {
        public string EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
