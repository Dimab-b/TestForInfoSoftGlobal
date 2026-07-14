using InfosoftGlobalTestApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Litters
{
    public record LitterPublishedDomainEvent(Guid LitterId, Guid BreederId) : IDomainEvent;
    
    
}
