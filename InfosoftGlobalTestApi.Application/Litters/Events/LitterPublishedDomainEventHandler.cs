using InfosoftGlobalTestApi.Application.Common.Interfaces;
using InfosoftGlobalTestApi.Domain.Breeders;
using InfosoftGlobalTestApi.Domain.Litters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Application.Litters.Events
{
    public class LitterPublishedDomainEventHandler : INotificationHandler<LitterPublishedDomainEvent>
    {
        private readonly IBreederRepository _breederRepository;
        private readonly INotificationService _notificationService;

        public LitterPublishedDomainEventHandler(
            IBreederRepository breederRepository,
            INotificationService notificationService)
        {
            _breederRepository = breederRepository;
            _notificationService = notificationService;
        }

        public async Task Handle(LitterPublishedDomainEvent notification, CancellationToken cancellationToken)
        {
            var breeder = await _breederRepository.GetByIdAsync(notification.BreederId, cancellationToken);
            if (breeder == null) return;

            var emailAddress = breeder.Email.Value;

            await _notificationService.SendEmailAsync(
                emailAddress,
                "Твій виводок успішно опубліковано!",
                $"Привіт, {breeder.FirstName}! Твій виводок (ID: {notification.LitterId}) тепер бачать усі користувачі.",
                cancellationToken);
        }
    }
}
