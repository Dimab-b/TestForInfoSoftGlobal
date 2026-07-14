using InfosoftGlobalTestApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Breeders
{
    public class BreederBenefit : Entity
    {
        public Guid Id { get; private set; } 
        public Guid BreederId { get; private set; } 
        public int FreeLimit { get; private set; }
        public int UsedCount { get; private set; }

        internal BreederBenefit(Guid breederId, int freeLimit, int usedCount)
        {
            Id = Guid.NewGuid();
            BreederId = breederId;
            FreeLimit = freeLimit;
            UsedCount = usedCount;
        }

        private BreederBenefit() { } 

        public bool HasAvailableLimits() => UsedCount < FreeLimit;

        internal void IncrementUsage()
        {
            UsedCount++;
        }
    }
}
