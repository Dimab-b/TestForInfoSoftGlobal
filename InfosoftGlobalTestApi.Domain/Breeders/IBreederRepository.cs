using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Breeders
{
    public interface IBreederRepository
    {
        Task<Breeder?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Breeder breeder, CancellationToken cancellationToken = default);
    }
}
