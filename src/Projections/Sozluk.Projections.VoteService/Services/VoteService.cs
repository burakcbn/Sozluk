using Dapper;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Projections.VoteService.Services
{
    internal class VoteService
    {
        private readonly string connectionString;

        public VoteService(string connectionString)
        {
            this.connectionString = connectionString;
        }
       
        public async Task CreateEntryVote(CreateEntryVoteEvent vote)
        {
            await DeleteEntrtVote(Guid.Parse(vote.EntryId),vote.CreatedBy);
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("INSERT INTO EntryVote (Id,EntryId,VoteType,CreatedById,CreatedDate) VALUES(@Id,@EntryId,@VoteType,@CreatedById,GETDATE())",
                  new
                  {
                      Id = Guid.NewGuid(),
                      EntryId = vote.EntryId,
                      CreatedById = vote.CreatedBy,
                      VoteType = (int)VoteType.UpVote,
                  });


        }
        public async Task DeleteEntrtVote(Guid entryId,Guid userId )
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE FROM EntryVote where EntryId=@EntryId and CREATEDBYID=@UserId",
                  new
                  {
                      EntryId =entryId,
                      UserId= userId
                  });

        }
    }
}
