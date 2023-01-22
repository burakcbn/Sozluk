using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Infrastructure.Persistence.Repositories
{

    public class EntryVoteRepository : GenericRepository<EntryVote>, IEntryVoteRepository
    {
        public EntryVoteRepository(DbContext context) : base(context)
        {
        }
    }
}
