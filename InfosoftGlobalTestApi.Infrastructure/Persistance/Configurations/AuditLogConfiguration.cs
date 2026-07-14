using InfosoftGlobalTestApi.Domain.AuditLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Infrastructure.Persistance.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.EntityId)
                   .IsRequired();

            builder.HasIndex(a => a.EntityId);

            builder.Property(a => a.Action)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(a => a.CreatedAt)
                   .IsRequired();
        }
    }
}
