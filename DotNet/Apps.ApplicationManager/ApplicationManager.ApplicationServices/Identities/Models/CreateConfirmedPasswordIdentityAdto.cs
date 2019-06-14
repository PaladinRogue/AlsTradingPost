using System;

namespace ApplicationManager.ApplicationServices.Identities.Models
{
    public class CreateConfirmedPasswordIdentityAdto
    {
        public Guid IdentityId { get; set; }

        public string Token { get; set; }

        public string Identifier { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}