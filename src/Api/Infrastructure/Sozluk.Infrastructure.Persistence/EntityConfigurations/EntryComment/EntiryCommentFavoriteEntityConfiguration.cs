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
    internal class EntiryCommentFavoriteEntityConfiguration : BaseEntityConfiguration<EntryCommentFavorite>
    {
        public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycommentfavorite", SozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(e => e.CreatedBy)
                .WithMany(u => u.EntryCommentFavorites)
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.EntryComment)
                .WithMany(e => e.EntryCommentFavorites)
                .HasForeignKey(e => e.EntryCommentId);
        }
    }
}
