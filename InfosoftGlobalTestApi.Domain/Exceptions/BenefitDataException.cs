using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Exceptions
{
    public class BenefitDataException : DomainException
    {
        public BenefitDataException(string message) : base(message) { }
    }
}
