using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Infrastructure.Exceptions;
using Sozluk.Common.Infrastructure.Extensions;
using Sozluk.Common.Models.Page;
using Sozluk.Common.Models.Queries;

namespace Sozluk.Api.Application.Features.Queries.GetUserEntriesDetail
{
    public class GetUserEntriesDetailQueryHandler : IRequestHandler<GetUserEntriesDetailQueryRequest, PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        public GetUserEntriesDetailQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesDetailQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryable();

            if (request.UserId.HasValue && request.UserId != null && request.UserId != Guid.Empty)
                query = query.Where(x => x.CreatedById == request.UserId);

            else if (!string.IsNullOrEmpty(request.UserName))
                query = query.Where(x => x.CreatedBy.UserName == request.UserName);
            else
                throw new DatabaseValidationException("User not found!");


            query = query.Include(x => x.CreatedBy)
                         .Include(x => x.EntryFavorites);

            var list = query.Select(x => new GetUserEntriesDetailViewModel()
            {
                Id = x.Id.ToString(),
                Content = x.Content,
                CreatedByUserName = x.CreatedBy.UserName,
                CreatedDate = x.CreatedDate,
                FavoritedCount = x.EntryFavorites.Count,
                IsFavorited = false,
                Subject = x.Subject,
            });

            var userEntries = await list.GetPaged(request.Page, request.PageSize);
            return userEntries;
        }
    }
}
