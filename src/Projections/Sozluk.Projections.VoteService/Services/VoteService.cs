using Dapper;
using Sozluk.Common.Events.Entry;
using Sozluk.Common.Events.EntryComment;
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
            await DeleteEntryVote(Guid.Parse(vote.EntryId),vote.CreatedBy);
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("INSERT INTO EntryVote (Id,EntryId,VoteType,CreatedById,CreatedDate) VALUES(@Id,@EntryId,@VoteType,@CreatedById,GETDATE())",
                  new
                  {
                      Id = Guid.NewGuid(),
                      EntryId = Guid.Parse(vote.EntryId),
                      CreatedById = vote.CreatedBy,
                      VoteType = (int)VoteType.UpVote,
                  });


        }

        public async Task DeleteEntryVote(Guid entryId,Guid userId )
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE FROM EntryVote where EntryId=@EntryId and CreatedById=@UserId",
                  new
                  {
                      EntryId =entryId,
                      UserId= userId
                  });

        }

        public async Task CreateEntryCommentVote(CreateEntryCommentVoteEvent vote)
        {
            await DeleteEntryCommentVote(Guid.Parse(vote.EntryCommentId), vote.UserId);
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("INSERT INTO EntryCommentVote (Id,EntryCommentId,VoteType,CreatedById,CreatedDate) VALUES(@Id,@EntryCommentId,@VoteType,@CreatedById,GETDATE())",
                  new
                  {
                      Id = Guid.NewGuid(),
                      EntryId = Guid.Parse(vote.EntryCommentId),
                      CreatedById = vote.UserId,
                      VoteType = (int)VoteType.UpVote,
                  });


        }

        public async Task DeleteEntryCommentVote(Guid entryCommentId, Guid userId)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE FROM EntryCommentVote where EntryCommentId=@EntryCommentId and CreatedById=@UserId",
                  new
                  {
                      EntryId = entryCommentId,
                      UserId = userId
                  });

        }
    }
}
