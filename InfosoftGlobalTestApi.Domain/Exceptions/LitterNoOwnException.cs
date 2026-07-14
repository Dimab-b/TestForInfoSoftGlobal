using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Exceptions
{
    public class LitterNoOwnException : DomainException
    {
        public LitterNoOwnException(string message) : base(message) { }
    }
}
