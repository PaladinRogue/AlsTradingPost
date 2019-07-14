using System;

namespace ApplicationManager.ApplicationServices.Identities.Claims
{
    public class UpdateIdentityClaimAdto
    {
        public Guid IdentityId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}