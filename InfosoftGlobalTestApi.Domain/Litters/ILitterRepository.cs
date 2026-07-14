using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Litters
{
    public interface ILitterRepository
    {
        Task<Litter?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(Litter litter, CancellationToken cancellationToken = default);
    }
}
