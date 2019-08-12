using PaladinRogue.Library.Core.Application.Concurrency;
using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Authentication.Application.Identities.Models
{
    public class ResetPasswordAdto : IInboundVersionedAdto
    {
        public string Token { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}