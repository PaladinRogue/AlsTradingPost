using System;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Api.Validation.Attributes;

namespace PaladinRogue.Authentication.Api.Authentication
{
    public class AuthenticateClientCredentialTemplate : ITemplate
    {
        [Ignore]
        public Guid Id { get; set; }

        [Required]
        public string RedirectUri { get; set; }

        [Required]
        public string Token { get; set; }
    }
}