using InfosoftGlobalTestApi.Domain.Litters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Infrastructure.Persistance.Configurations
{
    public class LitterConfiguration : IEntityTypeConfiguration<Litter>
    {
        public void Configure(EntityTypeBuilder<Litter> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasIndex(l => l.BreederId);
        }
    }
}
