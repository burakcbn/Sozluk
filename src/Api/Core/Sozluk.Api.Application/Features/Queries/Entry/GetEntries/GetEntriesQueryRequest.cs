using MediatR;

namespace Sozluk.Api.Application.Features.Queries.Entry.GetEntries
{
    public class GetEntriesQueryRequest:IRequest<List<GetEntriesQueryResponse>>
    {
        public bool TodayEntries { get; set; }
        public int Count { get; set; } = 100;
    }
}