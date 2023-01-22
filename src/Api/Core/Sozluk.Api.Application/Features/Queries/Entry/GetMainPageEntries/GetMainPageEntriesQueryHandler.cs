using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Infrastructure.Extensions;
using Sozluk.Common.Models;
using Sozluk.Common.Models.Page;
using Sozluk.Common.Models.Queries;

namespace Sozluk.Api.Application.Features.Queries.Entry.GetMainPageEntries
{
    public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();
            query.Include(e => e.EntryVotes)
                 .Include(x => x.EntryFavorites)
                 .Include(x => x.CreatedBy);

            var list = query.Select(i => new GetEntryDetailViewModel()
            {
                Id = i.Id.ToString(),
                Content = i.Content,
                Subject=i.Subject,
                IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
                FavoritedCount = i.EntryFavorites.Count,
                CreatedDate=i.CreatedDate,
                CreatedByUserName=i.CreatedBy.UserName,
                VoteType= request.UserId.HasValue&& i.EntryVotes.Any(j=>j.CreateById==request.UserId.Value)
                                ? i.EntryVotes.FirstOrDefault(i=>i.CreateById==request.UserId).VoteType
                                :VoteType.None,
            });

            var entries= await list.GetPaged(request.Page, request.PageSize);
            return entries;
        }
    }
}
