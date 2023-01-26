using Dapper;
using Microsoft.Data.SqlClient;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Events.EntryComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Projections.FavoriteService.Services
{
    public class FavoriteService
    {
        private readonly string connectionString;

        public FavoriteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryFav(CreateEntryFavEvent @event)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("INSERT INTO EntryFavorite (Id,EntryId,CreatedById,CreatedDate) VALUES(@Id,@EntryId,@CreatedById,GETDATE())",
                  new
                  {
                      Id=Guid.NewGuid(),
                      EntryId=Guid.Parse(@event.EntryId),
                      CreatedById=@event.CreatedById
                  });


        }

        public async Task CreateEntryCommentFav(EntryCommentFavEvent @event)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("INSERT INTO EntryCommentFavorite (Id,EntryCommentId,CreatedById,CreatedDate) VALUES(@Id,@EntryCommentId,@CreatedById,GETDATE())",
                  new
                  {
                      Id = Guid.NewGuid(),
                      EntryId = Guid.Parse(@event.EntryCommentId),
                      CreatedById = @event.UserId
                  });


        }
        public async Task DeleteEntryFav(DeleteEntryFavEvent @event)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE FROM EntryFavorite WHERE EntryId=@EntryId and CreatedById=@CreatedById,",
                  new
                  {
                      EntryId = Guid.Parse(@event.EntryId),
                      CreatedById = @event.UserId
                  });


        }

        public async Task DeleteEntryCommentFav(DeleteEntryCommentFavEvent @event)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE FROM EntryCommentFavorite WHERE EntryCommentId=@EntryCommentId and CreatedById=@CreatedById,",
                  new
                  {
                      EntryId = Guid.Parse(@event.EntryCommentId),
                      CreatedById = @event.UserId
                  });


        }
    }
}
