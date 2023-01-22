using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Infrastructure.Persistence.EntityConfigurations.Entry
{
    public class EntryEntityConfiguration:BaseEntityConfiguration<Api.Domain.Models.Entry>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.Entry> builder)
        {
            base.Configure(builder);
            builder.ToTable("entry", SozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(e => e.CreatedBy)
                   .WithMany(e => e.Entries)
                   .HasForeignKey(e => e.CreatedById);
        }
    }
}
