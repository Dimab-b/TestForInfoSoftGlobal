using InfosoftGlobalTestApi.Domain.AuditLogs;
using InfosoftGlobalTestApi.Domain.Breeders;
using InfosoftGlobalTestApi.Domain.Common;
using InfosoftGlobalTestApi.Domain.Common.ValueObjects;
using InfosoftGlobalTestApi.Domain.Litters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace InfosoftGlobalTestApi.Infrastructure.Persistance
{
    public class AppDbContext : DbContext , IUnitOfWork
    {
        private readonly IPublisher _publisher;
       
        public DbSet<Breeder> Breeders { get; set; }
        public DbSet<BreederBenefit> BreederBenefits { get; set; }
        public DbSet<Litter> Litters { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options , IPublisher publisher) : base(options) { _publisher = publisher; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var testBreederId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            modelBuilder.Entity<Breeder>().HasData(new
            {
                Id = testBreederId,
                FirstName = "Test",
                LastName = "Breeder", 
                CreatedAt = DateTime.UtcNow
            });
            modelBuilder.Entity<Breeder>().OwnsOne(b => b.Email).HasData(new
            {
                BreederId = testBreederId, 
                Value = "test@breeder.com" 
            });

            modelBuilder.Entity<BreederBenefit>().HasData(new
            {
                Id = Guid.NewGuid(),
                BreederId = testBreederId,
                FreeLimit = 3,
                UsedCount = 0
            });

            modelBuilder.Entity<Litter>().HasData(
                new { Id = Guid.NewGuid(), BreederId = testBreederId, Status = Status.Approved, CreatedAt = DateTime.UtcNow },

                new { Id = Guid.NewGuid(), BreederId = testBreederId, Status = Status.Submitted, CreatedAt = DateTime.UtcNow },

                new { Id = Guid.NewGuid(), BreederId = Guid.NewGuid(), Status = Status.Approved, CreatedAt = DateTime.UtcNow }
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var entity in domainEntities)
            {
                entity.Entity.ClearDomainEvents();
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
