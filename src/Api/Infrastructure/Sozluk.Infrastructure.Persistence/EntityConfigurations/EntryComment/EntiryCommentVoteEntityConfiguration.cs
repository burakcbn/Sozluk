using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment
{
    public class EntiryCommentVoteEntityConfiguration:BaseEntityConfiguration<EntryCommentVote>
    {
        public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycommentvote", SozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(v => v.EntryComment)
                .WithMany(c => c.EntryCommentVotes)
                .HasForeignKey(v => v.EntryCommentId);
        }
    }
}
