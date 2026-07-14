using InfosoftGlobalTestApi.Domain.Common;
using InfosoftGlobalTestApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace InfosoftGlobalTestApi.Domain.Litters
{
    public enum Status {Draft , Submitted , Approved , Published }
    public class Litter : Entity , IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid BreederId { get; private set; }
        public Status Status { get; private set; }
        public DateTime CreatedAt { get; private set; }


        private Litter () { }

        private Litter(Guid id , Guid breederId , Status status)
        {
            Id = id;
            BreederId = breederId;
            Status = status;
            CreatedAt = DateTime.UtcNow;
        }

        public static Litter Create(Guid breederId)
        {
            return new Litter(Guid.NewGuid() , breederId , Status.Draft);
        }


        public void EnsureCanBePublished(Guid currentBreederId)
        {
            if (this.BreederId != currentBreederId)
                throw new LitterNoOwnException("You do not own this litter.");

            if (Status != Status.Approved)
                throw new InvalidLitterStatusException($"Litter cannot be published. Current status: {this.Status}.");
        }

        public void Publish()
        {
            this.Status = Status.Published;

            AddDomainEvent(new LitterPublishedDomainEvent(this.Id, this.BreederId));
        }
    }
}

