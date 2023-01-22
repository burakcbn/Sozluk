using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Models;
using Sozluk.Common.Models.Queries;

namespace Sozluk.Api.Application.Features.Queries.Entry.GetEntryDetail
{
    public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQueryRequest, GetEntryDetailViewModel>
    {
        private IEntryRepository _entryRepository;

        public GetEntryDetailQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();

            query.Include(x => x.EntryFavorites)
                 .Include(x => x.CreatedBy)
                 .Include(x => x.EntryVotes)
                 .Include(x => x.EntryVotes)
                 .Where(x => x.Id == Guid.Parse(request.EntryId));

            var entryDetail = query.Select(e => new GetEntryDetailViewModel()
            {
                Id = e.Id.ToString(),
                Content = e.Content,
                CreatedByUserName = e.CreatedBy.UserName,
                CreatedDate = e.CreatedDate,
                Subject = e.Subject,
                FavoritedCount = e.EntryFavorites.Count,
                IsFavorited = request.UserId.HasValue && e.EntryFavorites.Any(j => j.CreatedById == request.UserId),
                VoteType = request.UserId.HasValue && e.EntryVotes.Any(x => x.Id == request.UserId) ?
                                 e.EntryVotes.FirstOrDefault(x => x.CreateById == request.UserId).VoteType :
                                 VoteType.None,
            });

            return await entryDetail.FirstOrDefaultAsync(cancellationToken);

        }
    }
}
