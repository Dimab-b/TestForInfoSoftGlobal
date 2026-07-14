using InfosoftGlobalTestApi.Domain.Breeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Infrastructure.Persistance.Configurations
{
    public class BreederConfiguration : IEntityTypeConfiguration<Breeder>
    {
        public void Configure(EntityTypeBuilder<Breeder> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(b => b.LastName).IsRequired().HasMaxLength(100);

            builder.OwnsOne(b => b.Email, emailBuilder =>
            {
                emailBuilder.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(255);

                emailBuilder.HasIndex(e => e.Value).IsUnique();
            });

            builder.HasOne(b => b.Benefit)
                   .WithOne()
                   .HasForeignKey<BreederBenefit>(bb => bb.BreederId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
