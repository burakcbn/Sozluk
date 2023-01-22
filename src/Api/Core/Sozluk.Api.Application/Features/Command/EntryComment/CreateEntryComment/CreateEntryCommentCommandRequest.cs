using MediatR;

namespace Sozluk.Api.Application.Features.Command.EntryComment.CreateEntryComment
{
    public class CreateEntryCommentCommandRequest:IRequest<string>
    {
        public string? EntryCommentId { get; set; }
        public string Content { get; set; }
        public string? CreatedById { get; set; }
    }
}