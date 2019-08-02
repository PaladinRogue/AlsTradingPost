using System;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;

namespace Authentication.Api.Authentication
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