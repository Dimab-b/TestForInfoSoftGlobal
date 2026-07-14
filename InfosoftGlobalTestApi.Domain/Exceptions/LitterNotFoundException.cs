using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Exceptions
{
    public class LitterNotFoundException : DomainException
    {
        public LitterNotFoundException(Guid id)
            : base($"Litter with ID {id} was not found.") { }
    }
}
