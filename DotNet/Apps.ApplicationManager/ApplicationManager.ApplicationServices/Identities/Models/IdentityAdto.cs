using System;
using Common.Application.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace ApplicationManager.ApplicationServices.Identities.Models
{
    public class IdentityAdto : IOutboundVersionedAdto
    {
        public Guid Id { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}