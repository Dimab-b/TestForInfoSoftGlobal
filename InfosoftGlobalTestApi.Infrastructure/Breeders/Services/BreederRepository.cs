using InfosoftGlobalTestApi.Domain.Breeders;
using InfosoftGlobalTestApi.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Infrastructure.Breeders.Services
{
    public class BreederRepository : IBreederRepository
    {
        private readonly AppDbContext _context;

        public BreederRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Breeder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Breeders
                .Include(b => b.Benefit)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task AddAsync(Breeder breeder, CancellationToken cancellationToken = default)
        {
            await _context.Breeders.AddAsync(breeder, cancellationToken);
        }
    }
}
