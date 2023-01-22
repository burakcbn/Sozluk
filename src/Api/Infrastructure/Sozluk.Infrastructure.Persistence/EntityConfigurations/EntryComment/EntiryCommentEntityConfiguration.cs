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
    public class EntiryCommentEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryComment>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryComment> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycomment", SozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(e => e.CreatedBy)
                .WithMany(u => u.EntryComments)
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Entry)
                .WithMany(e => e.EntryComments)
                .HasForeignKey(e => e.EntryId);

        }
    }
}
