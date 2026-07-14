using InfosoftGlobalTestApi.Domain.AuditLogs;
using InfosoftGlobalTestApi.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Infrastructure.AuditLogs.Services
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AuditLog log, CancellationToken cancellationToken = default)
        {
            await _context.AuditLogs.AddAsync(log, cancellationToken);
        }
    }
}
