using InfosoftGlobalTestApi.Domain.Breeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Infrastructure.Persistance.Configurations
{
    public class BreederBenefitConfiguration : IEntityTypeConfiguration<BreederBenefit>
    {
        public void Configure(EntityTypeBuilder<BreederBenefit> builder)
        {
            builder.HasKey(bb => bb.Id);

            builder.Property(bb => bb.FreeLimit).IsRequired();
            builder.Property(bb => bb.UsedCount).IsRequired();
        }
    }
}
