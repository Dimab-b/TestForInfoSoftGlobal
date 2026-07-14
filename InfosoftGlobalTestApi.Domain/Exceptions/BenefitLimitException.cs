using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Exceptions
{
    public class BenefitLimitException : DomainException
    {
        public BenefitLimitException(int freeUse, int used) : base($"Available free publishing: {freeUse} , used {used} ") { }
    }
}
