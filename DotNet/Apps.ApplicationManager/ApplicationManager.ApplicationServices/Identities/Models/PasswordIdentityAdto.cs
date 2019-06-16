using Common.Application.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace ApplicationManager.ApplicationServices.Identities.Models
{
    public class PasswordIdentityAdto : IOutboundVersionedAdto
    {
        public string Identifier { get; set; }

        public string Password { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}