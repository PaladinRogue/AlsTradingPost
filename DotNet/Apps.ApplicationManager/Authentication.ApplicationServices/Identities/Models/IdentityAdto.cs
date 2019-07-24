using System;
using Common.ApplicationServices.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace Authentication.ApplicationServices.Identities.Models
{
    public class IdentityAdto : IOutboundVersionedAdto
    {
        public Guid Id { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}