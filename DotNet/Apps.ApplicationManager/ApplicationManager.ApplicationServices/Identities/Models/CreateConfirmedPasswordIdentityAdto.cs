using System;
using Common.Application.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace ApplicationManager.ApplicationServices.Identities.Models
{
    public class CreateConfirmedPasswordIdentityAdto : IInboundVersionedAdto
    {
        public Guid IdentityId { get; set; }

        public string Token { get; set; }

        public string Identifier { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}