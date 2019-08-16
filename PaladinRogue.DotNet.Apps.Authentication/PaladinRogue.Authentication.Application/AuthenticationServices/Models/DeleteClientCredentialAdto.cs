using System;
using PaladinRogue.Library.Core.Application.Concurrency;
using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Authentication.Application.AuthenticationServices.Models
{
    public class DeleteClientCredentialAdto : IInboundVersionedAdto
    {
        public Guid Id { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}