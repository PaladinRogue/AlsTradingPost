using System;

namespace Common.Authorisation.ApplicationServices
{
    public class AddClaimAdto
    {
        public Guid IdentityId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}