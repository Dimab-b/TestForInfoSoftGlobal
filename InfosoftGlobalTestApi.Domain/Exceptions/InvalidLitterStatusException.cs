using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Exceptions
{
    public class InvalidLitterStatusException : DomainException
    {
        public InvalidLitterStatusException(string message) : base(message) { }
    }
}
