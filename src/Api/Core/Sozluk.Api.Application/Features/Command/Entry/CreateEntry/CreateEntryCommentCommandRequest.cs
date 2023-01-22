using MediatR;

namespace Sozluk.Api.Application.Features.Command.Entry.CreateEntry
{
    public class CreateEntryCommandRequest : IRequest<string>
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public string? CreatedById { get; set; }
    }
}
