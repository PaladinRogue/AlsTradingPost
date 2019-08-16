using System;

namespace PaladinRogue.Authentication.Application.Identities.Claims
{
    public class UpdateIdentityClaimAdto
    {
        public Guid IdentityId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}