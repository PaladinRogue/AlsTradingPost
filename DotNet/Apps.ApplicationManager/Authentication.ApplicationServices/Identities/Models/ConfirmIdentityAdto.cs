using System;

namespace Authentication.ApplicationServices.Identities.Models
{
    public class ConfirmIdentityAdto
    {
        public Guid IdentityId { get; set; }

        public string Token { get; set; }
    }
}