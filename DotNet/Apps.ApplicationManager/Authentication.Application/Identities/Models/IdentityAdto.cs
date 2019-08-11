using System;
using PaladinRogue.Libray.Core.Application.Concurrency;
using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Authentication.Application.Identities.Models
{
    public class IdentityAdto : IOutboundVersionedAdto
    {
        public Guid Id { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}