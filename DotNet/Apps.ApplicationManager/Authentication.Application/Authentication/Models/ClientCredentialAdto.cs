using System;

namespace PaladinRogue.Authentication.Application.Authentication.Models
{
    public class ClientCredentialAdto
    {
        public Guid Id { get; set; }

        public string RedirectUri { get; set; }

        public string Token { get; set; }
    }
}