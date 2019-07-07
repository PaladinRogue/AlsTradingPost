using System;
using Common.ApplicationServices.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace ApplicationManager.ApplicationServices.AuthenticationServices.Models
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

        public string AppAccessToken { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}