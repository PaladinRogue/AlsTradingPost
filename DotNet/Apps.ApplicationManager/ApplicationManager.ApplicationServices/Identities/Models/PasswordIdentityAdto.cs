using System;

namespace ApplicationManager.ApplicationServices.Identities.Models
{
    public class PasswordIdentityAdto
    {
        public Guid Id { get; set; }

        public string Identifier { get; set; }

        public string Password { get; set; }
    }
}