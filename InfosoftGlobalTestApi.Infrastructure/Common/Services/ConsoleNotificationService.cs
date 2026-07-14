using InfosoftGlobalTestApi.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Infrastructure.Common.Services
{
    public class ConsoleNotificationService : INotificationService
    {
        public Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("\n=== [SIMULATED EMAIL SENT] ===");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            Console.WriteLine("==============================\n");
            Console.ResetColor();

            return Task.CompletedTask;
        }
    }
}
