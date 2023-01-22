namespace Sozluk.Api.Application.Features.Queries.Entry.GetEntries
{
    public class GetEntriesQueryResponse
    {
        public Guid Id { get; set; }
        public string Subject{ get; set; }
        public int CommentCount{ get; set; }
    }
}