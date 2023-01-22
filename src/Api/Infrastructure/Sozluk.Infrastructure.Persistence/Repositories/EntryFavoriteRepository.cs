using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;

namespace Sozluk.Infrastructure.Persistence.Repositories
{
    public class EntryFavoriteRepository : GenericRepository<EntryFavorite>, IEntryFavoriteRepository
    {
        public EntryFavoriteRepository(DbContext context) : base(context)
        {
        }
    }
}
