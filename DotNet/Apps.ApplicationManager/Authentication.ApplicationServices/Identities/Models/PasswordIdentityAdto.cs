using System;
using Common.ApplicationServices.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace Authentication.ApplicationServices.Identities.Models
{
    public class PasswordIdentityAdto : IOutboundVersionedAdto
    {
        public Guid IdentityId { get; set; }

        public string Identifier { get; set; }

        public string Password { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}