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
    public class EntiryFavoriteEntityConfiguration:BaseEntityConfiguration<EntryFavorite>
    {
        public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entryfavorite", SozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(f => f.CreatedBy)
                   .WithMany(u => u.EntryFavorites)
                   .HasForeignKey(f => f.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Entry)
                   .WithMany(e => e.EntryFavorites)
                   .HasForeignKey(f => f.EntryId);

        }
    }
}
