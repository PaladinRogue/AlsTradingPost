using System;

namespace PaladinRogue.Libray.Authorisation.Application.ApplicationServices
{
    public class AddClaimAdto
    {
        public Guid IdentityId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}