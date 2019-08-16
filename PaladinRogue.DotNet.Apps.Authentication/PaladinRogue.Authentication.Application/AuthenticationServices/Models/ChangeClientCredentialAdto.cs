using System;
using PaladinRogue.Library.Core.Application.Concurrency;
using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Authentication.Application.AuthenticationServices.Models
{
    public class ChangeClientCredentialAdto : IInboundVersionedAdto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string ClientGrantAccessTokenUrl { get; set; }

        public string GrantAccessTokenUrl { get; set; }

        public string ValidateAccessTokenUrl { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}