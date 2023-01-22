using MediatR;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Common.Infrastructure.Extensions;
using Sozluk.Common.Models;
using Sozluk.Common.Models.Page;
using Sozluk.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Queries.Entry.GetEntryComment
{
    public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentViewModel>>
    {
        private readonly IEntryCommentRepository _commentRepository;

        public GetEntryCommentsQueryHandler(IEntryCommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<PagedViewModel<GetEntryCommentViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = _commentRepository.AsQueryable();

            query.Include(x => x.EntryCommentFavorites)
                 .Include(x => x.CreatedBy)
                 .Include(x => x.EntryCommentVotes)
                 .Where(x => x.EntryId == Guid.Parse(request.EntryId));

           var list= query.Select(x => new GetEntryCommentViewModel()
            {
                Id = x.Id.ToString(),
                Content = x.Content,
                CreatedDate = x.CreatedDate,
                CreatedUserName = x.CreatedBy.UserName,
                FavoritedCount = x.EntryCommentFavorites.Count,
                IsFavorited = request.UserId.HasValue && x.EntryCommentFavorites.Any(x => x.Id == request.UserId),
                VoteType = request.UserId.HasValue && x.EntryCommentVotes.Any(x => x.Id == request.UserId) ?
                                x.EntryCommentVotes.FirstOrDefault(x => x.CreateById == request.UserId).VoteType :
                                VoteType.None,
            });

            var comments=await list.GetPaged(request.Page, request.PageSize);
            return comments;
        }
    }
}
