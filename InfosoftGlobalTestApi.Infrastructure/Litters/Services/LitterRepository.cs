using InfosoftGlobalTestApi.Domain.Litters;
using InfosoftGlobalTestApi.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Infrastructure.Litters.Services
{
    public class LitterRepository : ILitterRepository
    {
        private readonly AppDbContext _context;

        public LitterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Litter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Litters
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        }

        public async Task AddAsync(Litter litter, CancellationToken cancellationToken = default)
        {
            await _context.Litters.AddAsync(litter, cancellationToken);
        }

    }
}
