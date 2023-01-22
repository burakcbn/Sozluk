using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public class EntiryVoteEntityConfiguration : BaseEntityConfiguration<EntryVote>
    {
        public override void Configure(EntityTypeBuilder<EntryVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("entryvote", SozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(e => e.Entry)
                .WithMany(e => e.EntryVotes)
                .HasForeignKey(e => e.EntryId);
        }
    }
}
