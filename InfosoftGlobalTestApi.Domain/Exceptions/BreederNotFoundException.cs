using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Exceptions
{
    public class BreederNotFoundException : DomainException
    {
        public BreederNotFoundException(Guid id)
            : base($"Breeder with ID {id} was not found.") { }
    }
}
