using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;

namespace Sozluk.Api.Application.Features.Queries.Entry.GetEntries
{
    public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQueryRequest, List<GetEntriesQueryResponse>>
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetEntriesQueryResponse>> Handle(GetEntriesQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();
            if (request.TodayEntries)
            {
                query = query
                    .Where(x => x.CreatedDate >= DateTime.Now.Date)
                    .Where(x => x.CreatedDate <= DateTime.Now.AddDays(1).Date);
            }
          query= query.Include(x => x.EntryComments)
                 .OrderBy(x => Guid.NewGuid())
                 .Take(request.Count);

        return await query.ProjectTo<GetEntriesQueryResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
