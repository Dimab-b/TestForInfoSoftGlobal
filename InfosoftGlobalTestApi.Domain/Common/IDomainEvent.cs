using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Common
{
    public interface IDomainEvent : INotification
    {
    }
}
