using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
    }
}
