using InfosoftGlobalTestApi.Domain.Common;
using InfosoftGlobalTestApi.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace InfosoftGlobalTestApi.Domain.Breeders
{
    public class Breeder  : Entity , IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public DateTime CreatedAt { get; private set; }

        
        public BreederBenefit Benefit { get; private set; }


        private Breeder () { }

        private Breeder(Guid id , string firstName , string lastName , Email email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedAt = DateTime.UtcNow;

            Benefit = new BreederBenefit(Id , 3 , 0);
        }


        public static Breeder Create(string name , string surname , Email email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name must be filled");

            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentNullException("Surname must be filled");

            var breeder = new Breeder(Guid.NewGuid() , name , surname , email);


            return breeder;
        }


        public void UseFreeLimit()
        {
            if (Benefit == null)
                throw new Exception("Benefit data is missing for this breeder.");

            if (!Benefit.HasAvailableLimits())
                throw new Exception("Publish attempt failed - limits exceeded.");

            Benefit.IncrementUsage();
        }


    }
}
