using System;
using PaladinRogue.Libray.Core.Application.Concurrency;
using PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Authentication.Application.AuthenticationServices.Models
{
    public class DeleteClientCredentialAdto : IInboundVersionedAdto
    {
        public Guid Id { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}