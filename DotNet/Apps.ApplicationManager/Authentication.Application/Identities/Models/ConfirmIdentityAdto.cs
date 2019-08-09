using System;

namespace Authentication.Application.Identities.Models
{
    public class ConfirmIdentityAdto
    {
        public Guid IdentityId { get; set; }

        public string Token { get; set; }
    }
}