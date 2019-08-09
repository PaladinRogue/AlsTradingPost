using Common.Application.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace Authentication.Application.Identities.Models
{
    public class ResetPasswordAdto : IInboundVersionedAdto
    {
        public string Token { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}