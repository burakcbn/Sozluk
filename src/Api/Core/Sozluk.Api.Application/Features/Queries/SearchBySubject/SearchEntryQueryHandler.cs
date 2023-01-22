using MediatR;
using Sozluk.Common.Models.Queries;
using Sozluk.Api.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Sozluk.Api.Application.Features.Queries.SearchBySubject
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQueryRequest, List<SearchEntryViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        public SearchEntryQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQueryRequest request, CancellationToken cancellationToken)
        {
            var result = _entryRepository
                        .Get(u => EF.Functions.Like(u.Subject, $"{request.SearchText}%"))
                        .Select(i => new SearchEntryViewModel()
                        {
                            Id = i.Id.ToString(),
                            Subject = i.Subject,
                        });

            return await result.ToListAsync(cancellationToken);
        }
    }
}
