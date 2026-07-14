using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.AuditLogs
{
    public interface IAuditLogRepository
    {
        Task AddAsync(AuditLog log, CancellationToken cancellationToken = default);
    }
}
