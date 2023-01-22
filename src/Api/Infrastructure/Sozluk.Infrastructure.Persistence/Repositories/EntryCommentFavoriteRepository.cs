using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;

namespace Sozluk.Infrastructure.Persistence.Repositories
{
    public class EntryCommentFavoriteRepository : GenericRepository<EntryCommentFavorite>, IEntryCommentFavoriteRepository
    {
        public EntryCommentFavoriteRepository(DbContext context) : base(context)
        {
        }
    }
}
