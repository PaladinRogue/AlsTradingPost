using Common.ApplicationServices.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace Authentication.ApplicationServices.Identities.Models
{
    public class ResetPasswordAdto : IInboundVersionedAdto
    {
        public string Token { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}