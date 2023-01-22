using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;

namespace Sozluk.Infrastructure.Persistence.Repositories
{
    public class EntryCommentVoteRepository : GenericRepository<EntryCommentVote>, IEntryCommentVoteRepository
    {
        public EntryCommentVoteRepository(DbContext context) : base(context)
        {
        }
    }
}
