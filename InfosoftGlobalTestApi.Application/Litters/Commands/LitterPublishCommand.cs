using InfosoftGlobalTestApi.Domain.AuditLogs;
using InfosoftGlobalTestApi.Domain.Breeders;
using InfosoftGlobalTestApi.Domain.Common;
using InfosoftGlobalTestApi.Domain.Exceptions;
using InfosoftGlobalTestApi.Domain.Litters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Application.Litters.Commands
{
    public record LitterPublishCommand(Guid Id , Guid BreederId) : IRequest<Unit>;

    public class LitterPublishCommandHandler : IRequestHandler<LitterPublishCommand , Unit>
    {
        private readonly ILitterRepository _litterRepository;
        private readonly IBreederRepository _breederRepository;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IUnitOfWork _uow;
        public LitterPublishCommandHandler(ILitterRepository litterRepository , IBreederRepository breederRepository , IAuditLogRepository auditLogRepository , IUnitOfWork uow)
        {
            _litterRepository = litterRepository;
            _breederRepository = breederRepository;
            _auditLogRepository = auditLogRepository;
            _uow = uow;
        }

        public async Task<Unit> Handle(LitterPublishCommand command , CancellationToken cancellationToken = default)
        {
            var litter = await _litterRepository.GetByIdAsync(command.Id, cancellationToken);
            if (litter == null) throw new LitterNotFoundException(command.Id);

            var breeder = await _breederRepository.GetByIdAsync(command.BreederId, cancellationToken);
            if (breeder == null) throw new BreederNotFoundException(command.BreederId);

            try
            {
                litter.EnsureCanBePublished(breeder.Id);
                breeder.EnsureHasLimits();
            }
            catch (BenefitLimitException ex)
            {
                var failLog = AuditLog.Create(litter.Id, "Publish attempt failed - limits exceeded");
                await _auditLogRepository.AddAsync(failLog, cancellationToken);

                await _uow.SaveChangesAsync(cancellationToken);

                throw; 
            }
            catch (InvalidLitterStatusException ex)
            {
                var failLog = AuditLog.Create(litter.Id, "Publish attempt failed - invalid status");
                await _auditLogRepository.AddAsync(failLog, cancellationToken);

                await _uow.SaveChangesAsync(cancellationToken);

                throw;
            }

            catch (LitterNoOwnException ex)
            {
                var failLog = AuditLog.Create(litter.Id, "Publish attempt failed - Not owner");
                await _auditLogRepository.AddAsync(failLog, cancellationToken);

                await _uow.SaveChangesAsync(cancellationToken);

                throw;
            }


            breeder.UseFreeLimit();
            litter.Publish();

            var successLog = AuditLog.Create(litter.Id, "Published for free");
            await _auditLogRepository.AddAsync(successLog, cancellationToken);

            await _uow.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
