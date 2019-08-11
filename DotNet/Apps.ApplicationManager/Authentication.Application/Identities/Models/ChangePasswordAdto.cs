using System;
using PaladinRogue.Libray.Core.Application.Concurrency;
using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Authentication.Application.Identities.Models
{
    public class ChangePasswordAdto : IInboundVersionedAdto
    {
        public Guid IdentityId { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}